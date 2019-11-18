using GRINTSYS.SAPRestApi.Domain.Services;
using GRINTSYS.SAPRestApi.Models;
using GRINTSYS.SAPRestApi.Persistence.Repositories;
using System.Web.Http;
using Unity;

namespace GRINTSYS.SAPRestApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Configuración y servicios de API web
            var container = new UnityContainer();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IPaymentRepository, PaymentRepository>();
            container.RegisterType<IPaymentService, PaymentService>();
            container.RegisterType<IPurchaseOrderRepository, PurchaseOrderRepository>();
            container.RegisterType<IPurchaseOrderService, PurchaseOrderService>();
            container.RegisterType<IInvoiceRepository, InvoiceRepository>();
            container.RegisterType<IInvoiceService, InvoiceService>();
            container.RegisterType<ISapDocumentService, SapOrder>();
            container.RegisterType<ISapDocumentService, SapPayment>();
            container.RegisterType<ISapDocumentService, SapPurchaseOrder>();
            container.RegisterType<IClientRepository, ClientRepository>();
            container.RegisterType<IClientService, ClientService>();


            config.DependencyResolver = new UnityResolver(container);

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
