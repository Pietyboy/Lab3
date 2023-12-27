//Требуется дополнить реализацию лабораторной работы №2 таким образом,
//чтобы она осуществляла сохранение и загрузку данных в форматах  JSON и XML,
//а также в базу данных SQLite.

/*Выбор варианта сохранения и загрузки данных осуществляется из меню, 
 * отображаемого пользователю при запуске программы, 
 * дополняя уже существующее (разработанное в рамках работы №2).
*/


using Lab_3;
using Lab_3.DB;
using Lab_3.XML;

using var db = new AppContext();

Console.WriteLine($"Database path: {db.DbPath}");

bool quit = false;
string command;

Console.WriteLine("Choose a way to input data");
Console.WriteLine("\t\"json\" to use JSON");
Console.WriteLine("\t\"db\" to use Sqlite");
Console.WriteLine("\t\"xml\" to use XML");
command = Console.ReadLine();


switch (command)
{
    case "json":
        new JSON_Version();
        break;
    case "db":
        new DbVersion();
        break;
    case "xml":
        new XML_Version();
        break;

}

