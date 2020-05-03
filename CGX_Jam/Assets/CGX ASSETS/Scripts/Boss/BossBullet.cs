using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    Vector3 velocity;
    public float speed = 20f;

    Rigidbody rb;

    public Elements type;

    //elapsed time since enabling
    float eTime;

    [Range(0f, 4f)]
    public float intensity;

    // Start is called before the first frame update
    void OnEnable()
    {
        eTime = 0f;

        velocity = transform.forward * speed;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = velocity;

        float factor = Mathf.Pow(2, intensity);

        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        renderer.material.color = Elemental.colours[(int)type] * factor;
    }

    // Update is called once per frame
    void Update()
    {
        eTime += Time.fixedDeltaTime;

        if (eTime > 5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Elemental tryPlayer;

        if (other.TryGetComponent <Elemental>(out tryPlayer))
        {
            tryPlayer.Damage();
        }

        if (other.tag != "Boss" && !other.isTrigger)
            gameObject.SetActive(false);
    }
}
