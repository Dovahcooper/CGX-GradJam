using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XBOX;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    Stick leftStick;

    Rigidbody rb;
    Camera mainCam;

    [Range(10f, 1000f)]
    public float moveSpeed = 15f;

    public GameObject playerMesh;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (XInput.GetConnected(0))
        {
            leftStick = XInput.GetLeftStick(0);

            Move(leftStick.xAxis, leftStick.yAxis);
        }
        else
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Move(moveX, moveY);
        }
    }

    void Move(float x, float y)
    {
        Vector3 pControl = new Vector3(x, 0f, y);

        Vector3 tempForward = mainCam.transform.forward;

        mainCam.transform.forward = Vector3.Normalize(new Vector3(tempForward.x, 0f, tempForward.z));

        Vector3 newForward = mainCam.transform.TransformVector(pControl) * moveSpeed * Time.fixedDeltaTime;

        rb.velocity = new Vector3(newForward.x, rb.velocity.y, newForward.z);

        if (newForward.magnitude > 0f)
        {
            playerMesh.transform.forward = Vector3.Normalize(new Vector3(newForward.x, 0f, newForward.z));
        }
        else
        {
            playerMesh.transform.forward = playerMesh.transform.forward;
        }

        mainCam.transform.forward = tempForward;
    }
}
