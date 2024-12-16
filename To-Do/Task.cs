using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List_Console
{
    internal class Task
    {
        public string Name { get; set; }
        public Guid Id { get; private set; }
        public bool IsImportant { get; set; }
        public DateTime Date { get; set; }

        public Task(string name, bool isImportant, int year, int month, int day, int hour, int minute)
        {
            Random rnd = new Random();
            Name = name;
            Id = Guid.NewGuid();
            IsImportant = isImportant;
            Date = new DateTime(year, month, day, hour, minute, 0);
        }
        public Task(string name, int year, int month, int day, int hour, int minute)
        {
            Random rnd = new Random();
            Name = name;
            Id = Guid.NewGuid();
            Date = new DateTime(year, month, day, hour, minute, 0);
        }
        public override string ToString()
        {
            return $"Id: {Id} | {Name} | Срок: {Date}";
        }
        public override bool Equals(object obj)
        {
            if (obj is Task otherTask)
            {
                return this.Id == otherTask.Id;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}