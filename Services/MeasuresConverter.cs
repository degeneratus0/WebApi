using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    public class MeasuresConverter : IWeighings<Measure, MeasureDTO, MeasureDTOid>
    {
        /*
        WeighingsContext context;
        IData<Measure> Measures;
        public MeasuresService(WeighingsContext context)
        {
            this.context = context;
        }
        public IEnumerable<MeasureDTOid> ReadAll()
        {
            return Measures.ReadAll().Select(AsDTOid);
        }
        public MeasureDTOid Read(int id)
        {
            return context.Measures.Where(x => x.IDMeasure == id)
                .Select(AsDTOid)
                .FirstOrDefault();
        }
        public void Add(MeasureDTO measure)
        {
            context.Add(new Measure { MeasureName = measure.MeasureName });
            context.SaveChanges();
        }
        public void Edit(int id, MeasureDTO measureDTO)
        {
            Measure measure = context.Set<Measure>().Local.FirstOrDefault(x => x.IDMeasure == id);
            context.Entry(measure).State = EntityState.Detached;
            measure.MeasureName = measureDTO.MeasureName;
            context.Entry(measure).State = EntityState.Modified;
            context.Measures.Update(measure);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            context.Measures.Remove(context.Measures.FirstOrDefault(x => x.IDMeasure == id));
            context.SaveChanges();
        }*/
        public MeasureDTO AsDTO(Measure measure)
        {
            return new MeasureDTO
            {
                MeasureName = measure.MeasureName
            };
        }
        public MeasureDTOid AsDTOid(Measure measure)
        {
            return new MeasureDTOid
            {
                IDMeasure = measure.IDMeasure,
                MeasureName = measure.MeasureName
            };
        }
        public Measure FromDTO(MeasureDTO measure)
        {
            return new Measure
            {
                MeasureName = measure.MeasureName
            };
        }
    }
}
