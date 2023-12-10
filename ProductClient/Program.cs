using ProductClient;
using static System.Net.WebRequestMethods;
ProductHelper.BaseUrl = "https://localhost:5001";
A:
Console.WriteLine(new string('=', 120));
Console.WriteLine("\t\t\t\t\t\t | \t Login \t |");
Console.WriteLine(new string('=', 120));

Console.Write("\t\t\t\t\t Enter your username: ");
var user=Console.ReadLine();
Console.Write("\t\t\t\t\t Enter your password: ");
var pass = Console.ReadLine();
if(user =="samut" && pass == "123" || user=="Samut" && pass=="123")
{
    Console.Clear();
    Console.WriteLine(new string('=', 120));
    Console.WriteLine("\t\t\t\t\t\t |\tCoffe Management \t|");
    Console.WriteLine(new string('=', 120));
    ProductHelper.MenuBank.MenuSimulate(() => { Console.WriteLine(); });
}
else
{
    Console.Clear();
    Console.WriteLine("Invalid username and password ..............");
    goto A;
}




