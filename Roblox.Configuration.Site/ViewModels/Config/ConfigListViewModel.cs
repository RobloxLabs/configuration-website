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
        public IEnumerable<ConfigGroupModel> ConfigGroups { get; set; }
        public IEnumerable<SelectListItem> ConfigGroupSelectList
        {
            get
            {
                var list = new List<SelectListItem>();
                foreach (ConfigGroupModel configGroup in ConfigGroups)
                {
                    list.Add(new SelectListItem
                    {
                        Text = configGroup.Name,
                        Value = configGroup.Name // We can't use ID since it isn't unique and is only used for indexing
                    });
                }

                return list;
            }
        }
    }
}