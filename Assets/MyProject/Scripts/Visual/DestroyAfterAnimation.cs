using UnityEngine;
namespace SAEP {
public class DestroyAfterAnimation:MonoBehaviour {
 [SerializeField,Min(.01f)] float lifetime=.6f;
 void Start(){Destroy(gameObject,lifetime);}
}
}
