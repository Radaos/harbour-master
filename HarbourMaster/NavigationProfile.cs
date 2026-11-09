namespace HarbourMaster.Models
{
    // Represents a named set of navigation constraints used to evaluate a route
    // or berth plan. Different profiles can model different vessel types or
    // operating conditions without changing the rest of the system.
    public class NavigationProfile
    {
        // Unique identifier for the profile, used to look it up in the registry.
        public string Id;

        // User-facing name shown in overlays or debug output.
        public string DisplayName;

        // Minimum safe depth required for navigation under this profile.
        public float SafeDepth;

        // Required channel width for the route or approach area.
        public float ChannelWidth;

        // Minimum turning radius expected for manoeuvring under this profile.
        public float TurningRadius;

        // Creates a profile with the specific parameters needed to evaluate a
        // proposed harbour route or basin against the selected operating rules.
        public NavigationProfile(
            string id,
            string displayName,
            float safeDepth,
            float channelWidth,
            float turningRadius)
        {
            Id = id;
            DisplayName = displayName;
            SafeDepth = safeDepth;
            ChannelWidth = channelWidth;
            TurningRadius = turningRadius;
        }
    }
}