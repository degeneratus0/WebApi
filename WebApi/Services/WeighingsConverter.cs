using System.Linq;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    public class WeighingsConverter : IConverter<Weighing, WeighingDTO, WeighingDTOid>
    {
        WeighingsContext context;

        public WeighingsConverter(WeighingsContext context)
        {
            this.context = context;
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

        public Weighing FromDTO(WeighingDTO weighing)
        {
            return new Weighing
            {
                Item = weighing.Item,
                Weight = weighing.Weight,
                IdMeasure = context.Measures.FirstOrDefault(x => x.MeasureName == weighing.Measure).IDMeasure,
                TareType = weighing.TareType
            };
        }
    }
}
