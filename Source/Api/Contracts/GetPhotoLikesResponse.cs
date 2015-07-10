using System.Collections.Generic;
using System.Runtime.Serialization;
using CCSWE.FiveHundredPx.Models;

namespace CCSWE.FiveHundredPx.Contracts
{
    [DataContract]
    public class GetPhotoLikesResponse: PagedResponse
    {
        [DataMember(Name = "users")]
        public List<User> Users { get; set; }
    }
}
