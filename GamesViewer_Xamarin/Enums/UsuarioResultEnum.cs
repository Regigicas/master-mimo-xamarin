namespace GamesViewer_Xamarin.Enums
{
    public enum UsuarioResultEnum
    {
        ExistingUser = 0,
        ExistingEmail,
        InvalidEmail,
        UsernameNotFound,
        PasswordMismatch,
        OldNewPasswordSame,
        InternalError,
        Ok
    }

    public static class UsuarioResultEnumExtensions
    {
        public static string GetText(this UsuarioResultEnum result)
        {
            switch (result)
            {
                case UsuarioResultEnum.ExistingUser:
                    return Resx.AppResources.EnumExistingUser;
                case UsuarioResultEnum.ExistingEmail:
                    return Resx.AppResources.EnumExistingEmail;
                case UsuarioResultEnum.InvalidEmail:
                    return Resx.AppResources.EnumInvalidEmail;
                case UsuarioResultEnum.UsernameNotFound:
                    return Resx.AppResources.EnumUsernameNotFound;
                case UsuarioResultEnum.PasswordMismatch:
                    return Resx.AppResources.EnumPasswordMismatch;
                case UsuarioResultEnum.OldNewPasswordSame:
                    return Resx.AppResources.EnumOldNewPasswordSame;
                case UsuarioResultEnum.InternalError:
                    return Resx.AppResources.EnumInternalError;
                case UsuarioResultEnum.Ok:
                    return Resx.AppResources.EnumOK;
                default:
                    break;
            }

            return "";
        }
    }
}
