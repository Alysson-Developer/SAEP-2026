using System.Collections;
using UnityEngine;
namespace SAEP {
public class EnemySpawner:MonoBehaviour {
 [SerializeField] EnemyShip[] normalPrefabs;
 [SerializeField] EnemyShip bossPrefab;
 [SerializeField] Transform bossSpawn;
 [SerializeField] Transform[] spawnPoints;
 [SerializeField] float horizontalSpawnRange=6f;
 int spawned;bool bossSpawned;
 public void Begin(){StartCoroutine(SpawnRoutine());}
 IEnumerator SpawnRoutine(){var gm=GameManager.Instance;while(spawned<gm.Settings.enemyGoal){yield return new WaitForSeconds(gm.Settings.spawnInterval);if(gm.State!=GameState.Playing)yield break;if(normalPrefabs==null||normalPrefabs.Length==0)yield break;
 Vector3 pos=spawnPoints!=null&&spawnPoints.Length>0?spawnPoints[Random.Range(0,spawnPoints.Length)].position:transform.position+Vector3.right*Random.Range(-horizontalSpawnRange,horizontalSpawnRange);
 Instantiate(normalPrefabs[Random.Range(0,normalPrefabs.Length)],pos,Quaternion.identity);spawned++;}}
 public void CheckBossSpawn(){var gm=GameManager.Instance;if(bossSpawned||gm.DefeatedEnemies<gm.Settings.enemyGoal||spawned<gm.Settings.enemyGoal||bossPrefab==null)return;bossSpawned=true;Instantiate(bossPrefab,bossSpawn?bossSpawn.position:transform.position,Quaternion.identity);}
}
}
