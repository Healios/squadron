using System.IO;

namespace Squadron
{
    /// <summary>
    /// Options when importing a Keycloak realm from a given file
    /// </summary>
    public class ImportRealmFromFileOptions
    {
        /// <summary>
        /// Source file information
        /// </summary>
        public FileInfo File { get; set; }

        /// <summary>
        /// Destination path (unix).
        /// Default destination is "/tmp"
        /// </summary>
        internal string Destination { get; set; } =
            "/tmp";

        /// <summary>
        /// Name of the realm to import
        /// </summary>
        public string Realm { get; set; }
    }
}
