using System.ComponentModel.DataAnnotations;

namespace AudioStreamingPlatform.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public int ArtistId { get; set; }

        public Artist? Artist { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
//asddasasdadasdsadsdaadsadsdsadssds