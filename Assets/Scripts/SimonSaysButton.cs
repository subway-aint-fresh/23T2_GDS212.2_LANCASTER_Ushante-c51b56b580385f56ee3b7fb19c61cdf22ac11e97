using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysButton : MonoBehaviour
{
    public int buttonIndex;
    public GameObject coloredButton;

    private SimonSaysManager simonSaysGame;

    private void Start()
    {
        simonSaysGame = FindObjectOfType<SimonSaysManager>();
    }

    private void OnMouseDown()
    {
        coloredButton.SetActive(true);
        simonSaysGame.HandlePlayerInput(buttonIndex);
    }
}

