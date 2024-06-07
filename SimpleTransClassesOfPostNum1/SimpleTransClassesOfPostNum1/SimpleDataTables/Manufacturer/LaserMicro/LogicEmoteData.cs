using SimpleTransClassesOfPostNum1.SimpleDataTables.DataSector;
using SimpleTransClassesOfPostNum1.SimpleDataTables.Omniscient;

namespace SimpleTransClassesOfPostNum1.SimpleDataTables.Manufacturer.LaserMicro;

public class LogicEmoteData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private int _bundleCode;
    private string _character = null!;
    private bool _disabled;
    private string _exportName = null!;
    private string _fileName = null!;
    private bool _giveOnSkinUnlock;
    private bool _isDefaultBattleEmote;
    private bool _lockedForChronos;
    private int _rarity;
    private string _skin = null!;
    private int _type;

    // LogicEmoteData.

    public override void CreateReferences()
    {
        _disabled = GetBooleanValue("Disabled", 0);
        _fileName = GetValue("FileName", 0);
        _exportName = GetValue("ExportName", 0);
        _character = GetValue("Character", 0);
        _skin = GetValue("Skin", 0);
        _type = GetIntegerValue("Type", 0);
        _rarity = GetIntegerValue("Rarity", 0);
        _lockedForChronos = GetBooleanValue("LockedForChronos", 0);
        _bundleCode = GetIntegerValue("BundleCode", 0);
        _isDefaultBattleEmote = GetBooleanValue("IsDefaultBattleEmote", 0);
        _giveOnSkinUnlock = GetBooleanValue("GiveOnSkinUnlock", 0);
    }

    public bool GetDisabled()
    {
        return _disabled;
    }

    public string GetFileName()
    {
        return _fileName;
    }

    public string GetExportName()
    {
        return _exportName;
    }

    public string GetCharacter()
    {
        return _character;
    }

    public string GetSkin()
    {
        return _skin;
    }

    public new int GetType()
    {
        return _type;
    }

    public int GetRarity()
    {
        return _rarity;
    }

    public bool GetLockedForChronos()
    {
        return _lockedForChronos;
    }

    public int GetBundleCode()
    {
        return _bundleCode;
    }

    public bool GetIsDefaultBattleEmote()
    {
        return _isDefaultBattleEmote;
    }

    public bool GetGiveOnSkinUnlock()
    {
        return _giveOnSkinUnlock;
    }
}