using System.ComponentModel.DataAnnotations;

namespace PTA.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public string Filename { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
