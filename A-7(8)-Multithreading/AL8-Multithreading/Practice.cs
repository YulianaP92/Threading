using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced_Lesson_6_Multithreading
{
    class Practice
    {
        /// <summary>
        /// LA8.P1/X. Написать консольные часы, которые можно останавливать и запускать с 
        /// консоли без перезапуска приложения.
        /// </summary>
        public static void LA8_P1_5()
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine(DateTime.Now.ToString());
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }
            });
            thread.Start();

            while (true)
            {
                var key = Console.ReadKey().KeyChar;
                if (key == '1')
                {
                    thread.Suspend();//приостановить
                }
                if (key == '2')
                {
                    thread.Resume();//возобновить
                }
            }
        }

        /// <summary>
        /// LA8.P2/X. Написать консольное приложение, которое “делает массовую рассылку”. 
        /// </summary>
        public static void LA8_P2_5()
        {
            for (int i = 0; i < 50; i++)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    string path = Path.Combine(@"D:\Task\", $"{i}.txt");
                    Console.WriteLine("{0} поток", Thread.CurrentThread.ManagedThreadId);
                    using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine($"{i}");
                    }
                });
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Написать код, который в цикле (10 итераций) эмулирует посещение 
        /// сайта увеличивая на единицу количество посещений для каждой из страниц.  
        /// </summary>

        /// <summary>
        /// LA8.P4/X. Отредактировать приложение по “рассылке” “писем”. 
        /// Сохранять все “тела” “писем” в один файл. Использовать блокировку потоков, чтобы избежать проблем синхронизации.  
        /// </summary>
        private static object objectLock = new object();
        public static void LA8_P4_5()
        {

            for (int i = 0; i < 50; i++)
            {

                ThreadPool.QueueUserWorkItem(state =>
                {
                    Console.WriteLine("{0} поток", Thread.CurrentThread.ManagedThreadId);
                    string path = Path.Combine(@"D:\Task\", "new.txt");
                    lock (objectLock)
                    {
                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine($"{i}");
                        }
                    }
                });
                Thread.Sleep(20);
            }
        }

        /// <summary>
        /// LA8.P5/5. Асинхронная “отсылка” “письма” с блокировкой вызывающего потока 
        /// и информировании об окончании рассылки (вывод на консоль информации 
        /// удачно ли завершилась отсылка). 
        /// </summary>
        public async static void LA8_P5_5()
        {
        }
    }
}
