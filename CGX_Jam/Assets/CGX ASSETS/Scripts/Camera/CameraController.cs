using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XBOX;

public class CameraController : MonoBehaviour
{
    Stick rightStick;
    public bool cameraLockMode = false;

    public Transform lockPosition;

    Camera mainCam;

    Vector2 camControl;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!PauseMenu.paused && Elemental.Alive)
        {
            if (!cameraLockMode)
            {
                InputChecks();
            }
            else
            {
                CameraLockMove();
            }

            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                SwitchCamMode();
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void InputChecks()
    {
        if (XInput.GetConnected(0))
        {
            XInput.DownloadPackets(1);
            rightStick = XInput.GetRightStick(0);

            camControl.x += rightStick.xAxis;
            camControl.y += -rightStick.yAxis;

            //Debug.Log(rightStick.xAxis + ", " + rightStick.yAxis);

            CameraMove(camControl);

        }
        else
        {
            camControl.x += Input.GetAxis("Mouse X");
            camControl.y += -Input.GetAxis("Mouse Y");

            CameraMove(camControl);
        }
    }

    public void SwitchCamMode()
    {
        cameraLockMode = !cameraLockMode;
    }

    void CameraMove(Vector2 camLook)
    {
        Vector2 rot;

        rot.y = camLook.x;

        rot.x = Mathf.Clamp(camLook.y, -30.0f, 85.0f);

        Quaternion camRotate = Quaternion.Euler(rot.x, rot.y, 0f);
        transform.rotation = camRotate;
    }

    void CameraLockMove()
    {
        transform.forward = Vector3.Normalize(lockPosition.position - transform.position);
        mainCam.transform.localPosition = new Vector3(0.0f, 0.0f, -6.0f);
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
