using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(LineRenderer))]
    public class CircleRenderer : MonoBehaviour
    {
        public int steps = 100;
        public float radius = 1.0f;
        private LineRenderer lineRenderer;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            DrawCircle();
        }

        void DrawCircle()
        {
            lineRenderer.positionCount = steps + 1; // Ensure the circle closes by adding an extra point
            lineRenderer.useWorldSpace = false; // Use local space coordinates
            lineRenderer.loop = true; // Close the circle

            float angle = 2 * Mathf.PI / steps;

            for (int i = 0; i <= steps; i++)
            {
                float x = Mathf.Cos(i * angle) * radius;
                float z = Mathf.Sin(i * angle) * radius;
                lineRenderer.SetPosition(i, new Vector3(x, 0, z));
            }
        }
    }
    