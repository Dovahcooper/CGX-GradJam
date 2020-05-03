using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindHazard : MonoBehaviour
{
    public Vector3 moveVec;

    public float throwForce;

    [Range(2f, 10f)]
    public float amplitude = 2f;

    float xTime;
    Vector3 startLoc;

    // Start is called before the first frame update
    void OnEnable()
    {
        startLoc = transform.localPosition;
        xTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.paused)
            xTime += Time.fixedDeltaTime;
        transform.localPosition = startLoc + moveVec * (Mathf.Sin(xTime) * amplitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        Elemental e;
        if (other.TryGetComponent(out e))
        {
            e.Damage();
            e.GetComponentInParent<Rigidbody>().AddForce(throwForce * Vector3.up, ForceMode.Impulse);
        }
    }
}
