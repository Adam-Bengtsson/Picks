using System;

namespace Picks.Infrastructure
{
    public class CustomAppSettings
    {
        public CustomAppSettings()
        {
            // Ändra värdena i appsettings.json
            SendGridApiKey = "LoremIpsum";
        }

        public string SendGridApiKey { get; set; }
    }
}