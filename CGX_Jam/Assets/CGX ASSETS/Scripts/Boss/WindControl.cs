using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControl : MonoBehaviour
{
    public List<GameObject> windHazardList;

    [Range(1f, 60f)]
    public float rotateSpeed;
    float yRot;

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            WindHazard temp;
            if(child.TryGetComponent(out temp))
            {
                windHazardList.Add(temp.gameObject);
            }
        }
    }

    void OnEnable()
    {
        for (int i = 0; i < windHazardList.Count; i++)
        {
            windHazardList[i].SetActive(true);
        }
        yRot = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        yRot += Time.fixedDeltaTime * rotateSpeed;

        Vector3 eulerAngle = new Vector3(0f, yRot, 0f);

        transform.eulerAngles = eulerAngle;
    }

    public void KillTornado()
    {
        for (int i = 0; i < windHazardList.Count; i++)
        {
            windHazardList[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
