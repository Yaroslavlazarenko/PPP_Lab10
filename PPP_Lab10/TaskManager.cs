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

        public async Task CreateUsersRatingDictionaryInParallelAsync(RatingSystem ratingSystem, int userCount, int threadCount)
        {
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

                tasks[i] = Task.Run(() =>
                {
                    CreateUsersAndRatingRange(ratingSystem, start, end);
                });
            }

            await Task.WhenAll(tasks);
        }

        public static void CreateUsersAndRatingRange(RatingSystem ratingSystem, int start, int end)
        {
            const int maxAge = 121;
            const int minRatingValue = -100000;
            const int maxRatigValue = 1000000;

            for (int i = start; i < end; i++)
            {
                ratingSystem.AddOrUpdateRating(new User(i, Utils.GetRandomFirstName(), Utils.GetRandomLastName(), Utils.GetRandomAge(maxAge)), Utils.GetRandomRating(minRatingValue, maxRatigValue));
            }
        }


        public async Task<User[]> CreateUsersArrayInParallelAsync(int userCount, int threadCount)
        {
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

                tasks[i] = Task.Run(() =>
                {
                    CreateUserRange(users, start, end);
                });
            }

            await Task.WhenAll(tasks);
            return users;
        }
        private static void CreateUserRange(User[] users, int start, int end)
        {
            const int maxAge = 121;

            for (int i = start; i < end; i++)
            {
                  users[i] = new User(i, Utils.GetRandomFirstName(), Utils.GetRandomLastName(), Utils.GetRandomAge(maxAge));
            }
        }

        public static User[] MergeSortByField(User[] users, CompareDelegate compareDelegate)
        {
            if (users == null || users.Length <= 1)
                return users;

            return MergeSort(users, compareDelegate);
        }

        private static User[] MergeSort(User[] users, CompareDelegate compareDelegate)
        {
            if (users.Length <= 1)
                return users;

            int middle = users.Length / 2;
            User[] left = users.Take(middle).ToArray();
            User[] right = users.Skip(middle).ToArray();

            Task<User[]> leftSort = Task.Run(() => MergeSort(left, compareDelegate));
            Task<User[]> rightSort = Task.Run(() => MergeSort(right, compareDelegate));

            Task.WaitAll(leftSort, rightSort);

            return Merge(leftSort.Result, rightSort.Result, compareDelegate);
        }

        private static User[] Merge(User[] left, User[] right, CompareDelegate compareDelegate)
        {
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
