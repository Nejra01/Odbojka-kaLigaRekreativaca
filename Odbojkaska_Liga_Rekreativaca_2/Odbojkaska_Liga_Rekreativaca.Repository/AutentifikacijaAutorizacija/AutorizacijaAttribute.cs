using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FIT_Api_Examples.Helper.AutentifikacijaAutorizacija
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool zapisnicar, bool sudija, bool admin)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] {zapisnicar, sudija, admin  };
        }
    }


    public class MyAuthorizeImpl : IActionFilter
    {
        private readonly bool _sudija;
        private readonly bool _zapisnicar;
        private readonly bool _Admin;
        

        public MyAuthorizeImpl(bool zapisnicar, bool sudija, bool admin)
        {
           _sudija = sudija;
            _zapisnicar = zapisnicar;
            _Admin = admin;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {


        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            MyAuthTokenExtension.LoginInformacije loginInformacije = filterContext.HttpContext.GetLoginInfo();
            if (loginInformacije.isLogiran || loginInformacije.korisnickiNalog==null)
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }

            KretanjePoSistemu.Save(filterContext.HttpContext);
            
            if (loginInformacije.korisnickiNalog.isSudija)
            {
                return;//ok - ima pravo pristupa
            } 
            
            if (loginInformacije.korisnickiNalog.isZapisnicar)
            {
                return;//ok - ima pravo pristupa
            } 
            if (loginInformacije.korisnickiNalog.isAdmin)
            {
                return;//ok - ima pravo pristupa
            }
           

            //else nema pravo pristupa
            filterContext.Result = new UnauthorizedResult();
        }
    }
}
