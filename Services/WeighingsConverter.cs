using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    public class WeighingsConverter : IWeighings<Weighing, WeighingDTO, WeighingDTOid>
    {
        
        WeighingsContext context;
        public WeighingsConverter(WeighingsContext context)
        {
            this.context = context;
        }
        /*
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
            return context.Weighings.Include(x => x.Measure).Select(AsDTOid).OrderBy(x => x.IDWeighing);
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
            if (!context.Measures.Any(x => x.MeasureName == weighing.Measure))
            {
                throw new Exception();
            }
            context.Add(new Weighing { Item = weighing.Item, Weight = weighing.Weight, idMeasure = context.Measures.FirstOrDefault(x => x.MeasureName == weighing.Measure).IDMeasure, TareType = weighing.TareType });
            context.SaveChanges();
        }
        public void Edit(int id, WeighingDTO weighingDTO)
        {
            if (!context.Measures.Any(x => x.MeasureName == weighingDTO.Measure))
            {
                throw new Exception();
            }
            Weighing weighing = context.Set<Weighing>().Local.FirstOrDefault(x => x.IDWeighing == id);
            context.Entry(weighing).State = EntityState.Detached;
            weighing.Item = weighingDTO.Item;
            weighing.Weight = weighingDTO.Weight;
            weighing.idMeasure = context.Measures.FirstOrDefault(x => x.MeasureName == weighingDTO.Measure).IDMeasure;
            context.Entry(weighing).State = EntityState.Modified;
            context.Update(weighing);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            context.Weighings.Remove(context.Weighings.FirstOrDefault(x => x.IDWeighing == id));
            context.SaveChanges();
        }
        */
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
        public Weighing FromDTO(WeighingDTO weighing)
        {
            return new Weighing
            {
                Item = weighing.Item,
                Weight = weighing.Weight,
                idMeasure = context.Measures.FirstOrDefault(x => x.MeasureName == weighing.Measure).IDMeasure,
                TareType = weighing.TareType
            };
        }
    }
}
