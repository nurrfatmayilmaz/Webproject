using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalStoreMvc.Models.Domain
{
    public class Animal
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? ReleaseYear { get; set; }

        public string? AnimalImage { get; set; }  // stores movie image name with extension (eg, image0001.jpg)
        [Required]
        public string? Cast { get; set; }
        [Required]
        public string? Finder { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Animaltype { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? AnimaltypeList { get; set; }
        [NotMapped]
        public string? AnimaltypeName { get; set; }

        [NotMapped]
        public MultiSelectList? MultiAnimaltypeList { get; set; }

    }
}
