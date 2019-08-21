using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Unity;
using GRINTSYS.SAPRestApi.Persistence.Repositories;
using GRINTSYS.SAPRestApi.Domain.Services;
using GRINTSYS.SAPRestApi.Models;

[assembly: OwinStartup(typeof(GRINTSYS.SAPRestApi.Startup))]

namespace GRINTSYS.SAPRestApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("HangFireConextion");

            var container = new UnityContainer();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IPaymentRepository, PaymentRepository>();
            container.RegisterType<IPaymentService, PaymentService>();
            container.RegisterType<IInvoiceRepository, InvoiceRepository>();
            container.RegisterType<IInvoiceService, InvoiceService>();
            container.RegisterType<ISapDocumentService, SapOrder>();
            container.RegisterType<ISapDocumentService, SapPayment>();
            container.RegisterType<IClientRepository, ClientRepository>();
            container.RegisterType<IClientService, ClientService>();

            //app.DependencyResolver = new UnityResolver(container);
            GlobalConfiguration.Configuration.UseActivator(new UnityJobActivator(container));

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
