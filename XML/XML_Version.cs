using Newtonsoft.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lab_3.XML
{
    internal class XML_Version
    {
        private List<Author> list = new List<Author>();
        private string path = "C:/Users/danlu/Documents/ITMO/C#/Lab_3/singer_xml.xml";
        XmlDocument xml;



        public XML_Version()
        {
            if (!File.Exists(path))
            {
                ToXMLAndSave(list, path);
            }
            ToObject(path);
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

                        list.Add(new Author()
                        {
                            Name = name
                        });
                        ToXMLAndSave(list, path);
                        break;

                    case "add song":
                        Console.WriteLine("Input singer name:");
                        string singer = Console.ReadLine();
                        if (list.Where(x => x.Name == singer) == null)
                        {
                            list.Add(new Author()
                            {
                                Name = singer
                            });
                        }
                        Console.WriteLine("Input song title:");
                        string song = Console.ReadLine();
                        list.Where(x => x.Name == singer).First().Song.Add(new Song() { SongTitle = song });
                        ToXMLAndSave(list, path);
                        break;

                    case "del":
                        Console.WriteLine("Input singer's name that will be delete");
                        string delName = Console.ReadLine();

                        try
                        {
                            list.Where(x => x.Name == delName).Single();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Please input another singer");
                            delName = Console.ReadLine();
                        }

                        list.Remove(list.Where(x => x.Name == delName).First());
                        ToXMLAndSave(list, path);
                        break;

                    case "search":
                        Console.WriteLine("Please input name that you looking for:");
                        string search = Console.ReadLine();
                        try
                        {
                            list.Where(x => x.Name == search).First();
                        }
                        catch
                        {
                            Console.WriteLine("This samething strange");
                            Console.WriteLine("Please input another singer");
                            search = Console.ReadLine();
                        }
                        var output = list.Where(x => x.Name == search).First();
                        Console.WriteLine(output.Name);
                        foreach (var item in output.Song) Console.WriteLine("\t-" + item.SongTitle);
                        break;

                    case "list":
                        foreach (var item in list)
                        {
                            Console.WriteLine(item.Name);
                            for (int i = 0; i < item.Song.Count; i++)
                            {
                                Console.WriteLine("\t-" + item.Song[i].SongTitle);
                            }
                        }
                        break;
                }
            }

        }


        private void ToObject(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Author>));
            
            using (Stream reader = new FileStream(file, FileMode.Open))
            {
                
                list = (List<Author>)serializer.Deserialize(reader);
            }
        }

        private void ToXMLAndSave<Author>(List<Author> obj, string file)
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Author>));
                serializer.Serialize(writer, obj);
            }

            /*XmlSerializer x = new XmlSerializer (list.GetType());
            TextWriter writer = new StreamWriter(path);
            x.Serialize(writer, list);
            writer.Close();*/
        }
        private bool SingerChecker(string name)
        {
            return list.Where(x => x.Name == name).Count() == 0;
        }
    }
}
