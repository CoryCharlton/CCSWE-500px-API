using System;
using System.Diagnostics;

namespace CCSWE.FiveHundredPx
{
    public static class Converter
    {
        public static string ConvertCategoriesToQueryParameterValue(Categories categories)
        {
            var returnValue = string.Empty;

            if (categories.HasFlag(Categories.Abstract))
            {
                returnValue += Categories.Abstract + ",";
            }
            if (categories.HasFlag(Categories.Animals))
            {
                returnValue += Categories.Animals + ",";
            }
            if (categories.HasFlag(Categories.BlackAndWhite))
            {
                returnValue += "black and white,";
            }
            if (categories.HasFlag(Categories.Celebrities))
            {
                returnValue += Categories.Celebrities + ",";
            }
            if (categories.HasFlag(Categories.CityAndArchitecture))
            {
                returnValue += "City and Architecture,";
            }
            if (categories.HasFlag(Categories.Commercial))
            {
                returnValue += Categories.Commercial + ",";
            }
            if (categories.HasFlag(Categories.Concert))
            {
                returnValue += Categories.Concert + ",";
            }
            if (categories.HasFlag(Categories.Family))
            {
                returnValue += Categories.Family + ",";
            }
            if (categories.HasFlag(Categories.Fashion))
            {
                returnValue += Categories.Fashion + ",";
            }
            if (categories.HasFlag(Categories.Film))
            {
                returnValue += Categories.Film + ",";
            }
            if (categories.HasFlag(Categories.FineArt))
            {
                returnValue += Categories.FineArt + ",";
            }
            if (categories.HasFlag(Categories.Food))
            {
                returnValue += Categories.Food + ",";
            }
            if (categories.HasFlag(Categories.Journalism))
            {
                returnValue += Categories.Journalism + ",";
            }
            if (categories.HasFlag(Categories.Landscapes))
            {
                returnValue += Categories.Landscapes + ",";
            }
            if (categories.HasFlag(Categories.Macro))
            {
                returnValue += Categories.Macro + ",";
            }
            if (categories.HasFlag(Categories.Nature))
            {
                returnValue += Categories.Nature + ",";
            }
            if (categories.HasFlag(Categories.Nude))
            {
                returnValue += Categories.Nude + ",";
            }
            if (categories.HasFlag(Categories.People))
            {
                returnValue += Categories.People + ",";
            }
            if (categories.HasFlag(Categories.PerformingArts))
            {
                returnValue += Categories.PerformingArts + ",";
            }
            if (categories.HasFlag(Categories.Sport))
            {
                returnValue += Categories.Sport + ",";
            }
            if (categories.HasFlag(Categories.StillLife))
            {
                returnValue += Categories.StillLife  + ",";
            }
            if (categories.HasFlag(Categories.Street))
            {
                returnValue += Categories.Street + ",";
            }
            if (categories.HasFlag(Categories.Transportation))
            {
                returnValue += Categories.Transportation + ",";
            }
            if (categories.HasFlag(Categories.Travel))
            {
                returnValue += Categories.Travel + ",";
            }
            if (categories.HasFlag(Categories.Uncategorized))
            {
                returnValue += Categories.Uncategorized + ",";
            }
            if (categories.HasFlag(Categories.Underwater))
            {
                returnValue += Categories.Underwater + ",";
            }
            if (categories.HasFlag(Categories.UrbanExploration))
            {
                returnValue += Categories.UrbanExploration + ",";
            }
            if (categories.HasFlag(Categories.Wedding))
            {
                returnValue += Categories.Wedding + ",";
            }

            if (returnValue.Length > 0)
            {
                returnValue = returnValue.Substring(0, returnValue.Length - 1);
            }

            Debug.WriteLine("ConvertCategoriesToQueryParameterValue: " + returnValue);
            return returnValue;
        }

        public static string ConvertCategoriesToString(Categories categories)
        {
            switch (categories)
            {
                case Categories.None:
                    return ConvertCategoryToString(0);
                case Categories.Abstract:
                    return ConvertCategoryToString(10);
                case Categories.Animals:
                    return ConvertCategoryToString(11);
                case Categories.BlackAndWhite:
                    return ConvertCategoryToString(5);
                case Categories.Celebrities:
                    return ConvertCategoryToString(1);
                case Categories.CityAndArchitecture:
                    return ConvertCategoryToString(9);
                case Categories.Commercial:
                    return ConvertCategoryToString(15);
                case Categories.Concert:
                    return ConvertCategoryToString(16);
                case Categories.Family:
                    return ConvertCategoryToString(20);
                case Categories.Fashion:
                    return ConvertCategoryToString(14);
                case Categories.Film:
                    return ConvertCategoryToString(2);
                case Categories.FineArt:
                    return ConvertCategoryToString(24);
                case Categories.Food:
                    return ConvertCategoryToString(23);
                case Categories.Journalism:
                    return ConvertCategoryToString(3);
                case Categories.Landscapes:
                    return ConvertCategoryToString(8);
                case Categories.Macro:
                    return ConvertCategoryToString(12);
                case Categories.Nature:
                    return ConvertCategoryToString(18);
                case Categories.Nude:
                    return ConvertCategoryToString(4);
                case Categories.People:
                    return ConvertCategoryToString(7);
                case Categories.PerformingArts:
                    return ConvertCategoryToString(19);
                case Categories.Sport:
                    return ConvertCategoryToString(17);
                case Categories.StillLife:
                    return ConvertCategoryToString(6);
                case Categories.Street:
                    return ConvertCategoryToString(21);
                case Categories.Transportation:
                    return ConvertCategoryToString(26);
                case Categories.Travel:
                    return ConvertCategoryToString(13);
                case Categories.Uncategorized:
                    return ConvertCategoryToString(0);
                case Categories.Underwater:
                    return ConvertCategoryToString(22);
                case Categories.UrbanExploration:
                    return ConvertCategoryToString(27);
                case Categories.Wedding:
                    return ConvertCategoryToString(25);
                default:
                    throw new ArgumentOutOfRangeException("categories", "Invalid 'category'");
            }
        }

        public static string ConvertCategoryToString(int category)
        {
            switch (category)
            {
                case 0:
                    return "Uncategorized";
                case 10:
                    return "Abstract";
                case 11:
                    return "Animals";
                case 5:
                    return "Black and White";
                case 1:
                    return "Celebrities";
                case 9:
                    return "City and Architecture";
                case 15:
                    return "Commercial";
                case 16:
                    return "Concert";
                case 20:
                    return "Family";
                case 14:
                    return "Fashion";
                case 2:
                    return "Film";
                case 24:
                    return "Fine Art";
                case 23:
                    return "Food";
                case 3:
                    return "Journalism";
                case 8:
                    return "Landscapes";
                case 12:
                    return "Macro";
                case 18:
                    return "Nature";
                case 4:
                    return "Nude";
                case 7:
                    return "People";
                case 19:
                    return "Performing Arts";
                case 17:
                    return "Sport";
                case 6:
                    return "Still Life";
                case 21:
                    return "Street";
                case 26:
                    return "Transportation";
                case 13:
                    return "Travel";
                case 22:
                    return "Underwater";
                case 27:
                    return "Urban Exploration";
                case 25:
                    return "Wedding";
                default:
                    throw new ArgumentOutOfRangeException("category", "Invalid 'category'");

            }    
        }

        public static string ConvertFeatureToQueryParameterValue(Feature feature)
        {
            switch (feature)
            {
                case Feature.Popular:
                {
                    return "popular";
                }
                case Feature.HighestRated:
                {
                    return "highest_rated";
                }
                case Feature.Upcoming:
                {
                    return "upcoming";
                }
                case Feature.Editors:
                {
                    return "editors";
                }
                case Feature.FreshToday:
                {
                    return "fresh_today";
                }
                case Feature.FreshYesterday:
                {
                    return "fresh_yesterday";
                }
                case Feature.FreshWeek:
                {
                    return "fresh_week";
                }
                case Feature.User:
                {
                    return "user";
                }
                case Feature.UserFriends:
                {
                    return "user_friends";
                }
                case Feature.UserFavorites:
                {
                    return "user_favorites";
                }
                default:
                    throw new ArgumentOutOfRangeException("feature", "Invalid 'feature'");                    
            }
        }

        public static string ConvertFeatureToString(Feature feature)
        {
            switch (feature)
            {
                case Feature.Popular:
                    {
                        return "Popular";
                    }
                case Feature.HighestRated:
                    {
                        return "Highest Rated";
                    }
                case Feature.Upcoming:
                    {
                        return "Upcoming";
                    }
                case Feature.Editors:
                    {
                        return "Editors";
                    }
                case Feature.FreshToday:
                    {
                        return "Fresh (Today)";
                    }
                case Feature.FreshYesterday:
                    {
                        return "Fresh (Yesterday)";
                    }
                case Feature.FreshWeek:
                    {
                        return "Fresh (Week)";
                    }
                case Feature.User:
                    {
                        return "User";
                    }
                case Feature.UserFriends:
                    {
                        return "User's Friends";
                    }
                case Feature.UserFavorites:
                    {
                        return "User's Favorites";
                    }
                default:
                    throw new ArgumentOutOfRangeException("feature", "Invalid 'feature'");
            }
        }

        public static bool ConvertIntToBool(int value)
        {
            return value > 0;
        }

        public static string ConvertSortToQueryParameterValue(Sort sort)
        {
            switch (sort)
            {
                case Sort.Created:
                    {
                        return "created_at";
                    }
                case Sort.Rating:
                    {
                        return "rating";
                    }
                case Sort.HighestRating:
                    {
                        return "highest_rating";
                    }
                case Sort.TimesViewed:
                    {
                        return "times_viewed";
                    }
                case Sort.Likes:
                    {
                        return "votes_count";
                    }
                case Sort.Favorites:
                    {
                        return "favorites_count";
                    }
                case Sort.Comments:
                    {
                        return "comments_count";
                    }
                case Sort.Taken:
                    {
                        return "taken_at";
                    }
                default:
                    throw new ArgumentOutOfRangeException("sort", "Invalid 'sort'");                    
            }
        }

        public static string ConvertSortDirectionToQueryParameterValue(SortDirection sortDirection)
        {
            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    {
                        return "asc";
                    }
                case SortDirection.Descending:
                    {
                        return "desc";
                    }
                default:
                    throw new ArgumentOutOfRangeException("sortDirection", "Invalid 'sortDirection'");                    
            }
        }
    }
}
