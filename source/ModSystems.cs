using ConfigLib;
using CombatOverhaul.Armor;
using Vintagestory.API.Common;

namespace QuiversAndSheaths;

public sealed class QuiversAndSheathsSettings
{
    public bool AllowStonesInSlingPouch { get; set; } = false;
}

public sealed class QuiversAndSheathsSystem : ModSystem
{
    public static QuiversAndSheathsSettings Settings { get; } = new();

    public override void Start(ICoreAPI api)
    {
        api.RegisterCollectibleBehaviorClass("QuiversAndSheaths:ShapeTexturesFromAttributes", typeof(ShapeTexturesFromAttributes));
        api.RegisterCollectibleBehaviorClass("QuiversAndSheaths:ShapeReplacement", typeof(ShapeReplacement));
        api.RegisterCollectibleBehaviorClass("QuiversAndSheaths:Sheath", typeof(SheathBehavior));
        api.RegisterCollectibleBehaviorClass("QuiversAndSheaths:Quiver", typeof(QuiverBehavior));
        api.RegisterCollectibleBehaviorClass("QuiversAndSheaths:VariantFromSlot", typeof(VariantFromSlotBehavior));

        if (api.ModLoader.IsModEnabled("configlib"))
        {
            SubscribeToConfigChange(api);
        }
    }

    private static void SubscribeToConfigChange(ICoreAPI api)
    {
        ConfigLibModSystem system = api.ModLoader.GetModSystem<ConfigLibModSystem>();

        system.SettingChanged += (domain, config, setting) =>
        {
            if (domain != "quiversandsheaths") return;

            setting.AssignSettingValue(Settings);
        };

        system.ConfigsLoaded += () =>
        {
            system.GetConfig("quiversandsheaths")?.AssignSettingsValues(Settings);
        };
    }
}
