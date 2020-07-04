using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    public class MeasuresDBWork : IData<Measure>
    {
        WeighingsContext context;
        public MeasuresDBWork(WeighingsContext context)
        {
            this.context = context;
        }
        public void Set()
        {
            if (!context.Measures.Any())
            {
                context.Measures.Add(new Measure { MeasureName = "г" });
                context.Measures.Add(new Measure { MeasureName = "кг" });
                context.SaveChanges();
            }
        }
        public IEnumerable<Measure> ReadAll()
        {
            return context.Measures;
        }
        public Measure Read(int id)
        {
            return context.Measures.Where(x => x.IDMeasure == id)
                .FirstOrDefault();
        }
        public void Add(Measure measure)
        {
            context.Add(new Measure { MeasureName = measure.MeasureName });
            context.SaveChanges();
        }
        public void Edit(int id, Measure measure)
        {
            Measure editedMeasure = context.Set<Measure>().Local.FirstOrDefault(x => x.IDMeasure == id);
            context.Entry(editedMeasure).State = EntityState.Detached;
            editedMeasure.MeasureName = measure.MeasureName;
            context.Entry(editedMeasure).State = EntityState.Modified;
            context.Measures.Update(editedMeasure);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            context.Measures.Remove(context.Measures.FirstOrDefault(x => x.IDMeasure == id));
            context.SaveChanges();
        }
    }
}
