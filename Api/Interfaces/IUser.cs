namespace CCSWE.FiveHundredPx.Interfaces
{
    public interface IUser
    {
        long Affection { get; set; }
        string City { get; set; }
        string County { get; set; }
        string FirstName { get; set; }
        long FollowersCount { get; set; }
        string FullName { get; set; }
        long Id { get; set; }
        string LastName { get; set; }
        int UpgradeStatus { get; set; }
        string UserName { get; set; }
        string UserPicUrl { get; set; }
    }
}