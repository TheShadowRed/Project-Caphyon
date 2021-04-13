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
        public void TaskThird() 
        {
            ReadInput();
            ReadComputeRInput();
            
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
        void ProccesWriteFilesThird(string Path) 
        {
            using (TextWriter tw = new StreamWriter(Path))
            {
                string writetoPath = "";
                //merge prost la dataseturi mari
                //o versiune mai rapid este posibila folosind hashsets
                var distinctItems = WorkingDependencyList.GroupBy(x => x.PackageName).Select(y => y.First());
                List<Package> SortedList = distinctItems.OrderBy(o => o.PackageName).ToList();
                foreach (var pack in SortedList)
                {

                    if (writetoPath == "")
                    {
                        //delimitarea
                        writetoPath = pack.PackageName.ToString();
                    }
                    else 
                    {
                        writetoPath = writetoPath+" " + pack.PackageName.ToString();
                    }
                }
                tw.WriteLine(writetoPath);
            }
        }
        void ProccesWriteFilesThirdApend(string Path)
        {
            using (StreamWriter w = File.AppendText(Path))
            {
                string writetoPath = "";
                //merge prost la dataseturi mari
                //o versiune mai rapid este posibila folosind hashsets
                var distinctItems = WorkingDependencyList.GroupBy(x => x.PackageName).Select(y => y.First());
                List<Package> SortedList = distinctItems.OrderBy(o => o.PackageName).ToList();
                foreach (var pack in SortedList)
                {

                    if (writetoPath == "")
                    {
                        //delimitarea
                        writetoPath = pack.PackageName.ToString();
                    }
                    else
                    {
                        writetoPath = writetoPath + " " + pack.PackageName.ToString();
                    }
                }
                w.WriteLine(writetoPath);
            }
        }
        void ReadComputeRInput()
        {
            WorkingDependencyList.Clear();
            string path = @"task3.out";
            File.Delete(path);
            //read file line by line
            var lines = File.ReadLines("computers.in");
            //clear list content
            //noduri cu dependente
            foreach (var line in lines)
            {
                //populate workingset with data
                string[] separatedFiles = line.Split(' ');
                foreach (var SingleFile in separatedFiles) 
                {
                    //get dependecy
                    recusiveDependency(SingleFile);
                }
                 //remove main package
                foreach (var SingleFile in separatedFiles)
                {
                    //get dependecy
                    WorkingDependencyList.RemoveAll(x => x.PackageName == SingleFile);
                }
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    ProccesWriteFilesThird(path);
                }
                else if (File.Exists(path))
                {
                    ProccesWriteFilesThirdApend(path);
                }
                WorkingDependencyList.Clear();
            }

        }
        void ProcessWriteDataOne(String Path) 
        {
            using (TextWriter tw = new StreamWriter(Path))
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
        void ProcessData() 
        {
            string path = @"task1.out";

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                ProcessWriteDataOne(path);
            }
            else if (File.Exists(path))
            {
                File.Delete(path);
                File.Create(path).Dispose();
                ProcessWriteDataOne(path);
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
        void ProccesWriteDataTwo(String Path) 
        {
            using (TextWriter tw = new StreamWriter(Path))
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
        void ProcessDataTask2() 
        {
            string path = @"task2.out";

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                ProccesWriteDataTwo(path);
            }
            else if (File.Exists(path))
            {
                File.Delete(path);
                File.Create(path).Dispose();
                ProccesWriteDataTwo(path);
            }

        }
    }
}
