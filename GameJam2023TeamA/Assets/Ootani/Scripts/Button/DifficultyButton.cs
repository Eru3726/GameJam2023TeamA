using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : ButtonManager
{
    public Text descriptionText;

    [TextArea(3, 20)]
    public string description;
    private void Start()
    {
        onPointerAction = textDisplay;
    }

    void textDisplay()
    {

    }
}
