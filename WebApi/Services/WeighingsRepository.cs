using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    internal class WeighingsRepository : IRepository<Weighing>
    {
        private readonly WeighingsContext context;


        public WeighingsRepository(WeighingsContext context)
        {
            this.context = context;
        }

        public void Set()
        {
            if (!context.Weighings.Any())
            {
                if (!context.Measures.Any())
                {
                    context.Measures.Add(new Measure { MeasureName = "g" });
                    context.Measures.Add(new Measure { MeasureName = "kg" });
                    context.SaveChanges();
                }
                context.Weighings.Add(new Weighing { Item = "Juice", Weight = 200, IdMeasure = 1, TareType = "Box" });
                context.Weighings.Add(new Weighing { Item = "Lemonade", Weight = 1, IdMeasure = 2, TareType = "Bottle" });
                context.SaveChanges();
            }
        }

        public IEnumerable<Weighing> ReadAll()
        {
            return context.Weighings.Include(x => x.Measure).OrderBy(x => x.Id);
        }

        public Weighing Read(int id)
        {
            return context.Weighings
                .Include(x => x.Measure)
                .SingleOrDefault(x => x.Id == id);
        }

        public void Add(Weighing weighing)
        {
            context.Add(new Weighing
            {
                Item = weighing.Item,
                Weight = weighing.Weight,
                Measure = context.Measures.Single(x => x.Id == weighing.Measure.Id),
                TareType = weighing.TareType
            });
            context.SaveChanges();
        }

        public void Edit(int id, Weighing weighing)
        {
            Weighing editedWeighing = context.Weighings.Single(x => x.Id == id);
            editedWeighing.Item = weighing.Item;
            editedWeighing.Weight = weighing.Weight;
            editedWeighing.IdMeasure = context.Measures.Single(x => x.MeasureName == weighing.Measure.MeasureName).Id;
            editedWeighing.TareType = weighing.TareType;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Weighing weighing = context.Weighings.SingleOrDefault(x => x.Id == id);
            if (weighing != null)
            {
                context.Weighings.Remove(weighing);
                context.SaveChanges();
            }
        }
    }
}
