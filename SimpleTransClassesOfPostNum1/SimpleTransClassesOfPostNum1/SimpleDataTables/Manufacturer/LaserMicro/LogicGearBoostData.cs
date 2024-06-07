using SimpleTransClassesOfPostNum1.SimpleDataTables.DataSector;
using SimpleTransClassesOfPostNum1.SimpleDataTables.Omniscient;

namespace SimpleTransClassesOfPostNum1.SimpleDataTables.Manufacturer.LaserMicro;

public class LogicGearBoostData(CsvRow row, LogicDataTable table) : LogicData(row, table)
{
    private int _damageBoost;
    private int _heal;
    private int _healthShield;
    private string _iconSwf = null!;
    private int _logicType;
    private string _resource = null!;
    private string _shopFrameName = null!;
    private int _speedIncrease;
    private string _tokenTypeTid = null!;
    private string _upgradeInfoTid = null!;
    private string _upgradeTargetTid = null!;
    private int _visionTicks;

    // LogicGearBoostData.

    public override void CreateReferences()
    {
        _logicType = GetIntegerValue("LogicType", 0);
        _speedIncrease = GetIntegerValue("SpeedIncrease", 0);
        _heal = GetIntegerValue("Heal", 0);
        _damageBoost = GetIntegerValue("DamageBoost", 0);
        _visionTicks = GetIntegerValue("VisionTicks", 0);
        _healthShield = GetIntegerValue("HealthShield", 0);
        _resource = GetValue("Resource", 0);
        _iconSwf = GetValue("IconSWF", 0);
        _tokenTypeTid = GetValue("TokenTypeTID", 0);
        _shopFrameName = GetValue("ShopFrameName", 0);
        _upgradeInfoTid = GetValue("UpgradeInfoTID", 0);
        _upgradeTargetTid = GetValue("UpgradeTargetTID", 0);
    }

    public int GetLogicType()
    {
        return _logicType;
    }

    public int GetSpeedIncrease()
    {
        return _speedIncrease;
    }

    public int GetHeal()
    {
        return _heal;
    }

    public int GetDamageBoost()
    {
        return _damageBoost;
    }

    public int GetVisionTicks()
    {
        return _visionTicks;
    }

    public int GetHealthShield()
    {
        return _healthShield;
    }

    public string GetResource()
    {
        return _resource;
    }

    public string GetIconSwf()
    {
        return _iconSwf;
    }

    public string GetTokenTypeTid()
    {
        return _tokenTypeTid;
    }

    public string GetShopFrameName()
    {
        return _shopFrameName;
    }

    public string GetUpgradeInfoTid()
    {
        return _upgradeInfoTid;
    }

    public string GetUpgradeTargetTid()
    {
        return _upgradeTargetTid;
    }
}