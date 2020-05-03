using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraClipping : MonoBehaviour
{
    const float cameraDistance = 5.0f;

    Vector3 camPos;
    public Vector3 originalPos;

    bool behind = false;
    bool wall = false;
    bool leftRay = false;
    bool rightRay = false;

    public Vector3 noClipPositionBehind;
    public Vector3 noClipPositionLeft;
    public Vector3 noClipPositionRight;

    [HideInInspector]
    public Vector3 forward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        camPos = transform.localPosition.normalized;
        originalPos = transform.localPosition;

        wall = false;
        leftRay = false;
        rightRay = false;
        behind = false;
    }

    // Update is called once per frame
    void Update()
    {
        collisionCheck();
    }

    void collisionCheck()
    {
        noClipPositionBehind = transform.parent.TransformPoint(new Vector3(camPos.x, camPos.y, camPos.z - 0.25f) * cameraDistance);
        noClipPositionLeft = transform.parent.TransformPoint(new Vector3(camPos.x - 0.1f, camPos.y, camPos.z - 0.15f) * cameraDistance);
        noClipPositionRight = transform.parent.TransformPoint(new Vector3(camPos.x + 0.1f, camPos.y, camPos.z - 0.15f) * cameraDistance);

        RaycastHit contact;

        if(Physics.Linecast(transform.parent.position, noClipPositionBehind, out contact) && !wall)
        {
            behind = true;
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(contact.point.x + contact.normal.x * 0.3f, contact.point.y + contact.normal.y * 0.15f, contact.point.z + contact.normal.z * 0.3f), Time.deltaTime * 25.0f);
        }
        else if (Physics.Linecast(transform.parent.position, noClipPositionLeft, out contact) && !rightRay && !behind)
        {
            leftRay = wall = true;
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(contact.point.x + contact.normal.x * 0.2f, contact.point.y + contact.normal.y * 0.15f, contact.point.z + contact.normal.z * 0.2f), Time.deltaTime * 25.0f);
        }
        else if (Physics.Linecast(transform.parent.position, noClipPositionRight, out contact) && !leftRay && !behind)
        {
            rightRay = wall = true;
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(contact.point.x + contact.normal.x * 0.3f, contact.point.y + contact.normal.y * 0.15f, contact.point.z + contact.normal.z * 0.3f), Time.deltaTime * 25.0f);
        }
        else
        {
            wall = false;
            leftRay = false;
            rightRay = false;
            behind = false;

            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime * 15.0f);
        }
    }
}
