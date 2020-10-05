using UnityEngine;
using UnityEngine.Events;

namespace GameJamStarterKit.HealthSystem
{
    /// <summary>
    /// A Quick solution to adding health to various types of GameObjects. Has unity events for health changes and death.
    /// </summary>
    public class HealthComponent : MonoBehaviour
    {
        /// <summary>
        /// the current health on this component
        /// </summary>
        public int Health => CurrentHealth;

        /// <summary>
        /// the maximum health for this component
        /// </summary>
        public int MaximumHealth;

        [SerializeField]
        private int CurrentHealth;

        [SerializeField]
        private bool StartWithMaximumHealth = true;

        /// <summary>
        /// returns the current health as a 0 to 1 range
        /// </summary>
        public float NormalizedCurrentHealth => CurrentHealth / (float) MaximumHealth;

        /// <summary>
        /// Called when this HealthComponent loses health
        /// <para>Passes the new current health</para>
        /// </summary>
        public UnityIntEvent OnHealthLost;

        /// <summary>
        /// Called when this HealthComponent no longer has any health
        /// </summary>
        public UnityEvent OnHealthEmpty;

        /// <summary>
        /// Called when this HealthComponent gains health
        /// <para>Passes the new current health</para>
        /// </summary>
        public UnityIntEvent OnHealthGained;

        /// <summary>
        /// Modifies the health by the amount given.
        /// </summary>
        /// <param name="amount"></param>
        private void ModifyHealth(int amount)
        {
            if (amount == 0 || CurrentHealth <= 0)
                return;

            CurrentHealth += amount;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnHealthEmpty.Invoke();
                return;
            }

            if (amount > 0)
            {
                OnHealthGained.Invoke(CurrentHealth);
            }
            else
            {
                OnHealthLost.Invoke(CurrentHealth);
            }
        }

        /// <summary>
        /// applies the amount of damage to this HealthComponent
        /// </summary>
        /// <param name="amount">damage to apply</param>
        public void Damage(int amount)
        {
            ModifyHealth(-amount);
        }

        /// <summary>
        /// applies the amount of healing to this HealthComponent
        /// </summary>
        /// <param name="amount">heal to apply</param>
        public void Heal(int amount)
        {
            ModifyHealth(amount);
        }

        protected virtual void Start()
        {
            if (StartWithMaximumHealth)
                CurrentHealth = MaximumHealth;
        }
    }
}