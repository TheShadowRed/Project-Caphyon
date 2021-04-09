using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Caphyon
{
    public class Package
    {
        public string PackageName { get; set; }
        public string PackageDependency { get; set; }
        public Package(string name, string depen)
        {
            PackageName = name;
            PackageDependency = depen;
        }
    }
}
