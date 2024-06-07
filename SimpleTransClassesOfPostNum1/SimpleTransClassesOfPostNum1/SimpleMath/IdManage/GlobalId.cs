namespace SimpleTransClassesOfPostNum1.SimpleMath.IdManage;

public static class GlobalId
{
    public static int CreateGlobalId(int classId, int instanceId)
    {
        if (instanceId >= 1000000 - 1 || classId == 0) return 0;
        return 1000000 * classId + instanceId;
    }

    public static int GetClassId(int globalId)
    {
        return globalId / 1000000;
    }

    public static int GetInstanceId(int globalId)
    {
        return globalId % 1000000;
    }
}