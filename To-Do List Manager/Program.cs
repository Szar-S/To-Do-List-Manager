using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace To_Do_List_Manager
{
    static class FileLocations
    {
        public static string saveTask = "TList.xml";
    }
    internal class Program
    {
        static string AskAndReceive(string question)
        {
            Console.Write(question);
            return Console.ReadLine();
        }
        static void Main(string[] args)
        {
            bool isdone = false;
            Console.WriteLine("Welcome to the program");
            while (!isdone)
            {
                File.Open(FileLocations.saveTask, FileMode.OpenOrCreate).Close();
                Console.WriteLine("What would you like to do");
                Console.WriteLine("1: Create new task");
                Console.WriteLine("2: Check the tasks");
                Console.WriteLine("3: Delete Task");
                Console.WriteLine("4: Exit");
                string userChoice = AskAndReceive("Your choice: ");
                while (userChoice != "1" && userChoice != "2" && userChoice != "3" && userChoice != "4")
                {
                    Console.Write("Choose 1, 2, 3 or 4: ");
                    userChoice = Console.ReadLine();
                }

                if (userChoice == "1")
                {
                    CreateNewTask(args);
                }
                if (userChoice == "2")
                {
                    CheckTasks();
                }
                if (userChoice == "3")
                {
                    Console.Write("Which task will you delete(Use numbers): ");
                    DeleteATask(FileLocations.saveTask, Convert.ToInt32(Console.ReadLine()));
                    Console.WriteLine("Task deleted.");
                }
                if (userChoice == "4")
                {
                    isdone = true;
                }
            }
        }
        static void CheckTasks()
        {
            try
            {
                Console.WriteLine(File.ReadAllText(FileLocations.saveTask));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void DeleteATask(string fileName, int line_to_edit)
        {
            try
            {
                List<string> arrLine = File.ReadAllLines(fileName).ToList();
                arrLine.RemoveAt(line_to_edit - 1);
                File.WriteAllLines(fileName, arrLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void CreateNewTask(string[] args)
        {
            try
            {
                string name = AskAndReceive("Name: ");
                string description = AskAndReceive("Description: ");
                string iss = AskAndReceive("Is it complete(T/F): ").Replace(" ", "");

                bool iscomplete;

                while (iss != "T" && iss != "F")
                {
                    iss = AskAndReceive("Use 'T' or 'F': ").Replace(" ", "");
                }

                if (iss == "T")
                {
                    iscomplete = true;
                }
                else
                {
                    iscomplete = false;
                }

                TList.Add(new Task(name, description, iscomplete));
                TList.update(FileLocations.saveTask);
                Console.WriteLine("Succesfully done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            }
    }
    class Task
    {
        public string Name;
        public string Description;
        public bool iscomplete;
        public Task(string Name, string Description, bool iscomplete)
        {
            this.Name = Name;
            this.Description = Description;
            this.iscomplete = iscomplete;
        }
        public override string ToString()
        {
            string ss;
            if (!iscomplete)
            {
                ss = "This task isn't completed.";
            }
            else
            {
                ss = "This task is completed.";
            }
            return Convert.ToString(Name +"-"+ Description +"-" + ss);
        }
    }
    class TList
    {
        public static Task taskT;
        public static void update(string saveTask)
        {
            List<string> lines = new List<string>();
            lines = File.ReadLines(saveTask).ToList();
            File.AppendAllLines(saveTask, new[] { taskT.ToString() });
        }
        public static void Tell()
        {
            Console.WriteLine(taskT);
        }
        public static void Add(Task task)
        {
            taskT = task;
        }
    }
}
