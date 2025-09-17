using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    [SerializeField]
    float areaRadius;
    [SerializeField]
    Transform center;

    public bool RandomPoint(out Vector3 result)
    {
        for (int i = 0; i < 1000; i++)
        {
            Vector3 randomPoint = center.position + Random.insideUnitSphere * areaRadius;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    #region GIZMOS
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        int segments = 32;
        Vector3 position = transform.position;

        Vector3 prevPoint = position + new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)) * areaRadius;

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * 2 * Mathf.PI / segments;
            Vector3 nextPoint = position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * areaRadius;

            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }
    #endregion
}