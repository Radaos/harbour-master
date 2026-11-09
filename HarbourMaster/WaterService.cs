using UnityEngine;

namespace HarbourMaster.Services
{
    // WaterService is currently a lightweight abstraction over the water level
    // query used by the survey pipeline. It isolates the source of water-height
    // data so the rest of the system does not depend on a specific implementation.
    public static class WaterService
    {
        // Returns the current water surface level at the supplied world-space point.
        //
        // This is deliberately a placeholder for now. The eventual implementation
        // should derive the water height from the game world, terrain, or a custom
        // water simulation depending on the project's runtime needs.
        public static float GetWaterLevel(
            Vector3 point)
        {
            // Temporary placeholder until a real water-height source is integrated.
            return 30f;
        }
    }
}