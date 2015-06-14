using System.Runtime.Serialization;
using CCSWE.FiveHundredPx.Models;

namespace CCSWE.FiveHundredPx.Contracts
{
    [DataContract]
    public class RemoveFriendResponse : Response
    {
        [DataMember(Name = "user")]
        public User User { get; set; }
    }
}
