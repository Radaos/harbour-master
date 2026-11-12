using UnityEngine;

namespace HarbourMaster.Rendering
{
    public static class HarbourPreviewRenderer
    {
        public static void DrawRadius(
            Vector3 center,
            float radius)
        {
            const int segments = 96;

            for (int i = 0;
                 i < segments;
                 i++)
            {
                float angle0 =
                    ((float)i / segments) *
                    Mathf.PI * 2f;

                float angle1 =
                    ((float)(i + 1) / segments) *
                    Mathf.PI * 2f;

                Vector3 p1 =
                    center +
                    new Vector3(
                        Mathf.Cos(angle0) * radius,
                        0.25f,
                        Mathf.Sin(angle0) * radius);

                Vector3 p2 =
                    center +
                    new Vector3(
                        Mathf.Cos(angle1) * radius,
                        0.25f,
                        Mathf.Sin(angle1) * radius);

                Debug.DrawLine(
                    p1,
                    p2,
                    Color.green,
                    0f,
                    false);
            }

            // Horizontal centre marker

            Debug.DrawLine(
                center +
                    new Vector3(-2f, 0.5f, 0f),
                center +
                    new Vector3(2f, 0.5f, 0f),
                Color.red,
                0f,
                false);

            Debug.DrawLine(
                center +
                    new Vector3(0f, 0.5f, -2f),
                center +
                    new Vector3(0f, 0.5f, 2f),
                Color.red,
                0f,
                false);

            // Vertical survey pole

            Debug.DrawLine(
                center,
                center +
                    Vector3.up * 10f,
                Color.yellow,
                0f,
                false);

            // Small flag at the top of the pole

            Debug.DrawLine(
                center +
                    Vector3.up * 10f,
                center +
                    Vector3.up * 10f +
                    Vector3.right * 2f,
                Color.yellow,
                0f,
                false);
        }
    }
}