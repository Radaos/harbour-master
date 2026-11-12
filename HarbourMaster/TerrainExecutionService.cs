using UnityEngine;

namespace HarbourMaster.Services
{
    public static class TerrainExecutionService
    {
        public static void InspectTerrain(
            Vector3 centre,
            float radius)
        {
            Collider[] colliders =
                Physics.OverlapSphere(
                    centre,
                    radius);

            HarbourMasterPlugin.Log.LogInfo(
                "Terrain inspection started");

            HarbourMasterPlugin.Log.LogInfo(
                "Collider count: " +
                colliders.Length);

            for (int i = 0;
                 i < colliders.Length;
                 i++)
            {
                Collider collider =
                    colliders[i];

                string objectName =
                    collider.gameObject.name;

                HarbourMasterPlugin.Log.LogInfo(
                    "Collider: " +
                    objectName);

                Component[] components =
                    collider.gameObject
                        .GetComponents<Component>();

                for (int j = 0;
                     j < components.Length;
                     j++)
                {
                    if (components[j] == null)
                    {
                        continue;
                    }

                    HarbourMasterPlugin.Log.LogInfo(
                        "  Component: " +
                        components[j]
                            .GetType()
                            .FullName);
                }
            }

            HarbourMasterPlugin.Log.LogInfo(
                "Terrain inspection complete");
        }
    }
}