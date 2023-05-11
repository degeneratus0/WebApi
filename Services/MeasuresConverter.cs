using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    public class MeasuresConverter : IConverter<Measure, MeasureDTO, MeasureDTOid>
    {
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
