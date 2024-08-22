using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _Project
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Health _health;
        [SerializeField] private ItemsStorage _itemsStorage;
        [SerializeField] private PlayerAttackSettings _attackSettings;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _attackCooldown = 0.2f;
        [SerializeField] private float _attackRangeCheckInterval = 0.5f;
        
        private Coroutine _attackCoroutine;

        public void Interact(IInteractable interactable)
        {
            if (interactable is Enemy enemy)
            {
                Attack(enemy);
            }
            else if (interactable is Loot loot)
            {
                Collect(loot);
            }
        }

        private void Attack(Enemy enemy)
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }

            _attackCoroutine = StartCoroutine(AttackEnemy(enemy));
        }

        
        private IEnumerator AttackEnemy(Enemy enemy)
        {
            Debug.Log("AttackEnemy coroutine started.");
            Health enemyHealth = enemy.GetComponent<Health>();
            PlayerAnimator playerAnimator = GetComponent<PlayerAnimator>();
            
            while (enemy != null && enemyHealth.CurrentHealth > 0)
            {
                _navMeshAgent.updateRotation = false;
                transform.forward = (enemy.transform.position - transform.position).normalized;
                
                if (Vector3.Distance(transform.position, enemy.transform.position) > _attackSettings.CurrentRange)
                {
                    yield return new WaitForSeconds(_attackRangeCheckInterval);
                }
                playerAnimator.Shoot();
                Debug.Log("Attacking enemy. Current health: " + enemyHealth.CurrentHealth);
                
                enemyHealth.TakeDamage(_attackSettings.CurrentDamage);

                yield return new WaitForSeconds(_attackCooldown);
            }

            Debug.Log("AttackEnemy coroutine ended.");
            _attackCoroutine = null;
        }


      private void Collect(Loot loot)
        {
            Item item = _itemsStorage.GetItemById(loot.Id);

            // if health is not at max ->heal, otherwise add to inventory
            if (item != null)
            {
                if (item is Potion potion && _health.CurrentHealth < _health.MaxHealth)
                {
                    _health.Heal(potion.HealAmount);
                    Debug.Log("Used potion to heal.");
                }
                else
                {
                    _inventory.Put(item.Id);
                }

                loot.DestroyLoot();
            }
        }

        public void DropInteraction()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }
        }
    }
}
