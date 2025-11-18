using Entities.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Character targetCharacter;
        void Start()
        {
            if (healthSlider == null || targetCharacter == null)
            {
                Debug.Log("Error: Health bar ui -> healthSlider or targetCharacter is null.");
            }
            targetCharacter.OnHealthChange += UpdateHealthBar;
            UpdateHealthBar(targetCharacter.CurrentHealth, targetCharacter.MaxHealth);
        }

        private void UpdateHealthBar(int currentHealth, int maxHealth)
        {
            if (maxHealth == 0) 
            {
                healthSlider.value = 0;
                return;
            }
            healthSlider.value = (float)currentHealth / maxHealth;
        }

        private void OnDestroy()
        {
            if (targetCharacter != null)
            {
                targetCharacter.OnHealthChange -= UpdateHealthBar;
            }
        }
    }
}


