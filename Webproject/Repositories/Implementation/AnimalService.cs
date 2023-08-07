using AnimalStoreMvc.Models.Domain;
using AnimalStoreMvc.Models.DTO;
using AnimalStoreMvc.Repositories.Abstract;

namespace AnimalStoreMvc.Repositories.Implementation
{
    public class AnimalService : IAnimalService
    {
        private readonly DatabaseContext ctx;
        public AnimalService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Animal model)
        {
            try
            {

                ctx.Animal.Add(model);
                ctx.SaveChanges();
                foreach (int AnimaltypeId in model.Animaltype)
                {
                    var AnimaltypeAnimal = new AnimaltypeAnimal
                    {
                        AnimalId = model.Id,
                        AnimaltypeId = AnimaltypeId
                    };
                    ctx.AnimaltypeAnimal.Add(AnimaltypeAnimal);
                }
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                var AnimaltypeAnimal = ctx.AnimaltypeAnimal.Where(a => a.AnimalId == data.Id);
                foreach (var AnimaltypeAnimals in AnimaltypeAnimal)
                {
                    ctx.AnimaltypeAnimal.Remove(AnimaltypeAnimals);
                }
                ctx.Animal.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Animal GetById(int id)
        {
            return ctx.Animal.Find(id);
        }
        public AnimalListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new AnimalListVm();

            var list = ctx.Animal.ToList();


            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Title.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                // here we will apply paging
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }

            foreach (var animal in list)
            {
                var Animaltypes = (from Animaltype in ctx.Animaltype
                                   join mg in ctx.AnimaltypeAnimal
                              on Animaltype.Id equals mg.AnimaltypeId
                                   where mg.AnimalId == animal.Id
                              select Animaltype.AnimaltypeName
                              ).ToList();
                var AnimaltypeNames= string.Join(',', Animaltypes);
                animal.AnimaltypeName = AnimaltypeNames;
            }
            data.AnimalList = list.AsQueryable();
            return data;
        }


        public bool Update(Animal model)
        {
            try
            {
                // these genreIds are not selected by users and still present is AnimalGenre table corresponding to
                // this AnimalId. So these ids should be removed.
                var animaltypesToDeleted = ctx.AnimaltypeAnimal.Where(a => a.AnimalId == model.Id && !model.Animaltype.Contains(a.AnimaltypeId)).ToList();
                foreach (var mGenre in animaltypesToDeleted)
                {
                    ctx.AnimaltypeAnimal.Remove(mGenre);
                }
                foreach (int genId in model.Animaltype)
                {
                    var AnimaltypeAnimal = ctx.AnimaltypeAnimal.FirstOrDefault(a => a.AnimalId == model.Id && a.AnimaltypeId == genId);
                    if (AnimaltypeAnimal == null)
                    {
                        AnimaltypeAnimal = new AnimaltypeAnimal { AnimaltypeId = genId, AnimalId = model.Id };
                        ctx.AnimaltypeAnimal.Add(AnimaltypeAnimal);
                    }
                }

                ctx.Animal.Update(model);
                // we have to add these genre ids in AnimalGenre table
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<int> GetAnimaltypeByAnimalId(int animalId)
        {
            var animaltypeIds = ctx.AnimaltypeAnimal.Where(a => a.AnimalId == animalId).Select(a => a.AnimaltypeId).ToList();
            return animaltypeIds;
        }

    }
}