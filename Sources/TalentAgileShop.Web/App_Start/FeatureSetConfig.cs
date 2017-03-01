using System;
using TalentAgileShop.Model;

namespace TalentAgileShop.Web
{
    public static class FeatureSetConfig
    {

        

        public static void Initialize(FeatureSet featureSet)
        {
            featureSet.ThumbnailView = false;
            featureSet.CatalogCategories = false;

            OverrideWithEnvVariableConfig(featureSet);
        }

        private static void OverrideWithEnvVariableConfig(FeatureSet featureSet)
        {
            featureSet.ThumbnailView = GetBoolValueFromEnv("EnableThumbnailView", featureSet.ThumbnailView);
            featureSet.CatalogCategories = GetBoolValueFromEnv("EnableCatalogCategories", featureSet.CatalogCategories);

        }

        private static bool GetBoolValueFromEnv(string environmentVariable, bool defaultValue)
        {
            var envValue = System.Environment.GetEnvironmentVariable(environmentVariable);

            if (envValue == null)
            {
                return defaultValue;                
            }

            return string.Compare(envValue.Trim(), "true", StringComparison.InvariantCultureIgnoreCase) == 0;

        }
    }
}