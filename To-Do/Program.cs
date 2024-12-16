using System;
using System.Threading;
using System.Threading.Tasks;

namespace To_Do_List_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var x = new TaskManager();
            /*System.Threading.Tasks.Task.Run(() =>
            {
                while (true)
                {
                    foreach (var zxc in x.GetTasks())
                    {
                        if (zxc.Date < DateTime.Now)
                        {
                            x.ToOverue(zxc);
                        }
                    }
                    Thread.Sleep(2000);
                    break;
                }
            });*/

            /*Thread thread = new Thread(() =>
            {
                Console.WriteLine();
                while (true)
                {
                    Console.Write($"\r{DateTime.Now}");
                    Thread.Sleep(1000);
                }
            });
            */

            while (true)
            {
                Console.WriteLine($"To-Do List Manager");
                Console.WriteLine();
                Console.WriteLine($"==================");
                Console.WriteLine();
                Console.WriteLine($"1. Добавить задачу");
                Console.WriteLine($"2. Удалить задачу");
                Console.WriteLine($"3. Просмотреть список задач");
                Console.WriteLine($"4. Очистить список задач");
                Console.WriteLine($"5. Сохранить задачи в файл");
                Console.WriteLine($"6. Загрузить задачи из файла");
                Console.WriteLine($"7. Выход");
                Console.Write($"Выберите опцию цифрой: ");

                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine();
                        Console.Write("Название задачи: ");
                        string name = Console.ReadLine();
                        int year, month, day, hour, minutes;

                        Console.Write($"\nГод: ");
                        year = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"\nМесяц: ");
                        month = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"\nДень: ");
                        day = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"\nЧасы: ");
                        hour = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"\nМинуты: ");
                        minutes = Convert.ToInt32(Console.ReadLine());

                        x.AddTask(new Task(name, year, month, day, hour, minutes));
                        break;
                    case 2:
                        Console.WriteLine();
                        int i = 0;
                        foreach (var task in x.GetTasks())
                        {
                            Console.WriteLine($"{i++} - " + task.ToString());
                        }
                        Console.WriteLine("Введите номер задачи которую нужно удалить: ");
                        x.RemoveTask(x.GetTaskByNumber(Convert.ToInt32(Console.ReadLine())));
                        break;
                    case 3:
                        Console.WriteLine();
                        Console.WriteLine("-------------------------");
                        foreach (var task in x.GetTasks())
                        {
                            Console.WriteLine(task.ToString());
                        }
                        Console.WriteLine("-------------------------");
                        Console.WriteLine();
                        Thread.Sleep(2000);
                        break;
                    case 4:
                        x.Clear();
                        Console.WriteLine();
                        break;
                    case 5:
                        Console.WriteLine();
                        x.SaveFileTask();
                        break;
                    case 6:
                        Console.WriteLine();
                        x.LoadFileTask();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                    default:
                        throw new InvalidOperationException("Недопустимая опция");
                }
            }
        }
    }
}