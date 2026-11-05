using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace SAEP {
public class HUDController:MonoBehaviour {
 [SerializeField] TMP_Text enemiesLabel,bossLabel;
 [SerializeField] Image[] lifeIcons;
 [SerializeField] Sprite aliveSprite,lostSprite;
 GameManager gm;
 void Start(){gm=GameManager.Instance;gm.Changed+=Refresh;Refresh();}
 void Refresh(){if(!gm)return;if(enemiesLabel)enemiesLabel.text=$"{gm.DefeatedEnemies}/{gm.Settings.enemyGoal} piratas simples derrotados";if(bossLabel)bossLabel.text=$"{gm.DefeatedBosses}/1 Chefe dos piratas derrotado";for(int i=0;i<lifeIcons.Length;i++){if(lifeIcons[i])lifeIcons[i].sprite=i<gm.Lives?aliveSprite:lostSprite;}}
 void OnDestroy(){if(gm)gm.Changed-=Refresh;}
}
}
