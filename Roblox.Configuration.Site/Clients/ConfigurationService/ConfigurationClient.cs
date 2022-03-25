using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;
using Roblox.ApiClientBase;

namespace Roblox.Configuration.Site.Clients.ConfigurationService
{
	/// <summary>
	/// Client used to access the configuration service, pulled from Roblox/event-ingestor
	/// </summary>
	public class ConfigurationClient : ApiClientBase.ApiClientBase, IConfigurationClient
	{
		/// <inheritdoc />
		public override Encoding Encoding
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Gets client name
		/// </summary>
		public override string Name
		{
			get
			{
				return "ConfigurationApiClient";
			}
		}

		/// <inheritdoc />
		protected override string Endpoint
		{
			get
			{
				return this._EndpointGetter();
			}
		}

		/// <inheritdoc />
		protected override TimeSpan Timeout
		{
			get
			{
				return TimeSpan.FromSeconds(6.0);
			}
		}

		/// <inheritdoc />
		protected override string ApiKey
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Configuration client
		/// </summary>
		/// <param name="endpointGetter"></param>
		/// <param name="sslVerificationEnabled"></param>
		public ConfigurationClient(Func<string> endpointGetter, bool sslVerificationEnabled = true)
		{
			this._EndpointGetter = endpointGetter;
			if (!sslVerificationEnabled)
			{
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(this.ValidateRemoteCertificate));
			}
		}

		/// <inheritdoc />
		public Setting GetSetting(string groupName, string settingName)
		{
			KeyValuePair<string, object>[] parameters = new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("groupName", groupName),
				new KeyValuePair<string, object>("settingName", settingName)
			};
			return this.Get<Setting>("/v1/GetSetting", parameters);
		}

		/// <inheritdoc />
		public Setting GetSetting(int id)
		{
			KeyValuePair<string, object>[] parameters = new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("id", id)
			};
			return this.Get<Setting>("/v1/GetSetting", parameters);
		}

		/// <inheritdoc />
		public Setting UnmaskSetting(string groupName, string settingName)
		{
			KeyValuePair<string, object>[] parameters = new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("groupName", groupName),
				new KeyValuePair<string, object>("settingName", settingName)
			};
			return this.Post<Setting>("/v1/UnmaskSetting", parameters);
		}

		/// <inheritdoc />
		public IEnumerable<Setting> GetSettings(string groupName)
		{
			KeyValuePair<string, object>[] parameters = new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("groupName", groupName),
			};
			return this.Get<List<Setting>>("/v1/GetSettings", parameters);
		}

		/// <inheritdoc />
		public IEnumerable<string> GetGroupNames()
		{
			return this.Get<List<string>>("/v1/GetGroupNames");
		}

		/// <inheritdoc />
		public void CreateSetting(ISetting setting)
		{
			this.CreateSetting(setting.GroupName, setting.Name, setting.Type, setting.Value, setting.Comment, setting.IsEnvironmentSpecific, setting.Updated, setting.IsMasked, setting.IsValueSameForAllTestEnvironments, setting.IsValueUniqueForProduction);
		}

		/// <summary>
		/// Creates a setting
		/// </summary>
		/// <param name="groupName"></param>
		/// <param name="settingName"></param>
		/// <param name="type"></param>
		/// <param name="value"></param>
		/// <param name="comment"></param>
		/// <param name="isEnvironmentSpecific"></param>
		/// <param name="created"></param>
		/// <param name="isMasked"></param>
		/// <param name="isValueSameForAllTestEnvironments"></param>
		/// <param name="isValueUniqueForProduction"></param>
		public void CreateSetting(string groupName, string settingName, string type, string value, string comment, bool isEnvironmentSpecific, DateTime created, bool isMasked, bool isValueSameForAllTestEnvironments, bool isValueUniqueForProduction)
		{
			KeyValuePair<string, object>[] parameters = new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("groupName", groupName),
				new KeyValuePair<string, object>("settingName", settingName),
				new KeyValuePair<string, object>("type", type),
				new KeyValuePair<string, object>("value", value),
				new KeyValuePair<string, object>("comment", comment),
				new KeyValuePair<string, object>("isEnvironmentSpecific", isEnvironmentSpecific),
				new KeyValuePair<string, object>("created", created),
				new KeyValuePair<string, object>("isMasked", isMasked),
				new KeyValuePair<string, object>("isValueSameForAllTestEnvironments", isValueSameForAllTestEnvironments),
				new KeyValuePair<string, object>("isValueUniqueForProduction", isValueUniqueForProduction)
			};
			this.Post("/v1/CreateSetting", parameters);
		}

		/// <inheritdoc />
		public void SetSetting(ISetting setting)
		{
			this.SetSetting(setting.GroupName, setting.Name, setting.Type, setting.Value, setting.Comment, setting.IsEnvironmentSpecific, setting.Updated, setting.IsMasked, setting.IsValueSameForAllTestEnvironments, setting.IsValueUniqueForProduction);
		}

		/// <summary>
		/// Updates a setting
		/// </summary>
		/// <param name="groupName"></param>
		/// <param name="settingName"></param>
		/// <param name="type"></param>
		/// <param name="value"></param>
		/// <param name="comment"></param>
		/// <param name="isEnvironmentSpecific"></param>
		/// <param name="updated"></param>
		/// <param name="isMasked"></param>
		/// <param name="isValueSameForAllTestEnvironments"></param>
		/// <param name="isValueUniqueForProduction"></param>
		public void SetSetting(string groupName, string settingName, string type, string value, string comment, bool isEnvironmentSpecific, DateTime updated, bool isMasked, bool isValueSameForAllTestEnvironments, bool isValueUniqueForProduction)
		{
			KeyValuePair<string, object>[] parameters = new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("groupName", groupName),
				new KeyValuePair<string, object>("settingName", settingName),
				new KeyValuePair<string, object>("type", type),
				new KeyValuePair<string, object>("value", value),
				new KeyValuePair<string, object>("comment", comment),
				new KeyValuePair<string, object>("isEnvironmentSpecific", isEnvironmentSpecific),
				new KeyValuePair<string, object>("updated", updated),
				new KeyValuePair<string, object>("isMasked", isMasked),
				new KeyValuePair<string, object>("isValueSameForAllTestEnvironments", isValueSameForAllTestEnvironments),
				new KeyValuePair<string, object>("isValueUniqueForProduction", isValueUniqueForProduction)
			};
			this.Post("/v1/SetSetting", parameters);
		}

		/// <summary>
		/// Override
		/// </summary>
		protected override void OnRequestStarting()
		{
		}

		private T Fetch<T>(string path, IEnumerable<KeyValuePair<string, object>> parameters, bool httpPost = false)
		{
			try
			{
				if (httpPost)
				{
					this.Post(path, parameters, (IEnumerable<KeyValuePair<string, object>>) null, (IEnumerable<KeyValuePair<string, string>>) null, (string) null, (string) null);
					return default(T);
				}
				return this.Get<T>(path, parameters, (IEnumerable<KeyValuePair<string, string>>) null, (string) null);
			}
			catch (ApiClientException val)
			{
				ApiClientException val2 = val;
				WebException ex = ((Exception) (object) val2).InnerException as WebException;
				if (ex != null)
				{
					HttpWebResponse httpWebResponse = (HttpWebResponse) ex.Response;
					Stream responseStream;
					if (httpWebResponse == null || httpWebResponse.StatusCode != HttpStatusCode.BadRequest || (responseStream = httpWebResponse.GetResponseStream()) == null)
					{
						throw;
					}
					string text = new StreamReader(responseStream).ReadToEnd();
					ConfigurationServiceErrorResponse configurationServiceErrorResponse = JsonConvert.DeserializeObject<ConfigurationServiceErrorResponse>(text);
					throw new Exception(configurationServiceErrorResponse.Message);
				}
				throw;
			}
		}

		private T Get<T>(string path, IEnumerable<KeyValuePair<string, object>> parameters)
		{
			return this.Fetch<T>(path, parameters, false);
		}

		private void Post(string path, IEnumerable<KeyValuePair<string, object>> parameters)
		{
			this.Fetch<object>(path, parameters, true);
		}

		/// <summary>
		/// Certificate validation callback.
		/// </summary>
		private bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
		{
			return true;
		}

		private readonly Func<string> _EndpointGetter;
	}
}
