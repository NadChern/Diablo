using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using _Project;

[RequireComponent(typeof(NavMeshAgent))]
public class BotBehavior : MonoBehaviour
{
    [SerializeField] protected float detectionRange = 3.0f;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] protected float baseDamage = 1.0f;
    [SerializeField] protected float minAttackCooldown = 1.0f;
    [SerializeField] protected float maxAttackCooldown = 2.0f;
    [SerializeField] private PlayerAttackSettings _attackSettings;
    [SerializeField] protected float initialAttackDelay = 1.5f; 
    protected Transform Player;
    protected NavMeshAgent AgentBot;
    protected Coroutine AttackCoroutine;

    protected virtual void Start()
    {
        AgentBot = GetComponent<NavMeshAgent>();
        StartCoroutine(CheckPlayerDistance());
    }

    private IEnumerator CheckPlayerDistance()
    {
        while (true)
        {
            if (Player == null)
            {
                yield return new WaitForSeconds(0.1f);
                continue;
            }

            float distanceToPlayer =
                Vector3.Distance(transform.position, Player.position);
            if (distanceToPlayer <= detectionRange)
            {
                Debug.Log("Player detected within range. Moving towards player.");
                AgentBot.SetDestination(Player.position);
                if (distanceToPlayer <= attackRange)
                {
                    if (AttackCoroutine == null)
                    {
                        Debug.Log("Enemy starting attack on player.");
                        AttackCoroutine = StartCoroutine(AttackPlayerWithDelay());
                    }
                }
                else if (AttackCoroutine != null)
                {
                    Debug.Log("Enemy stopping attack on player.");
                    StopCoroutine(AttackCoroutine);
                    AttackCoroutine = null;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    
    private IEnumerator AttackPlayerWithDelay()
    {
        // Initial delay before starting the attack coroutine
        yield return new WaitForSeconds(initialAttackDelay);
        AttackCoroutine = StartCoroutine(AttackPlayer());
    }
    protected virtual IEnumerator AttackPlayer()
    {
        while (Player != null && Vector3.Distance(transform.position, Player.position) <=
               attackRange)
        {
            Health playerHealth = Player.GetComponent<Health>();
            if (playerHealth != null && playerHealth.CurrentHealth > 0)
            {
                float damageResistance = _attackSettings.CurrentDamageResistance;
                float effectiveDamage = baseDamage * (1 - damageResistance);
                Debug.Log(
                    $"Enemy attacking player. Base Damage: {baseDamage}, Damage Resistance: {damageResistance}, Effective Damage: {effectiveDamage}");
                playerHealth.TakeDamage(effectiveDamage);

                // Calculate a random delay for the next attack
                float randomCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                yield return new WaitForSeconds(randomCooldown);
            }
            else
            {
                Debug.Log("Player is dead or destroyed. Stopping attack.");
                break;
            }
        }

        Debug.Log("Stopping AttackPlayer coroutine.");
        AttackCoroutine = null;
    }

    public void SetPlayer(Transform player)
    {
        Player = player;
    }
}

    