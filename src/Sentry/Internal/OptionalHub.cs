using Sentry.Extensibility;

namespace Sentry.Internal
{
    // Depending on Options:
    // - A proxy to a new Hub instance
    // or
    // - A proxy to SentrySdk which could hold a Hub if SentrySdk.Init was called, or a disabled Hub
    internal static class OptionalHub
    {
        public static IHub FromOptions(SentryOptions options)
        {
            options.SetupLogging();

            if (options.Dsn == null)
            {
                options.Dsn = DsnLocator.FindDsnStringOrDisable();
            }

            if (Dsn.IsDisabled(options.Dsn))
            {
                options.DiagnosticLogger?.LogWarning("Init was called but no DSN was provided nor located. Sentry SDK will be disabled.");
                return DisabledHub.Instance;
            }

            return new Hub(options);
        }
    }
}
