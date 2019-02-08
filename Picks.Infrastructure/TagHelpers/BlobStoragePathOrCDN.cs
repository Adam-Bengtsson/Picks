using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using Picks.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.Infrastructure.TagHelpers
{
    [HtmlTargetElement("blob-storage-path-or-cdn")]
    public class BlobStoragePathOrCDN : TagHelper
    {
        private readonly CdnSettings _cdnSettings;
        private readonly AzureStorageConfig _storageConfig;
        public BlobStoragePathOrCDN(IOptions<AzureStorageConfig> storageConfig, IOptions<CdnSettings> cdnSettings)
        {
            _storageConfig = storageConfig.Value;
            _cdnSettings = cdnSettings.Value;
        }

        public string Type { get; set; }
        public string Path { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(Type) || string.IsNullOrEmpty(Path))
            {
                output.SuppressOutput();
                return;
            }

            switch (Type.ToLower())
            {
                case ("a"):
                    {
                        output.TagName = "a";
                        output.Attributes.Add("href", SetPath());
                        break;
                    }
                case "img":
                    {
                        output.TagName = "img";
                        output.Attributes.Add("src", SetPath());
                        output.TagMode = TagMode.SelfClosing;
                        break;
                    }
                default:
                    output.SuppressOutput();
                    return;
            }
        }

        public string SetPath()
        {
            if (_cdnSettings.Enabled)
            {
                return $"{_cdnSettings.EndpointStorageUrl}{Path}";
            }

            else
            {
                return $"{_storageConfig.BlobBaseUrl}{Path}";
            }
        }
    }
}