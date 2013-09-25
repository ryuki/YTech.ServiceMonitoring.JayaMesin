using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;
using YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels;
using System.Linq;
using YTech.ServiceTracker.JayaMesin.Domain;
using System;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers
{
    [HandleError]
    public partial class CustomerController : Controller
    {
        private readonly IMCustomerTasks customerTasks;
        public CustomerController(IMCustomerTasks customerTasks)
        {
            this.customerTasks = customerTasks;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Customers_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetCustomers().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customers_Create([DataSourceRequest] DataSourceRequest request, CustomerViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                MCustomer cust = new MCustomer();
                cust.SetAssignedIdTo(Guid.NewGuid().ToString());

                cust.CustomerName = custVM.CustomerName;
                cust.CustomerPhone = custVM.CustomerPhone;
                cust.CustomerAddress = custVM.CustomerAddress;

                cust.CreatedDate = DateTime.Now;
                cust.CreatedBy = User.Identity.Name;
                cust.DataStatus = "New";

                customerTasks.Insert(cust);
            }

            return Json(new[] { custVM }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customers_Update([DataSourceRequest] DataSourceRequest request, CustomerViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                var target = customerTasks.One(custVM.CustomerID);
                if (target != null)
                {
                    target.CustomerName = custVM.CustomerName;
                    target.CustomerPhone = custVM.CustomerPhone;
                    target.CustomerAddress = custVM.CustomerAddress;

                    target.ModifiedDate = DateTime.Now;
                    target.ModifiedBy = User.Identity.Name;
                    target.DataStatus = "Updated";

                    customerTasks.Update(target);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customers_Destroy([DataSourceRequest] DataSourceRequest request, CustomerViewModel custVM)
        {
            if (custVM != null)
            {
                var target = customerTasks.One(custVM.CustomerID);
                if (target != null)
                {
                    customerTasks.Delete(target);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<CustomerViewModel> GetCustomers()
        {
            var customers = this.customerTasks.GetAllCustomers();

            return from cust in customers
                   select new CustomerViewModel
        {
            CustomerID = cust.Id,
            CustomerName = cust.CustomerName,
            CustomerPhone = cust.CustomerPhone,
            CustomerAddress = cust.CustomerAddress
        };

        }

    }
}
