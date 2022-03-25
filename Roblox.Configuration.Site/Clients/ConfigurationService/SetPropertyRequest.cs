namespace Roblox.Configuration.Site.Clients.ConfigurationService
{
    public class SetPropertyRequest
    {
        /// <summary>
		/// Gets or sets the setting group name
		/// </summary>
        public string GroupName { get; set; }

        /// <summary>
		/// Gets or sets the setting name
		/// </summary>
        public string Name { get; set; }

        /// <summary>
		/// Gets or sets the setting type
		/// </summary>
        public string Type { get; set; }

        /// <summary>
		/// Gets or sets the setting value
		/// </summary>
        public string Value { get; set; }

        /// <summary>
		/// Gets or sets the setting comment
		/// </summary>
        public string Comment { get; set; } = null;

        /// <summary>
		/// Gets or sets a value indicating whether the setting is a connection string or not.
		/// </summary>
        public bool? IsConnectionString { get; set; } = false;
    }
}
