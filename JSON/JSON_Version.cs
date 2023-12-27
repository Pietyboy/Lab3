using Newtonsoft.Json;

namespace Lab_3
{
    internal class JSON_Version
    {
        private readonly List<Author> json_file;
        private string path = "C:\\Users\\danlu\\Documents\\ITMO\\C#\\Lab_3\\singer.json";

        public JSON_Version() 
        {
            string result;
            try
            {
                json_file = JsonConvert.DeserializeObject<List<Author>>(File.ReadAllText(path));
            } catch(Exception e)
            {
                Console.WriteLine("Json didn't create");
                json_file = new List<Author>();
                result = JsonConvert.SerializeObject(json_file);
                File.WriteAllText(path, result);
            }
            bool quit = false;
            string command;

            while (!quit)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("Type one of commands:");
                Console.WriteLine("\t\"list\" to display all items of catalog");
                Console.WriteLine("\t\"search\" to go find items in catalog");
                Console.WriteLine("\t\"add singer\" to add new item");
                Console.WriteLine("\t\"add song\" to add new item");
                Console.WriteLine("\t\"del\" to remove some item from catalog");
                Console.WriteLine("\t\"quit\" to exit");
                Console.WriteLine("Write a command:");

                command = Console.ReadLine();
                switch (command)
                {
                    case "quit":
                        quit = true;
                        break;

                    case "add singer":
                        Console.WriteLine("Input singer's name:");
                        string name = Console.ReadLine();

                        while (!SingerChecker(name)) 
                        {
                            Console.WriteLine("This singer already in list");
                            Console.WriteLine("Please enter another singer or press \"Enter\"");
                            name = Console.ReadLine();
                            if (name == "") break;
                        }

                        json_file.Add(new Author()
                        {
                            Name = name
                        });
                        result = JsonConvert.SerializeObject(json_file);
                        File.WriteAllText(path, result);
                        break;

                    case "add song":
                        Console.WriteLine("Input singer name:");
                        string singer = Console.ReadLine();
                        if (json_file.Where(x => x.Name == singer) == null)
                        {
                            json_file.Add(new Author()
                            {
                                Name = singer
                            });
                        }
                        Console.WriteLine("Input song title:");
                        string song = Console.ReadLine();
                        json_file.Where(x => x.Name == singer).First().Song.Add( new Song() { SongTitle = song });
                        result = JsonConvert.SerializeObject(json_file);
                        File.WriteAllText(path, result);
                        break;

                    case "del":
                        Console.WriteLine("Input singer's name that will be delete");
                        string delName = Console.ReadLine();

                        try
                        {
                            json_file.Where(x => x.Name == delName).Single();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Please input another singer");
                            delName = Console.ReadLine();
                        }
                        
                        json_file.Remove(json_file.Where(x => x.Name == delName).First());
                        result = JsonConvert.SerializeObject(json_file);
                        File.WriteAllText(path, result);
                        break;

                    case "search":
                        Console.WriteLine("Please input name that you looking for:");
                        string search = Console.ReadLine();
                        try
                        {
                            json_file.Where(x => x.Name == search).First();
                        }
                        catch
                        {
                            Console.WriteLine("This samething strange");
                            Console.WriteLine("Please input another singer");
                            search = Console.ReadLine();
                        }
                        var output = json_file.Where(x => x.Name == search).First();
                        Console.WriteLine(output.Name);
                        foreach (var item in output.Song) Console.WriteLine("\t-" + item.SongTitle);
                        break;

                    case "list":
                        foreach (var item in json_file)
                        {
                            Console.WriteLine(item.Name);
                            for(int i = 0; i < item.Song.Count; i++)
                            {
                                Console.WriteLine("\t-" + item.Song[i].SongTitle);
                            }
                        }
                        break;
                }
            }
        }   

        private bool SingerChecker(string name)
        {
            return json_file.Where(x => x.Name == name).Count() == 0;
        }
    }
}
