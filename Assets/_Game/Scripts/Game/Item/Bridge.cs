using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class Bridge : MonoBehaviour
{
    [Header("Generation Settings")]
    [SerializeField]
    int stepCount;
    [SerializeField]
    float stepHeight;
    [SerializeField]
    BrickOnBridge brickPref;
    [SerializeField]
    Transform holdPoint;
    public List<BrickOnBridge> steps = new();
    public Transform EndPoint;

    public BrickOnBridge firstStep => steps[0];

    public int BrickCountByColor(COLOR color)
    {
        return steps.Count(x => x.Color == color);
    }

#if UNITY_EDITOR
    public void Generate()
    {
        for (int i = 0; i < stepCount; i++)
        {
            var obj = Instantiate(brickPref, holdPoint);
            obj.transform.localPosition = new Vector3(0, stepHeight * i, i);
            obj.name = brickPref.name;
            steps.Add(obj);
        }

        EndPoint.localPosition = new Vector3(0, stepHeight * stepCount, stepCount * 0.5f);
    }
    public void Clear()
    {
        foreach (var step in steps)
        {
            DestroyImmediate(step.gameObject);
        }
        steps.Clear();
    }
#endif
}

[CustomEditor(typeof(Bridge))]
public class BridgeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        Bridge bridge = (Bridge)target;
        if (GUILayout.Button("Generate"))
        {
            bridge.Generate();
        }
        if (GUILayout.Button("Clear"))
        {
            bridge.Clear();
        }
    }
}
