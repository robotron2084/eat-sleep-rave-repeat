using UnityEngine;

namespace GameJamStarterKit.HealthSystem
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// applies to damage to this GameObject's HealthComponent.
        /// <para>Does nothing if this doesn't have a HealthComponent</para>
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="amount">damage to apply</param>
        public static void Damage(this GameObject gameObject, int amount)
        {
            gameObject.GetComponent<HealthComponent>()?.Damage(amount);
        }

        /// <summary>
        /// applies a heal to this GameObject's HealthComponent.
        /// <para>Does nothing if this doesn't have a HealthComponent</para>
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="amount">amount to heal for</param>
        public static void Heal(this GameObject gameObject, int amount)
        {
            gameObject.GetComponent<HealthComponent>()?.Heal(amount);
        }

        /// <summary>
        /// returns the current health for this GameObject's HealthComponent. Returns -1 if it has no component.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static int GetCurrentHealth(this GameObject gameObject)
        {
            var health = gameObject.GetComponent<HealthComponent>();
            return health != null ? health.Health : -1;
        }
    }
}