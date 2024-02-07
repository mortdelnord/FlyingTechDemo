using System.Collections;


        Cursor.lockState = CursorLockMode.Locked;


    }
    {
        Movement();
    }


        
        
    {


        #region Returns Values to Normal

        playerRigidbody.angularVelocity = Vector3.zero; //Prevents Jiggling When Colliding With Environment
        playerAcceleration = 1f;
        

        #endregion

        Vector2 axis = movementInput.action.ReadValue<Vector2>();

        float localFOV = Mathf.Clamp(playerRigidbody.velocity.magnitude * speedMultiplierFOV, fovMin, fovMax);
        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, localFOV, Time.deltaTime);

        localRot.x += axis.x * xRotationSpeed;
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

    }