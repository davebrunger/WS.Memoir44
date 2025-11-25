namespace WS.Memoir44.Lib.Models;

[Option("Forest")]
[Option("Hedgerow")]
[Option("Hill")]
[Option("Town")]
[Option("Church")]
[Option("River")]
[Option("Bridge")]
[Option("Beach")]
[Option("Ocean")]
[Option("SeaBluff")]
[Option("Cliff")]
[Option("SteepHill")]
[Option("Lake")]
public partial class TerrainTypeName
{
    public static implicit operator Option<TerrainTypeName>(TerrainTypeName input) => Option<TerrainTypeName>.Some(input);
}

public record TerrainType(
    TerrainTypeName Name,
    int Height,
    int MovementHexCostFromLowerHeight,
    bool Impassable,
    bool StopOnEnter,
    int? MaxEnterAndLeaveMovementHexes,
    int? MaximumMovementHexes,
    ImmutableDictionary<(Option<TerrainTypeName> From, UnitCategory UnitCategory), int?> TerrainMovementHexCostOverride,
    bool CanEnterAndAttack,
    bool CanAttack,
    int? HexesToBlockLineOfSight,
    ImmutableDictionary<UnitCategory, int> AttackReduction,
    ImmutableDictionary<UnitCategory, int> ProtectionFrom,
    bool ProtectionOnlyFromLowerHeight,
    bool IgnoreFlag,
    bool BlocksRetreat,
    ImmutableHashSet<(Option<TerrainTypeName> From, UnitCategory UnitCategory)> CannotTakeGroundOverride)
{
    private static readonly TerrainType Template = new(
        null!,  // Name
        0,      // Height
        1,      // MovementHexCostFromLowerHeight
        false,  // Impassable
        false,  // StopOnEnter
        null,   // MaxEnterAndLeaveMovementHexes
        null,   // MaximumMovementHexes
        ImmutableDictionary<(Option<TerrainTypeName> From, UnitCategory UnitCategory), int?>.Empty, // TerrainMovementHexCostOverride
        true,   // CanEnterAndAttack
        true,   // CanAttack
        null,   // HexesToBlockLineOfSight
        ImmutableDictionary<UnitCategory, int>.Empty,   // AttackReduction
        ImmutableDictionary<UnitCategory, int>.Empty,   // ProtectionFrom
        false,  // ProtectionOnlyFromLowerHeight
        false,  // IgnoreFlag
        false,  // BlocksRetreat
        []      // CannotTakeGroundOverride
    );

    public static readonly TerrainType Forest = Template with
    {
        Name = TerrainTypeName.Forest,
        StopOnEnter = true,
        CanEnterAndAttack = false,
        HexesToBlockLineOfSight = 1,
        ProtectionFrom = new Dictionary<UnitCategory, int>
        {
            [UnitCategory.Infantry] = 1,
            [UnitCategory.Armour] = 2
        }.ToImmutableDictionary()
    };

    public static readonly TerrainType Hedgerow = Forest with { Name = TerrainTypeName.Hedgerow, MaxEnterAndLeaveMovementHexes = 1 };

    public static readonly TerrainType Hill = Template with
    {
        Name = TerrainTypeName.Hill,
        Height = 1,
        HexesToBlockLineOfSight = 1,
        ProtectionFrom = new Dictionary<UnitCategory, int>
        {
            [UnitCategory.Infantry] = 1,
            [UnitCategory.Armour] = 1
        }.ToImmutableDictionary(),
        ProtectionOnlyFromLowerHeight = true
    };

    public static readonly TerrainType Town = Template with
    {
        Name = TerrainTypeName.Town,
        StopOnEnter = true,
        CanEnterAndAttack = false,
        HexesToBlockLineOfSight = 1,
        AttackReduction = new Dictionary<UnitCategory, int>
        {
            [UnitCategory.Armour] = 1
        }.ToImmutableDictionary(),
        ProtectionFrom = new Dictionary<UnitCategory, int>
        {
            [UnitCategory.Infantry] = 1,
            [UnitCategory.Armour] = 2
        }.ToImmutableDictionary()
    };

    public static readonly TerrainType Church = Town with { Name = TerrainTypeName.Church, IgnoreFlag = true };

    public static readonly TerrainType River = Template with { Name = TerrainTypeName.River, Impassable = true };

    public static readonly TerrainType Bridge = River with { Name = TerrainTypeName.Bridge, Impassable = false };

    public static readonly TerrainType Beach = Template with { Name = TerrainTypeName.Beach, MaximumMovementHexes = 2 };

    public static readonly TerrainType Ocean = Template with { Name = TerrainTypeName.Ocean, StopOnEnter = true, CanAttack = false, BlocksRetreat = true };

    public static readonly TerrainType SeeBluff = Hill with
    {
        Name = TerrainTypeName.SeaBluff,
        TerrainMovementHexCostOverride = new Dictionary<(Option<TerrainTypeName> From, UnitCategory UnitCategory), int?>
        {
            [(TerrainTypeName.Beach, UnitCategory.Infantry)] = 2,
            [(TerrainTypeName.Beach, UnitCategory.Armour)] = null,
            [(TerrainTypeName.Beach, UnitCategory.Artillery)] = null
        }.ToImmutableDictionary()
    };

    public static readonly TerrainType Cliff = SeeBluff with
    {
        Name = TerrainTypeName.Cliff,
        CannotTakeGroundOverride = [(TerrainTypeName.Beach, UnitCategory.Infantry)]
    };

    public static readonly TerrainType SteepHill = Hill with
    {
        Name = TerrainTypeName.SteepHill,
        MovementHexCostFromLowerHeight = 2
    };

    public static readonly TerrainType Lake = River with { Name = TerrainTypeName.Lake, HexesToBlockLineOfSight = 2 };
}
