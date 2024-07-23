using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;
    public Health playerHealth;
    public Image fillImage; // ref to Image component of the Fill area
    void Start()
    {
        if (playerHealth != null)
        {
            playerHealth.onHealthChanged.AddListener(UpdateHealthUI);
        }
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.maxValue = playerHealth.GetMaxHealth();
            healthSlider.value = playerHealth.GetCurrentHealth();
            
            // Calculate the health percentage
            float healthPercentage = playerHealth.GetCurrentHealth() 
                                     / (float)playerHealth.GetMaxHealth();

            // Set the fill color based on the health percentage
            if (healthPercentage >= 0.75f)
            {
                fillImage.color = Color.green; // Max health
            }
            else if (healthPercentage >= 0.25f)
            {
                fillImage.color = Color.yellow; // Mid health
            }
            else
            {
                fillImage.color = Color.red; // Low health
            }
        }
    }
}
        
    


