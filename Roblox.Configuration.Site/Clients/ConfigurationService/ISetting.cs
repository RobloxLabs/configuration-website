using System;

namespace Roblox.Configuration.Site.Clients.ConfigurationService
{
	public interface ISetting
	{
		/// <summary>
		/// Gets or sets the setting comment
		/// </summary>
		string Comment { get; set; }

		/// <summary>
		/// Gets or sets the setting group name
		/// </summary>
		string GroupName { get; set; }

		/// <summary>
		/// Gets or sets the setting id
		/// </summary>
		int Id { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the setting is environment specific
		/// </summary>
		bool IsEnvironmentSpecific { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the setting is masked
		/// </summary>
		bool IsMasked { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the setting is the same value for all test environments
		/// </summary>
		bool IsValueSameForAllTestEnvironments { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the setting is unique for production
		/// </summary>
		bool IsValueUniqueForProduction { get; set; }

		/// <summary>
		/// Gets or sets the setting name
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the setting type
		/// </summary>
		string Type { get; set; }

		/// <summary>
		/// Gets or sets the setting updated datetime
		/// </summary>
		DateTime Updated { get; set; }

		/// <summary>
		/// Gets or sets the setting value
		/// </summary>
		string Value { get; set; }
	}
}
