using System.Runtime.Serialization;

namespace CloudFoundryServiceBroker.Interfaces.Entities
{
    /// <summary>
    /// Service matadata
    /// </summary>
    [DataContract]
    public class ServiceMetadata
    {
        /// <summary>
        /// Display Name
        /// </summary>
        [DataMember(Order = 0, IsRequired = true, Name = "displayName")]
        public string DisplayName { get; set; }
        /// <summary>
        /// Image Url
        /// </summary>
        [DataMember(Order = 1, IsRequired = true, Name = "imageUrl")]
        public string ImageUrl { get; set; }
        /// <summary>
        /// Long Description
        /// </summary>
        [DataMember(Order = 2, IsRequired = true, Name = "longDescription")]
        public string LongDescription { get; set; }
        /// <summary>
        /// Provider Display Name
        /// </summary>
        [DataMember(Order = 3, IsRequired = true, Name = "providerDisplayName")]
        public string ProviderDisplayName { get; set; }
        /// <summary>
        /// Documentation Url
        /// </summary>
        [DataMember(Order = 4, IsRequired = true, Name = "documentationUrl")]
        public string DocumentationUrl { get; set; }
        /// <summary>
        /// Support Url
        /// </summary>
        [DataMember(Order = 5, IsRequired = true, Name = "supportUrl")]
        public string SupportUrl { get; set; }

        /// <summary>
        /// String representation of object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("ServiceMetadata [DisplayName: {0}; ImageUrl: {1}; LongDescription: {2}; ProviderDisplayName: {3}; DocumentationUrl: {4}; SupportUrl: {5}]",
                DisplayName, ImageUrl, LongDescription, ProviderDisplayName, DocumentationUrl, SupportUrl);
        }
    }
}
