using System;

namespace CCSWE.FiveHundredPx.Interfaces
{
    public interface IUserDetails: IUser
    {
        string About { get; set; }
        //admin - Boolean value that will be 1 if the user is a 500px team member.
        //avatars - A dictionary of different avatar sizes. Keys are default, large, small, tiny. default is up to 300x300px, large is 100x100px, small is 50x50px, tiny is 30x30px.
        //contacts — A dictionary of user’s contacts, object. Keys should be treated as provider names, and values as user IDs with given provider.
        string Domain { get; set; }
        //equipment - A dictionary of a user's equipment. Possible keys are camera, lens, misc. Each key will have an array of values.
        //following - A boolean value indicating whether or not you are following this user.
        long FriendsCount { get; set; }
        //in_favorites_count — Number of times any photo of the user was added to favorites, integer.
        string Locale { get; set; }
        long PhotosCount { get; set; }
        DateTime RegistrationDate { get; set; }
        //TODO: IUserDetails - This should not be nullable according to the spec :shrug:
        int? Sex { get; set; }
        bool ShowNude { get; set; }
        string State { get; set; }
        bool StoreEnabled { get; set; }
    }
}
