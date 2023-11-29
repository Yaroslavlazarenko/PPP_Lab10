using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPP_Lab10
{
    public class RatingSystemExtentions
    {
        public delegate bool CompareDelegate(User left, User right);
        public delegate bool SearchDelegate(User user, string searchValue);

        /// <summary>
        /// Сортирует список пар ключ-значение на основе предоставленного делегата сравнения.
        /// </summary>
        /// <param name="ratingSystem">Система рейтинга, содержащая список пар ключ-значение для сортировки.</param>
        /// <param name="compareDelegate">Делегат сравнения, определяющий порядок сортировки.</param>
        /// <returns>Отсортированный список пар ключ-значение.</returns>
        /// <exception cref="ArgumentNullException">Генерирует исключение, если ratingSystem равен null или отсутствует список пар ключ-значение.</exception>
        public static List<KeyValuePair<User, int>> Sort(RatingSystem ratingSystem, CompareDelegate compareDelegate)
        {
            if (ratingSystem != null && ratingSystem.UserRatings != null && ratingSystem.UserRatings.Count > 0)
            {
                List<KeyValuePair<User, int>> userRatingsList = ratingSystem.UserRatings.ToList();

                for (int i = 0; i < userRatingsList.Count-1; i++)
                {
                    for (int j = 0; j < userRatingsList.Count-1; j++)
                    {
                        if (compareDelegate(userRatingsList[j].Key, userRatingsList[j + 1].Key))
                        {
                            (userRatingsList[j], userRatingsList[j + 1]) = (userRatingsList[j + 1], userRatingsList[j]);
                        }
                    }
                }

                return userRatingsList;
            }
            else
            {
                throw new ArgumentNullException("В сортировку нужно подавать непустой класс и словарь в нём");
            }
        }

        /// <summary>
        /// Выполняет поиск по списку пользователей с рейтингами по ключевому слову.
        /// </summary>
        /// <param name="ratingSystem">Система рейтинга, содержащая список пользователей и их рейтинги.</param>
        /// <param name="searchValue">Значение, используемое для поиска и фильтрации пользователей.</param>
        /// <param name="searchDelegate">Делегат, определяющий условие поиска и фильтрации.</param>
        /// <returns>Список пар ключ-значение, представляющий результат поиска и фильтрации.</returns>
        /// <exception cref="ArgumentNullException">Выбрасывает исключение, если система рейтинга пуста или равна null.</exception>
        public static List<KeyValuePair<User, int>> Search(RatingSystem ratingSystem, string searchValue, SearchDelegate searchDelegate)
        {
            if (ratingSystem != null && ratingSystem.UserRatings != null && ratingSystem.UserRatings.Count > 0)
            {

                List<KeyValuePair<User, int>> userRatingsList = ratingSystem.UserRatings.ToList();
                List<KeyValuePair<User, int>> temp = new();

                for (int i = 0; i < userRatingsList.Count; i++)
                {
                    if (searchDelegate(userRatingsList[i].Key, searchValue))
                    {
                        temp.Add(userRatingsList[i]);
                    }
                }

                return temp;
            }
            else 
            {
                throw new ArgumentNullException("В поиск нужно подавать непустой класс и словарь в нём");
            }
        }
    }
}
