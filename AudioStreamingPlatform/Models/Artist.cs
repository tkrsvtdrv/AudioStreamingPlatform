using System.ComponentModel.DataAnnotations;

namespace AudioStreamingPlatform.Models
{
    public class Artist
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public ICollection<Song> SongsList { get; set; } = new List<Song>();
        public ICollection<ArtistGenre> ArtistGenres { get; set; } = new List<ArtistGenre>();
    }
}