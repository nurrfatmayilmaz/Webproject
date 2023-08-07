
using System.ComponentModel.DataAnnotations;

namespace AnimalStoreMvc.Models.Domain
{
    public class Animaltype
    {
        public int Id { get; set; }
        [Required]
        public string? AnimaltypeName { get; set; }
    }
}
