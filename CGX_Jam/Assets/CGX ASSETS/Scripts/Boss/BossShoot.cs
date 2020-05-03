using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public Boss boss;
    public Elements element;

    public Transform playerPos;

    public float shootInterval = 0.2f;
    float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shootTimer += Time.fixedDeltaTime;
        element = boss.bossType;

        if (boss.dissolveTime >= 1f)
        {
            if (shootTimer >= shootInterval)
            {
                Fire();
                shootTimer = 0f;
            }
        }
    }

    public void Fire()
    {
        GameObject bul = BossBulletPool.pool.getPooledObject();

        if(bul != null)
        {
            Vector3 dir = playerPos.position - transform.position;

            bul.transform.position = transform.position;
            bul.transform.forward = Vector3.Normalize(dir);
            bul.GetComponent<BossBullet>().type = element;

            bul.SetActive(true);
        }
    }
}
