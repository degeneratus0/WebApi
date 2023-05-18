using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Models.DTOs;

namespace WebApi.Services
{
    internal class MeasuresConverter : IConverter<Measure, MeasureDTO, MeasureDTOid>
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
                Id = measure.Id,
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
