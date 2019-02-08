using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.Infrastructure.Models
{
    public class CdnSettings
    {
        public bool Enabled { get; set; }
        public string EndpointUrl { get; set; }
        public string EndpointStorageUrl { get; set; }
    }
}
