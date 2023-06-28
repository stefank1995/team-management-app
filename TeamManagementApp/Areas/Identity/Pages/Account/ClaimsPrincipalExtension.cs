using System.Security.Claims;

namespace TeamManagementApp.Areas.Identity.Pages.Account
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                return null; //throw new ArgumentNullException(nameof(principal));

            string ret = "";

            try
            {
                ret = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            catch (System.Exception)
            {
            }
            return ret;
        }
    }
}
