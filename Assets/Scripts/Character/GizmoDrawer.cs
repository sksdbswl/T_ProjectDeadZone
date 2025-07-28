using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    private Vector3 boxCenter;
    private Vector3 boxSize;
    private Quaternion boxRotation;
    private float displayDuration;
    private float timer;

    public void Initialize(Vector3 center, Vector3 size, Quaternion rotation, float duration )
    {
        boxCenter = center;
        boxSize = size;
        boxRotation = rotation;
        displayDuration = duration;
        timer = 0;
        enabled = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= displayDuration)
        {
            enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (!enabled) return;

        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS( boxCenter, boxRotation, boxSize );
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
