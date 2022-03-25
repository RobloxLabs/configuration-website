using System;
using System.Diagnostics;

namespace Roblox.Configuration.Site.Clients.ConfigurationService
{

    /// <inheritdoc />
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
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

        private string GetDebuggerDisplay()
        {
			return $"{GroupName}.{Name}";
        }
    }
}
