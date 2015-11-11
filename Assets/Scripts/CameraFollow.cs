using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour {

    public Transform target;
    public Vector2 lookAtOffset;
    public float lookAtZ;
    public float cameraDistance;

    [Range(1, 100)]
    public float smooth = 0.001f;

    // camera follows target on x and y with fixed z
    void Update()
    {
        var targetLookAt = target.position + (Vector3)lookAtOffset;
        targetLookAt.z = lookAtZ;

        var desiredPosition = targetLookAt;
        desiredPosition.z -= cameraDistance;

        var distance = desiredPosition - transform.position;
        var distanceMag = distance.magnitude;
        var delta = distance.normalized * distanceMag / smooth;

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            delta = distance;
        }
#endif

            
        transform.Translate(delta, Space.World);

        transform.LookAt(targetLookAt);
    }
}
