using System.Reflection;
using SimpleTransClassesOfPostNum1.SimpleDataTables.DataSector;
using SimpleTransClassesOfPostNum1.SimpleDataTables.Manufacturer;
using SimpleTransClassesOfPostNum1.SimpleDataTables.Manufacturer.LaserMicro;
using SimpleTransClassesOfPostNum1.SimpleDataTables.Omniscient;
using SimpleTransClassesOfPostNum1.SimpleHTables;
using SimpleTransClassesOfPostNum1.SimpleMath.IdManage;
using SimpleTransClassesOfPostNum1.SimpleUtilities;

namespace SimpleTransClassesOfPostNum1.SimpleDataTables;

public static class LogicDataTables
{
    private static string _globalPath = null!;
    private static Dictionary<int, LogicDataTable>? _dataTables;
    private static bool _isLoaded;

    public static void CreateReferences(string gPath)
    {
        _globalPath = gPath;
        _dataTables = new Dictionary<int, LogicDataTable>();

        // or 1/>
        foreach (CsvFilesHelperTable csvMember in Enum.GetValues(typeof(CsvFilesHelperTable)))
            InitDataTable(csvMember.GetFileName(), csvMember.GetId());

        // or 2/>
        /*InitDataTable(CsvFilesHelperTable.Accessories.GetFileName(), CsvFilesHelperTable.Accessories.GetId());
        InitDataTable(CsvFilesHelperTable.AllianceBadges.GetFileName(), CsvFilesHelperTable.AllianceBadges.GetId());
        InitDataTable(CsvFilesHelperTable.AllianceRoles.GetFileName(), CsvFilesHelperTable.AllianceRoles.GetId());
        InitDataTable(CsvFilesHelperTable.AreaEffects.GetFileName(), CsvFilesHelperTable.AreaEffects.GetId());

        InitDataTable(CsvFilesHelperTable.Cards.GetFileName(), CsvFilesHelperTable.Cards.GetId());
        InitDataTable(CsvFilesHelperTable.Characters.GetFileName(), CsvFilesHelperTable.Characters.GetId());

        InitDataTable(CsvFilesHelperTable.Emotes.GetFileName(), CsvFilesHelperTable.Emotes.GetId());
        InitDataTable(CsvFilesHelperTable.GameModeVariation.GetFileName(),
            CsvFilesHelperTable.GameModeVariation.GetId());

        InitDataTable(CsvFilesHelperTable.GearBoosts.GetFileName(), CsvFilesHelperTable.GearBoosts.GetId());
        InitDataTable(CsvFilesHelperTable.GearLevels.GetFileName(), CsvFilesHelperTable.GearLevels.GetId());
        InitDataTable(CsvFilesHelperTable.Globals.GetFileName(), CsvFilesHelperTable.Globals.GetId());

        InitDataTable(CsvFilesHelperTable.Items.GetFileName(), CsvFilesHelperTable.Items.GetId());

        InitDataTable(CsvFilesHelperTable.Locations.GetFileName(), CsvFilesHelperTable.Locations.GetId());
        InitDataTable(CsvFilesHelperTable.LocationThemes.GetFileName(), CsvFilesHelperTable.LocationThemes.GetId());

        InitDataTable(CsvFilesHelperTable.Messages.GetFileName(), CsvFilesHelperTable.Messages.GetId());
        InitDataTable(CsvFilesHelperTable.Milestones.GetFileName(), CsvFilesHelperTable.Milestones.GetId());

        InitDataTable(CsvFilesHelperTable.NameColors.GetFileName(), CsvFilesHelperTable.NameColors.GetId());
        InitDataTable(CsvFilesHelperTable.PlayerThumbnails.GetFileName(), CsvFilesHelperTable.PlayerThumbnails.GetId());

        InitDataTable(CsvFilesHelperTable.Projectiles.GetFileName(), CsvFilesHelperTable.Projectiles.GetId());
        InitDataTable(CsvFilesHelperTable.Regions.GetFileName(), CsvFilesHelperTable.Regions.GetId());
        InitDataTable(CsvFilesHelperTable.Resources.GetFileName(), CsvFilesHelperTable.Resources.GetId());

        InitDataTable(CsvFilesHelperTable.Skills.GetFileName(), CsvFilesHelperTable.Skills.GetId());
        InitDataTable(CsvFilesHelperTable.SkinConfs.GetFileName(), CsvFilesHelperTable.SkinConfs.GetId());
        InitDataTable(CsvFilesHelperTable.Skins.GetFileName(), CsvFilesHelperTable.Skins.GetId());
        InitDataTable(CsvFilesHelperTable.Sprays.GetFileName(), CsvFilesHelperTable.Skins.GetId());

        InitDataTable(CsvFilesHelperTable.Themes.GetFileName(), CsvFilesHelperTable.Themes.GetId());
        InitDataTable(CsvFilesHelperTable.Tiles.GetFileName(), CsvFilesHelperTable.Tiles.GetId());*/

        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Start,
            $"Logic-element started! Information: element name: LogicDataTables; table buffer size: {_dataTables.Count}.");
        {
            _isLoaded = true;
        }
    }

    private static void InitDataTable(string path, int tableIndex)
    {
        try
        {
            if (!File.Exists(_globalPath + path)) return;
            {
                var lines = File.ReadAllLines(_globalPath + path);
                {
                    if (lines.Length <= 1) return;

                    var table = new LogicDataTable(tableIndex, new CsvNode(lines, path).GetTable());
                    {
                        if (table == null!)
                        {
                            ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Warn,
                                $"Implementation of the LogicDataTable ({tableIndex}) was not found (table).");
                            return;
                        }
                    }

                    if (table.CreateItem(new CsvNode(lines, path).GetTable().GetRowAt(tableIndex)) == null!)
                    {
                        ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Warn,
                            $"Implementation of the LogicDataTable ({tableIndex}) was not found (item).");
                        return;
                    }

                    _dataTables!.Add(tableIndex, table);
                    ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Info,
                        $"Implementation of the LogicDataTable ({tableIndex}) added to dict (ssf).");
                }
            }
        }
        catch
        {
            ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Warn,
                $"Implementation of the LogicDataTable ({tableIndex}) was not found (catch).");
        }
    }

    public static LogicDataTable GetDataFromTable(int tableIndex)
    {
        return tableIndex >= 0 && _dataTables![tableIndex] != null!
            ? _dataTables[tableIndex]
            : null!;
    }

    public static LogicData GetDataById(int globalId)
    {
        return GlobalId.GetClassId(globalId) >= 1 && _dataTables![GlobalId.GetClassId(globalId)] != null!
            ? _dataTables[GlobalId.GetClassId(globalId)].GetItemById(globalId)
            : null!;
    }

    public static T GetDataById<T>(int globalId) where T : LogicData
    {
        return GlobalId.GetClassId(globalId) >= 1 && _dataTables![GlobalId.GetClassId(globalId)] != null!
            ? (T)_dataTables[GlobalId.GetClassId(globalId)].GetItemById(globalId)
            : null!;
    }

    public static LogicData[] GetAllDataFromCsvById(int id)
    {
        if (GetDataFromTable(id) == null!) return null!;

        var data = Array.Empty<LogicData>();
        {
            for (var i = 0; i < GetDataFromTable(id).GetItemCount(); i++)
            {
                Array.Resize(ref data, data.Length + 1);
                {
                    data[^1] = GetDataById(GlobalId.CreateGlobalId(id, i));
                }
            }
        }

        return data;
    }

    public static T[] GetAllDataFromCsvById<T>(int id) where T : LogicData
    {
        if (GetDataFromTable(id) == null!) return null!;

        var data = Array.Empty<T>();
        {
            for (var i = 0; i < GetDataFromTable(id).GetItemCount(); i++)
            {
                Array.Resize(ref data, data.Length + 1);
                {
                    data[^1] = GetDataById<T>(GlobalId.CreateGlobalId(id, i));
                }
            }
        }

        return data;
    }

    public static LogicResourceData GetResourceByName(string name)
    {
        return (LogicResourceData)_dataTables![CsvFilesHelperTable.Resources.GetId()].GetDataByName(name);
    }

    public static LogicAccessoryData GetAccessoryByName(string name)
    {
        return (LogicAccessoryData)_dataTables![CsvFilesHelperTable.Accessories.GetId()].GetDataByName(name);
    }

    public static LogicCharacterData GetCharacterByName(string name)
    {
        return (LogicCharacterData)_dataTables![CsvFilesHelperTable.Characters.GetId()].GetDataByName(name);
    }

    public static LogicCardData GetCardByName(string name)
    {
        return (LogicCardData)_dataTables![CsvFilesHelperTable.Cards.GetId()].GetDataByName(name);
    }

    public static LogicProjectileData GetProjectileByName(string name)
    {
        return (LogicProjectileData)_dataTables![CsvFilesHelperTable.Projectiles.GetId()].GetDataByName(name);
    }

    public static LogicLocationData GetLocationByName(string name)
    {
        return (LogicLocationData)_dataTables![CsvFilesHelperTable.Locations.GetId()].GetDataByName(name);
    }

    public static LogicAllianceRoleData GetAllianceRoleByName(string name)
    {
        return (LogicAllianceRoleData)_dataTables![CsvFilesHelperTable.AllianceRoles.GetId()].GetDataByName(name);
    }

    public static LogicLocationThemeData GetLocationThemeByName(string name)
    {
        return (LogicLocationThemeData)_dataTables![CsvFilesHelperTable.LocationThemes.GetId()].GetDataByName(name);
    }

    public static LogicAreaEffectData GetAreaEffectByName(string name)
    {
        return (LogicAreaEffectData)_dataTables![CsvFilesHelperTable.AreaEffects.GetId()].GetDataByName(name);
    }

    public static LogicItemData GetItemByName(string name)
    {
        return (LogicItemData)_dataTables![CsvFilesHelperTable.Items.GetId()].GetDataByName(name);
    }

    public static LogicEmoteData GetEmoteByName(string name)
    {
        return (LogicEmoteData)_dataTables![CsvFilesHelperTable.Emotes.GetId()].GetDataByName(name);
    }

    public static LogicSkillData GetSkillByName(string name)
    {
        return (LogicSkillData)_dataTables![CsvFilesHelperTable.Skills.GetId()].GetDataByName(name);
    }

    public static LogicSkinData GetSkinByName(string name)
    {
        return (LogicSkinData)_dataTables![CsvFilesHelperTable.Skins.GetId()].GetDataByName(name);
    }

    public static LogicThemeData GetThemeByName(string name)
    {
        return (LogicThemeData)_dataTables![CsvFilesHelperTable.Themes.GetId()].GetDataByName(name);
    }

    public static LogicSkinConfData GetSkinConfByName(string name)
    {
        return (LogicSkinConfData)_dataTables![CsvFilesHelperTable.SkinConfs.GetId()].GetDataByName(name);
    }

    public static LogicGameModeVariationData GetGameModeVariationByName(string name)
    {
        return (LogicGameModeVariationData)_dataTables![CsvFilesHelperTable.GameModeVariation.GetId()]
            .GetDataByName(name);
    }

    public static LogicMilestoneData GetMilestoneByName(string name)
    {
        return (LogicMilestoneData)_dataTables![CsvFilesHelperTable.Milestones.GetId()].GetDataByName(name);
    }

    public static LogicSprayData GetSprayByName(string name)
    {
        return (LogicSprayData)_dataTables![CsvFilesHelperTable.Sprays.GetId()].GetDataByName(name);
    }

    public static int GetTableCount()
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Count(type => typeof(LogicData).IsAssignableFrom(type) && type != typeof(LogicData));
    }

    public static bool IsLoaded()
    {
        return _isLoaded;
    }
}