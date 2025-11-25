namespace WS.Memoir44.Lib.Models;

[Option("Bunker")]
[Option("Hedgehogs")]
[Option("Wire")]
[Option("Sandbags")]
[Option("SeaWall")]
public partial class ObstacleTypeName
{
}

public record ObstacleType
(
    ObstacleTypeName Name,
    ImmutableHashSet<UnitCategory> MayEnter,
    ImmutableHashSet<UnitCategory> MayLeave,
    bool StopOnEnter,
    ImmutableHashSet<UnitCategory> MustRemoveOnEnter,
    ImmutableHashSet<UnitCategory> CanRemove,
    bool RemovedOnLeave,
    bool BlocksLineOfSight,
    ImmutableDictionary<UnitCategory, int> AttackReduction,
    ImmutableDictionary<UnitCategory, int> ProtectionFrom,
    bool IgnoreFlag)
{
    private static readonly ObstacleType Template = new(
        null!,  // Name
        [],     // MayEnter
        [],     // MayLeave
        false,  // StopOnEnter
        [],     // MustRemoveOnEnter
        [],     // CanRemove
        false,  // RemovedOnLeave
        false,  // BlocksLineOfSight
        ImmutableDictionary<UnitCategory, int>.Empty, // AttackReduction
        ImmutableDictionary<UnitCategory, int>.Empty, // ProtectionFrom
        false   // IgnoreFlag
    );

    public static readonly ObstacleType Bunker = Template with
    {
        Name = ObstacleTypeName.Bunker,
        MayEnter = [UnitCategory.Infantry],
        MayLeave = [UnitCategory.Infantry],
        BlocksLineOfSight = true,
        ProtectionFrom = new Dictionary<UnitCategory, int>
        {
            [UnitCategory.Infantry] = 1,
            [UnitCategory.Armour] = 2
        }.ToImmutableDictionary(),
        IgnoreFlag = true
    };

    public static readonly ObstacleType Hedgehogs = Template with
    {
        Name = ObstacleTypeName.Hedgehogs,
        MayEnter = [UnitCategory.Infantry],
        MayLeave = [UnitCategory.Infantry],
        IgnoreFlag = true
    };

    public static readonly ObstacleType Wire = Template with
    {
        Name = ObstacleTypeName.Wire,
        StopOnEnter = true,
        MustRemoveOnEnter = [UnitCategory.Armour],
        CanRemove = [UnitCategory.Infantry],
        AttackReduction = new Dictionary<UnitCategory, int>
        {
            [UnitCategory.Infantry] = 1
        }.ToImmutableDictionary(),
    };

    public static readonly ObstacleType Sandbags = Template with
    {
        Name = ObstacleTypeName.Sandbags,
        RemovedOnLeave = true,
        ProtectionFrom = new Dictionary<UnitCategory, int>
        {
            [UnitCategory.Infantry] = 1,
            [UnitCategory.Armour] = 1
        }.ToImmutableDictionary(),
        IgnoreFlag = true
    };

    public static readonly ObstacleType SeaWall = Sandbags with { Name = ObstacleTypeName.SeaWall, RemovedOnLeave = false };
}
