using System;

namespace Roblox.Configuration.Site.Clients.ConfigurationService
{
	/// <inheritdoc />
	public class Setting : ISetting
	{
		/// <inheritdoc />
		public int Id { get; set; }

		/// <inheritdoc />
		public string GroupName { get; set; }

		/// <inheritdoc />
		public string Name { get; set; }

		/// <inheritdoc />
		public string Type { get; set; }

		/// <inheritdoc />
		public string Value { get; set; }

		/// <inheritdoc />
		public string Comment { get; set; }

		/// <inheritdoc />
		public bool IsEnvironmentSpecific { get; set; }

		/// <inheritdoc />
		public DateTime Updated { get; set; }

		/// <inheritdoc />
		public bool IsMasked { get; set; }

		/// <inheritdoc />
		public bool IsValueSameForAllTestEnvironments { get; set; }

		/// <inheritdoc />
		public bool IsValueUniqueForProduction { get; set; }

		public Setting()
		{
		}

		/// <summary>
		/// Create a new setting that is a copy of an existing setting.
		/// </summary>
		/// <param name="setting"></param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="setting" /></exception>
		public Setting(Setting setting)
		{
			if (setting == null)
			{
				throw new ArgumentNullException("setting");
			}
			this.Id = setting.Id;
			this.GroupName = setting.GroupName;
			this.Name = setting.Name;
			this.Type = setting.Type;
			this.Value = setting.Value;
			this.Comment = setting.Comment;
			this.IsEnvironmentSpecific = setting.IsEnvironmentSpecific;
			this.Updated = setting.Updated;
			this.IsMasked = setting.IsMasked;
			this.IsValueSameForAllTestEnvironments = setting.IsValueSameForAllTestEnvironments;
			this.IsValueUniqueForProduction = setting.IsValueUniqueForProduction;
		}
	}
}
