using System.ComponentModel.DataAnnotations;

namespace AnimalStoreMvc.Models.Domain
{
    public class AnimaltypeAnimal
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public int AnimaltypeId { get; set; }
    }
}