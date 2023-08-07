using AnimalStoreMvc.Models.Domain;
using AnimalStoreMvc.Repositories.Abstract;
using Humanizer.Localisation;
using AnimalStoreMvc.Models.Domain;
using AnimalStoreMvc.Repositories.Abstract;

namespace AnimalStoreMvc.Repositories.Implementation
{
    public class AnimaltypeService : IAnimaltypeService
    {
        private readonly DatabaseContext ctx;
        public AnimaltypeService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Animaltype model)
        {
            try
            {
                ctx.Animaltype.Add(model);
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
                ctx.Animaltype.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Animaltype GetById(int id)
        {
            return ctx.Animaltype.Find(id);
        }

        public IQueryable<Animaltype> List()
        {
            var data = ctx.Animaltype.AsQueryable();
            return data;
        }

        public bool Update(Animaltype model)
        {
            try
            {
                ctx.Animaltype.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}