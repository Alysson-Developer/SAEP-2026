using UnityEngine;
namespace SAEP {
[CreateAssetMenu(menuName="SAEP/Game Settings")]
public class GameSettings : ScriptableObject {
 [Header("Objetivos")] [Min(1)] public int enemyGoal=25;
 [Min(.1f)] public float spawnInterval=3f;
 [Header("Jogador")] [Range(1,20)] public int maximumLives=5;
 [Min(.1f)] public float enemySpeed=2f;
 [Min(1)] public int playerSpeedMultiplier=2;
 [Min(.1f)] public float respawnInvulnerability=3f;
 [Header("Drops")] [Range(0,1)] public float dropChance=.2f;
 [Range(0,1)] public float bonusLifeShare=.2f;
}
}
