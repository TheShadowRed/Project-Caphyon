using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project_Caphyon
{
    class TaskOne
    {
        List<Package> DependencyList = new List<Package>();
        List<Package> WorkingDependencyList = new List<Package>();
        public void TaskOneMain()
        {
            ReadInput();
            ProcessData();
        }
        public void TaskTwo() 
        {
            ReadInput();
            ProcessDataTask2();
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
                    //o versiune mai rapid este posibila folosind hashsets
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
                    //o versiune mai rapid este posibila folosind hashsets
                    var distinctItems = DependencyList.GroupBy(x => x.PackageName).Select(y => y.First());
                    List<Package> SortedList = distinctItems.OrderBy(o => o.PackageName).ToList();
                    foreach (var pack in SortedList)
                    {

                        tw.WriteLine(pack.PackageName);
                    }
                }
            }
        }
        void recusiveDependency(String MainPackage) 
        {
            var distinctItems = DependencyList.Where(a => a.PackageName == MainPackage).ToList();
            foreach (var dependent in distinctItems)
            {
                if (dependent.PackageDependency != "")
                {
                    recusiveDependency(dependent.PackageDependency);
                    WorkingDependencyList.Add(dependent);
                }
                else
                {
                    WorkingDependencyList.Add(dependent);
                }
            }
        }
        void ProcessDataTask2() 
        {
            string path = @"task2.out";

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    //merge prost la dataseturi mari
                    //o versiune mai rapid este posibila folosind hashsets
                    var distinctItems = DependencyList.GroupBy(x => x.PackageName).Select(y => y.First());
                    List<Package> SortedList = distinctItems.OrderBy(o => o.PackageName).ToList();
                    foreach (var pack in SortedList)
                    {
                        //get dependences nivel 1
                        distinctItems = DependencyList.Where(a => a.PackageName == pack.PackageName).ToList();
                        WorkingDependencyList.Clear();
                        foreach (var dependent in distinctItems)
                        {
                            if (dependent.PackageDependency != "")
                            {
                                recusiveDependency(dependent.PackageName);
                            }
                            else
                            {
                                WorkingDependencyList.Add(dependent);
                            }
                        }
                        var distinctItemsgorup = WorkingDependencyList.GroupBy(x => x.PackageName).Select(y => y.First());
                        List<Package> SortedListDependecy = distinctItemsgorup.OrderBy(o => o.PackageName).ToList();
                        string coada = "";
                        SortedListDependecy.RemoveAll(x => x.PackageName == pack.PackageName);
                        foreach (var dependent in SortedListDependecy)
                        {
                            coada = coada + " " + dependent.PackageName;
                        }
                        tw.WriteLine(pack.PackageName + coada);
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
                    //o versiune mai rapid este posibila folosind hashsets
                    var distinctItems = DependencyList.GroupBy(x => x.PackageName).Select(y => y.First());
                    List<Package> SortedList = distinctItems.OrderBy(o => o.PackageName).ToList();
                    foreach (var pack in SortedList)
                    {
                        //get dependences nivel 1
                        distinctItems = DependencyList.Where(a => a.PackageName==pack.PackageName).ToList();
                        WorkingDependencyList.Clear();
                        foreach (var dependent in distinctItems) 
                        {
                            if (dependent.PackageDependency != "")
                            {
                                recusiveDependency(dependent.PackageName);
                            }
                            else 
                            {
                                WorkingDependencyList.Add(dependent);
                            }
                        }
                        var distinctItemsgorup = WorkingDependencyList.GroupBy(x => x.PackageName).Select(y => y.First());
                        List<Package> SortedListDependecy = distinctItemsgorup.OrderBy(o => o.PackageName).ToList();
                        string coada = "";
                        SortedListDependecy.RemoveAll(x => x.PackageName == pack.PackageName);
                        foreach (var dependent in SortedListDependecy)
                        {
                            coada = coada + " " + dependent.PackageName;
                        }
                        tw.WriteLine(pack.PackageName+coada);
                    }
                }
            }

        }
    }
}
