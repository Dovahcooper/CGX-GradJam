using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XBOX;

public enum Elements
{
    Fire = 0,
    Water,
    Lightning,
    Wind
}

[RequireComponent(typeof(MeshRenderer))]
public class Elemental : MonoBehaviour
{
    public Buttons leftCycle = Buttons.LTrig;
    public Buttons rightCycle = Buttons.RTrig;

    public Elements curElement = Elements.Water;
    uint elementSelect = 1;

    MeshRenderer renderer;

    public static Color[] colours =
    { // colour   R               G         B  values
        new Color(1f,           64f/255f,   0f),
        new Color(0f,           0.3f,       1f),
        new Color(157f/255f,    1f,         0f),
        new Color(209f/255f,    1f,         209f/255f)
    };

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (XInput.GetConnected())
        {
            if (XInput.GetKeyPressed(0, (int)leftCycle))
                elementSelect--;
            if (XInput.GetKeyPressed(0, (int)rightCycle))
                elementSelect++;
        }
        else
        {
            if(Input.mouseScrollDelta.y > 0)
            {
                elementSelect += 1u;
            }
            else if(Input.mouseScrollDelta.y < 0)
            {
                elementSelect -= 1u;
            }
        }

        uint temp = elementSelect % 4;

        curElement = (Elements) temp;

        renderer.material.color = colours[temp];
    }
}
