using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web_Service.DataBase;

namespace Web_Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {       
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // ��������� ����������� � ���� ������
            DBClient.DB = new DBWorkerMySql(ReaderConfig.ConnectionStringDB);
            // ��������� �������
            Logger.InitLogger();
            Logger.Log.Info("������ �������!");
        }

        private void WebApiApplication_Disposed(object sender, EventArgs e)
        {
            Logger.Log.Info("������ ��������� ������");
        }
    }
}
