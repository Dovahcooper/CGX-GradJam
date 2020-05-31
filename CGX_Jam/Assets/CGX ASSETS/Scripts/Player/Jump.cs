using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XBOX;

public class Jump : MonoBehaviour
{
    public Buttons jumpButton = Buttons.A;

    [Range(4f, 20f)]
    public float firstJumpVel = 10f;
    float secondJumpVel;

    public Rigidbody rb;

    bool jumping;
    bool doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        jumping = doubleJump = false;
        secondJumpVel = 0.8f * firstJumpVel;
    }

    private void OnValidate()
    {
        secondJumpVel = firstJumpVel * 0.75f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!XInput.GetConnected(0))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                addJump();
            }
        }
    }

    public void addJump()
    {
        if (!PauseMenu.paused && Elemental.Alive)
        {
            if (!jumping)
            {
                jumping = true;
                rb.AddForce(firstJumpVel * Vector3.up, ForceMode.Impulse);
            }
            else if (jumping && !doubleJump)
            {
                doubleJump = true;
                rb.AddForce(secondJumpVel * Vector3.up, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            doubleJump = false;
            jumping = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        jumping = true;
    }
}
