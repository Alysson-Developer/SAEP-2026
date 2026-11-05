using System;
using UnityEngine;
namespace SAEP {
public class Health:MonoBehaviour {
 [SerializeField,Min(1)] int maxHealth=3;
 public int Current {get;private set;}
 public int Maximum=>maxHealth;
 public event Action<int,int> Changed;
 public event Action Died;
 bool dead;
 void Awake(){ResetHealth(maxHealth);}
 public void ResetHealth(int max){maxHealth=Mathf.Max(1,max);Current=maxHealth;dead=false;Changed?.Invoke(Current,maxHealth);}
 public void Damage(int damage){if(dead||damage<=0)return;Current=Mathf.Max(0,Current-damage);Changed?.Invoke(Current,maxHealth);if(Current==0){dead=true;Died?.Invoke();}}
}
}
