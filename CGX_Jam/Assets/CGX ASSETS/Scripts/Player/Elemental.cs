using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public int maxHealth;
    int health;

    [Range(0f, 10f)]
    public float intensity = 3f;
    float colourMod;

    public GameObject healthBar;
    public GameObject EndScreen;

    public static bool Alive;

    public static Color[] colours =
    { // colour   R               G         B  values
        new Color(1f,           64f/255f,   0f),
        new Color(0f,           0.3f,       1f),
        new Color(157f/255f,    1f,         0f),
        new Color(209f/255f,    1f,         209f/255f)
    };

    // Start is called before the first frame update
    void Awake()
    {
        Alive = true;
        health = maxHealth;
        colourMod = Mathf.Pow(2f, intensity);
    }

    private void OnValidate()
    {
        colourMod = Mathf.Pow(2, intensity);
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

        if(health <= 0)
        {
            Alive = false;
            EndScreen.GetComponent<EndScreenText>().winner = false;
            EndScreen.SetActive(true);
        }

        uint temp = elementSelect % 4;

        curElement = (Elements) temp;

        GetComponent<MeshRenderer>().material.SetColor("PlayerColor", colours[temp] * colourMod);

        healthBar.transform.localScale = new Vector3(Mathf.Lerp(0f, 2f, (float)health / maxHealth), 0.17011f, 0f);
    }

    public void resetPlayer()
    {
        health = maxHealth;
        Alive = true;
    }

    public void Damage()
    {
        if (Alive)
            health -= 1;
    }

    public void ElementInc()
    {
        if(Alive)
            elementSelect += 1u;
    }

    public void ElementDec()
    {
        if(Alive)
            elementSelect -= 1u;
    }
}
