using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool pool;

    public List<GameObject> poolObjects;
    public GameObject obj2Pool;
    public int poolSize;

    // Awake is called upon object instantiation
    void Awake()
    {
        pool = this;
    }

    //Start is called before first frame update
    void Start()
    {
        poolObjects = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject temp = (GameObject)Instantiate(obj2Pool);
            temp.SetActive(false);
            poolObjects.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getPooledObject()
    {
        for(int i = 0; i < poolObjects.Count; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
            {
                return poolObjects[i];
            }
        }

        return null;
    }

    public void ReturnPoolObject(GameObject finished)
    {
        finished.SetActive(false);
    }
}
