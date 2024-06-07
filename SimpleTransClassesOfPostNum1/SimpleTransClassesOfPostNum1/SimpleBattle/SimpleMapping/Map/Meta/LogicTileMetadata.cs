using System.Diagnostics.CodeAnalysis;

namespace SimpleTransClassesOfPostNum1.SimpleBattle.SimpleMapping.Map.Meta;

public class LogicTileMetadata(string? metaData)
{
    [MaybeNull]
    public string MetaData { get; private set; } = metaData!;

    public void Destruct()
    {
        MetaData = null!;
    }
}