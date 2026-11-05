using UnityEngine;
namespace SAEP {
public enum PickupType { Shield, BonusLife }
[RequireComponent(typeof(Collider2D))]
public class Pickup:MonoBehaviour {
 [SerializeField] PickupType type;
 [SerializeField,Min(.1f)] float duration=3;
 [SerializeField] float fallSpeed=.7f;
 [SerializeField] float sway=.25f;
 Vector3 start;float age;
 void Start(){start=transform.position;Destroy(gameObject,duration);}
 void Update(){age+=Time.deltaTime;transform.position=start+Vector3.down*fallSpeed*age+Vector3.right*Mathf.Sin(age*3)*sway;}
 void OnTriggerEnter2D(Collider2D other){
    var p=other.GetComponent<PlayerShip>();
    if(!p)return;
    if(type==PickupType.Shield)
    p.GiveShield();
    else
     GameManager.Instance.AddLife();
    Destroy(gameObject);}
}
}
