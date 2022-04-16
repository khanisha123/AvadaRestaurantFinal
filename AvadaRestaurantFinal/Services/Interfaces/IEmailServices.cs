using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Services.Interfaces
{
    public interface IEmailServices
    {
        Task SendEmailAsync(string emailTo, string userName, string html, string content);
    }
}
