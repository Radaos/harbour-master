using HarbourMaster.Models;
using HarbourMaster.Profiles;
using HarbourMaster.Rendering;
using HarbourMaster.Selection;
using HarbourMaster.Services;
using UnityEngine;

namespace HarbourMaster
{
    // Main in-game tool for toggling a harbour navigation overlay and
    // evaluating the seabed under the cursor against the selected profile.
    public class HarbourMasterTool
    {
        // Whether the overlay/tool is currently enabled.
        private bool _active;

        // Index of the currently selected navigation profile.
        private int _profileIndex;

        // Radius used when painting / evaluating an area around the target point.
        private float _radius = 15f;

        public void Update()
        {
            // Toggle the tool on/off with the configured key.
            if (UnityEngine.Input.GetKeyDown(
                HarbourMasterPlugin.Instance
                    .ToggleKey.Value))
            {
                _active = !_active;

                HarbourMasterPlugin.Log
                    .LogInfo(_active
                        ? "Harbour Master enabled"
                        : "Harbour Master disabled");
            }

            // If the tool is disabled, do nothing else.
            if (!_active)
            {
                return;
            }

            // Escape closes the tool and returns to the normal game state.
            if (UnityEngine.Input.GetKeyDown(
                KeyCode.Escape))
            {
                _active = false;

                HarbourMasterPlugin.Log
                    .LogInfo("Harbour Master disabled");

                return;
            }

            // Only operate when the local player is present.
            if (!Player.m_localPlayer)
                return;

            // Cycle through available navigation profiles with PageUp/PageDown.
            if (UnityEngine.Input.GetKeyDown(KeyCode.PageUp))
            {
                _profileIndex++;

                if (_profileIndex >=
                    ProfileRegistry
                        .GetProfiles()
                        .Count)
                {
                    _profileIndex = 0;
                }
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.PageDown))
            {
                _profileIndex--;

                if (_profileIndex < 0)
                {
                    _profileIndex =
                        ProfileRegistry
                            .GetProfiles()
                            .Count - 1;
                }
            }

            // Allow the active radius to be adjusted with the mouse wheel while
            // holding Ctrl; this gives a quick way to change the survey area.
            if (UnityEngine.Input.GetKey(
                    KeyCode.LeftControl))
            {
                _radius +=
                    UnityEngine.Input
                        .mouseScrollDelta.y;
            }

            // Clamp the radius to a practical range so it remains usable.
            _radius =
                Mathf.Clamp(
                    _radius,
                    5f,
                    100f);

            Vector3 point;

            // Determine the location under the cursor on the seabed.
            if (!SeabedRaycaster
                    .TryGetPoint(
                        out point))
            {
                return;
            }

            // Draw the visual radius marker at the surveyed point.
            HarbourPreviewRenderer
                .DrawRadius(
                    point,
                    _radius);

            // Use the currently selected profile to evaluate the survey data.
            NavigationProfile profile =
                ProfileRegistry.Get(
                    _profileIndex);

            SurveyResult survey =
                SurveyService.Survey(
                    point);

            // Compute the target elevation for this profile based on water depth.
            float targetElevation =
                ProfileService
                    .GetTargetElevation(
                        profile,
                        survey.WaterHeight);

            // Display the current status / overlay information for debugging.
            DrawOverlay(
                profile,
                survey,
                targetElevation);
        }

        // Temporary overlay method used to show profile information in a simple
        // debug form until a more polished HUD is implemented.
        private void DrawOverlay(
            NavigationProfile profile,
            SurveyResult survey,
            float targetElevation)
        {
            // v0.1:
            // replace with proper HUD later

            string text =
                profile.DisplayName
                + " Depth "
                + profile.SafeDepth
                + "m";

            Debug.Log(text);
        }
    }
}