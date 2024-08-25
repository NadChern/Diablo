
using UnityEngine;

public class WeaponVisualController : MonoBehaviour
{
   [SerializeField] private Transform[] _gunTransforms;
   
   [SerializeField] private Transform _shotgun;
   [SerializeField] private Transform _subMachineGun;
   [SerializeField] private Transform _rifle;
   [SerializeField] private string _defaultWeaponId = "6"; 
   private Transform _currentWeapon;
   private void Start()
   {
      // Equip shotgun by default at the start
      EquipWeapon(_defaultWeaponId);
   }

   public void EquipWeapon(string weaponId)
   {
      SwitchOffGuns();
      switch (weaponId)
      {
         case "6":
            _shotgun.gameObject.SetActive(true);
            _currentWeapon = _shotgun;
            break;
         case "4":
            _rifle.gameObject.SetActive(true);
            _currentWeapon = _rifle;
            break;
         case "3":
            _subMachineGun.gameObject.SetActive(true);
            _currentWeapon = _subMachineGun;
            break;
         default:
            Debug.LogWarning("Weapon ID not recognized: " + weaponId);
            break;
      }
   }
   private void SwitchOffGuns()
   {
      for (int i = 0; i < _gunTransforms.Length; i++)
      {
         _gunTransforms[i].gameObject.SetActive(false);
      }
   }
}
