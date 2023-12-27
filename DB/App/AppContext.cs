using Microsoft.EntityFrameworkCore;


public class AppContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Song> Songs { get; set; }

    public string DbPath { get; }

    public AppContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "data.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite($"Data Source={DbPath}");
}
