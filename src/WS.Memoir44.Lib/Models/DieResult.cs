namespace WS.Memoir44.Lib.Models;

[BasicWrapper(typeof(int), nameof(Validate))]
public partial class DieResult
{
    private static bool Validate(int value)
    {
        return value >= 1 && value <= 6;
    }
}
