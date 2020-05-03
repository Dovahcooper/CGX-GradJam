using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullets : MonoBehaviour
{
    Vector3 velocity;
    public float speed = 20f;

    Rigidbody rb;

    public Elements type;

    //elapsed time since enabling
    float eTime;

    // Start is called before the first frame update
    void OnEnable()
    {
        eTime = 0f;

        velocity = transform.forward * speed;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = velocity;

        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        renderer.material.color = Elemental.colours[(int)type];
    }

    // Update is called once per frame
    void Update()
    {
        eTime += Time.fixedDeltaTime;

        if(eTime > 5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Boss tryBoss;

        if (other.TryGetComponent<Boss>(out tryBoss))
        {
            tryBoss.Damage(type);
        }

        if(other.tag != "Player")
            gameObject.SetActive(false);
    }
}
