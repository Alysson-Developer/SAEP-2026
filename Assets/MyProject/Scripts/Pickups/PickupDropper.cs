using UnityEngine;
namespace SAEP {
public class PickupDropper:MonoBehaviour {
 [SerializeField] Pickup shieldPrefab,lifePrefab;
 public void TryDrop(Vector3 position){var s=GameManager.Instance.Settings;if(Random.value>=s.dropChance)return;var prefab=Random.value<s.bonusLifeShare?lifePrefab:shieldPrefab;if(prefab)Instantiate(prefab,position,Quaternion.identity);}
}
}
