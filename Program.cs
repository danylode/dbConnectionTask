using System;
using System.Collections.Generic;

namespace todoListCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            DBConnection db = new DBConnection();
            Menu(db);
        }

        private static void Menu(DBConnection db)
        {
            while (true)
            {
                Console.WriteLine("Menu:\n1.Create task\n2.Get tasks\n3.Change task by id\n4.Delete task by id\n");
                Console.Write("input >> ");
                char input = Console.ReadLine()[0];

                switch (input)
                {
                    case '1':
                        CreateTask(db);
                        break;
                    case '2':
                        Console.WriteLine();
                        WriteTasks(db.GetAllTasks());
                        Console.WriteLine();
                        break;
                    case '3':
                        ChangeTask(db);
                        break;
                    case '4':
                        DeleteTask(db);
                        break;
                    default:
                        Console.WriteLine("Command not found!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void WriteTasks(List<Task> tasks)
        {
            foreach (var task in tasks)
            {
                Console.WriteLine(task.ToString());
            }
        }

        private static void CreateTask(DBConnection db)
        {
            try
            {
                Console.WriteLine("Create new task:");
                Console.Write("Enter task name: ");
                var name = Console.ReadLine();
                Console.Write("Enter task done: ");
                bool done = Convert.ToBoolean(Console.ReadLine());
                Console.Write("Enter task end time: ");
                var endTime = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter task description: ");
                var description = Console.ReadLine();

                db.AddTask(done, name, endTime, description);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ChangeTask(DBConnection db)
        {
            try
            {
                Console.WriteLine("Change task:");
                Console.Write("Enter task id: ");
                int taskId = Convert.ToInt16(Console.ReadLine());
                Task previousTask = db.GetTaskById(taskId);

                Console.Write($"Change task name ({previousTask.TaskName}) ?: ");
                var taskName = Console.ReadLine();
                taskName = taskName != "y" ? previousTask.TaskName : taskName;

                Console.Write($"Change task done flag ({previousTask.Done}) ?: ");
                var taskDoneString = Console.ReadLine();
                bool taskDone = taskDoneString != "y" ? previousTask.Done : Convert.ToBoolean(taskDoneString);

                Console.Write($"Change task end time ({previousTask.TaskEndTime.ToString("yyyy-MM-dd")}) ?: ");
                var taskEndTimeString = Console.ReadLine();
                DateTime taskEndTime = taskEndTimeString != "y" ? previousTask.TaskEndTime : DateTime.Parse(taskEndTimeString);

                Console.Write($"Change task description ({previousTask.TaskDescription}) ?: ");
                var taskDescription = Console.ReadLine();
                taskDescription = taskDescription != "y" ? previousTask.TaskDescription : taskDescription;

                db.ChangeTask(taskId, new Task(taskId, taskDone, taskName, taskEndTime, taskDescription));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void DeleteTask(DBConnection db)
        {
            Console.WriteLine("Delete task:");
            Console.Write("Enter task id: ");
            int taskId = Convert.ToInt16(Console.ReadLine());
            db.RemoveTask(taskId);
        }
    }
}
