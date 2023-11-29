using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPP_Lab10
{
    public class Utils
    {
        private static Random _random = new();

        private static string[] _firstNames = {
            "Sophia", "Jackson", "Olivia", "Liam", "Emma", "Noah", "Ava", "Oliver", "Isabella", "Lucas",
            "Amelia", "Ethan", "Mia", "Aiden", "Harper", "Elijah", "Evelyn", "James", "Abigail", "Alexander",
            "Charlotte", "Benjamin", "Emily", "Henry", "Avery", "Sebastian", "Scarlett", "Michael", "Madison", "Daniel",
            "Elizabeth", "Matthew", "Aria", "Jackson", "Sofia", "David", "Chloe", "Joseph", "Ella", "Carter",
            "Grace", "Owen", "Lily", "Wyatt", "Victoria", "John", "Aubrey", "Jack", "Zoey", "Luke",
            "Penelope", "Jayden", "Lillian", "Dylan", "Addison", "Grayson", "Layla", "Levi", "Natalie", "Isaac",
            "Camila", "Gabriel", "Hannah", "Julian", "Brooklyn", "Mateo", "Zoe", "Anthony", "Nora", "Lincoln",
            "Riley", "Joshua", "Leah", "Christopher", "Savannah", "Andrew", "Audrey", "Theodore", "Claire", "Caleb",
            "Eleanor", "Ryan", "Skylar", "Asher", "Ellie", "Nathan", "Stella", "Thomas", "Paisley", "Leo",
            "Taylor", "Isaiah", "Violet", "Charles", "Hazel", "Josiah", "Lucy", "Christian", "Anna", "Hunter"
        };

        private static string[] _lastNames = {
            "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor",
            "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson",
            "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King",
            "Wright", "Lopez", "Hill", "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter",
            "Mitchell", "Perez", "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins",
            "Stewart", "Sanchez", "Morris", "Rogers", "Reed", "Cook", "Morgan", "Bell", "Murphy", "Bailey",
            "Rivera", "Cooper", "Richardson", "Cox", "Howard", "Ward", "Torres", "Peterson", "Gray", "Ramirez",
            "James", "Watson", "Brooks", "Kelly", "Sanders", "Price", "Bennett", "Wood", "Barnes", "Ross",
            "Henderson", "Coleman", "Jenkins", "Perry", "Powell", "Long", "Patterson", "Hughes", "Flores", "Washington",
            "Butler", "Simmons", "Foster", "Gonzales", "Bryant", "Alexander", "Russell", "Griffin", "Diaz", "Hayes"
        };

        public static string GetRandomFirstName()
        {
            return _firstNames[_random.Next(_firstNames.Length)];
        }

        public static string GetRandomLastName()
        {
            return _lastNames[_random.Next(_firstNames.Length)];
        }

        public static int GetRandomAge(int maxAge)
        {
            return _random.Next(1, maxAge);
        }

        public static int GetRandomRating(int minRatingValue, int maxRatingValue)
        {
            return _random.Next(1, maxRatingValue);
        }
    }
}
