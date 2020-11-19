using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace ChannelEngineHolder.Application.Tests.Helpers
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(CreateFixture)
        {
        }

        public static Fixture CreateFixture()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });
            fixture.Inject(Task.FromResult(false));
            return fixture;
        }
    }
}
