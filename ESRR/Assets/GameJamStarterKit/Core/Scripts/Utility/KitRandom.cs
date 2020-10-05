using UnityEngine;

namespace GameJamStarterKit
{
    public static class KitRandom
    {
        public static Vector3 Vector3(Vector3 min, Vector3 max)
        {
            var vector = new Vector3
            {
                x = Random.Range(min.x, max.x),
                y = Random.Range(min.y, max.y),
                z = Random.Range(min.z, max.z)
            };
            return vector;
        }

        public static Quaternion Rotation(Vector3 min, Vector3 max)
        {
            return Quaternion.Euler(Vector3(min, max));
        }

        /// <summary>
        /// returns true or false randomly.
        /// </summary>
        /// <returns></returns>
        public static bool CoinToss()
        {
            return Random.value > 0.5f;
        }

        /// <summary>
        /// Returns 1 or -1 randomly.
        /// </summary>
        /// <returns></returns>
        public static int RandomSign()
        {
            return CoinToss() ? 1 : -1;
        }


        /// <summary>
        /// returns a random coin flip, with the given weight between -1 (false) and 1 (true). 
        /// </summary>
        /// <param name="weight">value between -1 and 1, -1 being a false bias and 1 being a true bias</param>
        /// <returns></returns>
        public static bool WeightedCoinToss(float weight)
        {
            return Random.value + weight > 0.5f;
        }

        /// <summary>
        /// Rolls a dice with a given amount of sides
        /// </summary>
        /// <param name="sides">amount of sides to roll.</param>
        /// <returns></returns>
        public static int DiceRoll(int sides)
        {
            if (sides < 1)
                return 0;

            return Random.Range(1, sides + 1);
        }
    }
}