using UnityEngine;
namespace SAEP {
public class Weapon:MonoBehaviour {
 [SerializeField] Projectile projectilePrefab;
 [SerializeField] Transform muzzle;
 [SerializeField] Transform[] optionalMuzzles;
 [SerializeField] AudioSource audioSource;
 [SerializeField] AudioClip shotClip;
 [SerializeField] Faction faction;
 [SerializeField] Vector2 shotDirection=Vector2.up;
 ShipSettings config;float nextShot;
 public void Configure(ShipSettings settings){config=settings;}
 public void Fire(){if(config==null||projectilePrefab==null||Time.time<nextShot)return;nextShot=Time.time+1f/config.shotsPerSecond;
 int count=Mathf.Max(1,config.bulletsPerShot);
 for(int i=0;i<count;i++){
 Transform point=optionalMuzzles!=null&&optionalMuzzles.Length>0?optionalMuzzles[i%optionalMuzzles.Length]:(muzzle?muzzle:transform);
 float angle=count==1?0:(i-(count-1)*.5f)*config.bulletSpread;
 Vector2 heading=Quaternion.Euler(0,0,angle)*shotDirection.normalized;
 var bullet=Instantiate(projectilePrefab,point.position,Quaternion.identity);bullet.Launch(faction,heading,config.bulletSpeed,config.bulletDamage);
 }
 if(audioSource&&shotClip)audioSource.PlayOneShot(shotClip);
 }
}
}
