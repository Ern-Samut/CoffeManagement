namespace ProductLib;
public class CoffeResponse
{
    public string? Id {get;set;}=default;
    public string Code {get;set;}=default!;
    public string? Name {get;set;}=default;
    public string? Category {get;set;} = default;
    public float? Price { get; set; } = default!;
}