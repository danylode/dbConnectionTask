using System;

namespace todoListCRUD
{
    public class Task
    {
        private int taskId;
        private bool done;
        private string taskName;
        private DateTime taskEndTime;
        private string taskDescription;

        public int TaskId { get => taskId; set => taskId = value; }
        public bool Done { get => done; set => done = value; }
        public string TaskName { get => taskName; set => taskName = value; }
        public DateTime TaskEndTime { get => taskEndTime; set => taskEndTime = value; }
        public string TaskDescription { get => taskDescription; set => taskDescription = value; }

        public Task(int id, bool done, string taskName) : this(id, done, taskName, new DateTime(), null) { }
        public Task(int id, bool done, string taskName, DateTime taskEndTime) : this(id, done, taskName, taskEndTime, null) { }
        public Task(int id, bool done, string taskName, string taskDescription) : this(id, done, taskName, new DateTime(), taskDescription) { }
        public Task(int id, bool done, string taskName, DateTime taskEndTime, string taskDescription)
        {
            this.taskId = id;
            this.done = done;
            this.taskName = taskName;
            this.taskEndTime = taskEndTime;
            this.taskDescription = taskDescription;
        }

        public override string ToString()
        {
            char doneChar = done ? 'x' : ' ';
            string result = $"{taskId}. [{doneChar}] {taskName}";

            if (taskEndTime != new DateTime())
            {
                result += $" ({taskEndTime.ToString("MMM dd")})";
            }
            if (taskDescription != null)
            {
                result += $" {Environment.NewLine}   {taskDescription}";
            }
            return result;
        }
    }
}