using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3.DB
{


    public class DbVersion
    {
        public DbVersion()
        {
            var db = new AppContext();

            bool quit = false;
            string command;

            Console.WriteLine($"Database path: {db.DbPath}");
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
                        string author = Console.ReadLine();
                        if (author == "") break;
                        try
                        {
                            db.Authors.Where(x => x.Name == author).Single();
                        }
                        catch
                        {
                            Console.WriteLine("This singer already in list");
                            Console.WriteLine("Please enter another singer or press \"Enter\"");
                            author = Console.ReadLine();
                            if (author == "") break;
                        }
                        db.Add(new Author { Name = author });
                        db.SaveChanges();
                        break;

                    case "add song":
                        Console.WriteLine("Input song's title");
                        string song = Console.ReadLine();

                        Console.WriteLine("Input singer's name");
                        author = Console.ReadLine();
                        if (db.Authors.Where(x => x.Name == author).First().AuthorId == null)
                        {
                            db.Add(new Author { Name = author });
                        }
                        var authorID = db.Authors.Where(x => x.Name == author).First().AuthorId;
                        db.Songs.Add(new Song { AuthorId = authorID, SongTitle = song });
                        db.SaveChanges();
                        break;

                    case "del":
                        Console.WriteLine("Input author's name that should delete");
                        string delAuthorName = Console.ReadLine();
                        Author delAuthor = db.Authors.Where(x => x.Name == delAuthorName).First();
                        var delAuthorID = db.Authors.Where(x => x.Name == delAuthorName).First().AuthorId;
                        db.Authors.Remove(delAuthor);
                        int songsCount = db.Songs.Where(x => x.AuthorId == delAuthorID).Count();
                        while (songsCount != 0)
                        {
                            db.Songs.Remove(db.Songs.Where(x => x.AuthorId == delAuthorID).First());
                            songsCount--;
                        }
                        db.SaveChanges();
                        break;

                    case "search":
                        Console.WriteLine("Input singer that you wana search:");
                        string search = Console.ReadLine();
                        var searchId = db.Authors.Where(x => x.Name == search).First().AuthorId;

                        foreach (var songItem in db.Songs)
                        {
                            if (songItem.AuthorId == searchId)
                            {
                                Console.WriteLine("-" + songItem.SongTitle);
                            }
                        }
                        break;

                    case "list":
                        foreach (var singerItem in db.Authors)
                        {
                            Console.WriteLine(singerItem.Name + ":");
                            foreach (var songItem in db.Songs)
                            {
                                if (songItem.AuthorId == singerItem.AuthorId)
                                {
                                    Console.WriteLine("-" + songItem.SongTitle);
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}
