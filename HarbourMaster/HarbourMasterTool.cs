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

        // Multiplier applied to the profile-derived radius.
        private float _radiusMultiplier = 1.0f;

        private Vector3 _currentTarget;

        private bool _hasTarget;

        public float Radius
        {
            get { return _radius; }
        }

        public float RadiusMultiplier
        {
            get { return _radiusMultiplier; }
        }

        public bool Active
        {
            get { return _active; }
        }

        public int ProfileIndex
        {
            get { return _profileIndex; }
        }

        public bool HasTarget
        {
            get { return _hasTarget; }
        }

        public Vector3 CurrentTarget
        {
            get { return _currentTarget; }
        }

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
            if (Player.m_localPlayer == null)
            {
                return;
            }

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


            // Decrease dredge size multiplier.
            if (UnityEngine.Input.GetKeyDown(
                    KeyCode.Minus))
            {
                _radiusMultiplier -= 0.25f;
            }

            // Increase dredge size multiplier.
            if (UnityEngine.Input.GetKeyDown(
                    KeyCode.Equals))
            {
                _radiusMultiplier += 0.25f;
            }

            _radiusMultiplier =
                Mathf.Clamp(
                    _radiusMultiplier,
                    0.5f,
                    4.0f);


            // Decrease radius.
            if (UnityEngine.Input.GetKeyDown(
                    KeyCode.Minus))
            {
                _radius -= 1f;
            }

            // Increase radius.
            if (UnityEngine.Input.GetKeyDown(
                    KeyCode.Equals))
            {
                _radius += 1f;
            }


            Vector3 point;

            // Determine the location under the cursor on the seabed.
            if (!SeabedRaycaster
                    .TryGetPoint(
                        out point))
            {
                _hasTarget = false;
                return;
            }

            _hasTarget = true;
            _currentTarget = point;

            // Draw the visual radius marker at the surveyed point.
            HarbourPreviewRenderer
                .DrawRadius(
                    point,
                    _radius);

            // Use the currently selected profile to evaluate the survey data.
            NavigationProfile profile =
                ProfileRegistry.Get(
                    _profileIndex);

            _radius =
                profile.VesselWidth *
                2f *
                _radiusMultiplier;

            // Execute dredging operation.
            if (UnityEngine.Input.GetKeyDown(
                KeyCode.Delete))
            {
                ExecuteDredge(
                    point,
                    _radius,
                    profile);
            }

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

        private void ExecuteDredge(
            Vector3 centre,
            float radius,
            NavigationProfile profile)
        {
            Collider[] colliders =
                Physics.OverlapSphere(
                    centre,
                    radius);

            HarbourMasterPlugin.Log.LogInfo(
                "Found " +
                colliders.Length +
                " colliders");

            for (int i = 0;
                 i < colliders.Length;
                 i++)
            {
                Collider collider =
                    colliders[i];

                TerrainComp terrainComp =
                    collider.GetComponent<TerrainComp>();

                if (terrainComp != null)
                {
                    HarbourMasterPlugin.Log.LogInfo(
                        "TerrainComp: " +
                        terrainComp.name);
                }

                Heightmap heightmap =
                    collider.GetComponent<Heightmap>();

                if (heightmap != null)
                {
                    HarbourMasterPlugin.Log.LogInfo(
                        "Heightmap: " +
                        heightmap.name);
                }
            }

            Player.m_localPlayer.Message(
                MessageHud.MessageType.Center,
                "Terrain inspection complete");
        }

        // Temporary overlay method used to show profile information in a simple
        // debug form until a more polished HUD is implemented.
        private void DrawOverlay(
            NavigationProfile profile,
            SurveyResult survey,
            float targetElevation)
        {
            string text =
                profile.DisplayName
                + " Depth "
                + profile.SafeDepth
                + "m";

            Debug.Log(text);
        }
    }
}