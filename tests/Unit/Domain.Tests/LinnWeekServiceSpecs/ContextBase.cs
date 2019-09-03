namespace Linn.Production.Domain.Tests.LinnWeekServiceSpecs
{
    using Linn.Production.Domain.LinnApps.Repositories;
    using Linn.Production.Domain.LinnApps.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected LinnWeekService Sut { get; set; }

        protected ILinnWeekRepository LinnWeekRepository{ get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.LinnWeekRepository = Substitute.For<ILinnWeekRepository>();
            this.Sut = new LinnWeekService(this.LinnWeekRepository);
        }
    }
}
