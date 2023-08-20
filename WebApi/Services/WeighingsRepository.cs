using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    internal class WeighingsRepository : IRepository<Weighing>
    {
        private readonly WeighingsContext context;

        public WeighingsRepository(WeighingsContext context)
        {
            this.context = context;
        }

        public IQueryable<Weighing> Entities => context.Weighings.Include(x => x.Measure);

        public async Task<Weighing> ReadAsync(int id)
        {
            return await Entities
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Weighing weighing)
        {
            await context.AddAsync(new Weighing
            {
                Item = weighing.Item,
                Weight = weighing.Weight,
                Measure = await context.Measures.FirstAsync(x => x.Id == weighing.Measure.Id),
                Container = weighing.Container
            });
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, Weighing weighing)
        {
            Weighing editedWeighing = await ReadAsync(id);
            if (editedWeighing != null)
            {
                editedWeighing.Item = weighing.Item;
                editedWeighing.Weight = weighing.Weight;
                editedWeighing.Measure = await context.Measures.FirstAsync(x => x.Name == weighing.Measure.Name);
                editedWeighing.Container = weighing.Container;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            Weighing weighing = await ReadAsync(id);
            if (weighing != null)
            {
                context.Weighings.Remove(weighing);
                await context.SaveChangesAsync();
            }
        }
    }
}
