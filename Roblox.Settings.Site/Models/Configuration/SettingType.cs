using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roblox.Settings.Site.Models.Configuration
{
    public enum SettingType
    {
        Int32,              // System.Int32
        Int64,              // System.Int64
        Double,             // System.Double
        Decimal,            // System.Decimal
        Float,              // System.Float
        TimeSpan,           // System.TimeSpan
        RedisEndpoints,     // Roblox.Redis.RedisEndpoints
        Csv,                // Roblox.Csv
        WeightedCsv,        // Roblox.Configuration.WeightedCsv
        Boolean,            // System.Boolean
        Guid,               // System.Guid
        Byte,               // System.Byte
        String,             // System.String
    }
}