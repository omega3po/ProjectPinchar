using System;
using UnityEngine;

namespace Entities.Base
{
    public abstract class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] protected int maxHealth;
        [SerializeField] private string characterName = "";
        public int MaxHealth => maxHealth;
        public string CharacterName => characterName;
        public int CurrentHealth { get; protected set; }
        public int gridX;
        public int gridY;
        public event Action<int, int> OnHealthChange;

        protected virtual void Awake()
        {
            CurrentHealth = maxHealth;
        }
        
        protected virtual void Start()
        {
            OnHealthChange?.Invoke(CurrentHealth, maxHealth);
        }

        public virtual void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            OnHealthChange?.Invoke(CurrentHealth, maxHealth);
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public virtual void Heal(int amount)
        {
            CurrentHealth += amount;
            OnHealthChange?.Invoke(CurrentHealth, maxHealth);
            if (CurrentHealth > maxHealth)
            {
                CurrentHealth = maxHealth;
            }
        }

        protected abstract void Die();
        
    }

}
