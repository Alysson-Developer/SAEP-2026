using UnityEngine;
using UnityEngine.UI;
namespace SAEP {
public class EnemyHealthBar:MonoBehaviour {
 [SerializeField] Health health;
 [SerializeField] Image fill;
 void Start(){if(!health)health=GetComponentInParent<Health>();if(health){health.Changed+=UpdateBar;UpdateBar(health.Current,health.Maximum);}}
 void UpdateBar(int current,int max){if(fill)fill.fillAmount=max>0?(float)current/max:0;}
 void OnDestroy(){if(health)health.Changed-=UpdateBar;}
}
}
