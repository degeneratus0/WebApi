using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    /// <summary>
    /// Implementation of IConverter for <see cref="Measure">Measures</see>
    /// </summary>
    internal class MeasuresConverter : IConverter<Measure, MeasureDTO>
    {
        /// <inheritdoc cref="IConverter{Measure, MeasureDTO}.AsDTO(Measure)"/>
        public MeasureDTO AsDTO(Measure measure)
        {
            return new MeasureDTO
            {
                Name = measure.Name
            };
        }

        /// <inheritdoc cref="IConverter{Measure, MeasureDTO}.FromDTO(MeasureDTO)"/>
        public Measure FromDTO(MeasureDTO measure)
        {
            return new Measure
            {
                Name = measure.Name
            };
        }
    }
}
