using System.Collections.Concurrent;
using SimpleTransClassesOfPostNum1.SimpleBattle.SimpleObject;
using SimpleTransClassesOfPostNum1.SimpleDataStream;
using SimpleTransClassesOfPostNum1.SimpleMath.IdManage;

namespace SimpleTransClassesOfPostNum1.SimpleBattle.SimpleManage;

public class LogicGameObjectManagerServer
{
    private LogicBattleModeServer _logicBattleModeServer = null!;
    
    private readonly ConcurrentDictionary<int, LogicGameObjectServer> _allGameObjects = []; // -1 |> 0+1+2+3
    private readonly ConcurrentDictionary<int, LogicGameObjectServer> _gameObjectsOfCharacter = []; // 1 |> 0
    private readonly ConcurrentDictionary<int, LogicGameObjectServer> _gameObjectsOfProjectile = []; // 2 |> 1
    private readonly ConcurrentDictionary<int, LogicGameObjectServer> _gameObjectsOfAreaEffect = []; // 3 |> 2
    private readonly ConcurrentDictionary<int, LogicGameObjectServer> _gameObjectsOfItem = []; // 4 |> 3
    
    private Queue<LogicGameObjectServer> _removableGameObjects = new();
    private int _objectSideCounter;
    
    public void Encode(BitStream bitStream, int ownObjectId, int playerIndex, int teamIndex)
    {
        bitStream.WritePositiveIntMax2097151(ownObjectId);
        
        // your structure ->
        // code
    }

    public LogicBattleModeServer GetLogicBattleModeServer()
    {
        return _logicBattleModeServer;
    }

    public void SetLogicBattleModeServer(LogicBattleModeServer logicBattleModeServer)
    {
        _logicBattleModeServer = logicBattleModeServer;
    }

    public ConcurrentDictionary<int, LogicGameObjectServer> GetGameObjects(int objectType, bool copy = false)
    {
        var gameObjects = objectType switch
        {
            -1 => _allGameObjects,
            1 => _gameObjectsOfCharacter,
            2 => _gameObjectsOfProjectile,
            3 => _gameObjectsOfAreaEffect,
            _ => _gameObjectsOfItem
        };

        return !copy
            ? gameObjects
            : new ConcurrentDictionary<int, LogicGameObjectServer>(gameObjects);
    }

    public int GetNumGameObjects(int objectType)
    {
        return GetGameObjects(objectType).Count;
    }

    public LogicGameObjectServer? GetGameObjectById(int globalId)
    {
        return _allGameObjects!.GetValueOrDefault(globalId, null);
    }
    
    public void AddLogicGameObject(LogicGameObjectServer logicGameObjectServer)
    {
        logicGameObjectServer.AttachLogicGameObjectManager(this, GlobalId.CreateGlobalId(logicGameObjectServer.GetType(), _objectSideCounter++));
        
        GetGameObjects(logicGameObjectServer.GetType()).TryAdd(logicGameObjectServer.GetObjectGlobalId(), logicGameObjectServer);
        GetGameObjects(-1).TryAdd(logicGameObjectServer.GetObjectGlobalId(), logicGameObjectServer);
        
        System.Timers.Timer timer = new(50);
        {
            timer.Elapsed += logicGameObjectServer.Tick;
            timer.Enabled = true;
        }

        logicGameObjectServer.OpenTimer = timer;
    }

    public void RemoveGameObjectReferences(int objectGlobalId)
    {
        _removableGameObjects.Enqueue(GetGameObjectById(objectGlobalId)!);
    }

    public void RemoveGameObjectReferences(LogicGameObjectServer logicGameObjectServer)
    {
        RemoveGameObjectReferences(logicGameObjectServer.GetObjectGlobalId());
    }

    public void PreTick()
    {
        Parallel.ForEach(GetGameObjects(-1).ToArray(), gameObject =>
        {
            try
            {
                if (!gameObject.Value.ShouldDestruct()) return;
                RemoveGameObjectReferences(gameObject.Key);
            }
            catch
            {
                // secure exception;
            }
        });
    }

    public void Tick()
    {
        Parallel.ForEach(_removableGameObjects, logicGameObjectServer =>
        {
            if (logicGameObjectServer == null!) return;
            logicGameObjectServer.Destruct(); 
            
            GetGameObjects(logicGameObjectServer.GetType()).TryRemove(logicGameObjectServer.GetObjectGlobalId(), out _);
            GetGameObjects(-1).TryRemove(logicGameObjectServer.GetObjectGlobalId(), out _);
            
            if (logicGameObjectServer.OpenTimer == null!) return;
            logicGameObjectServer.OpenTimer.Dispose();
        });

        _removableGameObjects = new Queue<LogicGameObjectServer>();
    }
}