using CCSWE.FiveHundredPx.Interfaces;

namespace CCSWE.FiveHundredPx
{
    public static class UrlBuilder
    {
        #region Private Methods
        // ReSharper disable once UnusedMember.Local
        private static string AddPagingParameters(string url, int page = -1, int resultsPerPage = 20)
        {
            if (page > 0)
            {
                url += (url.Contains("?") ? "&" : "?") + "page=" + page;
            }

            if (resultsPerPage > 20 && resultsPerPage <= 100)
            {
                url += (url.Contains("?") ? "&" : "?") + "rpp=" + resultsPerPage;
            }

            return url;
        }

        private static string AddParameter(string url, string key, string value)
        {
            return url + (url.Contains("?") ? "&" : "?") + key + "=" + value;
        }
        #endregion

        #region Public Methods
        public static string GetCurrentUser()
        {
            return "https://api.500px.com/v1/users";
        }

        public static string GetFriends(long userId)
        {
            return string.Format("https://api.500px.com/v1/users/{0}/friends", userId);
        }

        public static string GetFollowers(long userId)
        {
            return string.Format("https://api.500px.com/v1/users/{0}/followers", userId);
        }

        public static string GetPhotoFavorites(long photoId)
        {
            return string.Format("https://api.500px.com/v1/photos/{0}/favorites", photoId);
        }

        public static string GetPhotoLikes(long photoId)
        {
            return string.Format("https://api.500px.com/v1/photos/{0}/votes", photoId);
        }

        public static string GetPhotos(IPhotoFilter filter)
        {
            //TODO: UrlBuilder.GetPhotos() - Add some validation...
            var url = "https://api.500px.com/v1/photos";

            url = AddParameter(url, "feature", Converter.ConvertFeatureToQueryParameterValue(filter.Feature));

            if (filter.Categories != Categories.None)
            {
                url = AddParameter(url, (filter.FilterMode == FilterMode.Exclude ? "exclude" : "only"), Converter.ConvertCategoriesToQueryParameterValue(filter.Categories));
            }

            if (filter.Sort != Sort.Default)
            {
                url = AddParameter(url, "sort", Converter.ConvertSortToQueryParameterValue(filter.Sort));
            }

            if (filter.SortDirection != SortDirection.Default)
            {
                url = AddParameter(url, "sort_direction", Converter.ConvertSortDirectionToQueryParameterValue(filter.SortDirection));
            }

            if (filter.UserId > 0)
            {
                url = AddParameter(url, "user_id", filter.UserId.ToString());
            }

            foreach (var size in filter.Sizes)
            {
                url = AddParameter(url, "image_size[]", size.ToString());
            }

            //Default Parameters
            url = AddParameter(url, "include_states", "voted");

            return url;
        }

        public static string GetUser(long userId)
        {
            return string.Format("https://api.500px.com/v1/users/show?id={0}", userId);
        }
        #endregion

    }
}
