using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float minXPos;
    public float maxXPos;
    public float cameraSpeed = 10f;

    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("CameraFollow: no target set and no object tagged Player was found.");
                return;
            }
            target = player.transform;
        }
    }

    // LateUpdate runs after everything else has moved this frame, so the camera never lags a frame behind the player
    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);

        transform.position = Vector3.MoveTowards(transform.position, newPos, cameraSpeed * Time.deltaTime);
    }
}
