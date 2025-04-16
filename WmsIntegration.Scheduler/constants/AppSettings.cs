using System.Reflection;

namespace WmsIntegration.Scheduler
{
    public static class AppSettings
    {
        // Static dictionary to store the settings
        private static readonly Dictionary<string, string?> _settings = new Dictionary<string, string?>();

        // Static method to initialize settings from IConfiguration
        public static void Initialize(IConfiguration configuration)
        {
            // Get all properties from the AppSettings class
            var properties = typeof(AppSettings).GetProperties(BindingFlags.Static | BindingFlags.Public);

            // Iterate through each property and match it with the appsettings.json key
            foreach (var property in properties)
            {
                // Match the property name to a key in appsettings.json
                var settingValue = configuration.GetValue<string>(property.Name);
                if (settingValue != null)
                {
                    // Set the property value
                    property.SetValue(null, settingValue);
                    _settings[property.Name] = settingValue;
                }
            }
        }

        // Method to retrieve the setting dynamically
        public static string? GetSetting(string key)
        {
            return _settings.ContainsKey(key) ? _settings[key] : null;
        }
        public static string? ERPURL { get; private set; }
    }
}
