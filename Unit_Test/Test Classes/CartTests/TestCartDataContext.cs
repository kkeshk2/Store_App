﻿using Store_App.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Test_Classes.CartTests
{
    public class TestCartDataContext : IDataContext
    {
        public ISqlHelper GetConnection(string command)
        {
            return new  TestCartSqlHelper(command);
        }
    }
}
