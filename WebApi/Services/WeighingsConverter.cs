using System;
using System.Linq;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    /// <summary>
    /// Implementation of IConverter for <see cref="Weighing">Weighings</see>
    /// </summary>
    internal class WeighingsConverter : IConverter<Weighing, WeighingDTO>
    {
        private readonly IRepository<Measure> _measures;

        public WeighingsConverter(IRepository<Measure> measures)
        {
            _measures = measures;
        }

        /// <inheritdoc cref="IConverter{Weighing, WeighingDTO}.AsDTO(Weighing)"/>
        public WeighingDTO AsDTO(Weighing weighing)
        {
            return new WeighingDTO
            {
                Item = weighing.Item,
                Weight = weighing.Weight,
                MeasureName = weighing.Measure.Name,
                Container = weighing.Container
            };
        }

        /// <inheritdoc cref="IConverter{Weighing, WeighingDTO}.FromDTO(WeighingDTO)"/>
        public Weighing FromDTO(WeighingDTO weighing)
        {
            Measure measure = _measures.Entities.FirstOrDefault(x => x.Name == weighing.MeasureName);
            if (measure == null)
            {
                throw new InvalidOperationException($"Measure with name '{weighing.MeasureName}' was not found");
            }
            return new Weighing
            {
                Item = weighing.Item,
                Weight = weighing.Weight,
                Measure = measure,
                Container = weighing.Container
            };
        }
    }
}
