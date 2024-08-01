using UnityEngine;
using _Project;
public class PlayerAttackSettings : MonoBehaviour
{
    [SerializeField] private float defaultDamage = 1.0f;
    [SerializeField] private float defaultRange = 1.5f;
    [SerializeField] private float defaultCooldown = 1.0f;

    public float CurrentDamage { get; private set; }
    public float CurrentRange { get; private set; }
    public float CurrentCooldown { get; private set; }

    private void Start()
    {
        ResetToDefault();
    }

    public void UpdateWeapon(float damage, float range, float cooldown)
    {
        CurrentDamage = damage;
        CurrentRange = range;
        CurrentCooldown = cooldown;
    }

    public void ResetToDefault()
    {
        CurrentDamage = defaultDamage;
        CurrentRange = defaultRange;
        CurrentCooldown = defaultCooldown;
    }
   
}
