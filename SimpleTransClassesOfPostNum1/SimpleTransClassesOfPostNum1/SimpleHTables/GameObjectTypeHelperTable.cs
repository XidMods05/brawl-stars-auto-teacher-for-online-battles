namespace SimpleTransClassesOfPostNum1.SimpleHTables;

public enum GameObjectTypeHelperTable : byte
{
    Character,
    Projectile,
    AreaEffect,
    Item
}

public static class GameObjectTypeHelperTableExtensions
{
    public static int GetLocalObjectType(this GameObjectTypeHelperTable objectTypeHelperTable)
    {
        return objectTypeHelperTable switch
        {
            GameObjectTypeHelperTable.Character => 1, // 0
            GameObjectTypeHelperTable.Projectile => 2, // 1
            GameObjectTypeHelperTable.AreaEffect => 3, // 2
            GameObjectTypeHelperTable.Item => 4, // 3
            _ => throw new ArgumentOutOfRangeException(nameof(objectTypeHelperTable), objectTypeHelperTable, null)
        };
    }
    
    public static int GetOriginalObjectType(this GameObjectTypeHelperTable objectTypeHelperTable, bool _)
    {
        return objectTypeHelperTable switch
        {
            GameObjectTypeHelperTable.Character => 0, // 1
            GameObjectTypeHelperTable.Projectile => 1, // 2
            GameObjectTypeHelperTable.AreaEffect => 2, // 3
            GameObjectTypeHelperTable.Item => 3, // 4
            _ => throw new ArgumentOutOfRangeException(nameof(objectTypeHelperTable), objectTypeHelperTable, null)
        };
    }
}

public static class GameObjectTypeHelperTableExtension
{
    public static int GetOriginalObjectTypeByLocalObjectType(int objectType)
    {
        return objectType - 1;
    }
}