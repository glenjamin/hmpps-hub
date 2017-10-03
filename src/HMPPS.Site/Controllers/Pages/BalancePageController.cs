using System;
using System.Web.Mvc;
using HMPPS.Authentication;
using HMPPS.Site.Controllers.Base;
using HMPPS.Site.ViewModels.Pages;
using HMPPS.Utilities.Interfaces;
using HMPPS.Utilities.Services;
using Sitecore.Data.Items;

namespace HMPPS.Site.Controllers.Pages
{
    public class BalancePageController : BaseController
    {
        private BalancePageViewModel _bpvm;
        private IUserDataService _userDataService;

        public BalancePageController(IUserDataService userDataService)
        {

            _userDataService = userDataService;
        }

        private void BuildViewModel(Item contextItem)
        {
            _bpvm = new BalancePageViewModel();

            _bpvm.BreadcrumbItems = BreadcrumbItems;

            _bpvm.ShowAccountBalance = contextItem["Show Account Balance"] == "1";
            _bpvm.ShowPhoneCredit = contextItem["Show Phone Credit"] == "1";

            var userData = _userDataService.GetUserDataFromCookie(System.Web.HttpContext.Current);

            if (userData != null)
            {
                //account balance
                _bpvm.AccountBalance = userData.AccountBalance;              
                _bpvm.AccountBalanceLastUpdate = userData.AccountBalanceLastUpdated;
                //phone credit
                _bpvm.PhoneCredit = 0; //TODO when BT API available              
                _bpvm.PhoneCreditLastUpdate = DateTime.Now;
            }
        }

        public ActionResult Index()
        {
            BuildViewModel(Sitecore.Context.Item);
            return View("/Views/Pages/BalancePage.cshtml", _bpvm);
        }
    }
}
