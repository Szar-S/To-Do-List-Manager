using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace To_Do_List_Manager
{
    sealed public class @Task
    {
        public string Name;
        public string Description;
        public bool iscomplete;
        public @Task(string Name, string Description, bool iscomplete)
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
            return Convert.ToString($"{Name} - {Description} - {ss}");
        }
    }
    abstract public class TaskList
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
        public static void changeStatus(string saveTask, int order)
        {
        }
    }
}
