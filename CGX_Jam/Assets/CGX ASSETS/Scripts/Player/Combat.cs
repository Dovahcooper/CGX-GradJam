using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XBOX;

public class Combat : MonoBehaviour
{
    public Elemental type;
    public Elements element;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        element = type.curElement;

        if (!XInput.GetConnected(0))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Shooting");
                Fire();
            }
        }
    }

    public void Fire()
    {
        GameObject bul = BulletPool.pool.getPooledObject();

        if (bul != null)
        {
            Camera mainCam = Camera.main;
            RaycastHit hit;
            Vector3 newF;
            
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)){
                newF = hit.point - transform.position;
            }
            else
            {
                newF = mainCam.transform.forward;
            }

            bul.transform.position = transform.position;
            bul.transform.forward = newF;
            bul.GetComponent<Bullets>().type = type.curElement;

            bul.SetActive(true);
        }
    }
}
