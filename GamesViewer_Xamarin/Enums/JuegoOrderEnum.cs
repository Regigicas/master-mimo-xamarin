using System;
namespace GamesViewer_Xamarin.Enums
{
    public enum JuegoOrderEnum
    {
        ByDefault,
        ByName,
        ByNameInverse,
        ByReleaseDate,
        ByReleaseDateInverse,
        ByAdded,
        ByAddedInverse,
        ByCreated,
        ByCreatedInverse,
        ByRating,
        ByRatingInverse
    }

    public static class JuegoOrderEnumExtensions
    {
        public static string GetApiValue(this JuegoOrderEnum result)
        {
            switch (result)
            {
                case JuegoOrderEnum.ByDefault:
                    return Resx.AppResources.ApiDefault;
                case JuegoOrderEnum.ByName:
                    return Resx.AppResources.ApiName;
                case JuegoOrderEnum.ByNameInverse:
                    return Resx.AppResources.ApiNameInverse;
                case JuegoOrderEnum.ByReleaseDate:
                    return Resx.AppResources.ApiReleased;
                case JuegoOrderEnum.ByReleaseDateInverse:
                    return Resx.AppResources.ApiReleasedInverse;
                case JuegoOrderEnum.ByAdded:
                    return Resx.AppResources.ApiAdded;
                case JuegoOrderEnum.ByAddedInverse:
                    return Resx.AppResources.ApiAddedInverse;
                case JuegoOrderEnum.ByCreated:
                    return Resx.AppResources.ApiCreated;
                case JuegoOrderEnum.ByCreatedInverse:
                    return Resx.AppResources.ApiCreatedInverse;
                case JuegoOrderEnum.ByRating:
                    return Resx.AppResources.ApiRating;
                case JuegoOrderEnum.ByRatingInverse:
                    return Resx.AppResources.ApiRatingInverse;
                default:
                    break;
            }

            return "";
        }

        public static string GetStringValue(this JuegoOrderEnum result)
        {
            switch (result)
            {
                case JuegoOrderEnum.ByDefault:
                    return Resx.AppResources.OrderDefault;
                case JuegoOrderEnum.ByName:
                    return Resx.AppResources.OrderName;
                case JuegoOrderEnum.ByNameInverse:
                    return Resx.AppResources.OrderNameInverse;
                case JuegoOrderEnum.ByReleaseDate:
                    return Resx.AppResources.OrderReleased;
                case JuegoOrderEnum.ByReleaseDateInverse:
                    return Resx.AppResources.OrderReleasedInverse;
                case JuegoOrderEnum.ByAdded:
                    return Resx.AppResources.OrderAdded;
                case JuegoOrderEnum.ByAddedInverse:
                    return Resx.AppResources.OrderAddedInverse;
                case JuegoOrderEnum.ByCreated:
                    return Resx.AppResources.OrderCreated;
                case JuegoOrderEnum.ByCreatedInverse:
                    return Resx.AppResources.OrderCreatedInverse;
                case JuegoOrderEnum.ByRating:
                    return Resx.AppResources.OrderRatingInverse;
                case JuegoOrderEnum.ByRatingInverse:
                    return Resx.AppResources.OrderRatingInverse;
                default:
                    break;
            }

            return "";
        }
    }
}
