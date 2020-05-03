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

    public int maxHealth;
    int health;

    public GameObject healthBar;

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
        health = maxHealth;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!PauseMenu.paused)
        {
            if (!XInput.GetConnected())
            {
                if (Input.mouseScrollDelta.y > 0)
                {
                    elementSelect += 1u;
                }
                else if (Input.mouseScrollDelta.y < 0)
                {
                    elementSelect -= 1u;
                }

                if (Input.GetKeyDown(KeyCode.L))
                {
                    resetPlayer();
                }
            }
        }

        uint temp = elementSelect % 4;

        curElement = (Elements) temp;

        renderer.material.color = colours[temp];

        healthBar.transform.localScale = new Vector3(Mathf.Lerp(0f, 2f, (float)health / maxHealth), 0.17011f, 0f);
    }

    public void resetPlayer()
    {
        health = maxHealth;
    }

    public void Damage()
    {
        health -= 1;
    }

    public void ElementInc()
    {
        elementSelect += 1u;
    }

    public void ElementDec()
    {
        elementSelect -= 1u;
    }
}
