using System.Collections.Generic;
using System.Linq;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    internal class MeasuresRepository : IRepository<Measure>
    {
        private readonly WeighingsContext context;

        public MeasuresRepository(WeighingsContext context)
        {
            this.context = context;
        }

        public void Set()
        {
            if (!context.Measures.Any())
            {
                context.Measures.Add(new Measure { MeasureName = "g" });
                context.Measures.Add(new Measure { MeasureName = "kg" });
                context.SaveChanges();
            }
        }

        public IEnumerable<Measure> ReadAll()
        {
            return context.Measures;
        }

        public Measure Read(int id)
        {
            return context.Measures.SingleOrDefault(x => x.Id == id);
        }

        public void Add(Measure measure)
        {
            context.Add(new Measure { MeasureName = measure.MeasureName });
            context.SaveChanges();
        }

        public void Edit(int id, Measure measure)
        {
            Measure editedMeasure = context.Measures.SingleOrDefault(x => x.Id == id);
            if (editedMeasure != null)
            {
                editedMeasure.MeasureName = measure.MeasureName;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            Measure measure = context.Measures.SingleOrDefault(x => x.Id == id);
            if (measure != null)
            {
                context.Measures.Remove(measure);
                context.SaveChanges();
            }
        }
    }
}
