using _Project;
using UnityEngine;


public class HealthTester : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    private void Update()
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

