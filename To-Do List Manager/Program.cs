using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace To_Do_List_Manager
{
    static class FileLocations
    {
        public static string saveTask = "TList.xml";
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isdone = false;
            Console.WriteLine("Welcome to the program");
            while (!isdone)
            {
                File.Open(FileLocations.saveTask, FileMode.OpenOrCreate).Close();
                Console.WriteLine();
                Console.WriteLine("-----------------------------");
                Console.WriteLine("What would you like to do");
                Console.WriteLine("1: Create new task");
                Console.WriteLine("2: Check the tasks");
                Console.WriteLine("3: Delete Task");
                Console.WriteLine("4: Change the status of a task");
                Console.WriteLine("5: Exit");
                string userChoice = AskAndReceive("Your choice: ");
                while (userChoice != "1" && userChoice != "2" && userChoice != "3" && userChoice != "4" && userChoice != "5")
                {
                    userChoice = AskAndReceive("Choose between 1, 2, 3, 4 or 5: ");
                }
                switch (userChoice)
                {
                    case "1":
                        CreateNewTask(args);
                        break;
                    case "2":
                        CheckTasks();
                        break;
                    case "3":
                        int deleteTask = Convert.ToInt32(AskAndReceive("Which task will you delete(Use numbers): "));
                        DeleteATask(FileLocations.saveTask, deleteTask);
                        break;
                    case "4":
                        int changeTask = Convert.ToInt32(AskAndReceive("Which task will you change(Use numbers): "));
                        ChangeStatus(FileLocations.saveTask, changeTask);
                        break;
                    case "5":
                        isdone = true;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        continue;
                }
                Console.WriteLine("-----------------------------");
                Console.WriteLine();
            }
        }
        static void CreateNewTask(string[] args)
        {
            try
            {
                string name = AskAndReceive("Name: ");
                while (name.Replace(" ", "") == "")
                {
                    name = AskAndReceive("Name can't be empty: ");
                }

                string description = AskAndReceive("Description: ");
                while (description.Replace(" ", "") == "")
                {
                    description = AskAndReceive("Description can't be empty: ");
                }

                string iss = AskAndReceive("Is it complete(T for True/F for False): ").Replace(" ", "").ToUpper();
                bool iscomplete = false;
                while (iss != "T" && iss != "F")
                {
                    switch (iss)
                    {
                        case "T":
                            iscomplete = true;
                            break;
                        case "F":
                            iscomplete = false;
                            break;
                        default:
                            iss = AskAndReceive("Use 'T' or 'F': ").Replace(" ", "").ToUpper();
                            continue;
                    }
                }


                TaskList.Add(new Task(name, description, iscomplete));
                TaskList.update(FileLocations.saveTask);
                Console.WriteLine("Succesfully done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void CheckTasks()
        {
            try
            {
                Console.WriteLine("**********************************************************************");
                Console.WriteLine("Tasks:");
                if (File.ReadAllText(FileLocations.saveTask) == "")
                {
                    Console.WriteLine("There is no task.");
                }
                else
                    Console.WriteLine(File.ReadAllText(FileLocations.saveTask));
                Console.WriteLine("**********************************************************************");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("**********************************************************************");
                Console.WriteLine("There is no task.");
                Console.WriteLine("**********************************************************************");
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
                Console.WriteLine("Task deleted.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("There is no task with that number.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("There is no task with that number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void ChangeStatus(string fileName, int line_to_edit)
        {
            try
            {
                string[] lines = File.ReadLines(fileName).ToArray();
                if (lines[line_to_edit - 1].Contains("isn't completed"))
                    lines[line_to_edit - 1] = lines[line_to_edit - 1].Replace("isn't completed", "is completed");
                else
                    lines[line_to_edit - 1] = lines[line_to_edit - 1].Replace("is completed", "isn't completed");
                File.WriteAllLines(fileName, lines);
                Console.WriteLine("Status succesfully changed.");

            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("There is no task with that number.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("There is no task with that number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static string AskAndReceive(string question)
        {
            Console.Write(question);
            return Console.ReadLine();
        }
    }
}