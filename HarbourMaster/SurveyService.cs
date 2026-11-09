using UnityEngine;

using HarbourMaster.Models;

namespace HarbourMaster.Services
{
    // Performs a survey at a world-space point and returns the measured water and
    // terrain data needed to evaluate the local navigation conditions.
    public static class SurveyService
    {
        // Collects the water level and terrain height for the supplied point and
        // wraps them in a SurveyResult object for downstream calculations.
        public static SurveyResult Survey(
            Vector3 point)
        {
            // Get the current water surface level at this location.
            float water =
                WaterService.GetWaterLevel(
                    point);

            // Get the ground/terrain height at the same world-space position.
            float terrain =
                ZoneSystem.instance
                          .GetGroundHeight(
                              point);

            // Package both readings into a single result so the calling code can
            // calculate depth and make navigation decisions consistently.
            return new SurveyResult(
                water,
                terrain);
        }
    }
}