using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    internal class MeasuresConverter : IConverter<Measure, MeasureDTO>
    {
        public MeasureDTO AsDTO(Measure measure)
        {
            return new MeasureDTO
            {
                Name = measure.Name
            };
        }

        public Measure FromDTO(MeasureDTO measure)
        {
            return new Measure
            {
                Name = measure.Name
            };
        }
    }
}
