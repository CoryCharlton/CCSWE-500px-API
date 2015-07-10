using System.Runtime.Serialization;
using CCSWE.FiveHundredPx.Interfaces;

namespace CCSWE.FiveHundredPx.Models
{
	[DataContract]
	public class User : IUser
	{
		#region Public Properties
		[DataMember(Name = "affection")]
		public long Affection { get; set; }

		[DataMember(Name = "city")]
		public string City { get; set; }

		[DataMember(Name = "country")]
		public string County { get; set; }

		[DataMember(Name = "firstname")]
		public string FirstName { get; set; }

		[DataMember(Name = "followers_count")]
		public long FollowersCount { get; set; }

		[DataMember(Name = "fullname")]
		public string FullName { get; set; }

		[DataMember(Name = "id")]
		public long Id { get; set; }

		[DataMember(Name = "lastname")]
		public string LastName { get; set; }

		[DataMember(Name = "upgrade_status")]
		public int UpgradeStatus { get; set; }

		[DataMember(Name = "username")]
		public string UserName { get; set; }

		[DataMember(Name = "userpic_url")]
		public string UserPicUrl { get; set; }
		#endregion

		#region Public Methods
		public override string ToString()
		{
			return FullName + " [" + Id + "]";
		}
		#endregion
	}
}
