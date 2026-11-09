using System.Collections.Generic;
using HarbourMaster.Models;

namespace HarbourMaster.Profiles
{
    // Central registry for available navigation profiles.
    //
    // This allows the rest of the system to select a profile by index without
    // hard-coding the list in each component. It also acts as the single source
    // of truth for supported vessel or channel operating modes.
    public static class ProfileRegistry
    {
        // Backing storage for the registered navigation profiles.
        // This list is static so the profiles are available globally without any
        // additional setup when the mod is running.
        private static readonly List<NavigationProfile>
            Profiles =
                new List<NavigationProfile>();

        static ProfileRegistry()
        {
            // Seed the registry with default profiles.
            // These initial entries define the built-in operating modes available to the tool.
            Profiles.Add(
                new NavigationProfile(
                    "longship",
                    "Longship",
                    4.5f,
                    8f,
                    15f));

            Profiles.Add(
                new NavigationProfile(
                    "drakkar",
                    "Drakkar",
                    7f,
                    16f,
                    25f));
        }

        // Returns the full list of profiles so callers can enumerate or cycle
        // through them without needing direct access to the backing field.
        public static List<NavigationProfile>
            GetProfiles()
        {
            return Profiles;
        }

        // Retrieves a profile by its index in the registry.
        // This is the lookup used by the tool when the user switches profiles
        // with the PageUp/PageDown keys.
        public static NavigationProfile Get(
            int index)
        {
            return Profiles[index];
        }
    }
}