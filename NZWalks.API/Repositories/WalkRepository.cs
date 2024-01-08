using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using System.Linq;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            //Assign new id
            walk.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk= await nZWalksDbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if(existingWalk == null)
            {
                return null;
            }

            nZWalksDbContext.Walks.Remove(existingWalk);
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await nZWalksDbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }

        public  Task<Walk> GetAsync(Guid id)
        {
          return nZWalksDbContext.Walks
               .Include(x => x.Region)
               .Include(x => x.WalkDifficulty)
               .FirstOrDefaultAsync(X => X.Id == id);

        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalksDbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if (existingWalk != null)
            {
                existingWalk.Length = walk.Length;
                existingWalk.Name = walk.Name;
                existingWalk.WalkDifficulty = walk.WalkDifficulty;
                existingWalk.RegionId=walk.RegionId;

                await nZWalksDbContext.SaveChangesAsync();
                return existingWalk;
            }

            return null;
        }

       
    }
}
