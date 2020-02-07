namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps;

    public class WswShortageStoryRepository : IQueryRepository<WswShortageStory>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WswShortageStoryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public WswShortageStory FindBy(Expression<Func<WswShortageStory, bool>> expression)
        {
            return this.serviceDbContext.WswShortageStories.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<WswShortageStory> FilterBy(Expression<Func<WswShortageStory, bool>> expression)
        {
            return this.serviceDbContext.WswShortageStories.Where(expression);
        }

        public IQueryable<WswShortageStory> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}