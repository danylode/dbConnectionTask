using System;
using System.Collections.Generic;
using Npgsql;

namespace todoListCRUD
{
    public class DBConnection
    {
        private const string CONNECTION_STRING = "Host=127.0.0.1;Username=todolistapp;Password=secret;Database=todolist";
        private NpgsqlConnection connection;

        public DBConnection()
        {
            connection = new NpgsqlConnection(CONNECTION_STRING);
        }

        public Task GetTaskById(int id)
        {
            connection.Open();
            using (var cmd = new NpgsqlCommand("SELECT * FROM task WHERE id=@id"))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                var taskId = reader.GetInt16(0);
                var taskDone = reader.GetBoolean(1);
                var taskName = reader.GetString(2);
                var taskEndtime = reader.GetDateTime(3);
                var taskDescription = reader.GetString(4);

                Task result = new Task(taskId, taskDone, taskName, taskEndtime, taskDescription);
                connection.Close();
                return result;
            }
        }

        public List<Task> GetAllTasks()
        {
            List<Task> result = new List<Task>();

            connection.Open();
            using (var cmd = new NpgsqlCommand("SELECT * FROM task", connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var taskId = reader.GetInt16(0);
                        var taskDone = reader.GetBoolean(1);
                        var taskName = reader.GetString(2);
                        var taskEndTime = reader.GetDateTime(3);
                        var taskDescription = reader.GetString(4);
                        result.Add(new Task(taskId, taskDone, taskName, taskEndTime, taskDescription));
                    }
                }
            }
            connection.Close();
            return result;
        }

        public void AddTask(bool done, string name, DateTime endTime, string description)
        {
            connection.Open();
            var cmd = new NpgsqlCommand("INSERT INTO task(done,taskname,taskendtime,taskdescription) VALUES (@done, @taskname, @taskendtime, @taskdescription)");
            cmd.Parameters.AddWithValue("done", done);
            cmd.Parameters.AddWithValue("taskname", name);
            cmd.Parameters.AddWithValue("taskendtime", endTime.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("taskdescription", description);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void ChangeTask(int taskId, Task newTask)
        {
            connection.Open();
            using (var updateCmd = new NpgsqlCommand("UPDATE task SET (done,taskname,taskendtime,taskdescription) VALUES (@done,@taskName,@taskEndTime,@taskDescription) WHERE id=@taskId"))
            {
                updateCmd.Parameters.AddWithValue("taskId", newTask.TaskId);
                updateCmd.Parameters.AddWithValue("done", newTask.Done);
                updateCmd.Parameters.AddWithValue("taskName", newTask.TaskName);
                updateCmd.Parameters.AddWithValue("taskEndTime", newTask.TaskEndTime);
                updateCmd.Parameters.AddWithValue("taskDescription", newTask.TaskDescription);
            }
            connection.Close();
        }

        public void RemoveTask(int id)
        {
            connection.Open();
            using (var cmd = new NpgsqlCommand($"DELETE FROM TASK WHERE id={id}", connection)) ;
            connection.Close();
        }

    }
}
