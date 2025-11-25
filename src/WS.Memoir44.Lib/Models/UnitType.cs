namespace WS.Memoir44.Lib.Models;

[Option("Infantry")]
[Option("Armour")]
[Option("Artillery")]
public partial class UnitCategory
{
}

[Option("Infantry")]
[Option("EliteInfantry")]
[Option("Resistance")]
[Option("Armour")]
[Option("EliteArmour")]
[Option("Artillery")]
public partial class UnitTypeName
{
    public UnitCategory Category => Match(
        () => UnitCategory.Infantry,
        () => UnitCategory.Infantry,
        () => UnitCategory.Infantry,
        () => UnitCategory.Armour,
        () => UnitCategory.Armour,
        () => UnitCategory.Artillery
    );
}

public record UnitType(
    UnitTypeName Name,
    int FiguresPerUnit,
    int MovementHexes,
    int MovementHexesAndAttack,
    bool IgnoreEnterAndAttackRestrictions,
    ImmutableList<int> AttackRange,
    bool CanTakeGround,
    bool CanOverrun,
    int MaxRetreatHexesPerFlag)
{
    public UnitCategory Category => Name.Category;

    public static readonly UnitType Infantry = new(UnitTypeName.Infantry, 4, 2, 1, false, [3, 2, 1], true, false, 1);
    public static readonly UnitType EliteInfantry = Infantry with { Name = UnitTypeName.EliteInfantry, MovementHexesAndAttack = 2 };
    public static readonly UnitType Resistance = Infantry with { Name = UnitTypeName.Resistance, FiguresPerUnit = 3, IgnoreEnterAndAttackRestrictions = true, MaxRetreatHexesPerFlag = 3 };

    public static readonly UnitType Armour = new(UnitTypeName.Armour, 3, 3, 3, false, [3, 3, 3], true, true, 1);
    public static readonly UnitType EliteArmour = Armour with { Name = UnitTypeName.EliteArmour, FiguresPerUnit = 4 };

    public static readonly UnitType Artillery = new(UnitTypeName.Artillery, 2, 1, 0, false, [3, 3, 2, 2, 1, 1], false, false, 1);
}
