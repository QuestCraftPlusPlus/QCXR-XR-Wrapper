namespace AndroidManifest
{
    /// <summary>
    /// Represents information about a manifest tag, including its node path, tag name, attribute name, attribute value, and any additional attributes.
    /// </summary>
    [System.Serializable]
    public class ManifestTagInfo
    {
        /// <summary>
        /// its node path
        /// </summary>
        public string nodePath;
        /// <summary>
        /// The name of the manifest tag.
        /// </summary>
        public string tag;

        /// <summary>
        /// The name of the attribute for the manifest tag.
        /// </summary>
        public string attrName ;

        /// <summary>
        /// The value of the attribute for the manifest tag.
        /// </summary>
        public string attrValue;

        /// <summary>
        /// An array of additional attributes for the manifest tag.
        /// </summary>
        public string[] attrs;

        /// <summary>
        /// Indicates whether this manifest tag is required.
        /// </summary>
        public bool required;

        /// <summary>
        /// Indicates whether to modify the manifest tag if it is found.
        /// </summary>
        public bool modifyIfFound;

    }
}