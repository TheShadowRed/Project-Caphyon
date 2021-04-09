using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project_Caphyon
{
    class TaskOne
    {
        List<Package> DependencyList = new List<Package>();
        public void TaskOneMain()
        {
            ReadInput();
            ProcessData();
        }
        void ReadInput()
        {
            //read file line by line
            var lines = File.ReadLines("deps.in");
            //clear list content
            DependencyList.Clear();
            //noduri cu dependente
            foreach (var line in lines)
            {
                //populate workingset with data
                string[] separatedFiles = line.Split(' ');
                DependencyList.Add(new Package(separatedFiles[0], separatedFiles[1]));
            }
            //noduri fara dependente
            foreach (var line in lines)
            {
                //populate workingset with data
                string[] separatedFiles = line.Split(' ');
                List<string> properties = DependencyList.Select(o => o.PackageName).ToList();
                if(!properties.Contains(separatedFiles[1]))
                DependencyList.Add(new Package(separatedFiles[1], ""));
            }
        }
        void ProcessData() 
        {
            string path = @"task1.out";

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    //merge prost la dataseturi mari
                    var distinctItems = DependencyList.GroupBy(x => x.PackageName).Select(y => y.First());
                    List<Package> SortedList = distinctItems.OrderBy(o => o.PackageName).ToList();
                    foreach (var pack in SortedList)
                    {
                        
                        tw.WriteLine(pack.PackageName);
                    }
                }

            }
            else if (File.Exists(path))
            {
                File.Delete(path);
                File.Create(path).Dispose();
                using (TextWriter tw = new StreamWriter(path))
                {
                    //merge prost la dataseturi mari
                    var distinctItems = DependencyList.GroupBy(x => x.PackageName).Select(y => y.First());
                    List<Package> SortedList = distinctItems.OrderBy(o => o.PackageName).ToList();
                    foreach (var pack in SortedList)
                    {

                        tw.WriteLine(pack.PackageName);
                    }
                }
            }
        }
    }
}
