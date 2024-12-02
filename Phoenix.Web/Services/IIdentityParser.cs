using System.Security.Principal;

namespace Phoenix.Web.Services
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
