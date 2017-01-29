using System.Configuration;
using Microsoft.ApplicationInsights.Extensibility;

namespace TalentAgileShop.Web
{
    public static class ApplicationInsightConfig
    {
        public static string IntrumentationKey { get; set; }
        public static bool IsConfigured => !string.IsNullOrEmpty(IntrumentationKey);


        public static void Initialize()
        {
            IntrumentationKey = ConfigurationManager.AppSettings["applicationInsightInstrumentationKey"];
            if (IsConfigured)
            {
                TelemetryConfiguration.Active.InstrumentationKey = IntrumentationKey;
            }

        }
    }
}