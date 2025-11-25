using WS.DomainModelling.Common;

namespace WS.Memoir44.Lib.Models;

[Option("Infantry")]
[Option("Armour")]
[Option("Grenade")]
[Option("Star")]
[Option("Flag")]
public partial class DieFace
{
    public static implicit operator Option<DieFace>(DieFace input) => Option<DieFace>.Some(input);
}

public static class Die
{
    public static DieFace GetDieFace(DieResult dieResult)
    {
        return dieResult.Value switch
        {
            1 => DieFace.Infantry,
            2 => DieFace.Infantry,
            3 => DieFace.Armour,
            4 => DieFace.Grenade,
            5 => DieFace.Star,
            _ => DieFace.Flag,
        };
    }
}
