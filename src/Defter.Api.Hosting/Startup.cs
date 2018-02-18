using Defter.Api.Hosting.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Swagger;

namespace Defter.Api.Hosting
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Defter API", Version = "v1" });
            });

            services.Configure<MvcOptions>(options =>
            {
                var xmlSerializerInputFormatter = new XmlSerializerInputFormatter();
                xmlSerializerInputFormatter.SupportedMediaTypes.Clear();
                xmlSerializerInputFormatter.SupportedMediaTypes.Add(
                    new MediaTypeHeaderValue("application/xml-xmlser"));
                xmlSerializerInputFormatter.SupportedMediaTypes.Add(
                    new MediaTypeHeaderValue("text/xml-xmlser"));

                var xmlSerializerOutputFormatter = new XmlSerializerOutputFormatter();
                xmlSerializerOutputFormatter.SupportedMediaTypes.Clear();
                xmlSerializerOutputFormatter.SupportedMediaTypes.Add(
                    new MediaTypeHeaderValue("application/xml-xmlser"));
                xmlSerializerOutputFormatter.SupportedMediaTypes.Add(
                    new MediaTypeHeaderValue("text/xml-xmlser"));

                var dcsInputFormatter = new XmlDataContractSerializerInputFormatter();
                dcsInputFormatter.SupportedMediaTypes.Clear();
                dcsInputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml-dcs"));
                dcsInputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml-dcs"));

                var dcsOutputFormatter = new XmlDataContractSerializerOutputFormatter();
                dcsOutputFormatter.SupportedMediaTypes.Clear();
                dcsOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml-dcs"));
                dcsOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml-dcs"));

                options.InputFormatters.Add(dcsInputFormatter);
                options.InputFormatters.Add(xmlSerializerInputFormatter);
                options.OutputFormatters.Add(dcsOutputFormatter);
                options.OutputFormatters.Add(xmlSerializerOutputFormatter);

                //xmlSerializerInputFormatter.WrapperProviderFactories.Add(new PersonWrapperProviderFactory());
                //xmlSerializerOutputFormatter.WrapperProviderFactories.Add(new PersonWrapperProviderFactory());
                //dcsInputFormatter.WrapperProviderFactories.Add(new PersonWrapperProviderFactory());
                //dcsOutputFormatter.WrapperProviderFactories.Add(new PersonWrapperProviderFactory());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Defter API v1");
            });

            app.UseMiddleware<DefterMiddleware>();

            app.UseMvc();
        }
    }
}
