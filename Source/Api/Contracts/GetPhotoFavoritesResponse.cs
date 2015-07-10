using System.Collections.Generic;
using System.Runtime.Serialization;
using CCSWE.FiveHundredPx.Models;

namespace CCSWE.FiveHundredPx.Contracts
{
    [DataContract]
    public class GetPhotoFavoritesResponse: PagedResponse
    {
        [DataMember(Name = "users")]
        public List<User> Users { get; set; } 
    }
}
