using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PPP_Lab10
{

    /// <summary>
    /// Класс создан так как XML-сериализатор не может обработать объект типа RatingSystem, потому что в RatingSystem есть поле UserRatings, которое представляет собой словарь (Dictionary).
    /// Кроме того, Json серилизация/десерилизация может без проблем работать с классом RatingSystem даже когда у него есть поле с словарём.
    /// </summary>
    [Serializable]
    public class UserRatingData
    {
        [JsonPropertyName("User")]
        public User User { get; set; }

        [JsonPropertyName("Rating")]
        public int Rating { get; set; }

        public UserRatingData()
        {
        }

    }
}
