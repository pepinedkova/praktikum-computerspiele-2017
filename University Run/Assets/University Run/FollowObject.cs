using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform transTarget;
//    public Transform transObject;

    public Vector3 vecOffset = new Vector3(0, 10, 0);
    public float ratioAdept = 0.01f;

    private void Awake()
    {
        transform.position = transTarget.position + vecOffset;
        Vector3 vecToTarget = transTarget.position - transform.position;
        transform.rotation = Quaternion.LookRotation(vecToTarget, transTarget.up);
    }

    private void FixedUpdate ()
    {
        Vector3 posProjected = transform.position - vecOffset;
        Vector3 vecToPlayerProjected = transTarget.position - posProjected;
        Vector3 vecToPlayerRatio = vecToPlayerProjected * ratioAdept;
        transform.position += vecToPlayerRatio;
    }
}