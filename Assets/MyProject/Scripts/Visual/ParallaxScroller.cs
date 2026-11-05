using UnityEngine;
namespace SAEP {
public class ParallaxScroller:MonoBehaviour {
 [SerializeField] Transform[] tiles;
 [SerializeField] Vector2 direction=Vector2.down;
 [SerializeField,Min(0)] float speed=.5f;
 [SerializeField,Min(.1f)] float tileHeight=10f;
 [SerializeField] bool continueWhilePaused=false;
 void Update(){if(!continueWhilePaused&&GameManager.Instance&&GameManager.Instance.State!=GameState.Playing)return;
 float dt=continueWhilePaused?Time.unscaledDeltaTime:Time.deltaTime;
 foreach(var tile in tiles){if(!tile)continue;tile.position+=(Vector3)(direction.normalized*speed*dt);}
 if(tiles==null||tiles.Length!=2||direction!=Vector2.down)return;
 Transform a=tiles[0],b=tiles[1];if(!a||!b)return;
 float bottom=Mathf.Min(a.position.y,b.position.y);float top=Mathf.Max(a.position.y,b.position.y);
 if(top-bottom>tileHeight*1.5f)return;
 float cameraY=Camera.main?Camera.main.transform.position.y:0;
 if(a.position.y<cameraY-tileHeight)a.position=new Vector3(a.position.x,b.position.y+tileHeight,a.position.z);
 if(b.position.y<cameraY-tileHeight)b.position=new Vector3(b.position.x,a.position.y+tileHeight,b.position.z);
 }
}
}
