using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenText : MonoBehaviour
{
    const string victory = "Congratulations, you've won\nThe Elemental Rush";
    const string defeat = "Better luck next time";

    public bool winner;

    // Start is called before the first frame update
    void OnEnable()
    {
        Text tempText = GetComponentInChildren<Text>();

        if (winner)
        {
            tempText.text = victory;
            tempText.color = Color.green;
        }
        else
        {
            tempText.text = defeat;
            tempText.color = Color.red;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L)){
            disable();
        }
    }

    public void disable()
    {
        gameObject.SetActive(false);
    }
}
