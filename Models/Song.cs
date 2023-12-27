
public class Song
{
    public int SongID { get; set; }
    public string SongTitle { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; }
}

