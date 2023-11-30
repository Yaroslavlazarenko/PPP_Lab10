
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using static PPP_Lab10.RatingSystemExtentions;

namespace PPP_Lab10
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();

            Stopwatch stopwatch = new Stopwatch();
            RatingSystem ratingSystem = new RatingSystem();

            stopwatch.Start();
            var task = taskManager.CreateUsersRatingDictionaryInParallelAsync(ratingSystem, 10000, 8);

            await task;

            stopwatch.Stop();
            Console.WriteLine($"Время создания словаря с пользователями и их рейтингом: {stopwatch.ElapsedMilliseconds} мс");

            stopwatch.Start();
            var task1 = taskManager.CreateUsersArrayInParallelAsync(1000000, 1);

            var users = await task1;

            stopwatch.Stop();
            Console.WriteLine($"Время создания массива пользователей: {stopwatch.ElapsedMilliseconds} мс");

            stopwatch.Start();
            users = await TaskManager.MergeSortByField(users, User.OrderByFirstNameLeft);

            stopwatch.Stop();
            Console.WriteLine($"Время сортировки слиянием по именам против алфавитного порядка: {stopwatch.ElapsedMilliseconds} мс");

            stopwatch.Start();
            users = await TaskManager.MergeSortByField(users, User.OrderByDescendingNameLeft);

            stopwatch.Stop();
            Console.WriteLine($"Время сортировки слиянием по именам за алфавитным порядком: {stopwatch.ElapsedMilliseconds} мс");
        }

        /// <summary>
        /// Выводит список пар ключ-значение на консоль.
        /// </summary>
        /// <param name="ratingList">Список пар ключ-значение для вывода.</param>
        /// <exception cref="ArgumentNullException"></exception>
        private static void consolePrintListOfKeyValuePair(List<KeyValuePair<User, int>> ratingList)
        {
            if (ratingList != null)
            {
                foreach (var item in ratingList)
                {
                    Console.WriteLine($"User Info:  Id: {item.Key.Id,-3}\tFirst Name: {item.Key.FirstName,-10}\tLast Name: {item.Key.LastName,-10}\tAge: {item.Key.Age,-3}\tRating: {item.Value,-3}");
                }
            }
            else
            {
                throw new ArgumentNullException("ratingList должен быть не null");
            }
        }
    }
}