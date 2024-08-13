using System.Collections;
using System.Linq;
using UnityEngine;

namespace _Project
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Health _health;
        [SerializeField] private ItemsStorage _itemsStorage;
        [SerializeField] private PlayerAttackSettings _attackSettings;
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

        
// TODO: what to do with cooldown? it doesnt used at all but part of the weapon
        private IEnumerator AttackEnemy(Enemy enemy)
        {
            Debug.Log("AttackEnemy coroutine started.");
            Health enemyHealth = enemy.GetComponent<Health>();

            while (enemy != null && enemyHealth.CurrentHealth > 0)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) > _attackSettings.CurrentRange)
                {
                    yield return new WaitForSeconds(0.5f);
                }

                Debug.Log("Attacking enemy. Current health: " + enemyHealth.CurrentHealth);
                
                enemyHealth.TakeDamage(_attackSettings.CurrentDamage);

                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                yield return new WaitForSeconds(0.2f);
            }

            Debug.Log("AttackEnemy coroutine ended.");
            _attackCoroutine = null;
        }


        // private IEnumerator AttackEnemy(Enemy enemy)
        // {
        //     Health enemyHealth = enemy.GetComponent<Health>();
        //     while (enemy != null && enemyHealth.CurrentHealth > 0)
        //     {
        //         if (Vector3.Distance(transform.position, enemy.transform.position) > _attackSettings.CurrentRange)
        //         {
        //             yield return new WaitForSeconds(.5f);
        //         }
        //
        //         Debug.Log("Attacking enemy. Current health: " + enemyHealth.CurrentHealth);
        //         enemyHealth.TakeDamage(_attackSettings.CurrentDamage);
        //         yield return new WaitForSeconds(_attackSettings.CurrentCooldown);
        //     }
        //
        //     _attackCoroutine = null;
        // }

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
