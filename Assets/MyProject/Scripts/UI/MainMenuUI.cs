using UnityEngine;
using UnityEngine.SceneManagement;
namespace SAEP {
public class MainMenuUI:MonoBehaviour {
 [SerializeField] GameObject controlsPanel;
 public void Play(){Time.timeScale=1;SceneManager.LoadScene("Game");}
 public void ShowControls(){if(controlsPanel)controlsPanel.SetActive(true);}
 public void HideControls(){if(controlsPanel)controlsPanel.SetActive(false);}
 public void Quit(){Application.Quit();}
}
}
