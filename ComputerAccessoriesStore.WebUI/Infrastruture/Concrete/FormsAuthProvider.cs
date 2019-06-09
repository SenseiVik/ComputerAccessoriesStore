using ComputerAccessoriesStore.WebUI.Infrastruture.Abstract;
using System.Web.Security;

namespace ComputerAccessoriesStore.WebUI.Infrastruture.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);

            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            return result;
        }
    }
}