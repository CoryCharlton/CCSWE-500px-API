using System.Runtime.Serialization;
using CCSWE.FiveHundredPx.Models;

namespace CCSWE.FiveHundredPx.Contracts
{
    [DataContract]
    public class GetUserDetailsResponse: Response
    {
        [DataMember(Name = "user")]
        public UserDetails User { get; set; }
    }
}
