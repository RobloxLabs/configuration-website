using System.Runtime.Serialization;

namespace Roblox.Configuration.Site.Models.Configuration
{
    [DataContract]
    public class ValidateWeightedCsvRequest
    {
        [DataMember(Name = "rawWeightedCsv", IsRequired = false)]
        public string WeightedCsv { get; set; }
    }
}