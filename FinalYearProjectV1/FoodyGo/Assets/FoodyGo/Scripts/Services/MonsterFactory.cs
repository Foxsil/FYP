using UnityEngine;
using packt.FoodyGO.Database;

namespace packt.FoodyGO.Services
{
    public static class MonsterFactory
    {
        public static string[] names = {
            "BearB",
            "PigA",
            "BunnyC",
            "DogA",
            "momo_n4",
            "momo_n5",
           
            };

        public static string[] skills =
        {
            "French",
            "Chinese",
            "Sushi",
            "Breakfast",
            "Hamburger",
            "Indian",
            "BBQ",
            "Mexican",
            "Cajun",
            "Thai",
            "Italian",
            "Fish",
            "Beef",
            "Bacon",
            "Hog",
            "Chicken"
        };

        public static int power = 0;
        public static int level = 0;
        public static int str = 0;
        public static int monsterPrefabID = 0;

        public static Monster CreateRandomMonster()
        {
            level = Random.Range(1, 10);
            str = level + Random.Range(0, 3);
            power = str * Random.Range(10, 12);

            var monster = new Monster
            {


                Name = BuildName(),
                PrefabID = monsterPrefabID,
                Skills = BuildSkills(),
                Level = level,
                Power = power,

                HP = level * Random.Range(10, 12),
                MP = level * Random.Range(10, 12),
                Str = str,
                Phy = level + Random.Range(0, 3),
                Int = level + Random.Range(0, 3),
                Spr = level + Random.Range(0, 3),
                Luk = level + Random.Range(0, 3),
                Spd = level + Random.Range(0, 3),

        

            };
            return monster;
        }



        private static string BuildSkills()
        {
            var max = skills.Length - 1;
            return skills[Random.Range(0, max)] + "," + skills[Random.Range(0, max)];
        }

        private static string BuildName()
        {
            var max = names.Length - 1;
            monsterPrefabID = Random.Range(0, names.Length);
            Debug.Log(monsterPrefabID);
            return names[monsterPrefabID];//[Random.Range(0, max)] + " " + names[Random.Range(0, max)];
        }
    }
}
