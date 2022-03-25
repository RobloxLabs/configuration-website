using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Roblox.Configuration.Site.Models.Configuration;

namespace Roblox.Configuration.Site.ViewModels.Configuration
{
    public class ConfigListViewModel
    {
        //public IEnumerable<SettingModel> Configuration { get; set; }
        public IEnumerable<string> ConfigGroupNames { get; set; }
        public IEnumerable<SelectListItem> ConfigGroupSelectList
        {
            get
            {
                var list = new List<SelectListItem>();
                foreach (string configGroupName in ConfigGroupNames)
                {
                    list.Add(new SelectListItem
                    {
                        Text = configGroupName,
                        Value = configGroupName
                    });
                }

                return list;
            }
        }
    }
}