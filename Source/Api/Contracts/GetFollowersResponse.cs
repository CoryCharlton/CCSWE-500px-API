using System.Collections.Generic;
using System.Runtime.Serialization;
using CCSWE.FiveHundredPx.Interfaces;
using CCSWE.FiveHundredPx.Models;

namespace CCSWE.FiveHundredPx.Contracts
{
    [DataContract]
    public class GetFollowersResponse: Response, IPagedResponse
    {
        [DataMember(Name = "page")]
        public int CurrentPage { get; set; }

        [DataMember(Name = "followers")]
        public List<User> Followers { get; set; }

        [DataMember(Name = "followers_count")]
        public int TotalItems { get; set; }

        [DataMember(Name = "followers_pages")]
        public int TotalPages { get; set; }
    }
}
