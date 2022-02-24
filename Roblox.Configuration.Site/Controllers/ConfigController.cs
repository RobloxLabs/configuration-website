using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roblox.Configuration;
using Roblox.Configuration.Site.Models.Configuration;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    [RoutePrefix("Config")]
    public class ConfigController : Controller
    {
        // GET: Config/GetSettingsHtmlAjax?groupName=Roblox.Properties.Settings
        [HttpGet]
        public ActionResult GetSettingsHtmlAjax(string groupName, string namePattern, string valuePattern)
        {
            int count = 200;
            // Populate Configuration
            var settings = new List<SettingModel>();
            for (int i = 1; i <= count; i++)
                settings.Add(new SettingModel
                {
                    Id = i,
                    GroupName = "Roblox.Test.Properties.Settings",
                    Name = "Roblox Test Setting #" + i.ToString(),
                    Type = typeof(String).FullName,
                    Value = "String Value # " + i.ToString(),
                    Comment = "Comment comment blah blah blah...",
                    IsEnvironmentSpecific = false,
                    LastModified = DateTime.Now.ToString(),
                    IsMasked = (i % 2 == 0) // Done to simulate masked value
                });

            return PartialView("~/Views/Settings/Table.cshtml", settings);
        }

        // GET: Config/GetSetting?settingGroupName=Roblox.Properties.Settings&settingName=TestSetting
        [HttpGet]
        public ActionResult GetSetting(string settingGroupName, string settingName)
        {
            var settingModel = new SettingModel
            {
                Id = 1,
                GroupName = settingGroupName,
                Name = settingName,
                Type = typeof(Boolean).FullName,
                Value = "True",
                Comment = "Comment comment blah blah blah...",
                IsEnvironmentSpecific = false,
                LastModified = DateTime.Now.ToString(),
                IsMasked = false
            };
            return Json(settingModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Config/GetMaskedSetting?settingGroupName=Roblox.Properties.Settings&settingName=TestSetting
        [HttpGet]
        public ActionResult GetMaskedSetting(string settingGroupName, string settingName)
        {
            var settingModel = new SettingModel
            {
                Id = 1,
                GroupName = settingGroupName,
                Name = settingName,
                Type = typeof(String).FullName,
                Value = "Masked value ooo", // We technically only need to return Value for this
                Comment = "This setting is masked",
                IsEnvironmentSpecific = false,
                LastModified = DateTime.Now.ToString(),
                IsMasked = true
            };
            return Json(settingModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Config/GetSettingAjax?id=1
        [HttpGet]
        public ActionResult GetSettingAjax(int id)
        {
            var settingModel = new SettingModel
            {
                Id = id,
                GroupName = "Roblox.Test.Properties.Settings",
                Name = "Roblox Test Setting #" + id.ToString(),
                Type = "Roblox.Configuration.WeightedCsv",
                Value = "String Value # " + id.ToString(),
                Comment = "Comment comment blah blah blah...",
                IsEnvironmentSpecific = false,
                LastModified = DateTime.Now.ToString(),
                IsMasked = false,
                IsRestrictedSetting = false,
                IsShutdownSetting = false
            };
            return Json(settingModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Config/GetTimeSpan
        [HttpGet]
        public ActionResult GetTimeSpan(
            int days = 0,
            int hours = 0,
            int minutes = 0,
            int seconds = 0,
            int milliseconds = 0
        ){
            // TODO: Couldn't we just do this really easily in JS?
            var ticks = TimeSpan.TicksPerDay * days;
            ticks += TimeSpan.TicksPerHour * hours;
            ticks += TimeSpan.TicksPerMinute * minutes;
            ticks += TimeSpan.TicksPerSecond * seconds;
            ticks += TimeSpan.TicksPerMillisecond * milliseconds;

            return Content(TimeSpan.FromTicks(ticks).ToString());
        }

        // POST: Config/DeleteSettingAjax
        [HttpPost]
        public ActionResult DeleteSettingAjax(FormCollection collection)
        {
            // Error
            //return Content("");

            // Success
            return Content("Successfully deleted setting!");
        }

        // POST: Config/SetSettingAjax
        [HttpPost]
        public ActionResult SetSettingAjax(FormCollection collection)
        {
            var response = new SetSettingResponseModel();
            // Do code stuff
            response.SettingSaved = true;
            response.Message = ""; // This needs to be empty on success

            /*
                Collection params:
                id,
                value,
                comment,
                env,
                isMasked,
                isValueSameForAllTestEnvironments,
                isValueUniqueForProduction,

                // following settings will NOT get updated on save
                group,
                type,
                name
            */

            try
            {

                var settingModel = new SettingModel
                {
                    Id = int.Parse(collection.Get("id")),
                    Value = collection.Get("value"),
                    Comment = collection.Get("comment"),
                    IsEnvironmentSpecific = bool.Parse(collection.Get("env")),
                    IsMasked = bool.Parse(collection.Get("isMasked"))
                    // ??
                    //IsValueSameForAllTestEnvironments = bool.Parse(collection.Get("isValueSameForAllTestEnvironments")),
                    //IsValueUniqueForProduction = bool.Parse(collection.Get("isValueUniqueForProduction")),
                };

                settingModel.GroupName = collection.Get("group");
                settingModel.Type = collection.Get("type");
                settingModel.Name = collection.Get("name");

                if (string.IsNullOrEmpty(settingModel.GroupName))
                    throw new ApplicationException("A value for GroupName is required");
                if (string.IsNullOrEmpty(settingModel.Type))
                    throw new ApplicationException("A value for Type is required");
                if (string.IsNullOrEmpty(settingModel.Name))
                    throw new ApplicationException("A value for Name is required");

                // Push to the config service if possible

            }
            catch (Exception ex)
            {
                response.SettingSaved = false;
                response.Message = ex.Message;
            }

            return Json(response);
        }

        // POST: Config/ValidateRedisEndpoints
        [HttpPost]
        public ActionResult ValidateRedisEndpoints(FormCollection collection)
        {
            var response = new ValidationResponseModel();

            /*
                Collection params:
                rawEndpoints
            */

            // Do code stuff
            response.Success = true;
            response.Message = "Success!";

            return Json(response);
        }

        // POST: Config/ValidateWeightedCsv
        [HttpPost]
        public ActionResult ValidateWeightedCsv(string rawWeightedCsv)
        {
            var response = new ValidationResponseModel();

            /*
                Collection params:
                rawWeightedCsv
            */

            response.Success = true;
            response.Message = "Success!";

            try
            {
                new WeightedCsv(rawWeightedCsv);
            }
            catch (WeightedCsvParseException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return Json(response);
        }
    }
}
