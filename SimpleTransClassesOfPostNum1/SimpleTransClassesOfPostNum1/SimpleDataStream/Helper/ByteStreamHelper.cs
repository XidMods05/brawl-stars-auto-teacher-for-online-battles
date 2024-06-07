using SimpleTransClassesOfPostNum1.SimpleDataStream.Checksum;
using SimpleTransClassesOfPostNum1.SimpleMath.IdManage;
using SimpleTransClassesOfPostNum1.SimpleMath.LongLogic;

namespace SimpleTransClassesOfPostNum1.SimpleDataStream.Helper;

public static class ByteStreamHelper
{
    public static LogicLong DecodeLogicLong(ByteStream byteStream)
    {
        var high = byteStream.ReadVInt();
        var low = byteStream.ReadVInt();

        return new LogicLong(high, low);
    }

    public static LogicLong EncodeLogicLong(ChecksumEncoder checksumEncoder, LogicLong value)
    {
        checksumEncoder.WriteVInt(value.GetHigherInt());
        checksumEncoder.WriteVInt(value.GetLowerInt());

        return value;
    }

    public static LogicLong EncodeLogicLong(ByteStream byteStream, LogicLong value)
    {
        byteStream.WriteVInt(value.GetHigherInt());
        byteStream.WriteVInt(value.GetLowerInt());

        return value;
    }

    public static int ReadDataReference(ByteStream byteStream)
    {
        var classId = byteStream.ReadVInt();
        if (classId == 0) return classId;
        var instanceId = byteStream.ReadVInt();
        return classId + instanceId < 1 ? 0 : GlobalId.CreateGlobalId(classId, instanceId);
    }

    public static int WriteGlobalId(ChecksumEncoder checksumEncoder, int globalId)
    {
        if (GlobalId.GetClassId(globalId) > 0)
        {
            checksumEncoder.WriteVInt(GlobalId.GetClassId(globalId));
            checksumEncoder.WriteVInt(GlobalId.GetInstanceId(globalId));
        }
        else
        {
            checksumEncoder.WriteVInt(0);
        }

        return globalId;
    }

    public static int WriteGlobalId(ByteStream byteStream, int globalId)
    {
        if (GlobalId.GetClassId(globalId) > 0)
        {
            byteStream.WriteVInt(GlobalId.GetClassId(globalId));
            byteStream.WriteVInt(GlobalId.GetInstanceId(globalId));
        }
        else
        {
            byteStream.WriteVInt(0);
        }

        return globalId;
    }

    public static int WriteDataReference(ChecksumEncoder checksumEncoder, int globalId)
    {
        checksumEncoder.WriteVInt(GlobalId.GetClassId(globalId));
        checksumEncoder.WriteVInt(GlobalId.GetInstanceId(globalId));

        return globalId;
    }

    public static int WriteDataReference(ByteStream byteStream, int globalId)
    {
        if (globalId > 0)
        {
            byteStream.WriteVInt(GlobalId.GetClassId(globalId));
            byteStream.WriteVInt(GlobalId.GetInstanceId(globalId));
        }
        else
        {
            byteStream.WriteVInt(0);
        }

        return globalId;
    }

    public static int WriteDataReference(ByteStream byteStream, int classId, int instanceId)
    {
        if (classId > 0)
        {
            byteStream.WriteVInt(classId);
            byteStream.WriteVInt(instanceId);
        }
        else
        {
            byteStream.WriteVInt(0);
        }

        return instanceId;
    }

    public static int WriteGlobalId(BitStream bitStream, int globalId)
    {
        if (GlobalId.GetClassId(globalId) > 0)
        {
            bitStream.WritePositiveIntMax31(GlobalId.GetClassId(globalId));
            bitStream.WritePositiveVIntMax65535(GlobalId.GetInstanceId(globalId));
        }
        else
        {
            bitStream.WritePositiveIntMax31(0);
        }

        return globalId;
    }

    public static int WriteDataReference(BitStream bitStream, int globalId)
    {
        if (GlobalId.GetClassId(globalId) > 0)
        {
            bitStream.WritePositiveIntMax31(GlobalId.GetClassId(globalId));
            bitStream.WritePositiveIntMax1023(GlobalId.GetInstanceId(globalId));
        }
        else
        {
            bitStream.WritePositiveIntMax31(0);
        }

        return globalId;
    }

    public static void WriteObjectRunningId(BitStream bitStream, int globalId)
    {
        bitStream.WritePositiveVIntMax65535(GlobalId.GetInstanceId(globalId));
    }

    public static void WriteIntList(ByteStream byteStream, List<int> list)
    {
        byteStream.WriteVInt(list.Count);
        foreach (var t in list.ToArray())
            byteStream.WriteVInt(t);
    }

    public static void WriteIntList(ChecksumEncoder checksumEncoder, List<int> list)
    {
        checksumEncoder.WriteVInt(list.Count);
        foreach (var t in list.ToArray())
            checksumEncoder.WriteVInt(t);
    }
}