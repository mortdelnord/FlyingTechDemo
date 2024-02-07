using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.InputSystem;using Cinemachine;public class Player : MonoBehaviour{    [SerializeField] InputActionReference movementInput;    [SerializeField] InputActionReference diveInput;    [SerializeField] InputActionReference brakeInput;    [SerializeField] InputActionReference boostInput;    public CinemachineVirtualCamera vCam;    public TrailRenderer trail1;    public TrailRenderer trail2;    Vector3 localRot;        public Transform playerTransform;    private Rigidbody playerRigidbody;        public bool canBoost = true;    public float boostTimer = 0f;    public float boostTimerMax = 1f;    public float brakeSpeed = 0.5f;    public float brakeRotation = -60f;    public float brakeRotationSpeed = 400f;    public float gravityDown = 9.8f;    public float gravityUp = 15f;    public float gravityAngleDown = 30f;    public float gravityAngleUp = -30f;    public float diveRotation = 60f;    public float diveAcceleration = 2f;    public float xRotationSpeed = 1f;    public float yRotationSpeed = 2f;    public float zRotationSpeed = 5f;    public float xRotationMin = -50f;    public float xRotationMax = 50f;            public float yRotationMin = -60f;    public float yRotationMax = 60f;    public float zReturnSpeed = 1f;    public float allRotationSpeed = 10f;    public float diveRotateSpeed = 1f;    public float speedMultiplierFOV = 2f;    public float fovMin = 40f;    public float fovMax = 120f;    public float trailSpeed = 33f;            public float playerForwardSpeed = 10f;    public float playerAcceleration = 1f;    public float boostSpeed = 20f;    public void Start()    {        playerRigidbody = GetComponent<Rigidbody>();


        Cursor.lockState = CursorLockMode.Locked;        Cursor.visible = false;


    }    public void FixedUpdate()
    {
        Movement();
    }    public void Update()    {


        
            }    public void Movement()
    {


        #region Returns Values to Normal

        playerRigidbody.angularVelocity = Vector3.zero; //Prevents Jiggling When Colliding With Environment
        playerAcceleration = 1f;        allRotationSpeed = 10f;        xRotationSpeed = 1f;
        

        #endregion

        Vector2 axis = movementInput.action.ReadValue<Vector2>();

        float localFOV = Mathf.Clamp(playerRigidbody.velocity.magnitude * speedMultiplierFOV, fovMin, fovMax);
        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, localFOV, Time.deltaTime);

        localRot.x += axis.x * xRotationSpeed;        localRot.y += axis.y * yRotationSpeed;        localRot.z -= axis.x * zRotationSpeed;         Quaternion DirRotation = Quaternion.Euler(localRot.y, localRot.x, localRot.z);  // combines rotations into a Quaternion to be used later
        //Quaternion DirRotation = Quaternion.Euler(localRot.y, localRot.x, 0f) ;
        Quaternion UP = Quaternion.Euler(localRot.y, localRot.x, 0f);


        if (playerRigidbody.velocity.magnitude >= trailSpeed)
        {

            trail1.emitting = true;
            trail2.emitting = true;
        }
        else
        {

            trail1.emitting = false;
            trail2.emitting = false;

        }


        #region Handle Braking
        if (brakeInput.action.inProgress)
        {
            playerAcceleration = brakeSpeed;
            localRot.y = brakeRotation;

            allRotationSpeed = brakeRotationSpeed;


        }
        #endregion

        #region Handle downward acceleration and upwards decceleration

        if (localRot.y >= gravityAngleDown)
        {
            playerRigidbody.AddForce(Vector3.down * gravityDown);

        }
        if (localRot.y <= gravityAngleUp)
        {
            playerRigidbody.AddForce(Vector3.down * gravityUp);
        }


        #endregion

        #region Handle Diving
        if (diveInput.action.inProgress)
        {
            vCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalRecentering.RecenterNow();
            vCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalRecentering.RecenterNow();

            playerAcceleration = diveAcceleration;

            localRot.y = diveRotation;

        }

        #endregion

        #region Handle Z-Axis rotation and realigning and rotations

        if (movementInput.action.ReadValue<Vector2>() != Vector2.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, DirRotation, Time.deltaTime * allRotationSpeed);
        }
        else
        {
            localRot.z = 0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, UP, Time.deltaTime * zReturnSpeed);
        }
        #endregion

        #region Handle Boost

        if (boostTimer >= boostTimerMax)
        {
            boostTimer = 0f;
            canBoost = true;
        }


        if (boostInput.action.inProgress && canBoost)
        {

            playerRigidbody.AddForce(playerTransform.forward * boostSpeed, ForceMode.Impulse);
            canBoost = false;

        }
        if (!canBoost)
        {
            boostTimer += Time.deltaTime;
        }
        #endregion


        playerRigidbody.AddForce(playerAcceleration * playerForwardSpeed * playerTransform.forward); //Main Force propenlling player

    }}