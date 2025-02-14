using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace SCP3199Config
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("ProjectSCP.SCP3199", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        private static ManualLogSource mls;
        public static int SCP3199SpawnRate;

        private void Awake()
        {
            // Plugin startup logic
            SCP3199SpawnRate = Config.Bind("General", "SCP3199SpawnRate", 15, "The spawn rate of SCP3199 (original value in the mod is 40).").Value;
            mls = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_GUID);
            var harmony = new Harmony(PluginInfo.PLUGIN_NAME);
            harmony.PatchAll(typeof(StartPatch));
            Log($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!", LogType.Message);
        }

        public enum LogType
        {
            Message,
            Warning,
            Error,
            Fatal,
            Debug
        }

        internal static void Log(string message, LogType type = LogType.Message)
        {
#if !DEBUG
            if (type == LogType.Debug) {
                mls.LogMessage(message);
                return;
            }
#endif
            switch (type)
            {
                case LogType.Warning: mls.LogWarning(message); break;
                case LogType.Error: mls.LogError(message); break;
                case LogType.Fatal: mls.LogFatal(message); break;
                default: mls.LogMessage(message); break;
            }
        }
    }
}
