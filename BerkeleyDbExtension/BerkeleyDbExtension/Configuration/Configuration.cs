using System.Configuration;
using BerkeleyDB;

namespace BerkeleyDbExtension.Configuration
{
    /// <summary>
    /// Define configration for 
    /// </summary>
    public sealed class Configuration : ConfigurationSection
    {
        private static readonly Configuration settings = ConfigurationManager.GetSection("BdbConfig") as Configuration;

        public static Configuration Settings { get { return settings; } }

        /// <summary>
        /// Gets or sets the creation policity.
        /// </summary>
        /// <value>
        /// The creation.
        /// </value>
        [ConfigurationProperty("creation")]
        public CreatePolicy? Creation
        {
            get { return (CreatePolicy?)base["creation"]; }
            set { base["creation"] = value; }
        }


        // Include as many properties as your work culture defines. 
    }
}
