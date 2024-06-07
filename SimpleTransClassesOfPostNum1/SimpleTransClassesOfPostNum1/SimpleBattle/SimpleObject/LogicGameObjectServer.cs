using System.Timers;
using SimpleTransClassesOfPostNum1.SimpleBattle.SimpleManage;
using SimpleTransClassesOfPostNum1.SimpleDataStream;
using SimpleTransClassesOfPostNum1.SimpleDataTables;
using SimpleTransClassesOfPostNum1.SimpleDataTables.Manufacturer.LaserMicro;
using SimpleTransClassesOfPostNum1.SimpleHTables;
using SimpleTransClassesOfPostNum1.SimpleMath;
using SimpleTransClassesOfPostNum1.SimpleMath.IdManage;
using SimpleTransClassesOfPostNum1.SimpleMath.SectorOfVector;

namespace SimpleTransClassesOfPostNum1.SimpleBattle.SimpleObject;

public class LogicGameObjectServer(
    LogicBattleModeServer logicBattleModeServer,
    int classId,
    int instanceId,
    int z,
    int index)
{
    public System.Timers.Timer OpenTimer { get; set; } = null!;
    
    private LogicGameObjectManagerServer _logicGameObjectManagerServer = null!;
    private int _objectGlobalId;

    private LogicVector2 _position = new();
    private readonly LogicVector2 _internalPosition = new();

    private int _dataId = GlobalId.CreateGlobalId(classId, instanceId);
    private int _fadeCounter = 10;

    public LogicBattleModeServer GetLogicBattleModeServer()
    {
        return logicBattleModeServer;
    }

    public LogicGameObjectManagerServer GetLogicGameObjectManager()
    {
        return _logicGameObjectManagerServer;
    }
    
    public virtual void Encode(BitStream bitStream, bool isOwnObject, int visionIndex, int visionTeam) // simple LogicGameObjectServer.Encode for versions like v49 \\
    {
        /*
         * a[7] = position.X;
         * a[8] = position.Y;
         * a[9] = z;
         * a[10] = visionIndex;
         * a[11] = visionTeam;
         */
        
        // _index generates something like a[10] + 0x10 * a[11] ( for unIndexed gameObjects: _index = >=160 | recommended values: 160; 170; 200 | )
        
        var v1 = LogicMath.Clamp(_position.GetX(), 0, 65535);
        var v2 = LogicMath.Clamp(_position.GetY(), 0, 65535);

        var v3 = false;
        {
            if (GetType() == GameObjectTypeHelperTable.Projectile.GetLocalObjectType())
            {
                v3 = true;
                
                var t = ((LogicProjectileData?)LogicDataTables.GetDataById(GetData())!).GetTravelType();
                if (t != 8)
                    v3 = t == 0xB;
            }
        }
        
        if (GameObjectTypeHelperTableExtension.GetOriginalObjectTypeByLocalObjectType(GetType()) == 3)
            if (LogicDataTables.GetDataById<LogicItemData>(_dataId).GetAlignToTiles())
            {
                bitStream.WritePositiveIntMax63(v1 / 0x12C);
                bitStream.WritePositiveIntMax63(v2 / 0x12C);

                goto label14;
            }

        if (v3)
        {
            bitStream.WriteIntMax65535(v1);
            bitStream.WriteIntMax65535(v2);
        }
        else
        {
            bitStream.WritePositiveVIntMax65535(v1);
            bitStream.WritePositiveVIntMax65535(v2);
        }

        bitStream.WritePositiveVIntMax65535(GetZ());
        
        label14:
        bitStream.WritePositiveVIntMax255(index);
        
        if (GameObjectTypeHelperTableExtension.GetOriginalObjectTypeByLocalObjectType(GetType()) != 1)
            bitStream.WritePositiveIntMax15(GetFadeCounterClient());
    }
    
    public int GetObjectGlobalId()
    {
        return _objectGlobalId;
    }
    
    public void SetObjectGlobalId(int id)
    {
        _objectGlobalId = id;
    }
    
    public LogicVector2 GetPosition(bool wasGetInternalPos = false)
    {
        return wasGetInternalPos ? _internalPosition : _position;
    }

    public void SetPosition(int x, int y, int newZ)
    {
        _position.Set(x, y);
        z = newZ;
    }

    public void SetPosition(LogicVector2 logicVector2)
    {
        _position.Set(logicVector2.GetX(), logicVector2.GetY());
    }

    public int GetX()
    {
        return _position.GetX();
    }

    public int GetY()
    {
        return _position.GetY();
    }

    public int GetTileX()
    {
        return _position.GetX() / 300;
    }

    public int GetTileY()
    {
        return _position.GetY() / 300;
    }

    public int GetZ()
    {
        return z;
    }
    
    public int GetData(int s = -1)
    {
        if (s > -1) _dataId = s;
        return _dataId;
    }
    
    public int GetFadeCounterClient()
    {
        return _fadeCounter;
    }
    
    public void ChangeFadeCounterServer(int newValue)
    {
        _fadeCounter = newValue;
    }
    
    public int GetIndex()
    {
        return index;
    }
    
    public void AttachLogicGameObjectManager(LogicGameObjectManagerServer logicGameObjectManagerServer, int globalId)
    {
        _logicGameObjectManagerServer = logicGameObjectManagerServer;
        _objectGlobalId = globalId;
    }

    public void ResetGameObject()
    {
        _dataId = 0;
        _position = null!;
        z = 0;
        index = 0;
        _objectGlobalId = 0;
        _fadeCounter = 0;
    }
    
    public virtual bool ShouldDestruct()
    {
        return false;
    }
    
    public void Destruct()
    {
        _position.Destruct();
    }

    public virtual bool IsAlive()
    {
        return true;
    }

    public virtual int GetRadius()
    {
        return 100;
    }

    public new virtual int GetType()
    {
        return -1;
    }

    public virtual void Tick()
    {
    }
    
    public void Tick(object? sender, ElapsedEventArgs e)
    {
        try
        {
            Tick();
        }
        catch
        {
            // secure exception;
        }
    }
}