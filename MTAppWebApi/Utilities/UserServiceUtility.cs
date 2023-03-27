using MTAPP.DAL.Model;
using MTAPP.Model;

namespace MTAppWebApi.Utilities
{
    public static class UserServiceUtility
    {
        public static UserModel ConvertToModel(AppUser appuser) =>
            new UserModel()
            {
                username = appuser.username,
                userid = appuser.userid,
                roleid = appuser.roleid,
                rolename = appuser.role?.rolename,
                email = appuser.email,
                cellphone = appuser.cellphone,
                createddate = appuser.createddate,
                passwordhash = appuser.passwordhash,
                refreshtoken = appuser.refreshtoken
            };

        public static AppUser ConvertToDBModel(UserModel userModel) =>
            new AppUser()
            {
                username = userModel.username,
                userid = userModel.userid,
                roleid = userModel.roleid,
                email = userModel.email,
                cellphone = userModel.cellphone,
                createddate = userModel.createddate,
                passwordhash = userModel.passwordhash,
                refreshtoken = userModel.refreshtoken
            };
    }
}
