using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static Action<PlayerController> onPlayerSpawn;

    [SerializeField] float damping = 2f;
    [SerializeField] float positionY;
    [SerializeField] float positionZ;

    PlayerController controller;

    void AssignTarget(PlayerController controller)
    {
        this.controller = controller;
    }

    void LateUpdate()
    {
        if (controller == null) return;
        var moveToPosition = new Vector3(controller.transform.position.x, controller.transform.position.y + positionY, controller.transform.position.z + positionZ);
        transform.position = Vector3.Lerp(transform.position, moveToPosition, damping * Time.deltaTime);
    }

    void OnEnable()
    {
        onPlayerSpawn += AssignTarget;
    }
    void OnDisable()
    {
        onPlayerSpawn -= AssignTarget;
    }
}
