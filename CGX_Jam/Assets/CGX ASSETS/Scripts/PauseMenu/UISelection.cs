using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using XBOX;

public class UISelection : MonoBehaviour
{
    RawImage img;

    public Texture XboxTex;
    public Texture KBTex;

    private void Awake()
    {
        img = GetComponent<RawImage>();
    }

    void OnEnable()
    {
        if (XInput.GetConnected())
        {
            img.texture = XboxTex;
        }
        else
        {
            img.texture = KBTex;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (XInput.GetConnected()){
            img.texture = XboxTex;
        }
        else
        {
            img.texture = KBTex;
        }
    }
}
