using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    /// <summary>
    /// Implementation of IRepository for Measures
    /// </summary>
    internal class MeasuresRepository : IRepository<Measure>
    {
        private readonly WeighingsContext context;

        public MeasuresRepository(WeighingsContext context)
        {
            this.context = context;
        }

        /// <inheritdoc cref="IRepository{Measure}.Entities"/>
        public IQueryable<Measure> Entities => context.Measures;

        /// <inheritdoc cref="IRepository{Measure}.ReadAsync(int)"/>
        public async Task<Measure> ReadAsync(int id)
        {
            return await Entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc cref="IRepository{Measure}.AddAsync(Measure)"/>
        public async Task AddAsync(Measure measure)
        {
            await context.AddAsync(new Measure { Name = measure.Name });
            await context.SaveChangesAsync();
        }

        /// <inheritdoc cref="IRepository{Measure}.EditAsync(int, Measure)"/>
        public async Task EditAsync(int id, Measure measure)
        {
            Measure editedMeasure = await ReadAsync(id);
            if (editedMeasure != null)
            {
                editedMeasure.Name = measure.Name;
                await context.SaveChangesAsync();
            }
        }

        /// <inheritdoc cref="IRepository{Measure}.DeleteAsync(int)"/>
        public async Task DeleteAsync(int id)
        {
            Measure measure = await ReadAsync(id);
            if (measure != null)
            {
                context.Measures.Remove(measure);
                await context.SaveChangesAsync();
            }
        }
    }
}
