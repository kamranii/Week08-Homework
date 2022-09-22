using System;
namespace Week08.Homework
{
    public class ToDo
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
        public override string ToString()
        {
            return $"User Id: {userId}\nid: {id}\nTitle: {title}\nCompleted: {completed}";
        }
    }
}

