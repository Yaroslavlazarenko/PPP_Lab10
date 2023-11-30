using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static PPP_Lab10.RatingSystemExtentions;

namespace PPP_Lab10
{
    public class TaskManager
    {
        public delegate bool CompareDelegate(User left, User right);

        /// <summary>
        /// Метод для создания словаря пользователей с рейтингами параллельно.
        /// </summary>
        /// <param name="ratingSystem">Система рейтингов.</param>
        /// <param name="userCount">Количество пользователей.</param>
        /// <param name="threadCount">Количество потоков.</param>
        /// <returns>Task - асинхронная операция.</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается, если переданная система рейтингов равна null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если userCount или threadCount меньше или равны нулю.</exception>
        public async Task CreateUsersRatingDictionaryInParallelAsync(RatingSystem ratingSystem, int userCount, int threadCount)
        {
            if (ratingSystem is null)
            {
                throw new ArgumentNullException(nameof(ratingSystem), "rating system при создании словаря пользователей не может быть null");
            }

            if (userCount <= 0 || threadCount <= 0)
            {
                throw new ArgumentOutOfRangeException("userCount и threadCount должны быть больше нуля");
            }

            int usersPerThread = userCount / threadCount;

            Task[] tasks = new Task[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                int end;
                int start = i * usersPerThread;
                if (i == threadCount - 1)
                {
                    end = userCount;
                }
                else
                {
                    end = (i + 1) * usersPerThread;
                }

                tasks[i] = CreateUsersAndRatingRange(ratingSystem, start, end);
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Создает диапазон пользователей и рейорнгов в словаре, начиная с определенного индекса и заканчивая другим.
        /// </summary>
        /// <param name="ratingSystem">Система рейтингов.</param>
        /// <param name="start">Начальный индекс диапазона создания пользователей.</param>
        /// <param name="end">Конечный индекс диапазона создания пользователей.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если переданная система рейтингов равна null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если значения start и end некорректны.</exception>
        public static Task CreateUsersAndRatingRange(RatingSystem ratingSystem, int start, int end)
        {
            if (ratingSystem == null)
            {
                throw new ArgumentNullException("RatingSystem не может быть null");
            }

            if (start < 0 || end < start)
            {
                throw new ArgumentOutOfRangeException("Некорректные значения start и end");
            }

            const int maxAge = 121;
            const int minRatingValue = -100000;
            const int maxRatigValue = 1000000;

            for (int i = start; i < end; i++)
            {
                ratingSystem.AddOrUpdateRating(new User(i, Utils.GetRandomFirstName(), Utils.GetRandomLastName(), Utils.GetRandomAge(maxAge)), Utils.GetRandomRating(minRatingValue, maxRatigValue));
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Создает массив пользователей параллельно, используя заданное количество потоков.
        /// </summary>
        /// <param name="userCount">Количество пользователей для создания в массиве.</param>
        /// <param name="threadCount">Количество потоков для параллельного создания пользователей.</param>
        /// <returns>Массив пользователей.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если userCount или threadCount меньше или равны нулю.</exception>
        public async Task<User[]> CreateUsersArrayInParallelAsync(int userCount, int threadCount)
        {
            if (userCount <= 0 || threadCount <= 0)
            {
                throw new ArgumentOutOfRangeException("userCount и threadCount должны быть больше нуля");
            }

            User[] users = new User[userCount];
            int usersPerThread = userCount / threadCount;

            Task[] tasks = new Task[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                int end;
                int start = i * usersPerThread;
                if (i == threadCount - 1)
                {
                    end = userCount;
                }
                else
                {
                    end = (i + 1) * usersPerThread;
                }

                tasks[i] = CreateUserRange(users, start, end);
            }

            await Task.WhenAll(tasks);
            return users;
        }


        /// <summary>
        /// Сортирует массив пользователей методом слияния (Merge Sort) на основе заданного делегата сравнения.
        /// </summary>
        /// <param name="users">Массив пользователей для сортировки.</param>
        /// <param name="compareDelegate">Делегат для сравнения пользователей.</param>
        /// <returns>Отсортированный массив пользователей.</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается, если переданный массив пользователей или делегат сравнения равны null.</exception>
        /// <exception cref="ArgumentException">Выбрасывается, если массив пользователей пуст или содержит только один элемент.</exception>
        private static Task CreateUserRange(User[] users, int start, int end)
        {
            if (users is null)
            {
                throw new ArgumentNullException("Массив пользователей не может быть null");
            }

            if (start < 0 || end < start || end > users.Length)
            {
                throw new ArgumentOutOfRangeException("Некорректные значения start и end для массива пользователей");
            }

            const int maxAge = 121;

            for (int i = start; i < end; i++)
            {
                  users[i] = new User(i, Utils.GetRandomFirstName(), Utils.GetRandomLastName(), Utils.GetRandomAge(maxAge));
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Сортирует массив пользователей методом слияния (Merge Sort) на основе заданного делегата сравнения.
        /// </summary>
        /// <param name="users">Массив пользователей для сортировки.</param>
        /// <param name="compareDelegate">Делегат для сравнения пользователей.</param>
        /// <returns>Отсортированный массив пользователей.</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается, если переданный массив пользователей или делегат сравнения равны null.</exception>
        /// <exception cref="ArgumentException">Выбрасывается, если массив пользователей пуст или содержит только один элемент.</exception>
        public static async Task<User[]> MergeSortByField(User[] users, CompareDelegate compareDelegate)
        {
            if (users is null || compareDelegate is null)
            {
                throw new ArgumentNullException(nameof(users), "Массив пользователей не может быть null");
            }

            if (users.Length == 0)
            {
                throw new ArgumentException("Массив пользователей пуст, невозможно выполнить сортировку", nameof(users));
            }

            if (users.Length == 1)
            {
                return users;
            }

            return await MergeSort(users, compareDelegate);
        }

        /// <summary>
        /// Рекурсивный метод сортировки слиянием.
        /// </summary>
        /// <param name="users">Массив пользователей для сортировки.</param>
        /// <param name="compareDelegate">Делегат для сравнения пользователей.</param>
        /// <returns>Отсортированный массив пользователей.</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается, если переданный массив пользователей равен null.</exception>
        /// <exception cref="ArgumentException">Выбрасывается, если массив пользователей пуст.</exception>
        private static async Task<User[]> MergeSort(User[] users, CompareDelegate compareDelegate)
        {
            if (users is null)
            {
                throw new ArgumentNullException("Массив пользователей не может быть null");
            }

            if (users.Length <= 1)
            {
                return users;
            }

            int middle = users.Length / 2;
            User[] left = users.Take(middle).ToArray();
            User[] right = users.Skip(middle).ToArray();

            Task<User[]> leftSortTask = MergeSort(left, compareDelegate);
            Task<User[]> rightSortTask = MergeSort(right, compareDelegate);

            await Task.WhenAll(leftSortTask, rightSortTask);

            return Merge(await leftSortTask, await rightSortTask, compareDelegate);
        }

        /// <summary>
        /// Объединение двух отсортированных массивов в один.
        /// </summary>
        /// <param name="left">Левый массив для объединения.</param>
        /// <param name="right">Правый массив для объединения.</param>
        /// <param name="compareDelegate">Делегат для сравнения пользователей.</param>
        /// <returns>Отсортированный объединенный массив пользователей.</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается, если один из сливаемых массивов является null
        /// или если делегат сравнения равен null.</exception>
        private static User[] Merge(User[] left, User[] right, CompareDelegate compareDelegate)
        {
            if (left is null || right is null)
            {
                throw new ArgumentNullException("Один из сливаемых массивов является null");
            }

            if (compareDelegate is null)
            {
                throw new ArgumentNullException("Делегат сравнения не может быть null");
            }

            int leftIndex = 0, rightIndex = 0, mergedIndex = 0;
            User[] merged = new User[left.Length + right.Length];

            while (leftIndex < left.Length && rightIndex < right.Length)
            {
                if (compareDelegate(left[leftIndex], right[rightIndex]))
                {
                    merged[mergedIndex] = left[leftIndex];
                    leftIndex++;
                }
                else
                {
                    merged[mergedIndex] = right[rightIndex];
                    rightIndex++;
                }
                mergedIndex++;
            }

            while (leftIndex < left.Length)
            {
                merged[mergedIndex] = left[leftIndex];
                leftIndex++;
                mergedIndex++;
            }

            while (rightIndex < right.Length)
            {
                merged[mergedIndex] = right[rightIndex];
                rightIndex++;
                mergedIndex++;
            }

            return merged;
        }
    }
}
