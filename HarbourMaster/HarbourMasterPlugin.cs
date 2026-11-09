using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;

namespace HarbourMaster
{
    // Entry point for the BepInEx plugin. This is the bootstrap object that
    // registers the mod with the game and creates the runtime tool instance.
    [BepInPlugin(
        "com.radaos.harbourmaster",
        "Harbour Master",
        "0.1.0")]
    public class HarbourMasterPlugin : BaseUnityPlugin
    {
        // Singleton reference used by the rest of the mod so other classes can
        // access plugin configuration and logging without passing references around.
        public static HarbourMasterPlugin Instance;

        // Configurable hotkey used to enable/disable the harbour inspection tool.
        internal ConfigEntry<KeyCode> ToggleKey;

        // The active runtime tool that handles the harbour survey and overlay
        // behaviour during gameplay.
        private HarbourMasterTool _tool;

        // Shared logger for the mod. This is used by the tool and other classes to
        // emit consistent diagnostics.
        public static ManualLogSource Log;

        private void Awake()
        {
            // Cache plugin instance for access across the mod.
            Instance = this;
            Log = Logger;

            // Register the toggle hotkey in the config system so it can be changed
            // by the user in the BepInEx config file.
            ToggleKey =
                Config.Bind(
                    "Controls",
                    "ToggleHarbourMaster",
                    KeyCode.F10,
                    "Toggle Harbour Master mode");

            // Create the runtime tool that is responsible for reading input,
            // conducting surveys, and rendering the overlay.
            _tool = new HarbourMasterTool();

            // Simple startup message to confirm the plugin is running.
            Logger.LogInfo(
                "Harbour Master loaded");
        }

        private void Update()
        {
            // Forward the frame update to the tool so it can respond to keypresses
            // and update the survey state each game tick.
            if (_tool != null)
            {
                _tool.Update();
            }
        }
    }
}