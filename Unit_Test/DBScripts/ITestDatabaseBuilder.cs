using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.DBScripts
{
    internal interface ITestDatabaseBuilder : IDisposable
    {
        public void ClearDB();
        public void CreateDB();
        public void PopulateDB();
    }
}
