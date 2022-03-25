using System;
using System.Collections.Generic;

namespace Roblox.Configuration.Site.Clients.ConfigurationService
{
	/// <summary>
	/// Client to access configuration settings
	/// </summary>
	public interface IConfigurationClient
	{
		/// <summary>
		/// Gets name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets a setting by group name and setting name
		/// </summary>
		/// <param name="groupName"></param>
		/// <param name="settingName"></param>
		/// <returns><see cref="T:Roblox.Configuration.Site.Clients.ConfigurationService.ISetting" /></returns>
		Setting GetSetting(string groupName, string settingName);

		/// <summary>
		/// Gets a setting by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns><see cref="T:Roblox.Configuration.Site.Clients.ConfigurationService.ISetting" /></returns>
		Setting GetSetting(int id);

		/// <summary>
		/// Gets the unmasked version of a masked setting by group name and setting name
		/// </summary>
		/// <param name="id"></param>
		/// <returns><see cref="T:Roblox.Configuration.Site.Clients.ConfigurationService.ISetting" /></returns>
		Setting UnmaskSetting(string groupName, string settingName);

		/// <summary>
		/// Gets a list of settings by group name
		/// </summary>
		/// <param name="groupName"></param>
		/// <returns><see cref="T:IEnumerable<Roblox.Configuration.Site.Clients.ConfigurationService.ISetting>" /></returns>
		IEnumerable<Setting> GetSettings(string groupName);

		/// <summary>
		/// Gets a list of settings group names
		/// </summary>
		/// <returns><see cref="T:IEnumerable<string>" /></returns>
		IEnumerable<string> GetGroupNames();

		/// <summary>
		/// Creates a setting
		/// </summary>
		/// <param name="setting"></param>
		void CreateSetting(ISetting setting);

		/// <summary>
		/// Updates a setting
		/// </summary>
		/// <param name="setting"></param>
		void SetSetting(ISetting setting);
	}
}
