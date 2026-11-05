using UnityEngine;
namespace SAEP {
public enum Faction { Player, Enemy }
[RequireComponent(typeof(Collider2D))]
public class Projectile:MonoBehaviour {
 [SerializeField] Rigidbody2D body;
 [SerializeField,Min(.1f)] float lifetime=6;
 Faction faction;int damage;Vector2 direction;float speed;
 public void Launch(Faction owner,Vector2 heading,float velocity,int hitDamage){faction=owner;direction=heading.normalized;speed=velocity;damage=hitDamage;Destroy(gameObject,lifetime);}
 void FixedUpdate(){if(body)body.MovePosition(body.position+direction*speed*Time.fixedDeltaTime);else transform.position+=(Vector3)(direction*speed*Time.fixedDeltaTime);}
 void OnTriggerEnter2D(Collider2D other){if(faction==Faction.Player){var e=other.GetComponentInParent<EnemyShip>();if(!e)return;e.TakeDamage(damage);}else{var p=other.GetComponentInParent<PlayerShip>();if(!p)return;p.TakeHit(damage);}Destroy(gameObject);}
}
}
