using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CityInfo2
{
    public class Startup
    {

        #region aggiunta file di config
        public static IConfiguration Configuration; //var statica che useremo in giro per l app per leggere il file di config

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));

#if DEBUG   //aggiungo come servizio il mio servizio mail custom, inoltre lo faccio tramite direttive di preprocessore, 
            //in modo ca cambiare l'implementaz del servizio in base al ambiente in cui mi trovo

            services.AddTransient<IMailService, LocalMailService>();  
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole();   //per loggare su console
            loggerFactory.AddDebug(); //posso aggiungere come param il livello di criticità min per loggare l'azione (es:LogLevel.Information)

            //loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());  //modo generico per aggiungere un qualunque provider
            loggerFactory.AddNLog();  //shortcut per aggiungere NLog come provider per il servizio di logging 


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseMvc();

            app.UseStatusCodePages();

            //app.Run((context) => { throw new Exception("example exception"); });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
