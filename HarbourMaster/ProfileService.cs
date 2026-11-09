using HarbourMaster.Models;

namespace HarbourMaster.Services
{
    // Provides profile-specific calculations used to convert raw water depth into
    // a usable target elevation for the selected navigation constraints.
    public static class ProfileService
    {
        // Calculates the target ground/elevation reference for a profile based on
        // the current water level and the minimum safe depth requirement.
        //
        // Example: if the water surface is at 10m and the profile requires a safe
        // depth of 4.5m, the target elevation becomes 5.5m. This indicates the
        // highest terrain point that should remain below the vessel's keel
        // under the given profile.
        public static float GetTargetElevation(
            NavigationProfile profile,
            float waterLevel)
        {
            return
                waterLevel -
                profile.SafeDepth;
        }
    }
}