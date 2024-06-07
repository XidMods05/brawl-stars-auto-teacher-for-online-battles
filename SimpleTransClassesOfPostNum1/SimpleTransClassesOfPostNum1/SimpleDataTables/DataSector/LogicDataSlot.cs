using Newtonsoft.Json.Linq;
using SimpleTransClassesOfPostNum1.SimpleDataStream;
using SimpleTransClassesOfPostNum1.SimpleDataStream.Checksum;
using SimpleTransClassesOfPostNum1.SimpleDataStream.Helper;

namespace SimpleTransClassesOfPostNum1.SimpleDataTables.DataSector;

public class LogicDataSlot(LogicData data, int count)
{
    public LogicData GetData()
    {
        return data;
    }

    public LogicDataSlot Clone()
    {
        return new LogicDataSlot(data, count);
    }

    public void Encode(ByteStream byteStream)
    {
        ByteStreamHelper.WriteDataReference(byteStream, data.GetGlobalId());
        byteStream.WriteVInt(-1);
        byteStream.WriteVInt(count);
    }

    public void Encode(ChecksumEncoder encoder)
    {
        ByteStreamHelper.WriteDataReference(encoder, data.GetGlobalId());
        encoder.WriteVInt(-1);
        encoder.WriteVInt(count);
    }

    public void Decode(ByteStream stream)
    {
        data = LogicDataTables.GetDataById(ByteStreamHelper.ReadDataReference(stream));
        _ = stream.ReadVInt();
        count = stream.ReadVInt();
    }

    public int GetCount()
    {
        return count;
    }

    public void SetCount(int countD)
    {
        count = countD;
    }

    public void ReadFromJson(JObject obj)
    {
        var id = (int)obj["data"]!;

        data = LogicDataTables.GetDataById(id);
        count = (int)obj["count"]!;
    }

    public JObject WriteToJson()
    {
        var obj = new JObject();
        {
            obj["data"] = data.GetGlobalId();
            obj["count"] = count;
        }

        return obj;
    }
}