using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.Infrastructure.Models
{
    public class AzureStorageConfig
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string Connectionstring { get; set; }
    }
}
