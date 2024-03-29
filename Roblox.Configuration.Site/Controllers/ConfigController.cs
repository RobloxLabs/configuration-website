﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Roblox.Configuration.Site.ViewModels.Configuration;
using Roblox.Configuration.Site.Models.Configuration;
using Roblox.Configuration.Site.ModelFactories.Configuration;
using Roblox.EventLog;
using Roblox.Configuration.Client;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    [RoutePrefix("Config")]
    public class ConfigController : Controller
    {
        private readonly ILogger _Logger;
        private readonly IConfigurationClient _ConfigurationClient;
        private readonly ConnectionStringModelFactory _ConnectionStringModelFactory;
        private readonly SettingModelFactory _SettingModelFactory;

        public ConfigController(ILogger logger, IConfigurationClient configurationClient, ConnectionStringModelFactory connectionStringModelFactory, SettingModelFactory settingModelFactory)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ConfigurationClient = configurationClient ?? throw new ArgumentNullException(nameof(configurationClient));
            _ConnectionStringModelFactory = connectionStringModelFactory ?? throw new ArgumentNullException(nameof(connectionStringModelFactory));
            _SettingModelFactory = settingModelFactory ?? throw new ArgumentNullException(nameof(settingModelFactory));
        }

        // GET: Config/Search
        [HttpGet]
        public ActionResult Search()
        {
            var groupNames = _ConfigurationClient.GetConfigurationGroupNames();

            var viewModel = new ConfigListViewModel
            {
                ConfigGroupNames = groupNames.Prepend("*")
            };

            return View(viewModel);
        }

        // GET: Config/GetSettingsHtmlAjax?groupName=Roblox.Properties.Settings
        [HttpGet]
        public ActionResult GetSettingsHtmlAjax(string groupName, string namePattern, string valuePattern)
        {
            return PartialView(
                "~/Views/Settings/Table.cshtml",
                _SettingModelFactory.GetSettings(
                    groupName,  // Group name
                    Properties.Settings.Default.MaxSettingsResults,
                    0           // Page number
                )
            );
        }

        // GET: Config/GetConnectionStringsHtmlAjax?groupName=Roblox.Properties.Settings
        [HttpGet]
        public ActionResult GetConnectionStringsHtmlAjax(string groupName, string namePattern, string valuePattern)
        {
            return PartialView(
                "~/Views/ConnectionStrings/Table.cshtml",
                _ConnectionStringModelFactory.GetConnectionStrings(
                    groupName,  // Group name
                    Properties.Settings.Default.MaxConnectionStringResults,
                    0           // Page number
                )
            );
        }

        // GET: Config/GetSetting?settingGroupName=Roblox.Properties.Settings&settingName=TestSetting
        [HttpGet]
        public ActionResult GetSetting(string settingGroupName, string settingName)
        {
            if (string.IsNullOrEmpty(settingGroupName))
                throw new ArgumentNullException("GroupName");
            else if (string.IsNullOrEmpty(settingName))
                throw new ArgumentNullException("Name");

            SettingModel settingModel = (SettingModel)_ConfigurationClient.GetSetting(settingGroupName, settingName);
            if (settingModel == null)
                return new HttpNotFoundResult("Setting not found");
            return Json(settingModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Config/GetMaskedSetting?settingGroupName=Roblox.Properties.Settings&settingName=TestSetting
        [HttpGet]
        public ActionResult GetMaskedSetting(string settingGroupName, string settingName)
        {
            if (string.IsNullOrEmpty(settingGroupName))
                throw new ArgumentNullException("GroupName");
            else if (string.IsNullOrEmpty(settingName))
                throw new ArgumentNullException("Name");

            SettingModel settingModel = new SettingModel(_ConfigurationClient.GetSetting(settingGroupName, settingName));
            return Json(settingModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Config/GetSettingAjax?id=1
        [HttpGet]
        public ActionResult GetSettingAjax(int? id)
        {
            if (!id.HasValue)
                throw new ArgumentNullException("ID");

            SettingModel model = new SettingModel(_ConfigurationClient.GetSetting(id.Value));
            if (model == null)
                return new HttpNotFoundResult("Setting with ID " + id + " not found");
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Config/GetConnectionStringAjax?id=1
        [HttpGet]
        public ActionResult GetConnectionStringAjax(int? id)
        {
            if (!id.HasValue)
                throw new ArgumentNullException("ID");

            var model = new ConnectionStringModel(_ConfigurationClient.GetConnectionString(id.Value));
            if (model == null)
                return new HttpNotFoundResult("Connection string with ID " + id + " not found");
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // POST: Config/TestConnectionStringAjax
        [HttpPost]
        public ActionResult TestConnectionStringAjax(int? id, string value)
        {
            var response = new ValidationResponseModel
            {
                Success = true,
                Message = "Success!"
            };

            try
            {
                if (!id.HasValue)
                    throw new ArgumentNullException("ID");

                // TODO: Test connection string

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
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
        public ActionResult DeleteSettingAjax(int? id)
        {
            try
            {
                if (!id.HasValue)
                    throw new ArgumentNullException("ID");

                _ConfigurationClient.DeleteSetting(id.Value);

                // Success
                return Content("Successfully deleted setting!");
            }
            catch
            {
                // Error
                return Content("");
            }
        }

        // POST: Config/DeleteConnectionStringAjax
        [HttpPost]
        public ActionResult DeleteConnectionStringAjax(int? id)
        {
            try
            {
                if (!id.HasValue)
                    throw new ArgumentNullException("ID");

                _ConfigurationClient.DeleteConnectionString(id.Value);

                // Success
                return Content("Successfully deleted connection string!");
            }
            catch
            {
                // Error
                return Content("");
            }
        }

        // POST: Config/SetSettingAjax
        [HttpPost]
        public ActionResult SetSettingAjax(SettingModel setting)
        {
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
            var response = new SetSettingResponseModel
            {
                SettingSaved = true,
                Message = "" // This needs to be empty on success
            };

            try
            {
                /*if (string.IsNullOrEmpty(setting.Value))
                    throw new ArgumentNullException("Value");*/

                if (setting.Id > 0)
                {
                    // Update setting
                    // TODO: Can we take the non-updates values and pass them in anyways without updating them?
                    // Maybe config service will only use them for identification?
                    var oldSetting = _ConfigurationClient.GetSetting(setting.Id);
                    setting.GroupName = oldSetting.GroupName;
                    setting.Type = oldSetting.Type;
                    setting.Name = oldSetting.Name;
                    _ConfigurationClient.SetSetting(setting);
                }
                else
                {
                    // Create a brand new setting
                    _ConfigurationClient.CreateSetting(setting);
                }
            }
            catch (Exception ex)
            {
                response.SettingSaved = false;
                response.Message = ex.Message;
            }

            return Json(response);
        }

        // POST: Config/SetConnectionStringAjax
        [HttpPost]
        public ActionResult SetConnectionStringAjax(ConnectionStringModel connectionString)
        {
            var response = new ValidationResponseModel
            {
                Success = true,
                Message = "Success!"
            };

            try
            {
                if (connectionString.Id > 0)
                {
                    // Update connection string
                    // TODO: Can we take the non-updates values and pass them in anyways without updating them?
                    // Maybe config service will only use them for identification?
                    var oldCS = _ConfigurationClient.GetConnectionString(connectionString.Id);
                    connectionString.GroupName = oldCS.GroupName;
                    connectionString.Name = oldCS.Name;
                    _ConfigurationClient.SetConnectionString(connectionString);
                }
                else
                {
                    // Create a brand new setting
                    _ConfigurationClient.CreateConnectionString(connectionString);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Exception:" + ex.Message + (ex.InnerException != null ? ("\nInner Exception: " + ex.InnerException.Message) : "");
            }

            return Json(response);
        }

        // POST: Config/ValidateRedisEndpoints
        [HttpPost]
        public ActionResult ValidateRedisEndpoints(string rawEndpoints)
        {
            var response = new ValidationResponseModel
            {
                Success = true,
                Message = "Success!"
            };

            try
            {

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return Json(response);
        }

        // POST: Config/ValidateWeightedCsv
        [HttpPost]
        public ActionResult ValidateWeightedCsv(string rawWeightedCsv)
        {
            var response = new ValidationResponseModel
            {
                Success = true,
                Message = "Success!"
            };

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
