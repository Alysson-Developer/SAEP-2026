using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SAEP {
public enum GameState { Playing, Paused, Victory, Defeat }
public class GameManager : MonoBehaviour {
 public static GameManager Instance {get;private set;}
 [SerializeField] GameSettings settings;
 [SerializeField] PlayerShip playerPrefab;
 [SerializeField] Transform playerSpawn;
 [SerializeField] EnemySpawner spawner;
 [SerializeField] GameObject pausePanel, victoryPanel, defeatPanel;
 [SerializeField] AudioSource musicSource;
 public GameSettings Settings=>settings;
 public GameState State {get;private set;}=GameState.Playing;
 public int Lives {get;private set;}
 public int DefeatedEnemies {get;private set;}
 public int DefeatedBosses {get;private set;}
 public PlayerShip Player {get;private set;}
 public event Action Changed;
 void Awake(){Instance=this;Time.timeScale=1;Lives=settings.maximumLives;}
 void Start(){SetPanels();SpawnPlayer(false);spawner.Begin();if(musicSource){musicSource.loop=true;musicSource.Play();}Notify();}
 void Update(){if(Input.GetKeyDown(KeyCode.Escape) && (State==GameState.Playing||State==GameState.Paused))TogglePause();}
 public void SpawnPlayer(bool immune){if(State!=GameState.Playing)return;Player=Instantiate(playerPrefab,playerSpawn.position,Quaternion.identity);Player.Initialize(this,immune?settings.respawnInvulnerability:0);}
 public void PlayerDestroyed(Vector3 position){if(State!=GameState.Playing)return;Lives=Mathf.Max(0,Lives-1);Notify();if(Lives==0){StartCoroutine(EndAfterDelay(GameState.Defeat));return;}StartCoroutine(Respawn());}
 IEnumerator Respawn(){yield return new WaitForSeconds(.7f);SpawnPlayer(true);}
 IEnumerator EndAfterDelay(GameState result){yield return new WaitForSeconds(.7f);Finish(result);}
 public void EnemyKilled(){if(State!=GameState.Playing)return;DefeatedEnemies++;Notify();spawner.CheckBossSpawn();}
 public void BossKilled(){if(State!=GameState.Playing)return;DefeatedBosses++;Notify();Finish(GameState.Victory);}
 public void AddLife(){Lives=Mathf.Min(settings.maximumLives,Lives+1);Notify();}
 public void TogglePause(){if(State==GameState.Playing){State=GameState.Paused;Time.timeScale=0;}else if(State==GameState.Paused){State=GameState.Playing;Time.timeScale=1;}SetPanels();Notify();}
 public void Resume(){if(State==GameState.Paused)TogglePause();}
 void Finish(GameState state){State=state;Time.timeScale=0;SetPanels();Notify();}
 void SetPanels(){if(pausePanel)pausePanel.SetActive(State==GameState.Paused);if(victoryPanel)victoryPanel.SetActive(State==GameState.Victory);if(defeatPanel)defeatPanel.SetActive(State==GameState.Defeat);}
 void Notify()=>Changed?.Invoke();
 public void BackToMenu(){Time.timeScale=1;SceneManager.LoadScene("Menu");}
 public void Restart(){Time.timeScale=1;SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);}
 void OnDestroy(){if(Instance==this){Time.timeScale=1;Instance=null;}}
}
}
