using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using _Project;

[RequireComponent(typeof(NavMeshAgent))]
public class BotBehavior : MonoBehaviour
{
    [SerializeField] private float detectionRange = 2.0f;
    [SerializeField] private float attackRange = 1.5f;
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
                Debug.Log(
                    "Player detected within range. Moving towards player.");
                AgentBot.SetDestination(Player.position);
                if (distanceToPlayer <= attackRange)
                {
                    if (AttackCoroutine == null)
                    {
                        Debug.Log("Enemy starting attack on player.");
                        AttackCoroutine = StartCoroutine(AttackPlayer());
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

    protected virtual IEnumerator AttackPlayer()
    {
        while (Player !=null && Vector3.Distance(transform.position, Player.position) <=
               attackRange)
        {
            Health playerHealth = Player.GetComponent<Health>();
            if (playerHealth != null && playerHealth.CurrentHealth > 0)
            {
                playerHealth.TakeDamage(1); // TODO avoid hardcoding
            }
            else
            {
                // Stop attacking if the player is dead or has been destroyed
                break;
            }
            yield return new WaitForSeconds(1.0f); // Attack cooldown
        }

        AttackCoroutine = null;
    }

    public void SetPlayer(Transform player)
    {
        Player = player;
    }
}
