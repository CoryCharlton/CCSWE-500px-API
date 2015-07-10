using System.Collections.Generic;
using System.Runtime.Serialization;
using CCSWE.FiveHundredPx.Interfaces;
using CCSWE.FiveHundredPx.Models;

namespace CCSWE.FiveHundredPx.Contracts
{
    [DataContract]
    public class GetFriendsResponse: Response, IPagedResponse
    {
        [DataMember(Name = "page")]
        public int CurrentPage { get; set; }

        [DataMember(Name = "friends")]
        public List<User> Friends { get; set; }

        [DataMember(Name = "friends_count")]
        public int TotalItems { get; set; }

        [DataMember(Name = "friends_pages")]
        public int TotalPages { get; set; }
    }
}
