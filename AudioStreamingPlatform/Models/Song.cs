using System.ComponentModel.DataAnnotations;

namespace AudioStreamingPlatform.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public int ArtistId { get; set; }

        [Required]
        public Artist Artist { get; set; } = null!;

        public TimeSpan Duration { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}