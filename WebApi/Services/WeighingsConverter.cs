using System;
using System.Linq;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    internal class WeighingsConverter : IConverter<Weighing, WeighingDTO, WeighingDTOid>
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
                Id = weighing.Id,
                Item = weighing.Item,
                Weight = weighing.Weight,
                Measure = weighing.Measure.MeasureName,
                TareType = weighing.TareType
            };
        }

        public Weighing FromDTO(WeighingDTO weighing)
        {
            try
            {
                return new Weighing
                {
                    Item = weighing.Item,
                    Weight = weighing.Weight,
                    Measure = context.Measures.Single(m => m.MeasureName == weighing.Measure),
                    TareType = weighing.TareType
                };
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException($"Measure with name '{weighing.Measure}' was not found", e);
            }
        }
    }
}
