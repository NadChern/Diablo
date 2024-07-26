using _Project;
using UnityEngine;
using UnityEngine.UI;


public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health playerHealth;

    [SerializeField] private Image fillImage; 
    // ref on Image component of Fill area

    private void Start()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += UpdateHealthUI;
        }
    }

    private void UpdateHealthUI()
    {
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.maxValue = playerHealth.MaxHealth;
            healthSlider.value = playerHealth.CurrentHealth;

            // Calculate health percentage
            float healthPercentage = playerHealth.CurrentHealth
                                     / playerHealth.MaxHealth;

            // Set fill color based on health percentage
            if (healthPercentage >= 0.75f)
            {
                fillImage.color = Color.green;
            }
            else if (healthPercentage >= 0.25f)
            {
                fillImage.color = Color.yellow;
            }
            else
            {
                fillImage.color = Color.red;
            }
        }
    }
}
