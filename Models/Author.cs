
public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; }

    public List<Song> Song { get; } = new();
}

