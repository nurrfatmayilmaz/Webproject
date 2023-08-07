using AnimalStoreMvc.Models.Domain;

namespace AnimalStoreMvc.Models.DTO
{
    public class AnimalListVm
    {
        public IQueryable<Animal> AnimalList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}