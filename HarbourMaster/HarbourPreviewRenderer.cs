using UnityEngine;

namespace HarbourMaster.Rendering
{
    // Static helper for drawing a temporary circular preview around a selected
    // world point. This is used as a visual aid when surveying or evaluating a
    // harbour area.
    public static class HarbourPreviewRenderer
    {
        // Draws a ring of line segments representing a circular radius.
        // The ring is created in the XZ plane, which matches the game's terrain
        // navigation plane in most cases.
        public static void DrawRadius(
            Vector3 center,
            float radius)
        {
            // Use a relatively high number of segments so the circle appears smooth
            // even when the radius is large or the camera is close.
            int segments = 64;

            for (int i = 0;
                 i < segments;
                 i++)
            {
                // Angle for the current segment start.
                float a0 =
                    ((float)i / segments)
                    * Mathf.PI
                    * 2f;

                // Angle for the current segment end.
                float a1 =
                    ((float)(i + 1) / segments)
                    * Mathf.PI
                    * 2f;

                // Compute the two vertices that form the current line segment.
                Vector3 p1 =
                    center
                    + new Vector3(
                        Mathf.Cos(a0) * radius,
                        0f,
                        Mathf.Sin(a0) * radius);

                Vector3 p2 =
                    center
                    + new Vector3(
                        Mathf.Cos(a1) * radius,
                        0f,
                        Mathf.Sin(a1) * radius);

                // Draw the preview in-engine using a visible cyan line.
                // This is intentionally temporary and diagnostic in nature.
                Debug.DrawLine(
                    p1,
                    p2,
                    Color.cyan);
            }
        }
    }
}