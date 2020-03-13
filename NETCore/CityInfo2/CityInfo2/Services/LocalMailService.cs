using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.Services
{
    public class LocalMailService : IMailService  //classe fake che rappresenta un servizio mail(giusto per capire come funzionano i servizi custom)
    {
        private string _mailTo = "fake1@mail.com";
        private string _mailFrom = Startup.Configuration["mailSettings:mailToAddress"];   //valore popolato dal file di config

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
