﻿using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTech.ServiceTracker.JayaMesin.Domain.Contracts.Tasks;
using YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers.ViewModels;
using YTech.ServiceTracker.JayaMesin.Domain;
using YTech.ServiceTracker.JayaMesin.Enums;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Diagnostics;

using System.Collections.Specialized;
using System.Net;
using RestSharp;
using Newtonsoft.Json;

namespace YTech.ServiceTracker.JayaMesin.Web.Mvc.Controllers
{
    [HandleError]
    public class WOController : Controller
    {
        private readonly IMCustomerTasks _customerTasks;
        private readonly ITWOTasks _woTasks;
        private readonly ITReferenceTasks _refTasks;
        private readonly ITWOLogTasks _woLogTasks;
        private readonly ITWOStatusTasks _woStatusTasks;
        private readonly IMSmsTemplateTasks _smsTemplateTasks;
        private readonly ITSmsTasks _smsTasks;
        public WOController(IMCustomerTasks customerTasks, ITWOTasks woTasks, ITReferenceTasks refTasks, ITWOLogTasks woLogTasks, ITWOStatusTasks woStatusTasks, IMSmsTemplateTasks smsTemplateTasks, ITSmsTasks smsTasks)
        {
            this._customerTasks = customerTasks;
            this._woTasks = woTasks;
            this._refTasks = refTasks;
            this._woLogTasks = woLogTasks;
            this._woStatusTasks = woStatusTasks;
            this._smsTemplateTasks = smsTemplateTasks;
            this._smsTasks = smsTasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        public ActionResult Index()
        {
            PopulateCustomers();
            PopulateWOStatus();
            PopulateSCToko();
            PopulatePriority();
            PopulateWOType();
            CalculateExpiredWO();
            return View();
        }

        private void CalculateExpiredWO()
        {
            var orderedWOS = GetWOs(_woTasks.GetListExpiredWO());
            ViewData["countExpiredWO"] = orderedWOS.Count();
        }

        public ActionResult ReloadCustomers(string random)
        {
            PopulateCustomers();
            return View();
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, TEKNISI")]
        public ActionResult UpdateStatus()
        {
            PopulateCustomers();
            PopulateWOStatus();
            PopulateSCToko();
            PopulatePriority();
            PopulateWOType();
            CalculateExpiredWO();
            return View();
        }

        [AllowAnonymous]
        public ActionResult Tracking()
        {
            WOTrackingViewModel vm = new WOTrackingViewModel();
            IList<WOViewModel> wos = new List<WOViewModel>();
            vm.Search = string.Empty;
            vm.WOs = wos;
            vm.FirstLoad = true;
            return View(vm);
        }

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tracking(WOTrackingViewModel woTrackVM)
        {
            IList<WOViewModel> wos = new List<WOViewModel>();
            TWO wo = _woTasks.GetWOByWONo(woTrackVM.Search);
            string msg = string.Empty;
            if (wo != null)
            {
                WOViewModel woVM = new WOViewModel();
                woVM.WOID = wo.Id;
                woVM.CustomerName = wo.CustomerId.CustomerName;
                woVM.CustomerPhone = wo.CustomerId.CustomerPhone;
                woVM.CustomerAddress = wo.CustomerId.CustomerAddress;
                woVM.WODate = wo.WODate;
                woVM.WONo = wo.WONo;
                woVM.WOItemType = wo.WOItemType;
                woVM.WOItemSN = wo.WOItemSn;
                woVM.WOIsGuarantee = wo.WOIsGuarantee;
                woVM.WOEquipments = wo.WOEquipments;
                woVM.WOScStore = wo.WOScStore;
                woVM.WOPriority = wo.WOPriority;
                woVM.WOBrokenDesc = wo.WOBrokenDesc;
                woVM.WOLastStatus = wo.WOLastStatus;
                woVM.WOStartDate = wo.WOStartDate;
                woVM.WOTotal = wo.WOTotal;
                woVM.WODp = wo.WODp;
                woVM.WOTakenDate = wo.WOTakenDate;
                woVM.WOInvoiceNo = wo.WOInvoiceNo;
                woVM.WOComplain = wo.WOComplain;
                woVM.WORemarkStatus = wo.WORemarkStatus;
                woVM.WOReceivedBy = wo.WOReceivedBy;
                woVM.WORepairedBy = wo.WORepairedBy;
                woVM.WOType = wo.WOType;
                wos.Add(woVM);
            }
            else
            {
                msg = string.Format("Maaf, WO dengan nomor {0} tidak ditemukan.", woTrackVM.Search);
            }

            WOTrackingViewModel vm = new WOTrackingViewModel();
            vm.Search = woTrackVM.Search;
            vm.WOs = wos;
            vm.StatusMessage = msg;
            vm.FirstLoad = false;
            return View(vm);
        }

        [AllowAnonymous]
        public ActionResult WODetail(string WoId)
        {
            WOViewModel woVM = new WOViewModel();
            TWO wo = _woTasks.One(WoId);
            if (wo != null)
            {
                woVM.WOID = wo.Id;
                woVM.CustomerName = wo.CustomerId.CustomerName;
                woVM.CustomerPhone = wo.CustomerId.CustomerPhone;
                woVM.CustomerAddress = wo.CustomerId.CustomerAddress;
                woVM.WODate = wo.WODate;
                woVM.WONo = wo.WONo;
                woVM.WOItemType = wo.WOItemType;
                woVM.WOItemSN = wo.WOItemSn;
                woVM.WOIsGuarantee = wo.WOIsGuarantee;
                woVM.WOEquipments = wo.WOEquipments;
                woVM.WOScStore = wo.WOScStore;
                woVM.WOPriority = wo.WOPriority;
                woVM.WOBrokenDesc = wo.WOBrokenDesc;
                woVM.WOLastStatus = wo.WOLastStatus;
                woVM.WOStartDate = wo.WOStartDate;
                woVM.WOTotal = wo.WOTotal;
                woVM.WODp = wo.WODp;
                woVM.WOTakenDate = wo.WOTakenDate;
                woVM.WOInvoiceNo = wo.WOInvoiceNo;
                woVM.WOComplain = wo.WOComplain;
                woVM.WORemarkStatus = wo.WORemarkStatus;
                woVM.WOReceivedBy = wo.WOReceivedBy;
                woVM.WORepairedBy = wo.WORepairedBy;
                woVM.WOType = wo.WOType;
            }
            IEnumerable<TWOStatus> woStatus = _woStatusTasks.GetWOStatus(WoId);

            WODetailViewModel vm = new WODetailViewModel();
            vm.Wo = woVM;
            vm.WOStatus = (from wos in woStatus
                           select new WOStatusViewModel
                           {
                               WOStatusId = wos.Id,
                               WOStatusUser = wos.WOStatusUser,
                               WOStatus = wos.WOStatus,
                               WOStatusDate = wos.WOStatusDate,
                               WOStatusBrokenDesc = wos.WOStatusBrokenDesc,
                               WOStatusStartDate = wos.WOStatusStartDate,
                               WOStatusFinishDate = wos.WOStatusFinishDate,
                               WOStatusDesc = wos.WOStatusDesc
                           }).ToList<WOStatusViewModel>();
            return View(vm);
        }

        private void PopulateWOStatus()
        {
            var wostatus = from EnumWOStatus e in Enum.GetValues(typeof(EnumWOStatus))
                           select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["wo_status"] = wostatus;
        }

        private void PopulateSCToko()
        {
            var wostatus = from EnumSCToko e in Enum.GetValues(typeof(EnumSCToko))
                           select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["sc_toko"] = wostatus;
        }

        private void PopulatePriority()
        {
            var wostatus = from EnumPriority e in Enum.GetValues(typeof(EnumPriority))
                           select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["priority"] = wostatus;

        }

        public JsonResult PopulateCustomers()
        {
            IEnumerable<CustomerViewModel> customers = from cust in _customerTasks.GetListNotDeleted()
                                                       select new CustomerViewModel
                                                       {
                                                           CustomerID = cust.Id,
                                                           CustomerName = cust.CustomerName + " - " + cust.CustomerPhone
                                                       };
            ViewData["customers"] = customers;
            return Json(customers, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 20170511 : add new wo type and populate to viewdata
        /// </summary>
        private void PopulateWOType()
        {
            var wo_type = from EnumWOType e in Enum.GetValues(typeof(EnumWOType))
                          select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["wo_type"] = wo_type;

        }

        public ActionResult PrintWOFactur(string random, string woId)
        {
            string msg = string.Empty;
            bool success = false;
            bool allowPrint = true;
            try
            {
                //get wo by wo id
                TWO wo = this._woTasks.One(woId);
                //check if user have print WO, if not, allow print for role CS
                if (User.IsInRole("CS"))
                {
                    allowPrint = !_woLogTasks.HaveBeenPrint(wo, User.Identity.Name);
                }

                if (allowPrint)
                {
                    ReportParameterCollection paramCol = null;
                    ReportDataSource[] repCol = new ReportDataSource[1];
                    //get data source
                    repCol[0] = GetWOById(wo);
                    //save log
                    SaveLog(wo, EnumWOLog.Print);

                    Session["ReportData"] = repCol;
                    Session["ReportParams"] = paramCol;

                    success = true;
                    msg = "Print WO success";
                }
                else
                {
                    success = false;
                    msg = "Anda sudah pernah mencetak WO";
                }
            }
            catch (Exception ex)
            {
                success = false;
                msg = ex.GetBaseException().Message;
            }
            var e = new
            {
                Success = success,
                Message = msg,
                UrlReport = string.Format("{0}", EnumReports.RptPrintWOFactur.ToString())
            };
            return Json(e, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogWO_Open(string random, string woId)
        {
            string msg = string.Empty;
            bool success = false;
            try
            {
                //get wo by wo id
                TWO wo = this._woTasks.One(woId);
                //save log
                SaveLog(wo, EnumWOLog.Read);

                success = true;
                msg = "Log WO success";
            }
            catch (Exception ex)
            {
                success = false;
                msg = ex.GetBaseException().Message;
            }
            var e = new
            {
                Success = success,
                Message = msg
            };
            return Json(e, JsonRequestBehavior.AllowGet);
        }

        private ReportDataSource GetWOById(TWO wo)
        {
            WOViewModel vm = new WOViewModel
                     {
                         WOID = wo.Id,
                         CustomerName = wo.CustomerId.CustomerName,
                         CustomerPhone = wo.CustomerId.CustomerPhone,
                         CustomerAddress = wo.CustomerId.CustomerAddress,
                         WODate = wo.WODate,
                         WONo = wo.WONo,
                         WOItemType = wo.WOItemType,
                         WOItemSN = wo.WOItemSn,
                         WOIsGuarantee = wo.WOIsGuarantee,
                         WOEquipments = wo.WOEquipments,
                         WOScStore = wo.WOScStore,
                         WOPriority = wo.WOPriority,
                         WOBrokenDesc = wo.WOBrokenDesc,
                         WOLastStatus = wo.WOLastStatus,
                         WOStartDate = wo.WOStartDate,
                         WOTotal = wo.WOTotal,
                         WODp = wo.WODp,
                         WOTakenDate = wo.WOTakenDate,
                         WOInvoiceNo = wo.WOInvoiceNo,
                         WOComplain = wo.WOComplain,
                         WORemarkStatus = wo.WORemarkStatus,
                         WOReceivedBy = wo.WOReceivedBy,
                         WORepairedBy = wo.WORepairedBy,
                         WOType = wo.WOType
                     };

            IList<WOViewModel> listWO = new List<WOViewModel>();
            listWO.Add(vm);
            ReportDataSource reportDataSource = new ReportDataSource("WOViewModel", listWO);

            return reportDataSource;
        }

        private void SaveLog(TWO wo, EnumWOLog enumWOLog)
        {
            TWOLog woLog = new TWOLog();
            woLog.SetAssignedIdTo(Guid.NewGuid().ToString());
            woLog.WOId = wo;
            woLog.LogUser = User.Identity.Name;
            woLog.LogDate = DateTime.Now;
            woLog.LogStatus = enumWOLog.ToString();

            woLog.CreatedDate = DateTime.Now;
            woLog.CreatedBy = User.Identity.Name;
            woLog.DataStatus = "New";
            _woLogTasks.Insert(woLog);
        }

        private string GetNewWONo()
        {
            TReference refer = _refTasks.GetByReferenceType(EnumReferenceType.WONo);
            bool automatedIncrease = true;
            decimal no = Convert.ToDecimal(refer.ReferenceValue) + 1;

            //reset wo no to 1 every month
            if (DateTime.Today.Day == 1 && refer.ModifiedDate < DateTime.Today)
                no = 1;

            refer.ReferenceValue = no.ToString();
            if (automatedIncrease)
            {
                refer.ModifiedDate = DateTime.Now;
                refer.ModifiedBy = User.Identity.Name;
                refer.DataStatus = "Updated";
                _refTasks.Update(refer);
            }

            StringBuilder result = new StringBuilder();
            result.Append("WO[YEAR][MONTH][XXX]");
            result.Replace("[XXX]", GetNo(3, no));
            result.Replace("[DAY]", DateTime.Today.Day.ToString());
            result.Replace("[MONTH]", DateTime.Today.ToString("MM").ToUpper());
            result.Replace("[YEAR]", DateTime.Today.ToString("yy"));
            return result.ToString();
        }

        private static string GetNo(int maxLength, decimal no)
        {
            int len = maxLength - no.ToString().Length;
            string factur = no.ToString();
            for (int i = 0; i < len; i++)
            {
                factur = "0" + factur;
            }
            return factur;
        }

        public ActionResult WO_Read([DataSourceRequest] DataSourceRequest request, string WOStatus, string TextSearch)
        {
            var orderedWOS = from wo in GetWOs(this._woTasks.GetListNotDeleted(User.Identity.Name, WOStatus, TextSearch))
                             orderby wo.HaveBeenRead, wo.WOPriority descending, wo.WONo descending
                             select wo;
            DataSourceResult result = orderedWOS.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WOStatus_Read(string woID, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetWOStatus(woID).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        public ActionResult WO_Create([DataSourceRequest] DataSourceRequest request, WOViewModel WOVM, FormCollection formCol)
        {
            var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();

            if (WOVM != null && ModelState.IsValid)
            {
                TWO wo = new TWO();
                wo.SetAssignedIdTo(Guid.NewGuid().ToString());

                wo.WONo = GetNewWONo();
                //get customer id from form collection, customerVM not catch the customer id :(
                ConvertToWO(WOVM, wo, formCol["HiddenCustomerId"]);

                wo.CreatedDate = DateTime.Now;
                wo.CreatedBy = User.Identity.Name;
                wo.DataStatus = "New";

                _woTasks.Insert(wo);
            }

            return Json(new[] { WOVM }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToWO(WOViewModel WOVM, TWO wo, string custId)
        {
            //wo.WONo = WOVM.WONo;
            wo.CustomerId = GetCustomer(custId);
            wo.WODate = WOVM.WODate;
            wo.WOItemType = WOVM.WOItemType;
            wo.WOItemSn = WOVM.WOItemSN;
            wo.WOIsGuarantee = WOVM.WOIsGuarantee;
            wo.WOEquipments = WOVM.WOEquipments;
            wo.WOScStore = WOVM.WOScStore;
            wo.WOPriority = WOVM.WOPriority;
            wo.WOStartDate = WOVM.WOStartDate;
            wo.WOLastStatus = WOVM.WOLastStatus;
            wo.WOEstFinishDate = WOVM.WOEstFinishDate;
            wo.WOTotal = WOVM.WOTotal;
            wo.WODp = WOVM.WODp;
            wo.WOInvoiceNo = WOVM.WOInvoiceNo;
            wo.WOTakenDate = WOVM.WOTakenDate;
            wo.WOBrokenDesc = WOVM.WOBrokenDesc;
            wo.WODesc = WOVM.WODesc;
            wo.WOComplain = WOVM.WOComplain;
            wo.WORemarkStatus = WOVM.WORemarkStatus;
            wo.WOReceivedBy = WOVM.WOReceivedBy;
            wo.WORepairedBy = WOVM.WORepairedBy;
            wo.WOType = WOVM.WOType;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        public ActionResult WO_Update([DataSourceRequest] DataSourceRequest request, WOViewModel WOVM, FormCollection formCol)
        {
            if (WOVM != null && ModelState.IsValid)
            {
                var wo = _woTasks.One(WOVM.WOID);
                if (wo != null)
                {
                    //get customer id from form collection, customerVM not catch the customer id :(
                    ConvertToWO(WOVM, wo, formCol["HiddenCustomerId"]);

                    wo.ModifiedDate = DateTime.Now;
                    wo.ModifiedBy = User.Identity.Name;
                    wo.DataStatus = "Updated";

                    _woTasks.Update(wo);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, TEKNISI")]
        public ActionResult WO_UpdateStatus([DataSourceRequest] DataSourceRequest request, WOViewModel WOVM)
        {
            if (WOVM != null && ModelState.IsValid)
            {
                var wo = _woTasks.One(WOVM.WOID);
                if (wo != null)
                {
                    wo.WONo = WOVM.WONo;
                    //wo.CustomerId = GetCustomer(WOVM.Customer.CustomerID);
                    //wo.WODate = WOVM.WODate;
                    //wo.WOItemType = WOVM.WOItemType;
                    //wo.WOItemSn = WOVM.WOItemSN;
                    //wo.WOIsGuarantee = WOVM.WOIsGuarantee;
                    //wo.WOEquipments = WOVM.WOEquipments;
                    //wo.WOScStore = WOVM.WOScStore;
                    //wo.WOPriority = WOVM.WOPriority;
                    wo.WOStartDate = WOVM.WOStartDate;
                    wo.WOLastStatus = WOVM.WOLastStatus;
                    wo.WOEstFinishDate = WOVM.WOEstFinishDate;
                    //wo.WOTotal = WOVM.WOTotal;
                    //wo.WODp = WOVM.WODp;
                    //wo.WOInvoiceNo = WOVM.WOInvoiceNo;
                    //wo.WOTakenDate = WOVM.WOTakenDate;
                    wo.WOBrokenDesc = WOVM.WOBrokenDesc;
                    wo.WORemarkStatus = WOVM.WORemarkStatus;
                    //wo.WODesc = WOVM.WODesc;

                    wo.ModifiedDate = DateTime.Now;
                    wo.ModifiedBy = User.Identity.Name;
                    wo.DataStatus = "Updated";

                    _woTasks.Update(wo);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR")]
        public ActionResult WO_Destroy([DataSourceRequest] DataSourceRequest request, WOViewModel WOVM)
        {
            if (WOVM != null && ModelState.IsValid)
            {
                var wo = _woTasks.One(WOVM.WOID);
                if (wo != null)
                {
                    wo.ModifiedDate = DateTime.Now;
                    wo.ModifiedBy = User.Identity.Name;
                    wo.DataStatus = "Deleted";

                    _woTasks.Update(wo);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult ServerDate(string random)
        {
            return Content(DateTime.Now.ToString());
        }

        private MCustomer GetCustomer(string custId)
        {
            return _customerTasks.One(custId);
        }

        private IEnumerable<WOViewModel> GetWOs(IEnumerable<TWOHaveRead> wos)
        {
            return from wo in wos
                   select new WOViewModel
                   {
                       WOID = wo.Id,
                       Customer = ConvertToCustomerVM(wo.CustomerId, wo.CustomerName),
                       CustomerName = wo.CustomerName + " - " + wo.CustomerPhone,
                       CustomerPhone = wo.CustomerPhone,
                       CustomerAddress = wo.CustomerAddress,
                       HiddenCustomerId = wo.CustomerId,
                       WODate = wo.WODate,
                       WONo = wo.WONo,
                       WOItemType = wo.WOItemType,
                       WOItemSN = wo.WOItemSn,
                       WOIsGuarantee = wo.WOIsGuarantee,
                       WOEquipments = wo.WOEquipments,
                       WOScStore = wo.WOScStore,
                       WOPriority = wo.WOPriority,
                       WOBrokenDesc = wo.WOBrokenDesc,
                       WOLastStatus = wo.WOLastStatus,
                       WOStartDate = wo.WOStartDate,
                       WOEstFinishDate = wo.WOEstFinishDate,
                       WOTotal = wo.WOTotal,
                       WODp = wo.WODp,
                       WOTakenDate = wo.WOTakenDate,
                       WOInvoiceNo = wo.WOInvoiceNo,
                       WOComplain = wo.WOComplain,
                       HaveBeenRead = wo.HaveBeenRead,
                       WORemarkStatus = wo.WORemarkStatus,
                       WOReceivedBy = wo.WOReceivedBy,
                       WORepairedBy = wo.WORepairedBy,
                       WOType = wo.WOType
                       //use stored procedure, looping to get read flag make wo list slow response
                       //HaveBeenRead = _woLogTasks.HaveBeenRead(wo, User.Identity.Name)
                   };
        }

        private IEnumerable<WOStatusViewModel> GetWOStatus(string woId)
        {
            var wos = this._woStatusTasks.GetWOStatus(woId);

            return from wo in wos
                   select new WOStatusViewModel
                   {
                       WOStatusId = wo.Id,
                       WOStatusUser = wo.WOStatusUser,
                       WOStatus = wo.WOStatus,
                       WOStatusDate = wo.WOStatusDate,
                       WOStatusBrokenDesc = wo.WOStatusBrokenDesc,
                       WOStatusStartDate = wo.WOStatusStartDate,
                       WOStatusFinishDate = wo.WOStatusFinishDate,
                       WOStatusDesc = wo.WOStatusDesc
                   };
        }

        private CustomerViewModel ConvertToCustomerVM(string customerId, string customerName)
        {
            CustomerViewModel custVM = new CustomerViewModel();
            if (string.IsNullOrEmpty(customerId))
            {
                custVM.CustomerID = string.Empty;
                custVM.CustomerName = "Select a value";
            }
            else
            {
                custVM.CustomerID = customerId;
                custVM.CustomerName = customerName;
            }
            return custVM;
        }

        /// <summary>
        /// 20170511 : add new feature, get expired wo list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 
        public ActionResult ExpiredWO_List([DataSourceRequest] DataSourceRequest request)
        {
            var orderedWOS = from wo in GetWOs(_woTasks.GetListExpiredWO())
                             orderby wo.WODate descending, wo.WONo
                             select wo;
            DataSourceResult result = orderedWOS.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult ExpiredWO()
        {
            return View();
        }

        public ActionResult SmsCustomer(string random, string woId)
        {
            bool success = true;

            //get wo by wo id
            TWO wo = this._woTasks.One(woId);
            MSmsTemplate smsTemplate = _smsTemplateTasks.GetByWOStatus(wo.WOLastStatus);

            StringBuilder msg = new StringBuilder();
            msg.Append(smsTemplate.SmsTemplateText);
            msg.Replace("[CustomerPhone]", wo.CustomerId.CustomerPhone);
            msg.Replace("[CustomerName]", wo.CustomerId.CustomerName);
            msg.Replace("[WONo]", wo.WONo);
            msg.Replace("[WODate]", wo.WODate.Value.ToString("dd-MMM-yyyy"));
            msg.Replace("[SMSSentDate]", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());


            string arg = string.Format(@" --sendsms TEXT {0} -text ""{1}  "" ", wo.CustomerId.CustomerPhone, msg.ToString());
            string fn = @"C:\Program Files\Gammu 1.38.2\bin\gammu.exe";
            var psi = new ProcessStartInfo(fn, arg)
            {
                WorkingDirectory = "C:\\Program Files\\Gammu 1.38.2\\bin",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            StringBuilder debug = new StringBuilder();
            using (var process = new Process { StartInfo = psi })
            {
                // delegate for writing the process output to the response output
                Action<Object, DataReceivedEventArgs> dataReceived = ((sender, e) =>
                {
                    if (e.Data != null) // sometimes a random event is received with null data, not sure why - I prefer to leave it out
                    {
                        debug.AppendLine(e.Data);
                    }
                });

                process.OutputDataReceived += new DataReceivedEventHandler(dataReceived);
                process.ErrorDataReceived += new DataReceivedEventHandler(dataReceived);


                // start the process and start reading the standard and error outputs
                process.Start();
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();

                // wait for the process to exit
                process.WaitForExit();
            }

            if (debug.ToString().Contains("Error"))
            {
                success = false;
            }
            else
            {
                success = true;
            }

            ///save sms
            ///
            TSms sms = new TSms();
            sms.SetAssignedIdTo(Guid.NewGuid().ToString());
            sms.SmsRecipient = wo.CustomerId.CustomerPhone;
            sms.SmsText = msg.ToString();
            sms.SmsReport = debug.ToString();
            sms.SmsDate = DateTime.Now;
            sms.SmsStatus = success.ToString();
            sms.CreatedBy = User.Identity.Name;
            sms.DataStatus = "New";
            sms.CreatedDate = DateTime.Now;
            sms.WOId = wo;
            _smsTasks.Insert(sms);

            //save logx
            SaveLog(wo, EnumWOLog.SMS);
            var result = new
            {
                Success = success.ToString(),
                Message = debug.ToString()
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SmsCustomerApi(string random, string woId)
        {

            bool success = true;

            //get wo by wo id
            TWO wo = this._woTasks.One(woId);
            MSmsTemplate smsTemplate = _smsTemplateTasks.GetByWOStatus(wo.WOLastStatus);

            StringBuilder msg = new StringBuilder();
            msg.Append(smsTemplate.SmsTemplateText);
            msg.Replace("[CustomerPhone]", wo.CustomerId.CustomerPhone);
            msg.Replace("[CustomerName]", wo.CustomerId.CustomerName);
            msg.Replace("[WONo]", wo.WONo);
            msg.Replace("[WODate]", wo.WODate.Value.ToString("dd-MMM-yyyy"));
            msg.Replace("[SMSSentDate]", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());

            StringBuilder debug = new StringBuilder();
            string smsUrl = string.Format("http://www.freesms4us.com/kirimsms.php?user=ryuki2009&pass=ciasen2009&no={0}&isi={1}&return=json&ket=yes", wo.CustomerId.CustomerPhone, msg.ToString());

            try
            {
                Stream response = Http.Get(smsUrl);
                StreamReader sr = new StreamReader(response);
                debug.Append(sr.ReadToEnd());
                response.Close();

                success = true;
            }
            catch (Exception ex)
            {
                debug.Append(ex.GetBaseException().ToString());
                success = false;
            }

            ///save sms
            ///
            TSms sms = new TSms();
            sms.SetAssignedIdTo(Guid.NewGuid().ToString());
            sms.SmsRecipient = wo.CustomerId.CustomerPhone;
            sms.SmsText = msg.ToString();
            sms.SmsReport = debug.ToString();
            sms.SmsDate = DateTime.Now;
            sms.SmsStatus = success.ToString();
            sms.CreatedBy = User.Identity.Name;
            sms.DataStatus = "New";
            sms.CreatedDate = DateTime.Now;
            sms.WOId = wo;
            _smsTasks.Insert(sms);

            //save logx
            SaveLog(wo, EnumWOLog.SMS);
            var result = new
            {
                Success = success.ToString(),
                Message = debug.ToString()
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SmsCustomerApiWithSmsViro(string random, string woId)
        {
            bool success = true;


            //get wo by wo id
            TWO wo = this._woTasks.One(woId);
            MSmsTemplate smsTemplate = _smsTemplateTasks.GetByWOStatus(wo.WOLastStatus);
            string phone = wo.CustomerId.CustomerPhone;
            phone = phone.Replace(" ", "");
            phone = phone.Replace("-", "");
            phone = phone.Replace("+", "");
            if (phone.StartsWith("0"))
            {
                phone = "62" + phone.Substring(1);
            }

            StringBuilder msg = new StringBuilder();
            msg.Append(smsTemplate.SmsTemplateText);
            msg.Replace("[CustomerPhone]", phone);
            msg.Replace("[CustomerName]", wo.CustomerId.CustomerName);
            msg.Replace("[WONo]", wo.WONo);
            msg.Replace("[WODate]", wo.WODate.Value.ToString("dd-MMM-yyyy"));
            msg.Replace("[SMSSentDate]", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());

            StringBuilder debug = new StringBuilder();
            //string smsUrl = string.Format("http://www.freesms4us.com/kirimsms.php?user=ryuki2009&pass=ciasen2009&no={0}&isi={1}&return=json&ket=yes", wo.CustomerId.CustomerPhone, msg.ToString());

            string isiSms = "testing isis sms" + DateTime.Now.ToString();

            string smsJson = "{\"from\":\"JAYA MESIN\",\"to\":\"[phone]\",\"text\":\"[isiSms]\"}";
            smsJson = smsJson.Replace("[phone]", phone);
            smsJson = smsJson.Replace("[isiSms]", msg.ToString().Replace(System.Environment.NewLine, " ").Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "));
            debug.Append(smsJson);

            try
            {
                var client = new
RestClient("http://107.20.199.106/restapi/sms/1/text/single");
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Basic amF5YW1lc2luOkpheWFtZXNpbjIwMTg=");
                //request.AddParameter("application/json", string.Format("{\"from\":\"InfoSMS\",\"to\":\"{0}\",\"text\":\"{1}\"}", wo.CustomerId.CustomerPhone, msg.ToString()), ParameterType.RequestBody);
                request.AddParameter("application/json", smsJson, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                debug.Append(response.Content);

                //string messageId = JsonConvert
                success = true;
            }
            catch (Exception ex)
            {
                debug.Append(ex.GetBaseException().ToString());
                success = false;
            }

            ///save sms
            ///
            TSms sms = new TSms();
            sms.SetAssignedIdTo(Guid.NewGuid().ToString());
            sms.SmsRecipient = phone;
            sms.SmsText = msg.ToString();
            sms.SmsReport = debug.ToString();
            sms.SmsDate = DateTime.Now;
            sms.SmsStatus = success.ToString();
            sms.CreatedBy = User.Identity.Name;
            sms.DataStatus = "New";
            sms.CreatedDate = DateTime.Now;
            sms.WOId = wo;
            _smsTasks.Insert(sms);

            //save logx
            SaveLog(wo, EnumWOLog.SMS);
            var result = new
            {
                Success = success.ToString(),
                Message = debug.ToString()
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public static class Http
        {
            public static Stream Get(string uri)
            {
                Stream response = null;
                using (WebClient client = new WebClient())
                {
                    response = client.OpenRead(uri);
                }
                return response;
            }
        }

        public ActionResult ListSMS_Read(string woID, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetSms(woID).ToDataSourceResult(request));
        }

        private IEnumerable<SmsViewModel> GetSms(string woID)
        {
            if (string.IsNullOrEmpty(woID))
            {
                return null;
            }
            var wos = this._smsTasks.GetSmsByWoId(woID);

            return from sms in wos
                   select new SmsViewModel
                   {
                       SmsId = sms.Id,
                       SmsRecipient = sms.SmsRecipient,
                       SmsText = sms.SmsText,
                       SmsStatus = sms.SmsStatus,
                       SmsDate = sms.SmsDate,
                       SmsReport = sms.SmsReport
                   };
        }
    }
}
