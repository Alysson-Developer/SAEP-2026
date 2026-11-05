using System.Collections;
using UnityEngine;
namespace SAEP {
[RequireComponent(typeof(Collider2D))]
public class PlayerShip:MonoBehaviour {
 [SerializeField] ShipSettings config;
 [SerializeField] Weapon weapon;
 [SerializeField] SpriteRenderer sprite;
 [SerializeField] GameObject shieldVisual;
 [SerializeField] GameObject explosionPrefab;
 [SerializeField] AudioClip deathClip;
 [SerializeField] float screenPadding=.35f;
 [SerializeField] KeyCode shootKey=KeyCode.Space;
 GameManager manager;Camera cam;bool shield;bool immune;bool destroyed;
 public void Initialize(GameManager owner,float immunity){manager=owner;cam=Camera.main;weapon.Configure(config);if(shieldVisual)shieldVisual.SetActive(false);if(immunity>0)StartCoroutine(Invulnerable(immunity));}
 void Update(){if(manager==null||manager.State!=GameState.Playing||destroyed)return;
 Vector2 axis=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;
 float speed=manager.Settings.enemySpeed*manager.Settings.playerSpeedMultiplier;
 Vector3 target=transform.position+(Vector3)(axis*speed*Time.deltaTime);
 if(cam){float z=Mathf.Abs(cam.transform.position.z-transform.position.z);Vector3 min=cam.ViewportToWorldPoint(new Vector3(0,0,z));Vector3 max=cam.ViewportToWorldPoint(new Vector3(1,1,z));target.x=Mathf.Clamp(target.x,min.x+screenPadding,max.x-screenPadding);target.y=Mathf.Clamp(target.y,min.y+screenPadding,max.y-screenPadding);}
 transform.position=target;
 if(Input.GetKey(shootKey)||Input.GetMouseButton(0))weapon.Fire();
 }
 public void GiveShield(){shield=true;if(shieldVisual)shieldVisual.SetActive(true);}
 public void TakeHit(int damage){if(destroyed||immune||manager.State!=GameState.Playing)return;if(shield){shield=false;if(shieldVisual)shieldVisual.SetActive(false);return;}destroyed=true;if(explosionPrefab)Instantiate(explosionPrefab,transform.position,Quaternion.identity);AudioOneShot.Play(deathClip,transform.position);manager.PlayerDestroyed(transform.position);Destroy(gameObject);}
 IEnumerator Invulnerable(float duration){immune=true;float t=0;while(t<duration){if(sprite)sprite.enabled=!sprite.enabled;t+=.12f;yield return new WaitForSeconds(.12f);}if(sprite)sprite.enabled=true;immune=false;}
 void OnTriggerEnter2D(Collider2D other){if(other.GetComponentInParent<EnemyShip>())TakeHit(1);}
}
}
