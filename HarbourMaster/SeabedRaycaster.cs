using UnityEngine;

namespace HarbourMaster.Selection
{
    // Casts a ray from the current mouse position and resolves the nearest point
    // on a terrain heightmap beneath the cursor. This is used to identify the
    // seabed location for surveying and preview rendering.
    public static class SeabedRaycaster
    {
        // Tries to find the world point under the mouse cursor that intersects a
        // terrain heightmap. Returns true when a valid seabed hit was found.
        public static bool TryGetPoint(
            out Vector3 point)
        {
            // Default to zero in case no valid hit is found.
            point = Vector3.zero;

            // Grab the active camera from the game's camera singleton. This is used
            // to convert mouse screen coordinates into a world-space ray.
            Camera camera =
                GameCamera.instance
                          .gameObject
                          .GetComponent<Camera>();

            if (camera == null)
                return false;

            // Build a ray from the current mouse position in screen space.
            Ray ray =
                camera.ScreenPointToRay(
                    UnityEngine.Input.mousePosition);

            // Query all colliders along the ray within a reasonable distance. The
            // terrain is usually represented by a heightmap, but other objects may
            // also fall within the hit list.
            RaycastHit[] hits =
                Physics.RaycastAll(
                    ray,
                    200f);

            // Track the nearest valid heightmap hit so the selected point is the
            // closest seabed intersection rather than a more distant object.
            float best =
                float.MaxValue;

            bool found =
                false;

            for (int i = 0;
                 i < hits.Length;
                 i++)
            {
                RaycastHit hit =
                    hits[i];

                // Only consider hits that originate from a heightmap component.
                // This filters out non-terrain colliders and keeps selection on the seabed.
                Heightmap map =
                    hit.collider
                       .GetComponent<Heightmap>();

                if (map == null)
                    continue;

                if (hit.distance < best)
                {
                    best =
                        hit.distance;

                    point =
                        hit.point;

                    found =
                        true;
                }
            }

            // Return whether a valid seabed point was found under the mouse cursor.
            return found;
        }
    }
}