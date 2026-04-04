using UnityEngine;

public class CameraInitializer : MonoBehaviour
{
    void Start()
    {
        // Move the camera to the saved coordinates
        // We keep Z at -10 for 2D cameras
        transform.position = new Vector3(SceneData.TargetCoordinates.x, SceneData.TargetCoordinates.y, -10f);
    }
}