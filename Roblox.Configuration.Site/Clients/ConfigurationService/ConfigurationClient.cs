using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Roblox.ApiClientBase;

namespace Roblox.Configuration.Site.Clients.ConfigurationService
{
    /// <summary>
    /// Client used to access the configuration service
    /// </summary>
    public class ConfigurationClient : GuardedApiClientBase
    {
        /// <inheritdoc />
        public override Encoding Encoding => null;
        /// <summary>
        /// Gets client name
        /// </summary>
        public override string Name => "ConfigurationApiClient";
        /// <inheritdoc />
        protected override string Endpoint => _EndpointGetter();
        /// <inheritdoc />
        protected override TimeSpan Timeout => TimeSpan.FromSeconds(6.0);
        /// <inheritdoc />
        protected override string ApiKey => _ApiKeyGetter?.Invoke();

        protected override void OnRequestStarting() { }

        /// <summary>
        /// Configuration client
        /// </summary>
        /// <param name="endpointGetter"></param>
        /// <param name="sslVerificationEnabled"></param>
        public ConfigurationClient(Func<string> endpointGetter, Func<string> apiKeyGetter = null, bool sslVerificationEnabled = true)
        {
            _EndpointGetter = endpointGetter;
            _ApiKeyGetter = apiKeyGetter;
            if (!sslVerificationEnabled) ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructCreateConnectionStringRequest(string groupName, string connectionStringName, string value, DateTime created)
        {
            yield return new KeyValuePair<string, object>("groupName", groupName);
            yield return new KeyValuePair<string, object>("connectionStringName", connectionStringName);
            yield return new KeyValuePair<string, object>("value", value);
            yield return new KeyValuePair<string, object>("created", created);
            yield break;
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructSetConnectionStringRequest(string groupName, string connectionStringName, string value, DateTime updated)
        {
            yield return new KeyValuePair<string, object>("groupName", groupName);
            yield return new KeyValuePair<string, object>("connectionStringName", connectionStringName);
            yield return new KeyValuePair<string, object>("value", value);
            yield return new KeyValuePair<string, object>("updated", updated);
            yield break;
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructSetConnectionStringRequest(int id, string value, DateTime updated)
        {
            yield return new KeyValuePair<string, object>("id", id);
            yield return new KeyValuePair<string, object>("value", value);
            yield return new KeyValuePair<string, object>("updated", updated);
            yield break;
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructCreateSettingRequest(string groupName, string settingName, string type, string value, string comment, bool isEnvironmentSpecific, DateTime created, bool isMasked, bool isValueSameForAllTestEnvironments, bool isValueUniqueForProduction)
        {
            yield return new KeyValuePair<string, object>("groupName", groupName);
            yield return new KeyValuePair<string, object>("name", settingName);
            yield return new KeyValuePair<string, object>("type", type);
            yield return new KeyValuePair<string, object>("value", value);
            yield return new KeyValuePair<string, object>("comment", comment);
            yield return new KeyValuePair<string, object>("isEnvironmentSpecific", isEnvironmentSpecific);
            yield return new KeyValuePair<string, object>("created", created);
            yield return new KeyValuePair<string, object>("isMasked", isMasked);
            yield return new KeyValuePair<string, object>("isValueSameForAllTestEnvironments", isValueSameForAllTestEnvironments);
            yield return new KeyValuePair<string, object>("isValueUniqueForProduction", isValueUniqueForProduction);
            yield break;
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructSetSettingRequest(string groupName, string settingName, string type, string value, string comment, bool isEnvironmentSpecific, DateTime updated, bool isMasked, bool isValueSameForAllTestEnvironments, bool isValueUniqueForProduction)
        {
            yield return new KeyValuePair<string, object>("groupName", groupName);
            yield return new KeyValuePair<string, object>("name", settingName);
            yield return new KeyValuePair<string, object>("type", type);
            yield return new KeyValuePair<string, object>("value", value);
            yield return new KeyValuePair<string, object>("comment", comment);
            yield return new KeyValuePair<string, object>("isEnvironmentSpecific", isEnvironmentSpecific);
            yield return new KeyValuePair<string, object>("updated", updated);
            yield return new KeyValuePair<string, object>("isMasked", isMasked);
            yield return new KeyValuePair<string, object>("isValueSameForAllTestEnvironments", isValueSameForAllTestEnvironments);
            yield return new KeyValuePair<string, object>("isValueUniqueForProduction", isValueUniqueForProduction);
            yield break;
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructSetSettingRequest(int id, string type, string value, string comment, bool isEnvironmentSpecific, DateTime updated, bool isMasked, bool isValueSameForAllTestEnvironments, bool isValueUniqueForProduction)
        {
            yield return new KeyValuePair<string, object>("id", id);
            yield return new KeyValuePair<string, object>("type", type);
            yield return new KeyValuePair<string, object>("value", value);
            yield return new KeyValuePair<string, object>("comment", comment);
            yield return new KeyValuePair<string, object>("isEnvironmentSpecific", isEnvironmentSpecific);
            yield return new KeyValuePair<string, object>("updated", updated);
            yield return new KeyValuePair<string, object>("isMasked", isMasked);
            yield return new KeyValuePair<string, object>("isValueSameForAllTestEnvironments", isValueSameForAllTestEnvironments);
            yield return new KeyValuePair<string, object>("isValueUniqueForProduction", isValueUniqueForProduction);
            yield break;
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructGroupRequest(string groupName)
        {
            yield return new KeyValuePair<string, object>("groupName", groupName);
            yield break;
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructConnectionStringBaseRequest(string groupName, string connectionStringName)
        {
            yield return new KeyValuePair<string, object>("groupName", groupName);
            yield return new KeyValuePair<string, object>("connectionStringName", connectionStringName);
            yield break;
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructIDRequest(int id)
        {
            yield return new KeyValuePair<string, object>("id", id);
            yield break;
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructSettingBaseRequest(string groupName, string settingName)
        {
            yield return new KeyValuePair<string, object>("groupName", groupName);
            yield return new KeyValuePair<string, object>("settingName", settingName);
            yield break;
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructPageRequest(int pageSize, int page)
        {
            yield return new KeyValuePair<string, object>("pageSize", pageSize);
            yield return new KeyValuePair<string, object>("page", page);
            yield break;
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructGroupPageRequest(string groupName, int pageSize, int page)
        {
            yield return new KeyValuePair<string, object>("groupName", groupName);
            yield return new KeyValuePair<string, object>("pageSize", pageSize);
            yield return new KeyValuePair<string, object>("page", page);
            yield break;
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructSetPropertyRequest(string groupName, string settingName, string type, string value, string comment, bool? isConnectionString)
        {
            yield return new KeyValuePair<string, object>("groupName", groupName);
            yield return new KeyValuePair<string, object>("settingName", settingName);
            yield return new KeyValuePair<string, object>("type", type);
            yield return new KeyValuePair<string, object>("value", value);
            yield return new KeyValuePair<string, object>("comment", comment);
            yield return new KeyValuePair<string, object>("isConnectionString", isConnectionString);
            yield break;
        }

        /// <inheritdoc />
        public void CreateConnectionString(ConnectionString connectionString)
        {
            CreateConnectionString(connectionString.GroupName, connectionString.Name, connectionString.Value, connectionString.Updated);
        }
        /// <inheritdoc />
        public void CreateConnectionString(string groupName, string connectionStringName, string value, DateTime updated)
        {
            var @params = ConstructCreateConnectionStringRequest(groupName, connectionStringName, value, updated);
            PostJsonV2("/v1/CreateConnectionString", @params);
        }
        /// <inheritdoc />
        public void CreateSetting(Setting setting)
        {
            CreateSetting(setting.GroupName, setting.Name, setting.Type, setting.Value, setting.Comment, setting.IsEnvironmentSpecific, setting.Updated, setting.IsMasked, setting.IsValueSameForAllTestEnvironments, setting.IsValueUniqueForProduction);
        }
        /// <inheritdoc />
        public void CreateSetting(string groupName, string settingName, string type, string value, string comment, bool isEnvironmentSpecific, DateTime created, bool isMasked, bool isValueSameForAllTestEnvironments, bool isValueUniqueForProduction)
        {
            var @params = ConstructCreateSettingRequest(groupName, settingName, type, value, comment, isEnvironmentSpecific, created, isMasked, isValueSameForAllTestEnvironments, isValueUniqueForProduction);
            PostJsonV2("/v1/CreateSetting", @params);
        }
        /// <inheritdoc />
        public void DeleteAllConnectionStringsInGroup(string groupName)
        {
            PostJsonV2("/v1/DeleteAllConnectionStringsInGroup", ConstructGroupRequest(groupName));
        }
        /// <inheritdoc />
        public void DeleteAllSettinsInGroup(string groupName)
        {
            PostJsonV2("/v1/DeleteAllSettinsInGroup", ConstructGroupRequest(groupName));
        }
        /// <inheritdoc />
        public void DeleteConnectionString(string groupName, string connectionStringName)
        {
            PostJsonV2("/v1/DeleteConnectionString", ConstructConnectionStringBaseRequest(groupName, connectionStringName));
        }
        /// <inheritdoc />
        public void DeleteConnectionString(int id)
        {
            PostJsonV2("/v1/DeleteConnectionString", ConstructIDRequest(id));
        }
        /// <inheritdoc />
        public void DeleteSetting(string groupName, string settingName)
        {
            PostJsonV2("/v1/DeleteSetting", ConstructSettingBaseRequest(groupName, settingName));
        }
        /// <inheritdoc />
        public void DeleteSetting(int id)
        {
            PostJsonV2("/v1/DeleteSetting", ConstructIDRequest(id));
        }
        /// <inheritdoc />
        public ConnectionString GetConnectionString(string groupName, string connectionStringName)
        {
            return GetV2<ConnectionString>("/v1/GetConnectionString", ConstructConnectionStringBaseRequest(groupName, connectionStringName));
        }
        /// <inheritdoc />
        public ConnectionString GetConnectionString(int id)
        {
            return GetV2<ConnectionString>("/v1/GetConnectionString", ConstructIDRequest(id));
        }
        /// <inheritdoc />
        public ConnectionString[] GetConnectionStrings(string groupName, int pageSize, int page)
        {
            return GetV2<ConnectionString[]>("/v1/GetConnectionStrings", ConstructGroupPageRequest(groupName, pageSize, page));
        }
        /// <inheritdoc />
        public int GetCountOfConnectionStringsInGroup(string groupName)
        {
            return GetV2<int>("/v1/GetCountOfConnectionStringsInGroup", ConstructGroupRequest(groupName));
        }
        /// <inheritdoc />
        public int GetCountOfSettingsInGroup(string groupName)
        {
            return GetV2<int>("/v1/GetCountOfSettingsInGroup", ConstructGroupRequest(groupName));
        }
        /// <inheritdoc />
        public string[] GetGroupNames(int pageSize, int page)
        {
            return GetV2<string[]>("/v1/GetGroupNames", ConstructPageRequest(pageSize, page));
        }
        /// <inheritdoc />
        public Setting GetMaskedSetting(string groupName, string settingName)
        {
            return GetV2<Setting>("/v1/GetMaskedSetting", ConstructSettingBaseRequest(groupName, settingName));
        }
        /// <inheritdoc />
        public Setting GetMaskedSetting(int id)
        {
            return GetV2<Setting>("/v1/GetMaskedSetting", ConstructIDRequest(id));
        }
        /// <inheritdoc />
        public Setting GetSetting(string groupName, string settingName)
        {
            return GetV2<Setting>("/v1/GetSetting", ConstructSettingBaseRequest(groupName, settingName));
        }
        /// <inheritdoc />
        public Setting GetSetting(int id)
        {
            return GetV2<Setting>("/v1/GetSetting", ConstructIDRequest(id));
        }
        /// <inheritdoc />
        public Setting[] GetSettings(string groupName, int pageSize, int page)
        {
            return GetV2<Setting[]>("/v1/GetSettings", ConstructGroupPageRequest(groupName, pageSize, page));
        }
        /// <inheritdoc />
        public void MaskSetting(string groupName, string settingName)
        {
            PostJsonV2("/v1/MaskSetting", ConstructSettingBaseRequest(groupName, settingName));
        }
        /// <inheritdoc />
        public void MaskSetting(int id)
        {
            PostJsonV2("/v1/MaskSetting", ConstructIDRequest(id));
        }
        /// <inheritdoc />
        public void SetConnectionString(ConnectionString connectionString)
        {
            if (connectionString.Id != default(int))
            {
                SetConnectionString(connectionString.Id, connectionString.Value, connectionString.Updated);
                return;
            }

            SetConnectionString(connectionString.GroupName, connectionString.Name, connectionString.Value, connectionString.Updated);
        }
        /// <inheritdoc />
        public void SetConnectionString(string groupName, string connectionStringName, string value, DateTime updated)
        {
            var @params = ConstructSetConnectionStringRequest(groupName, connectionStringName, value, updated);
            PostJsonV2("/v1/SetConnectionString", @params);
        }
        /// <inheritdoc />
        public void SetConnectionString(int id, string value, DateTime updated)
        {
            var @params = ConstructSetConnectionStringRequest(id, value, updated);
            PostJsonV2("/v1/SetConnectionString", @params);
        }
        public void SetProperty(SetPropertyRequest request)
        {
            SetProperty(request.GroupName, request.Name, request.Type, request.Value, request.Comment, request.IsConnectionString);
        }
        public void SetProperty(string groupName, string settingName, string type, string value, string comment, bool? isConnectionString)
        {
            var @params = ConstructSetPropertyRequest(groupName, settingName, type, value, comment, isConnectionString);
            PostJsonV2("/v1/SetProperty", @params);
        }
        public void SetSetting(Setting setting)
        {
            if (setting.Id != default(int))
            {
                SetSetting(setting.Id, setting.Type, setting.Value, setting.Comment, setting.IsEnvironmentSpecific, setting.Updated, setting.IsMasked, setting.IsValueSameForAllTestEnvironments, setting.IsValueUniqueForProduction);
                return;
            }

            SetSetting(setting.GroupName, setting.Name, setting.Type, setting.Value, setting.Comment, setting.IsEnvironmentSpecific, setting.Updated, setting.IsMasked, setting.IsValueSameForAllTestEnvironments, setting.IsValueUniqueForProduction);
        }
        public void SetSetting(string groupName, string settingName, string type, string value, string comment, bool isEnvironmentSpecific, DateTime updated, bool isMasked, bool isValueSameForAllTestEnvironments, bool isValueUniqueForProduction)
        {
            var @params = ConstructSetSettingRequest(groupName, settingName, type, value, comment, isEnvironmentSpecific, updated, isMasked, isValueSameForAllTestEnvironments, isValueUniqueForProduction);
            PostJsonV2("/v1/SetSetting", @params);
        }
        public void SetSetting(int id, string type, string value, string comment, bool isEnvironmentSpecific, DateTime updated, bool isMasked, bool isValueSameForAllTestEnvironments, bool isValueUniqueForProduction)
        {
            var @params = ConstructSetSettingRequest(id, type, value, comment, isEnvironmentSpecific, updated, isMasked, isValueSameForAllTestEnvironments, isValueUniqueForProduction);
            PostJsonV2("/v1/SetSetting", @params);
        }
        public void UnmaskSetting(string groupName, string settingName)
        {
            PostJsonV2("/v1/UnmaskSetting", ConstructSettingBaseRequest(groupName, settingName));
        }
        public void UnmaskSetting(int id)
        {
            PostJsonV2("/v1/UnmaskSetting", ConstructIDRequest(id));
        }

        private void PostJsonV2(string path, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            WrapPost(path, null, null, JsonConvert.SerializeObject(parameters.ToDictionary(k => k.Key, v => v.Value)));
        }

        private T GetV2<T>(string path, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return WrapGet<T>(path, parameters, null, null);
        }

        private void WrapPost(string actionPath, IEnumerable<KeyValuePair<string, object>> queryStringParameters = null, IEnumerable<KeyValuePair<string, string>> headers = null, string jsonPostData = null, string actionName = null)
        {
            try
            {
                PostJson(actionPath, queryStringParameters, headers, jsonPostData, actionName);
            }
            catch (ApiClientException ex)
            {
                HandleConfigurationException(ex);
            }
        }

        private T WrapGet<T>(string actionPath, IEnumerable<KeyValuePair<string, object>> queryStringParameters = null, IEnumerable<KeyValuePair<string, string>> headers = null, string actionName = null)
        {
            try
            {
                return Get<T>(actionPath, queryStringParameters, headers, actionName);
            }
            catch (ApiClientException ex)
            {
                HandleConfigurationException(ex);
                return default(T);
            }
        }

        private void HandleConfigurationException(ApiClientException ex)
        {
            if (ex.InnerException is WebException e)
            {
                var response = (HttpWebResponse)e.Response;
                Stream stream;
                if (response == null || response.StatusCode != HttpStatusCode.Conflict || (stream = response.GetResponseStream()) == null)
                {
                    throw ex;
                }
                string text = new StreamReader(stream).ReadToEnd();
                var error = JsonConvert.DeserializeObject<ConfigurationServiceErrorResponse>(text);
                throw new Exception(error.Message);
            }
            throw ex;
        }

        private readonly Func<string> _EndpointGetter;
        private readonly Func<string> _ApiKeyGetter;
    }
}
