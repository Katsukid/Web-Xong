﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMVC.Common
{
    public static class CommonConstant
    {
        public static string USER_SESSION = "USER_SESSION";
        public static string SESSION_CREDENTIALS = "SESSION_CREDENTIALS";
        public static string CartSession = "CartSession";
        public static string CurrentCulture { set; get; }
    }
}