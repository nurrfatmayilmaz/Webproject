using AnimalStoreMvc.Models.Domain;
using Humanizer.Localisation;
using AnimalStoreMvc.Models.Domain;
using AnimalStoreMvc.Models.DTO;

namespace AnimalStoreMvc.Repositories.Abstract
{
    public interface IAnimaltypeService
    {
        bool Add(Animaltype model);
        bool Update(Animaltype model);
        Animaltype GetById(int id);
        bool Delete(int id);
        IQueryable<Animaltype> List();

    }
}