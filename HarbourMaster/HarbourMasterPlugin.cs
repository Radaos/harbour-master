using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;

using HarbourMaster.Models;
using HarbourMaster.Profiles;

using UnityEngine;

namespace HarbourMaster
{
    [BepInPlugin(
        "com.radaos.harbourmaster",
        "Harbour Master",
        "0.1.0")]
    public class HarbourMasterPlugin : BaseUnityPlugin
    {
        public static HarbourMasterPlugin Instance;

        public static ManualLogSource Log;

        internal ConfigEntry<KeyCode> ToggleKey;

        private HarbourMasterTool _tool;

        private GUIStyle _hudStyle;

        private void Awake()
        {
            Instance = this;

            Log = Logger;

            ToggleKey =
                Config.Bind(
                    "Controls",
                    "ToggleHarbourMaster",
                    KeyCode.F10,
                    "Toggle Harbour Master mode");

            _tool = new HarbourMasterTool();

            _hudStyle = new GUIStyle();

            _hudStyle.fontSize = 16;

            _hudStyle.normal.textColor =
                Color.cyan;


            Logger.LogInfo(
                "Harbour Master loaded");
        }

        private void Update()
        {
            if (_tool != null)
            {
                _tool.Update();
            }
        }

        private void OnGUI()
        {
            if (_tool == null)
            {
                return;
            }

            if (!_tool.Active)
            {
                return;
            }

            NavigationProfile profile =
                ProfileRegistry.Get(
                    _tool.ProfileIndex);


            GUI.Box(
                new Rect(
                    10,
                    10,
                    300,
                    160),
                "");

            string text =
                "HARBOUR MASTER ACTIVE\n\n" +
                "Profile: " +
                profile.DisplayName +

                "\nRadius: " +
                _tool.Radius.ToString("F0") +
                "m" +

                "\nScale: " +
                _tool.RadiusMultiplier.ToString("F2") +
                "x";

            if (_tool.HasTarget)
            {
                text +=
                    "\nTarget X: " +
                    _tool.CurrentTarget.x.ToString("F0") +
                    "\nTarget Z: " +
                    _tool.CurrentTarget.z.ToString("F0");
            }
            else
            {
                text += "\nNo terrain target";
            }

            GUI.Label(
                new Rect(
                    20,
                    20,
                    280,
                    140),
                            text,
                _hudStyle);
        }
    }
}