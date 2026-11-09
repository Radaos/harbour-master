using UnityEngine;

namespace HarbourMaster.Models
{
    // Represents a single planned navigation channel between two points.
    //
    // This is a lightweight value object describing a corridor that should be
    // analysed or rendered according to the rules in the selected profile.
    public class ChannelPlan
    {
        // Start point of the channel in world coordinates.
        // This is the beginning of the intended safe navigation corridor.
        public Vector3 Start;

        // End point of the channel in world coordinates.
        // This defines the destination or terminal point of the route.
        public Vector3 End;

        // The navigation profile that applies to this channel.
        // It governs the depth, clearance, and route constraints used for
        // evaluating the channel's suitability.
        public NavigationProfile Profile;

        // Creates a channel plan from a start point, end point, and profile.
        // This allows channel definitions to be passed around as a single object
        // instead of managing the three values separately.
        public ChannelPlan(
            Vector3 start,
            Vector3 end,
            NavigationProfile profile)
        {
            Start = start;
            End = end;
            Profile = profile;
        }
    }
}