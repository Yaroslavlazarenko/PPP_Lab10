using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PPP_Lab10
{
    [Serializable]
    public class User
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private int _age;

        public static readonly int maxPeopleAge = 122;

        [JsonPropertyName("Id")]

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        /// <value>Уникальный идентификатор пользователя.</value>
        /// 
        public int Id
        {
            get => _id;
            init => _id = value > -1 ? value : throw new ArgumentOutOfRangeException("Значение Id не может быть отрицательным.");
        }

        [JsonPropertyName("FirstName")]
        /// <summary>
        /// Свойство для доступа к имени пользователя. Имя не должно быть пустой или null строкой.
        /// При установке значения, свойство удаляет начальные и конечные пробелы из имени.
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            init => _firstName = !string.IsNullOrEmpty(value) ? value.Trim() : throw new ArgumentNullException("Имя не должно быть пустой или равной null строкой.");
        }

        [JsonPropertyName("LastName")]
        /// <summary>
        /// Свойство для доступа к фамилии пользователя. Фамилия не должна быть пустой или null строкой.
        /// При установке значения, свойство удаляет начальные и конечные пробелы из фамилии.
        /// </summary>
        public string LastName
        {
            get => _lastName;
            init => _lastName = !string.IsNullOrEmpty(value) ? value.Trim() : throw new ArgumentNullException("Фамилия не должна быть пустой или равной null строкой.");
        }

        [JsonPropertyName("Age")]
        /// <summary>
        /// Свойство возраста пользователя.
        /// Позволяет получить и установить возраст пользователя.
        /// Возраст должен быть больше 0 и меньше или равен 122.
        /// </summary>
        public int Age
        {
            get => _age;
            set => _age = value > 0 && value <= maxPeopleAge ? value : throw new ArgumentOutOfRangeException("Значение возраста должно быть больше 0 и меньше или равно 122.");
        }

        /// <summary>
        /// Конструктор класса пользователь.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <param name="firstName">Имя пользователя. Должно быть не пустым.</param>
        /// <param name="lastName">Фамилия пользователя. Должно быть не пустым.</param>
        /// <param name="age">Возраст пользователя. Должен быть больше 0 и меньше 123.</param>
        /// <param name="dateCreationUser">Дата создания пользователя.</param>
        public User(int id, string firstName, string lastName, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public User()
        {

        }

        /// <summary>
        /// Переопределение метода сравнения классов пользователей.
        /// Сравнивает текущий экземпляр с указанным объектом и возвращает true, если объекты равны, иначе возвращает false.
        /// Два объекта считаются равными, если они являются экземплярами класса User и все их свойства совпадают, включая Id, FirstName, LastName и Age.
        /// </summary>
        /// <param name="obj">Объект, с которым сравнивается текущий экземпляр.</param>
        /// <returns>True, если объекты равны, иначе False.</returns>
        public override bool Equals(object? obj)
        {
            return obj is not null
                    && obj is User user
                    && user.Id == Id
                    && user.FirstName == FirstName
                    && user.LastName == LastName
                    && user.Age == Age;
        }

        /// <summary>
        /// Переопределенный метод для вычисления хеш-кода объекта User.
        /// Объединяет хеш-коды идентификатора (Id), имени (FirstName), фамилии (LastName) и возраста (Age) пользователя.
        /// Обеспечивает корректное создание хеш-кода для правильной работы с коллекциями, такими как Dictionary.
        /// </summary>
        /// <returns>Целое число, представляющее хеш-код объекта User.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Age);
        }

        /// <summary>
        /// Сравнивает два объекта типа User по их свойству FirstName и определяет порядок сортировки.
        /// </summary>
        /// <param name="left">Первый объект User для сравнения.</param>
        /// <param name="right">Второй объект User для сравнения.</param>
        /// <returns>
        ///   Возвращает true, если свойство FirstName объекта 'left' идет после свойства FirstName объекта 'right'
        ///   в алфавитном порядке (с учетом регистра). В противном случае, возвращает false.
        /// </returns>
        public static bool OrderByFirstNameLeft(User left, User right)
        {
            if(left!=null && right != null)
            {
                return string.Compare(left.FirstName, right.FirstName, StringComparison.Ordinal) > 0;
            }
            else
            {
                throw new ArgumentNullException("Аргументы функции OrderByFirstNameLeft не могут быть null");
            }
        }

        /// <summary>
        /// Сравнивает два объекта типа User по свойству FirstName в обратном порядке.
        /// </summary>
        /// <param name="left">Первый объект User для сравнения.</param>
        /// <param name="right">Второй объект User для сравнения.</param>
        /// <returns>
        ///   Возвращает true, если свойство FirstName объекта 'left' идет до свойства FirstName объекта 'right'
        ///   в алфавитном порядке (с учетом регистра). В противном случае, возвращает false.
        /// </returns>
        public static bool OrderByDescendingNameLeft(User left, User right)
        {
            if (left != null && right != null)
            {
                return !OrderByFirstNameLeft(left, right);
            }
            else
            {
                throw new ArgumentNullException("Аргументы функции OrderByDescendingNameLeft не могут быть null");
            }
           
        }

        /// <summary>
        /// Сравнивает два объекта типа User по свойству Id.
        /// </summary>
        /// <param name="left">Первый объект User для сравнения.</param>
        /// <param name="right">Второй объект User для сравнения.</param>
        /// <returns>
        ///   Возвращает true, если свойство Id объекта 'left' больше, чем свойство Id объекта 'right'.
        ///   В противном случае, возвращает false.
        /// </returns>
        public static bool OrderByIdLeft(User left, User right)
        {
            if (left != null && right != null)
            {
                return left.Id > right.Id;
            }
            else
            {
                throw new ArgumentNullException("Аргументы функции OrderByIdLeft не могут быть null");
            }
        }

        /// <summary>
        /// Сравнивает два объекта типа User по свойству Id в обратном порядке.
        /// </summary>
        /// <param name="left">Первый объект User для сравнения.</param>
        /// <param name="right">Второй объект User для сравнения.</param>
        /// <returns>
        ///   Возвращает true, если свойство Id объекта 'left' меньше, чем свойство Id объекта 'right'.
        ///   В противном случае, возвращает false.
        /// </returns>
        public static bool OrderByIdDescendingIdLeft(User left, User right)
        {
            if (left != null && right != null)
            {
                return !OrderByIdLeft(left, right);
            }
            else
            {
                throw new ArgumentNullException("Аргументы функции OrderByIdDescendingIdLeft не могут быть null");
            }
        }

        /// <summary>
        /// Осуществляет поиск по свойству FirstName объекта User, игнорируя регистр.
        /// </summary>
        /// <param name="user">Объект User, в котором будет осуществляться поиск.</param>
        /// <param name="searchValue">Значение, по которому будет осуществляться поиск.</param>
        /// <returns>
        ///   Возвращает true, если свойство FirstName объекта 'user' содержит значение 'searchValue'
        ///   с учетом регистра. В противном случае, возвращает false.
        /// </returns>
        public static bool SearchByFirstName(User user, string searchValue)
        {
            if (user != null && searchValue != "")
            {
                return user.FirstName.Contains(searchValue, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                throw new ArgumentNullException("Аргументы функции SearchByFirstName не могут быть null или пустыми");
            }
            
        }

        /// <summary>
        /// Переопределение метода ToString для форматированного вывода информации о пользователе.
        /// </summary>
        /// <returns>Строка, представляющая объект класса User в удобочитаемом формате.</returns>
        public override string ToString()
        {
            return $"Id: {Id,-3}\tFirst Name: {FirstName, -10}\tLast Name: {LastName,-10}\tAge: {Age,-3}\tRating: ";
        }
    }
}
