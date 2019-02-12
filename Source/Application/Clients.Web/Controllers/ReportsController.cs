using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;
using Telerik.Reporting.Services.Engine;
using Telerik.Reporting.Services.WebApi;

namespace DS.Motel.Clients.Web.Controllers
{
    public class ReportsController : ReportsControllerBase
    {
        static ReportServiceConfiguration preservedConfiguration;

        static IReportServiceConfiguration PreservedConfiguration
        {
            get
            {
                if (null == preservedConfiguration)
                {
                    preservedConfiguration = new ReportServiceConfiguration
                    {
                        HostAppId = "MvcDemoApp",
                        Storage = new FileStorage(),
                        ReportResolver = CreateResolver(),
                        // ReportSharingTimeout = 0,
                        // ClientSessionTimeout = 15,
                    };
                }
                return preservedConfiguration;
            }
        }

        public ReportsController()
        {
            this.ReportServiceConfiguration = PreservedConfiguration;
        }

        static IReportResolver CreateResolver()
        {
            var reportsPath = HttpContext.Current.Server.MapPath("~/Reports");

            return new ReportFileResolver(reportsPath)
                .AddFallbackResolver(new ReportTypeResolver());
        }
    }
    //public class ReportsController : ReportsControllerBase
    //{
    //    static Telerik.Reporting.Services.ReportServiceConfiguration configurationInstance = new Telerik.Reporting.Services.ReportServiceConfiguration {
    //        HostAppId = "Application1",
    //        ReportResolver = new ReportFileResolver(HttpContext.Current.Server.MapPath("~/Reports")).AddFallbackResolver(new ReportTypeResolver()),
    //        Storage = new Telerik.Reporting.Cache.File.FileStorage(),
    //    };

    //    public ReportsController()
    //    {
    //        this.ReportServiceConfiguration = configurationInstance;
    //    }

    //    //#region SendMailMessage_Implementation

    //    //protected override HttpStatusCode SendMailMessage(MailMessage mailMessage)
    //    //{
    //    //    using (var smtpClient = new SmtpClient("smtp.companyname.com", 25))
    //    //    {
    //    //        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
    //    //        smtpClient.EnableSsl = true;
    //    //        smtpClient.Send(mailMessage);
    //    //    }
    //    //    return HttpStatusCode.OK;
    //    //}

    //    //#endregion
    //}
}