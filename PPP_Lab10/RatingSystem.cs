using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PPP_Lab10
{
    public class RatingSystem
    {
        private Dictionary<User, int> _userRatings = new Dictionary<User, int>();

        /// <summary>
        /// Свойство для получения словаря с рейтингами пользователей
        /// </summary>
        public Dictionary<User, int> UserRatings
        {
            get => _userRatings;
        }

        /// <summary>
        /// Метод для добавления пользователя в систему рейтинга или изменения рейтинга, если пользователь уже существует.
        /// </summary>
        /// <param name="user">Пользователь, для которого устанавливается или обновляется рейтинг.</param>
        /// <param name="rating">Рейтинг, который нужно присвоить пользователю.</param>
        /// <exception cref="ArgumentException">Исключение, если входной объект пользователя (user) равен null.</exception>
        public void AddOrUpdateRating(User user, int rating)
        {
            if (user != null)
            {
                User userWithMatchingId;

                lock (_userRatings)
                {
                    userWithMatchingId = _userRatings.FirstOrDefault(pair => pair.Key.Id == user.Id).Key;

                    if (_userRatings.ContainsKey(user))
                    {
                        _userRatings[user] = rating;
                    }
                    else if (userWithMatchingId != null)
                    {
                        _userRatings.Remove(userWithMatchingId);
                        _userRatings.Add(user, rating);
                    }
                    else
                    {
                        _userRatings.Add(user, rating);
                    }
                }
            }
            else
            {
                throw new ArgumentException("Входной объект пользователя (user) не должен быть null.");
            }
        }

        /// <summary>
        /// Удаляет пользователя из системы рейтинга.
        /// </summary>
        /// <param name="user">Пользователь, которого нужно удалить из системы рейтинга.</param>
        /// <exception cref="KeyNotFoundException">Исключение, если пользователь не найден в системе рейтинга.</exception>
        /// <exception cref="ArgumentException">Исключение, если входной объект пользователя (user) равен null.</exception>
        public void RemoveRating(User user)
        {
            if (user != null)
            {
                if (_userRatings.ContainsKey(user))
                {
                    _userRatings.Remove(user);
                }
                else
                {
                    throw new KeyNotFoundException($"Пользователь {user.FirstName} не найден в системе рейтинга.");
                }
            }
            else
            {
                throw new ArgumentException("Входной объект пользователя (user) не должен быть null.");
            }
        }

        /// <summary>
        /// Получает рейтинг пользователя.
        /// </summary>
        /// <param name="user">Объект класса User, для которого нужно получить рейтинг.</param>
        /// <returns>Рейтинг пользователя в виде целого числа.</returns>
        /// <exception cref="KeyNotFoundException">Исключение, если пользователь не найден в системе рейтинга.</exception>
        /// <exception cref="ArgumentException">Исключение, если входной объект пользователя (user) равен null.</exception>
        public int GetUserRatingValue(User user)
        {
            if (user != null)
            {
                if (_userRatings.ContainsKey(user))
                {
                    return _userRatings[user];
                }
                throw new KeyNotFoundException($"Пользователь {user.FirstName} не найден в системе рейтинга.");
            }
            throw new ArgumentException("Входной объект пользователя (user) не должен быть null.");
        }

        /// <summary>
        /// Изменяет рейтинг пользователя.
        /// </summary>
        /// <param name="user">Пользователь, объект класса User.</param>
        /// <param name="newRating">Новое значение рейтинга пользователя.</param>
        /// <exception cref="KeyNotFoundException">Исключение, если пользователь не найден в системе рейтинга.</exception>
        /// <exception cref="ArgumentException">Исключение, если входной объект пользователя (user) равен null.</exception>
        public void ModifyRatingByUser(User user, int newRating)
        {
            if (user != null)
            {
                if (!_userRatings.ContainsKey(user))
                {
                    throw new KeyNotFoundException($"Пользователь {user.FirstName} не найден в системе рейтинга.");
                }
                _userRatings[user] = newRating;
            }
            else
            {
                throw new ArgumentException("Входной объект пользователя (user) не должен быть null.");
            }
        }

        /// <summary>
        /// Метод для обновления рейтинга пользователя по его id.
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <param name="newRating">новый рейтинг пользователя</param>
        /// <exception cref="KeyNotFoundException">Появляется если пользователь не найден</exception>
        /// <exception cref="ArgumentException">Появляется если значение id отрицательное</exception>
        public void ModifyRatingByUserId(int id, int newRating)
        {
            if (id >= 0)
            {
                User userWithMatchingId = _userRatings.FirstOrDefault(pair => pair.Key.Id == id).Key;
                if (userWithMatchingId!=null)
                {
                    _userRatings[userWithMatchingId] = newRating;
                }
                else
                {
                    throw new KeyNotFoundException("Пользователя с введённым id не существует в базе данных.");
                }
            }
            else
            {
                throw new ArgumentException("Значение Id не может быть отрицательным.");
            }
        }

        /// <summary>
        /// Переопределение метода ToString для получения информации о рейтингах всех добавленных пользователей.
        /// Возвращает строку, содержащую информацию о каждом пользователе и его рейтинге.
        /// </summary>
        /// <returns>Информация о рейтингах пользователей.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var keyValuePair in _userRatings)
            {
                stringBuilder.AppendLine($"User Info {keyValuePair.Key.Id,-2}:  {keyValuePair.Key}\t{keyValuePair.Value,-5}");
            }
            return stringBuilder.ToString();
        }
    }
}
