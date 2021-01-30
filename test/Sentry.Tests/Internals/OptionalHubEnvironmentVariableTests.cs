using System;
using Sentry.Internal;
using Xunit;

namespace Sentry.Tests.Internals
{
    public class OptionalHubEnvironmentVariableTests : IDisposable
    {
        private const string _sentryDsn = "SENTRY_DSN";
        private readonly string _beforeEnv;

        public OptionalHubEnvironmentVariableTests()
        {
            _beforeEnv = Environment.GetEnvironmentVariable(_sentryDsn);
            Environment.SetEnvironmentVariable(_sentryDsn, DsnSamples.ValidDsnWithoutSecret);
        }

        [Fact]
        public void FromOptions_EnvironmentDsn_MapsToOptionsDsn()
        {
            var options = new SentryOptions();
            OptionalHub.FromOptions(options);
            Assert.Equal(DsnSamples.ValidDsnWithoutSecret, options.Dsn);
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable(_sentryDsn, _beforeEnv);
        }
    }
}
