namespace RSPlugCore.Library.Common
{
    public class PluginManifest
    {
        /// <summary>
        /// Plugin name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Plugin version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Plugin host version
        /// </summary>
        public string HostVersion { get; set; }

        /// <summary>
        /// Plugin description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Plugin load order
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Plugin assembly name
        /// </summary>
        public string Filename { get; set; }
    }
}
