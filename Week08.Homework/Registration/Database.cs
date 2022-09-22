using System;
using System.Collections.Generic;

namespace Week08.Homework.Registration
{
    public class Database
    {
        public static List<User> users { get; set; }
        static Database()
        {
            users = new List<User>();
        }
    }
}

