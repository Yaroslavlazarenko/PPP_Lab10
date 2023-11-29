using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PPP_Lab10
{
    public class SerializeOrDeserialize
    {
        /// <summary>
        /// Сериализует объект RatingSystem в формат JSON и сохраняет его в файл.
        /// </summary>
        /// <param name="ratingSystem">Объект RatingSystem для сериализации.</param>
        /// <param name="fileName">Имя файла JSON в формате "name.json", в который будет сохранен JSON.</param>
        /// <exception cref="ArgumentException">Выбрасывается, если объект ratingSystem или его словарь UserRatings пусты.</exception>
        public static void JsonSerialize(RatingSystem ratingSystem, string fileName)
        {
            if (ratingSystem != null && ratingSystem.UserRatings != null && ratingSystem.UserRatings.Count > 0)
            {
                var userRatingsList = new List<UserRatingData>();

                foreach (var keyValuePair in ratingSystem.UserRatings)
                {
                    userRatingsList.Add(new UserRatingData { User = keyValuePair.Key, Rating = keyValuePair.Value });
                }

                string json = JsonSerializer.Serialize(userRatingsList);

                using var outFile = new FileStream(fileName, FileMode.Create);
                using StreamWriter writer = new(outFile);

                writer.Write(json);
            }
            else
            {
                throw new ArgumentException("Ошибка JSON сериализации: 'ratingSystem' или 'ratingSystem.UserRatings' пусты.");
            }
        }

        /// <summary>
        /// Десериализует объект RatingSystem из файла в формате JSON.
        /// </summary>
        /// <param name="fileName">Имя файла JSON в формате "name.json" для десериализации.</param>
        /// <returns>Десериализованный объект RatingSystem.</returns>
        /// <exception cref="FileNotFoundException">Выбрасывается, если файл JSON не найден.</exception>
        /// <exception cref="InvalidOperationException">Выбрасывается, если файл пустой или данные десериализации пусты.</exception>
        public static RatingSystem JsonDeserialize(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Файл JSON не найден.", fileName);
            }

            using var file = new FileStream(fileName, FileMode.Open);
            using StreamReader reader = new(file);

            string json = reader.ReadToEnd();

            if (string.IsNullOrWhiteSpace(json))
            {
                throw new InvalidOperationException("Файл JSON пуст.");
            }

            var userRatingsList = JsonSerializer.Deserialize<List<UserRatingData>>(json);

            if (userRatingsList == null || userRatingsList.Count == 0)
            {
                throw new InvalidOperationException("Список пользователей и рейтинга в файле JSON пуст.");
            }

            RatingSystem ratingSystem = new();

            foreach (var userAndRating in userRatingsList)
            {
                ratingSystem.AddOrUpdateRating(userAndRating.User, userAndRating.Rating);
            }

            return ratingSystem;
        }

        /// <summary>
        /// Сериализует объект RatingSystem в XML-файл.
        /// </summary>
        /// <param name="ratingSystem">Объект RatingSystem для сериализации.</param>
        /// <param name="fileName">Имя файла XML в формате "name.xml", в который будет выполнена сериализация.</param>
        /// <exception cref="ArgumentException">Выбрасывается, если объект ratingSystem или его словарь UserRatings пусты.</exception>
        public static void XmlSerialize(RatingSystem ratingSystem, string fileName)
        {
            if (ratingSystem != null && ratingSystem.UserRatings != null && ratingSystem.UserRatings.Count > 0)
            {
                var userRatingsList = new List<UserRatingData>();

                foreach (var userAndRating in ratingSystem.UserRatings)
                {
                    userRatingsList.Add(new UserRatingData { User = userAndRating.Key, Rating = userAndRating.Value });
                }

                using var xml = new FileStream(fileName, FileMode.Create);
                var serializer = new XmlSerializer(typeof(List<UserRatingData>));

                serializer.Serialize(xml, userRatingsList);
            }
            else
            {
                throw new ArgumentException("Ошибка XML сериализации: 'ratingSystem' или 'ratingSystem.UserRatings' пусты.");
            }
        }

        /// <summary>
        /// Десериализует объект RatingSystem из XML-файла.
        /// </summary>
        /// <param name="fileName">Имя XML файла в формате "name.xml", из которого будет выполнена десериализация.</param>
        /// <returns>Объект RatingSystem, полученный из XML.</returns>
        /// <exception cref="FileNotFoundException">Выбрасывается, если файл XML не найден.</exception>
        /// <exception cref="InvalidOperationException">Выбрасывается, если файл пустой или данные десериализации пусты.</exception>
        public static RatingSystem XmlDeserialize(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Файл XML не найден.", fileName);
            }

            if (new FileInfo(fileName).Length == 0)
            {
                throw new InvalidOperationException("Файл XML пуст.");
            }

            var serializer = new XmlSerializer(typeof(List<UserRatingData>));

            using var xml = new FileStream(fileName, FileMode.Open);

            var usersRatingList = (List<UserRatingData>)serializer.Deserialize(xml);

            if (usersRatingList == null || usersRatingList.Count == 0)
            {
                throw new InvalidOperationException("XML десериализация не удалась или список пользователей пуст.");
            }

            RatingSystem ratingSystem = new RatingSystem();

            foreach (var userAndRating in usersRatingList)
            {
                ratingSystem.AddOrUpdateRating(userAndRating.User, userAndRating.Rating);
            }

            return ratingSystem;
        }

        /// <summary>
        /// Сериализует объект RatingSystem в бинарный файл.
        /// </summary>
        /// <param name="ratingSystem">Объект RatingSystem, который будет сериализован в бинарный файл.</param>
        /// <param name="fileName">Имя бинарного файла в формате "name.bin", в который будет выполнена сериализация.</param>
        /// <exception cref="ArgumentException">Выбрасывается, если объект ratingSystem или его словарь UserRatings пусты.</exception>
        public static void BinarySerialize(RatingSystem ratingSystem, string fileName)
        {
            if (ratingSystem != null && ratingSystem.UserRatings != null && ratingSystem.UserRatings.Count > 0)
            {
                using var stream = File.Open(fileName, FileMode.Create);
                using var writer = new BinaryWriter(stream, Encoding.UTF8, false);

                foreach (var userAndRating in ratingSystem.UserRatings)
                {
                    writer.Write(userAndRating.Key.Id);
                    writer.Write(userAndRating.Key.FirstName);
                    writer.Write(userAndRating.Key.LastName);
                    writer.Write(userAndRating.Key.Age);
                    writer.Write(userAndRating.Value);
                }
            }
            else
            {
                throw new ArgumentException("Ошибка бинарной сериализации: 'ratingSystem' или 'ratingSystem.UserRatings' пусты.");
            }
        }

        /// <summary>
        /// Десериализует объект RatingSystem из бинарного файла.
        /// </summary>
        /// <param name="fileName">Имя бинарного файла в формате "name.bin", который будет десериализован.</param>
        /// <returns>Объект RatingSystem, десериализованный из файла.</returns>
        /// <exception cref="FileNotFoundException">Выбрасывается, если файл не найден.</exception>
        /// <exception cref="InvalidOperationException">Выбрасывается, если файл пустой или данные десериализации пусты.</exception>
        public static RatingSystem BinaryDeserialize(string fileName)
        {
            RatingSystem ratingSystem = new();

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Файл не найден.", fileName);
            }

            if (new FileInfo(fileName).Length == 0)
            {
                throw new InvalidOperationException("Файл XML пуст.");
            }

            using var stream = File.Open(fileName, FileMode.Open);
            using var reader = new BinaryReader(stream, Encoding.UTF8, false);

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                ratingSystem.AddOrUpdateRating(new(reader.ReadInt32(), reader.ReadString(), reader.ReadString(), reader.ReadInt32()), reader.ReadInt32());
            }

            if (ratingSystem.UserRatings == null || ratingSystem.UserRatings.Count == 0)
            {
                throw new InvalidOperationException("Данные бинарной десериализации пусты.");
            }

            return ratingSystem;
        }
    }
}
