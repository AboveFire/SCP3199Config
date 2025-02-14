using HarmonyLib;
using static LethalBestiary.Modules.Enemies;

namespace SCP3199Config
{
    internal class StartPatch
    {
        [HarmonyPrefix, HarmonyPatch(typeof(GameNetworkManager), "Start")]
        static void Start_Patch()
        {
            ChangeSpawnRateSCP3199();
        }

        private static void ChangeSpawnRateSCP3199()
        {
            foreach (SpawnableEnemy spawnableEnemy in spawnableEnemies)
            {
                if (spawnableEnemy.enemy.enemyName == "scp3199")
                {
                    Plugin.Log("Changed spawn rate of Scp-3199");
                    foreach (var rarity in spawnableEnemy.levelRarities.Keys)
                    {
                        spawnableEnemy.levelRarities[rarity] = Plugin.SCP3199SpawnRate;
                    }
                }
            }
        }
    }
}
