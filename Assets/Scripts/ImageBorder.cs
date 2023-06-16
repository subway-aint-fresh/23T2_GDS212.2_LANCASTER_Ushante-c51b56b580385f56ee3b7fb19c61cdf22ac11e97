using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageBorder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    //I might just activate/deactivate a border child of the image prefabs

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Method to show the border
    public void ShowBorder()
    {
        // Enable the sprite renderer component
        spriteRenderer.enabled = true;
    }

    // Method to hide the border
    public void HideBorder()
    {
        // Disable the sprite renderer component
        spriteRenderer.enabled = false;
    }
}

