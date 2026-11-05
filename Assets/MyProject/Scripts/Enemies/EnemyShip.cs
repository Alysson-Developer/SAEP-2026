using UnityEngine;
namespace SAEP {
[RequireComponent(typeof(Health))]
public class EnemyShip:MonoBehaviour {
 [SerializeField] protected ShipSettings config;
 [SerializeField] Weapon weapon;
 [SerializeField] GameObject explosionPrefab;
 [SerializeField] AudioClip deathClip;
 [SerializeField] bool boss;
 [SerializeField] float horizontalRange=2f;
 [SerializeField] float swayFrequency=1.3f;
 [SerializeField] float entryY=3.2f;
 [SerializeField] float screenPadding=.4f;
 [SerializeField] PickupDropper dropper;
 Health health;Camera cam;Vector3 origin;float seed;bool arrived;bool dead;
 public Health Health=>health;
 void Awake(){health=GetComponent<Health>();health.Died+=Die;}
 void Start(){health.ResetHealth(config.maxHealth);weapon.Configure(config);cam=Camera.main;origin=transform.position;seed=Random.Range(0f,6.28f);}
 void Update(){if(GameManager.Instance==null||GameManager.Instance.State!=GameState.Playing||dead)return;
 float speed=boss?GameManager.Instance.Settings.enemySpeed:GameManager.Instance.Settings.enemySpeed;
 Vector3 pos=transform.position;
 if(boss&&!arrived){pos.y=Mathf.MoveTowards(pos.y,entryY,speed*Time.deltaTime);if(Mathf.Abs(pos.y-entryY)<.02f){arrived=true;origin=pos;}}
 else{pos.x=origin.x+Mathf.Sin(Time.time*swayFrequency+seed)*horizontalRange;
 if(!boss)pos.y=origin.y+Mathf.Sin(Time.time*swayFrequency*.65f+seed)*.7f;}
 if(cam){float z=Mathf.Abs(cam.transform.position.z-pos.z);Vector3 min=cam.ViewportToWorldPoint(new Vector3(0,0,z));Vector3 max=cam.ViewportToWorldPoint(new Vector3(1,1,z));pos.x=Mathf.Clamp(pos.x,min.x+screenPadding,max.x-screenPadding);pos.y=Mathf.Clamp(pos.y,min.y+screenPadding,max.y-screenPadding);}
 transform.position=pos;if(!boss||arrived)weapon.Fire();
 }
 public void TakeDamage(int amount){if(!dead)health.Damage(amount);}
 void Die(){if(dead)return;dead=true;if(explosionPrefab)Instantiate(explosionPrefab,transform.position,Quaternion.identity);AudioOneShot.Play(deathClip,transform.position);if(boss)GameManager.Instance.BossKilled();else{if(dropper)dropper.TryDrop(transform.position);GameManager.Instance.EnemyKilled();}Destroy(gameObject);}
 void OnTriggerEnter2D(Collider2D other){var p=other.GetComponentInParent<PlayerShip>();if(p)p.TakeHit(1);}
}
}
