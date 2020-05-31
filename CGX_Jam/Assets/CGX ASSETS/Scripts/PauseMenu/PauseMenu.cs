using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XBOX;

public class PauseMenu : MonoBehaviour
{
    public static bool paused;

    public GameObject UIObjects;

    public XInput controller;

    // Start is called before the first frame update
    void Start()
    {
        paused = true;
        UIObjects.SetActive(true);
        controller.aPress.AddListener(Pause);
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && Elemental.Alive)
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Application.Quit();
        }
    }

    public void Pause()
    {
        paused = !paused;
        UIObjects.SetActive(paused);

        if (paused)
        {
            controller.aPress.AddListener(Pause);
        }
        else
        {
            controller.aPress.RemoveListener(Pause);
        }
    }
}
