namespace ProductLib;

public class CoffeUpdateReq
{
    public string Key{ get; set; } = default!;
    public string? Name { get; set; } = default;
    public string? Category { get; set; } = default;
    public float? Price { get; set; } = default;
}