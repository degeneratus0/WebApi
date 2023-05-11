using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    public class WeighingsDBWork : IData<Weighing>
    {
        WeighingsContext context;

        public WeighingsDBWork(WeighingsContext context)
        {
            this.context = context;
        }

        public void Set()
        {
            if (!context.Weighings.Any())
            {
                context.Weighings.Add(new Weighing { Item = "Сок", Weight = 200, IdMeasure = 1, TareType = "Коробка" });
                context.Weighings.Add(new Weighing { Item = "Лимонад", Weight = 1, IdMeasure = 2, TareType = "Бутылка" });
                context.SaveChanges();
            }
        }

        public IEnumerable<Weighing> ReadAll()
        {
            return context.Weighings.Include(x => x.Measure).OrderBy(x => x.IDWeighing);
        }

        public Weighing Read(int id)
        {
            return context.Weighings.Include(x => x.Measure)
                .Where(x => x.IDWeighing == id)
                .FirstOrDefault();
        }

        public void Add(Weighing weighing)
        {
            if (!context.Measures.Any(x => x.MeasureName == weighing.Measure.MeasureName))
            {
                throw new Exception();
            }
            context.Add(new Weighing { Item = weighing.Item, Weight = weighing.Weight, IdMeasure = context.Measures.FirstOrDefault(x => x.MeasureName == weighing.Measure.MeasureName).IDMeasure, TareType = weighing.TareType });
            context.SaveChanges();
        }

        public void Edit(int id, Weighing weighing)
        {
            if (!context.Measures.Any(x => x.MeasureName == weighing.Measure.MeasureName))
            {
                throw new Exception();
            }
            Weighing editedWeighing = context.Set<Weighing>().Local.FirstOrDefault(x => x.IDWeighing == id);
            context.Entry(editedWeighing).State = EntityState.Detached;
            editedWeighing.Item = weighing.Item;
            editedWeighing.Weight = weighing.Weight;
            editedWeighing.IdMeasure = context.Measures.FirstOrDefault(x => x.MeasureName == weighing.Measure.MeasureName).IDMeasure;
            context.Entry(weighing).State = EntityState.Modified;
            context.Update(weighing);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Weighings.Remove(context.Weighings.FirstOrDefault(x => x.IDWeighing == id));
            context.SaveChanges();
        }
    }
}
