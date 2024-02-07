using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{




    [SerializeField] InputActionReference cameraInput;


    public Transform defaultCameraPos;

    public Transform Player;
    public Camera playerCamera;

    public float MouseSpeed = 3f;
    public float orbitDamping = 10f;

    Vector3 localRot;


    public void Update()
    {
        playerCamera.transform.LookAt(Player);

        transform.position = Player.position;

        Vector2 axis = cameraInput.action.ReadValue<Vector2>();

        localRot.x += axis.x * MouseSpeed;
        localRot.y -= axis.y * MouseSpeed;

        localRot.y = Mathf.Clamp(localRot.y, -100f, 100f);
        //localRot.x = Mathf.Clamp(localRot.x, -100, 100f);

        Quaternion QT = Quaternion.Euler(localRot.y, localRot.x, 0f);
        



       
        transform.rotation = Quaternion.Lerp(transform.rotation, QT, Time.deltaTime * orbitDamping);
        


    }






}
