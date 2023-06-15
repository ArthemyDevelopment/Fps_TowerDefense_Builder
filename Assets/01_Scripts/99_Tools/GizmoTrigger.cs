#if UNITY_EDITOR
using UnityEngine;

public class GizmoTrigger : MonoBehaviour
{

    public Color GizmoColor;
    public bool useScale = true;
    public BoxCollider col;

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;

            Gizmos.matrix = transform.localToWorldMatrix;

        if(useScale)
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
        else
            Gizmos.DrawCube(col.center, col.size);
    }
}

#endif
