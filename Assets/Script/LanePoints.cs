using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(LanePoints))]
public class LanePointsEditor : Editor
{
    private void OnSceneGUI()
    {
        LanePoints lanePoints = target as LanePoints;
        List<Vector2> points = lanePoints.points;
        Transform transform = lanePoints.transform;
        float z = transform.position.z;

        for (int i = 0; i < lanePoints.points.Count; i++)
        {
            EditorGUI.BeginChangeCheck();

            Vector3 worldPos = transform.TransformPoint(points[i]);
            worldPos.z = z;
            points[i] = (Vector2)transform.InverseTransformPoint(Handles.PositionHandle(worldPos, Quaternion.identity));

            if (EditorGUI.EndChangeCheck())
            {
                lanePoints.RefreshPoints();
                EditorUtility.SetDirty(lanePoints);
            }
        }
    }
}
#endif

[RequireComponent(typeof(EdgeCollider2D))]
public class LanePoints : MonoBehaviour
{
    public static List<LanePoints> instances = new();
    public List<Vector2> points;
    public EdgeCollider2D edgeCollider;

    private void Start()
    {
        RefreshPoints();
    }

    public void RefreshPoints()
    {
        if (!edgeCollider) edgeCollider = GetComponent<EdgeCollider2D>();
        if (!edgeCollider) edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        edgeCollider.points = points.ToArray();
    }

    private void Awake()
    {
        instances.Add(this);
    }
    private void OnDestroy()
    {
        instances.Remove(this);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Vector2 point = transform.TransformPoint(points[i]);
            Gizmos.DrawSphere(point, .1f);
            if (i > 0)
                Gizmos.DrawLine(point, transform.TransformPoint(points[i - 1]));
        }
    }

    public Vector3 GetWorldPoint(int index)
    {
        int pointCount = points.Count;
        if (pointCount < 1) return Vector3.zero;
        if (index < 0) index = 0;
        else if (index >= pointCount) index = pointCount - 1;

        return transform.TransformPoint(points[index]);

    }
}
