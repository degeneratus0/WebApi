using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    public class WeighingsDBWork : IWeighings<Weighing, WeighingDTO, WeighingDTOid>
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
                context.Weighings.Add(new Weighing { Item = "Сок", Weight = 200, idMeasure = 1, TareType = "Коробка" });
                context.Weighings.Add(new Weighing { Item = "Лимонад", Weight = 1, idMeasure = 2, TareType = "Бутылка" });
                context.SaveChanges();
            }
        }
        public IEnumerable<WeighingDTOid> ReadAll()
        {
            return context.Weighings.Include(x => x.Measure).Select(AsDTOid);
        }
        public WeighingDTOid Read(int id)
        {
            return context.Weighings.Include(x => x.Measure)
                .Where(x => x.IDWeighing == id)
                .Select(AsDTOid)
                .FirstOrDefault();
        }
        public void Add(WeighingDTO weighing)
        {
            context.Add(new Weighing { Item = weighing.Item, Weight = weighing.Weight, idMeasure = context.Measures.FirstOrDefault(x => x.MeasureName == weighing.Measure).IDMeasure, TareType = weighing.TareType });
            context.SaveChanges();
        }
        public void Edit(int id, WeighingDTO weighing) // не работает
        {
            context.Update(new Weighing { IDWeighing = id, Item = weighing.Item, Weight = weighing.Weight, idMeasure=context.Measures.FirstOrDefault(x => x.MeasureName == weighing.Measure).IDMeasure, TareType = weighing.TareType });
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            context.Weighings.Remove(context.Weighings.FirstOrDefault(x => x.IDWeighing == id));
            context.SaveChanges();
        }
        public WeighingDTO AsDTO(Weighing weighing)
        {
            return new WeighingDTO
            {
                Item = weighing.Item,
                Weight = weighing.Weight,
                Measure = weighing.Measure.MeasureName,
                TareType = weighing.TareType
            };
        }
        public WeighingDTOid AsDTOid(Weighing weighing)
        {
            return new WeighingDTOid
            {
                IDWeighing = weighing.IDWeighing,
                Item = weighing.Item,
                Weight = weighing.Weight,
                Measure = weighing.Measure.MeasureName,
                TareType = weighing.TareType
            };
        }
    }
}
