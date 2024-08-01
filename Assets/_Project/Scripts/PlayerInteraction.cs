using System.Collections;
using System.Linq;
using UnityEngine;

namespace _Project
{
    public class Potion : Item
    {
        public float HealAmount;
    }

    public class Gear : Item
    {
        public float DamageResistence;
    }

    public class Weapon : Item
    {
        public float Damage;
        public float Range;
        public float Cooldown;
    }

    public abstract class Item : ScriptableObject
    {
        public string Id;
        public ItemType ItemType;
        public Sprite Sprite;
    }

    public enum ItemType
    {
        Weapon,
        Potion,
        Gear
    }

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
            StartAttackCoroutine(enemy);
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
                if (Vector3.Distance(transform.position, enemy.transform.position) > _attackSettings.CurrentRange)
                {
                    // If the player is out of range, stop attacking
                    break;
                }

                Debug.Log("Attacking enemy. Current health: " + enemyHealth.CurrentHealth);
                enemyHealth.TakeDamage(_attackSettings.CurrentDamage);
                yield return new WaitForSeconds(_attackSettings.CurrentCooldown);
            }

            _attackCoroutine = null;
        }

        private void Collect(Loot loot)
        {
            Item item = _itemsStorage.GetItemById(loot.Id);

            // heal logic 
            // if health is not at max ->heal, otherwise add to inventory
            if (item != null)
            {
                if (item is Potion potion)
                {
                    if (_health.CurrentHealth < _health.MaxHealth)
                    {
                        _health.Heal(potion.HealAmount);
                        Debug.Log("Used potion to heal.");
                    }
                    else
                    {
                        _inventory.Put(item);
                        Debug.Log("Collected potion to inventory.");
                    }
                }
                loot.DestroyLoot();
            }
        }

        internal class ItemsStorage : ScriptableObject
        {
            [SerializeField] private Item[] _items;

            public Item GetItemById(string byId)
            {
                return _items.First(x => x.Id == byId);
            }
        }
    }
}
