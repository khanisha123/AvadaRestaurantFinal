﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvadaRestaurantFinal.Utilities.Helper
{
    public class EmailRequest
    {
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string SecretKey { get; set; }
    }
}
