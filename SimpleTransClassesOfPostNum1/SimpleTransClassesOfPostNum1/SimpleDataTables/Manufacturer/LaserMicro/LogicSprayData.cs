using SimpleTransClassesOfPostNum1.SimpleDataTables.DataSector;
using SimpleTransClassesOfPostNum1.SimpleDataTables.Omniscient;

namespace SimpleTransClassesOfPostNum1.SimpleDataTables.Manufacturer.LaserMicro;

public class LogicSprayData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private string _character = null!;
    private bool _disabled;
    private int _effectColorB;
    private int _effectColorG;
    private int _effectColorR;
    private string _exportName = null!;
    private string _fileName = null!;
    private bool _flipSprayForEnemies;
    private bool _isDefaultBattleSpray;
    private bool _lockedForChronos;
    private string _rarity = null!;
    private string _skin = null!;
    private string _sprayBundles = null!;
    private string _texture = null!;

    // LogicSprayData.

    public override void CreateReferences()
    {
        _disabled = GetBooleanValue("Disabled", 0);
        _fileName = GetValue("FileName", 0);
        _exportName = GetValue("ExportName", 0);
        _character = GetValue("Character", 0);
        _skin = GetValue("Skin", 0);
        _rarity = GetValue("Rarity", 0);
        _effectColorR = GetIntegerValue("EffectColorR", 0);
        _effectColorG = GetIntegerValue("EffectColorG", 0);
        _effectColorB = GetIntegerValue("EffectColorB", 0);
        _flipSprayForEnemies = GetBooleanValue("FlipSprayForEnemies", 0);
        _lockedForChronos = GetBooleanValue("LockedForChronos", 0);
        _sprayBundles = GetValue("SprayBundles", 0);
        _isDefaultBattleSpray = GetBooleanValue("IsDefaultBattleSpray", 0);
        _texture = GetValue("Texture", 0);
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

    public string GetRarity()
    {
        return _rarity;
    }

    public int GetEffectColorR()
    {
        return _effectColorR;
    }

    public int GetEffectColorG()
    {
        return _effectColorG;
    }

    public int GetEffectColorB()
    {
        return _effectColorB;
    }

    public bool GetFlipSprayForEnemies()
    {
        return _flipSprayForEnemies;
    }

    public bool GetLockedForChronos()
    {
        return _lockedForChronos;
    }

    public string GetSprayBundles()
    {
        return _sprayBundles;
    }

    public bool GetIsDefaultBattleSpray()
    {
        return _isDefaultBattleSpray;
    }

    public string GetTexture()
    {
        return _texture;
    }
}