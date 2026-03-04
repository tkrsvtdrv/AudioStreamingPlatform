using System.ComponentModel.DataAnnotations;

namespace AudioStreamingPlatform.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public ICollection<ArtistGenre> ArtistGenres { get; set; } = new List<ArtistGenre>();
    }
}