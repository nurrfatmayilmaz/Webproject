using AnimalStoreMvc.Models.Domain;
using AnimalStoreMvc.Models.DTO;

namespace AnimalStoreMvc.Repositories.Abstract
{
    public interface IAnimalService
    {
        bool Add(Animal model);
        bool Update(Animal model);
        Animal GetById(int id);
        bool Delete(int id);
        AnimalListVm List(string term = "", bool paging = false, int currentPage = 0);
        List<int> GetAnimaltypeByAnimalId(int AnimalId);
       // IQueryable<Animal> List();

    }
}