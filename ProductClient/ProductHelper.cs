using MenuLib;
using ProductLib;
using RestClientLib;

namespace ProductClient;
public static class ProductHelper
{
    public static string BaseUrl { get; set; } = "https://localhost:5001";
    public static MenuBank MenuBank { get; set; } = new MenuBank()
    {
        Title = "Coffe",
        Menus = new List<Menu>()
        {
            new Menu(){ Text= "\tViewing", Action=ViewingCoffes},
            new Menu(){ Text= "\tCreating", Action=CreatingCoffes},
            new Menu(){ Text= "\tUpdating", Action=UpdatingCoffes},
            new Menu(){ Text= "\tDeleting", Action=DeletingCoffes},
            new Menu(){ Text= "\tExiting", Action = ExitingProgram}
        }
    };
    public static void ExitingProgram()
        {
            Console.WriteLine("\n[Exiting Program]");
            Environment.Exit(0);
        }
    private static void DeletingCoffes()
    {
        Console.Clear();
        Task.Run(async () =>
        {
            RestClient<Coffe> restClient = new(BaseUrl);
            Console.WriteLine("\n[Deleting Coffe]");
            while (true)
            {
                Console.Write("Product Id/Code: ");
                var key = Console.ReadLine() ?? "";
                var endpoint = $"api/Coffes/{key}";
                var result = await restClient.DeleteAsync<Result<string>>(endpoint);
                if (result!.Data != null)
                {
                    Console.WriteLine($"Successfully delete the coffe with id/code, {key}");
                }
                else
                {
                    Console.WriteLine($"Failed to delete a coffe with id/code, {key}");
                }

                if (WaitForEscPressed("ESC to stop or any key for more deleting ..."))
                {
                    break;
                }
            }
        }).Wait();
    }
    private static void UpdatingCoffes()
    {
        Console.Clear();
        Task.Run(async () =>
        {
            RestClient<Coffe> restClient = new(BaseUrl);
            Console.WriteLine("\n[Updating Coffes]");
            while (true)
            {
                Console.Write("Coffe Id/Code(required): ");
                var key = Console.ReadLine() ?? "";
                var endpoint = "api/Coffes";
                Console.Write("New Name (optional)  : ");
                var name = Console.ReadLine();
                Console.WriteLine($"Category available: {Enum.GetNames<Category>().Aggregate((a, b) => a + ", " + b)}");
                Console.Write("New Category: ");
                var category = Console.ReadLine();
                Console.Write("New Price: ");
                var prices= Console.ReadLine();

                var result = await restClient.PutAsync<CoffeUpdateReq, Result<string>>(endpoint, new CoffeUpdateReq()
                {
                    Key = key,
                    Name = name,
                    Category = category,
                    Price = float.Parse(prices+"")
                }) ;

                if (result!.Data !=null)
                {
                    Console.WriteLine($"Successfully update the coffe with id/code, {key}");
                }
                else
                {
                    Console.WriteLine($"Failed to update the coffe with id/code, {key}");
                }
                Console.WriteLine();
                if (WaitForEscPressed("ESC to stop or any key for more updating...")) break;
            }
        }).Wait();
    }
    private static bool WaitForEscPressed(string text)
    { 
        Console.Write(text);;
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        Console.WriteLine(keyInfo.KeyChar);
        return keyInfo.Key == ConsoleKey.Escape;
    }
    private static void CreatingCoffes()
    {
        Console.Clear();
        Task.Run(async () =>
        {
            RestClient<Coffe> restClient = new(BaseUrl);
            Console.WriteLine("\n[Creating Coffe]");
            var endpoint = "api/Coffes";
            while (true)
            {
                var req = GetCreateProduct();
                if (req != null)
                {
                    var result = await restClient.PostAsync<CoffeCreateReq, Result<string>>(endpoint, req);
                    var id = result!.Data;
                    if (!string.IsNullOrEmpty(id))
                        Console.WriteLine($"Successfully created a new coffe with id, {id}");
                    else
                        Console.WriteLine($"Failed to create a new coffe code, {req.Code}");
                }

                Console.WriteLine();
                if (WaitForEscPressed("ESC to stop or any key for more creating...")) break;
            }
        }).Wait();
    }
    static CoffeCreateReq? GetCreateProduct()
    {
        Console.WriteLine($"Category available: {Enum.GetNames<Category>().Aggregate((a, b) => a + ", " + b)}");
        Console.Write("Product(code/name/category/price): ");
        string data = Console.ReadLine() ?? "";
        var dataParts = data.Split("/");
        
        if (dataParts.Length < 4)
        {
            Console.WriteLine("Invalid create coffe's data");
            return null;
        }
        var code = dataParts[0].Trim();
        var name = dataParts[1].Trim();
        var category = dataParts[2].Trim();
        var price = float.Parse(dataParts[3].Trim());
       
        return new CoffeCreateReq() { Code = code, Name = name, Category = category,Price=price};

    }
    private static  void ViewingCoffes()
    {
        Console.Clear();
        Task.Run(async () =>
        {
            RestClient<Coffe> restClient = new(BaseUrl);
            Console.WriteLine("\n[Viewing Coffes]");
            var endpoint = "api/Coffes";
            var result = await restClient.GetAsync<Result<List<CoffeResponse>>>(endpoint) ?? new();
            var all = result!.Data??new();
            var count = all.Count;
            Console.WriteLine($"There are {count} Coffes in Stock");
            if (count == 0) return;
            Console.WriteLine(new string('=', 120));
            Console.WriteLine($"{"Id",-36}|{"Code",-10}|{"Name",-30}|{"Category",-20}|{"Price",-20}");
            Console.WriteLine(new string('=', 36 + 1 + 10 + 1 + 30 + 1 + 20+20+1));
            foreach (var prd in all)
            {
                Console.WriteLine($"{prd.Id,-36} {prd.Code,-10} {prd.Name,-30} {prd.Category,-20} {prd.Price,-20} ");
            }
        }).Wait();
    }
}
