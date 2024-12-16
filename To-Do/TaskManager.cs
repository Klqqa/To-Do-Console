using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace To_Do_List_Console
{
    internal class TaskManager
    {
        private List<Task> Tasks { get; set; }
        private Queue<Task> TasksIsOverue { get; set; } = new Queue<Task>();
        public TaskManager()
        {
            Tasks = new List<Task>();

            System.Threading.Tasks.Task.Run(() =>
            {
                while (true)
                {
                    foreach (var x in Tasks)
                    {
                        if (x.Date < DateTime.Now)
                        {
                            TasksIsOverue.Enqueue(x);
                            Console.WriteLine($"Задача {x.ToString()} была перемещена в список просроченных");
                        }
                    }
                    Thread.Sleep(2000);
                    break;
                }
            });
        }
        public void AddTask(Task task)
        {
            if (Tasks.Any(x => x.Id == task.Id)) Console.WriteLine("Такая задача уже есть в списке");
            if (DateTime.Now > task.Date) Console.WriteLine($"Задача изначально просроченная {task.Date}");
            else
            {
                Tasks.Add(task);
                Console.WriteLine("Задача успешно добавлена");
            }
        }
        public void RemoveTask(Task task)
        {
            var result = Tasks.Remove(Tasks.FirstOrDefault(x => x.Id == task.Id));

            if (result)
            {
                Console.WriteLine("Задача успешно удалена");
                return;
            }
            Console.WriteLine("Задача отсутствует в списке");
        }
        public void Clear()
        {
            Tasks.Clear();
            Console.WriteLine("Список задач успешно очищен");
            Thread.Sleep(2000);
            Console.Clear();
        }
        public Task GetTaskByNumber(int number)
        {
            var result = Tasks.ElementAtOrDefault(number);
            if (result != null) return Tasks[number];

            throw new InvalidOperationException($"Задачи под номером {number} не существует");
        }
        public List<Task> GetTasks()
        {
            if (Tasks == null || Tasks.Count <= 0) throw new Exception("Список задач пустой");

            return Tasks;
        }
        public void ToOverue(Task x)
        {
            TasksIsOverue.Enqueue(x);
            Console.WriteLine($"Задача {x.ToString()} была перемещена в список просроченных");
        }
        public void SaveFileTask()
        {
            if (Tasks.Count <= 0)
            {
                throw new Exception("Для начала добавьте как минимум одну задачу");
            }

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Console.WriteLine("Для сохранения, введите название файла куда будут сохранены задачи");
            string filePath = Path.Combine(desktopPath, Console.ReadLine() + ".txt");

            if (File.Exists(filePath))
            {
                Console.WriteLine("Такой файл уже существует... удаляем и производим замену");
                File.Delete(filePath);
            }
            foreach (var x in Tasks)
            {
                File.AppendAllText(filePath, $"{x.ToString()}\n");
                Console.WriteLine($"Задача {x.Id} {x.Name} была успешно добавлена в файл");
            }

            Console.WriteLine("Все задачи успешно добавлены");
            Thread.Sleep(2000);
            Console.Clear();
        }
        public void LoadFileTask()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            desktopPath = desktopPath.TrimEnd(Path.DirectorySeparatorChar);

            Console.WriteLine($"Введите имя файла с рабочего стола, учтите, файл должен быть формата .txt\n");
            Console.WriteLine("Строки должны быть формата (Название задачи,год,месяц,день,часы,минуты)");
            Console.WriteLine();

            string filePath = Path.Combine(desktopPath, $"{Console.ReadLine()}.txt");

            if (string.IsNullOrEmpty(File.ReadAllText(filePath)) || File.ReadAllText(filePath).Any(x => x != ',' && !char.IsLetter(x) && x != ' ' && !char.IsDigit(x) && x != '\n' && x != '\r'))
            {
                throw new InvalidOperationException("Файл пустой или формат данных неверный");
            }
            foreach (var line in File.ReadLines(filePath))
            {
                var x = line.Split(',');
                if (x.Length == 6)
                {
                    var task = new Task(x[0], Convert.ToInt32(x[1]), Convert.ToInt32(x[2]), Convert.ToInt32(x[3]), Convert.ToInt32(x[4]), Convert.ToInt32(x[5]));
                    AddTask(task);
                }
                if (x.Length == 7)
                {
                    var task = new Task(x[0], Convert.ToBoolean(x[1]), Convert.ToInt32(x[2]), Convert.ToInt32(x[3]), Convert.ToInt32(x[4]), Convert.ToInt32(x[5]), Convert.ToInt32(x[6]));
                    AddTask(task);
                }
            }
            Thread.Sleep(2000);
            Console.Clear();
        }
    }
}