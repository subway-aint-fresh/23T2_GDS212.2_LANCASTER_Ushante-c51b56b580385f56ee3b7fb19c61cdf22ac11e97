using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public GameObject findImagePuzzle;    // Reference to the find image popup

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        // When the exit button is clicked, the find image prefab/pop-up is set to inactive
        findImagePuzzle.SetActive(false);

        // This will then trigger the email send button to be interactable
        // Eventually, it should increase the pop-up's closed counter
        gameManager.ClosedPopup();
    }
}

