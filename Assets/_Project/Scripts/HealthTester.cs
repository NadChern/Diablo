using UnityEngine;

public class HealthTester : MonoBehaviour
{
    public Health playerHealth;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerHealth.TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            playerHealth.Heal(1);
        }
    }
}

