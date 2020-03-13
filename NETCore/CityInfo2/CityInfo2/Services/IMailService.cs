using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.Services
{
    public interface IMailService  //includeremo in Startup un servizio con questa interfaccia, in modo poi da avere diverse implementazioni ma una sola inclusione
    {
        void Send(string subject, string message);
    }
}
