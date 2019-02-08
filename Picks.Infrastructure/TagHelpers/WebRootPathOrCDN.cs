using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using Picks.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.Infrastructure.TagHelpers
{
    [HtmlTargetElement("web-root-path-or-cdn")]
    public class WebRootPathOrCDN : TagHelper
    {
        private readonly CdnSettings _cdnSettings;
        public WebRootPathOrCDN(IOptions<CdnSettings> cdnSettings)
        {
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
                case "script":
                    {
                        output.TagName = "script";
                        output.Attributes.Add("src", SetPath());
                        output.TagMode = TagMode.StartTagAndEndTag;
                        break;
                    }
                case ("stylesheet"):
                    {
                        output.TagName = "link";
                        output.Attributes.Add("rel", "stylesheet");
                        output.Attributes.Add("href", SetPath());
                        output.TagMode = TagMode.SelfClosing;
                        break;
                    }
                case ("image/png"):
                    {
                        output.TagName = "link";
                        output.Attributes.Add("rel", "icon");
                        output.Attributes.Add("href", SetPath());
                        output.TagMode = TagMode.SelfClosing;
                        break;
                    }
                case "img":
                    {
                        output.TagName = "img";
                        output.Attributes.Add("src", SetPath());
                        output.TagMode = TagMode.SelfClosing;
                        break;
                    }
                case "image/x-icon":
                    {
                        output.TagName = "link";
                        output.Attributes.Add("rel", "icon");
                        output.Attributes.Add("href", SetPath());
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
                return $"{_cdnSettings.EndpointUrl}{Path}";
            }

            else
            {
                return $"{Path}";
            }
        }
    }
}