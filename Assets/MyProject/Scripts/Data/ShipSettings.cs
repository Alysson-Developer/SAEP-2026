using UnityEngine;
namespace SAEP {
[CreateAssetMenu(menuName="SAEP/Ship Settings")]
public class ShipSettings : ScriptableObject {
 [Min(1)] public int maxHealth=3;
 [Min(.1f)] public float speed=2f;
 [Min(.1f)] public float shotsPerSecond=1f;
 [Min(1)] public int bulletsPerShot=1;
 [Min(0)] public float bulletSpread=0f;
 [Min(0)] public float bulletSpeed=7f;
 [Min(1)] public int bulletDamage=1;
}
}
