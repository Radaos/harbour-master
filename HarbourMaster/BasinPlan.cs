using UnityEngine;

namespace HarbourMaster.Models
{
    // Represents a single harbour planning area: a circular zone centred on a
    // world-space position and associated with a specific navigation profile.
    //
    // This object is intended to describe the area that is being evaluated or
    // protected for a given approach, basin, or anchorage configuration.
    public class BasinPlan
    {
        // The world-space centre of the basin or survey area.
        // This is the reference point from which the radius is measured.
        public Vector3 Centre;

        // Radius of the circular area covered by this plan.
        // Used to determine the bounds of the survey or safe operating zone.
        public float Radius;

        // The navigation profile that defines the rules for this basin.
        // For example, safe depth, clearance, or route-specific constraints.
        public NavigationProfile Profile;

        // Creates a basin plan for a specific centre, radius, and profile.
        // This keeps the configuration self-contained and easy to pass around
        // as a single unit of analysis or rendering logic.
        public BasinPlan(
            Vector3 centre,
            float radius,
            NavigationProfile profile)
        {
            Centre = centre;
            Radius = radius;
            Profile = profile;
        }
    }
}