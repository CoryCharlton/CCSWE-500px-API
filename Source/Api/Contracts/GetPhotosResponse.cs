using System.Collections.Generic;
using System.Runtime.Serialization;
using CCSWE.FiveHundredPx.Models;

namespace CCSWE.FiveHundredPx.Contracts
{
    [DataContract]
    public class GetPhotosResponse: PagedResponse
    {
        [DataMember(Name = "photos")]
        public List<Photo> Photos { get; set; }
    }
}
