namespace HarbourMaster.Models
{
    // Represents the measured state of a surveyed point in the water/terrain
    // system. This object captures the water surface level and the terrain level
    // so downstream code can compute the effective depth at that location.
    public class SurveyResult
    {
        // Water surface height at the survey point.
        public float WaterHeight;

        // Height of the seabed or terrain at the survey point.
        public float TerrainHeight;

        // Calculated depth at this location, measured as the vertical difference
        // between the water surface and the terrain.
        public float Depth;

        // Creates a survey result from the measured water and terrain heights.
        // The depth is derived immediately so callers do not have to repeat the
        // calculation each time they read the result.
        public SurveyResult(
            float waterHeight,
            float terrainHeight)
        {
            WaterHeight = waterHeight;
            TerrainHeight = terrainHeight;

            Depth = waterHeight - terrainHeight;
        }
    }
}