namespace ProductLib;
public class Coffe
{
    public string Id {get;set;}=default!;
    public string Code {get;set;}=default!;
    public string? Name {get;set;}=default;
    public Category Category {get;set;} = Category.None;
    public float ? Price { get;set;}=default!;
    public DateTime? CreatedOn {get;set;}= default;
}
