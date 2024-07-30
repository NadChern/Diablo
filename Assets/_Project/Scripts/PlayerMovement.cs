using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _Project
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        public float DistanceToStop { get; private set; } = 1.5f;
        [SerializeField] private float attackCooldown = 1.0f;
        private NavMeshAgent _agent;
        private Camera _camera;
        private Coroutine _moveCoroutine;
        private Coroutine _attackCoroutine;
        // private Inventory _playerInventory;


        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.stoppingDistance = 0.5f;
            // _playerInventory = GetComponent<Inventory>();
        }

        public void Move(Vector3 destination, IInteractable currentTarget)
        {
            _agent.SetDestination(destination);
            if (currentTarget != null)
            {
                StartArrivalCoroutine(currentTarget);
            }
        }

        public void SetCamera(Camera cam) => _camera = cam;

        private void StartArrivalCoroutine(IInteractable currentTarget)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }

            _moveCoroutine = StartCoroutine(CheckArrival(currentTarget));
        }

        private IEnumerator CheckArrival(IInteractable target)
        {
            yield return new WaitUntil(() =>
                _agent.remainingDistance <= _agent.stoppingDistance &&
                !_agent.pathPending);
            target?.OnInteract();
            if (target is Enemy enemy)
            {
               StartAttackCoroutine(enemy);
            }

            _moveCoroutine = null;
        }

        private void StartAttackCoroutine(Enemy enemy)
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }

            Debug.Log("Starting AttackEnemy coroutine.");
            _attackCoroutine = StartCoroutine(AttackEnemy(enemy));
        }

        private IEnumerator AttackEnemy(Enemy enemy)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            while (enemy != null && enemyHealth.CurrentHealth > 0)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) > DistanceToStop)
                {
                    // If the player is out of range, stop attacking
                    break;
                }
                
                Debug.Log("Attacking enemy. Current health: " +
                          enemyHealth.CurrentHealth);
                // int damage = _playerInventory.GetEquippedWeaponDamage();
                enemyHealth.TakeDamage(1); 
                yield return new WaitForSeconds(attackCooldown);
            }

            _attackCoroutine = null;
        }
    }
}
