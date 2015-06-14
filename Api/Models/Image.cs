using System.Runtime.Serialization;

namespace CCSWE.FiveHundredPx.Models
{
    [DataContract]
    public class Image
    {
        [DataMember(Name = "format")]
        public string Format { get; set; }

        [DataMember(Name = "https_url")]
        public string HttpsUrl { get; set; }

        [DataMember(Name = "size")]
        public int Size { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}
