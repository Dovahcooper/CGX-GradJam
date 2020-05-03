using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHazard : MonoBehaviour
{
    float lerpTime;

    [Range(-1, 1)]
    public float lerpSpeed = 0.4f;

    float iFrame;
    bool checkIFrames;

    bool shouldLerp = false;

    [Range(0f, 1f)]
    public float maxIframe = 0.4f;

    const float fullheight = -38.5f;
    public float startHeight = -50f;

    // Onenable is called when the object is set to active
    void OnEnable()
    {
        lerpTime = 0f;

        transform.position = new Vector3(0f, startHeight, 0f);

        iFrame = 0f;
        checkIFrames = false;

        shouldLerp = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shouldLerp)
        {
            lerpTime += Time.fixedDeltaTime * lerpSpeed;
            transform.position = new Vector3(0f, Mathf.Lerp(startHeight, fullheight, lerpTime));

            if(lerpTime >= 1f)
            {
                shouldLerp = false;
            }
        }

        if (checkIFrames)
        {
            iFrame += Time.fixedDeltaTime;
        }

        if(lerpTime <= 0f && lerpSpeed < 0f)
        {
            gameObject.SetActive(false);
        }
    }

    public void EndHazard()
    {
        lerpSpeed *= -1f;
        shouldLerp = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            checkIFrames = true;
            iFrame = 0f;
            other.GetComponent<Elemental>().Damage();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(iFrame >= maxIframe)
        {
            if (other.tag == "Player")
            {
                iFrame = 0f;
                other.GetComponent<Elemental>().Damage();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            iFrame = 0f;
            checkIFrames = false;
        }
    }

    private void OnDisable()
    {
        shouldLerp = false;
    }
}
