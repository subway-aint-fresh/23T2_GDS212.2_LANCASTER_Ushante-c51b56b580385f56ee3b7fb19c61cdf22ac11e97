using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysManager : MonoBehaviour
{
    private List<int> playerSequence = new List<int>();   // List to store the player's button presses
    private List<int> currentSequence = new List<int>();  // List to store the current sequence to follow

    public GameObject button1;              // Reference to the non-colored object button 1
    public GameObject button1Color;         // Reference to the colored child object of button 1

    public GameObject button2;              // Button 2 non-colored 
    public GameObject button2Color;         // Button 2 colored child

    public GameObject button3;              // Button 3 non-colored 
    public GameObject button3Color;         // Button 3 colored child

    public GameObject button4;              // Button 4 non-colored 
    public GameObject button4Color;         // Button 4 colored child

    public AudioClip button1AudioClip;           // Audio beeps for buttons
    public AudioClip button2AudioClip;
    public AudioClip button3AudioClip;
    public AudioClip button4AudioClip;

    public AudioSource errorSound;             // Sound to play on incorrect sequence
    public AudioSource winSound;               // Sound to play on successful sequence

    private AudioSource button1AudioSource; // Temporary storage for audio components
    private AudioSource button2AudioSource;
    private AudioSource button3AudioSource;
    private AudioSource button4AudioSource;

    private bool playerInputEnabled = false;  // Flag to control player input

    private int failedAttempts = 0; // Counter for the number of failed attempts

    private GameManager gameManager;        // Reference to the GameManager script

    void Start()
    {
        // Set the initial state of the colored versions of the buttons
        button1Color.SetActive(false);
        button2Color.SetActive(false);
        button3Color.SetActive(false);
        button4Color.SetActive(false);

        // Get the AudioSources attached to the buttons
        button1AudioSource = button1Color.GetComponent<AudioSource>();
        button2AudioSource = button2Color.GetComponent<AudioSource>();
        button3AudioSource = button3Color.GetComponent<AudioSource>();
        button4AudioSource = button4Color.GetComponent<AudioSource>();

        // Assign audio clips to audio sources
        button1AudioSource.clip = button1AudioClip;
        button2AudioSource.clip = button2AudioClip;
        button3AudioSource.clip = button3AudioClip;
        button4AudioSource.clip = button4AudioClip;

        StartCoroutine(PlaySequence());

        gameManager = FindObjectOfType<GameManager>();
    }

    IEnumerator PlaySequence()
    {
        // Generate a random sequence of button indices
        GenerateSequence(4);

        // Play the sequence automatically
        for (int i = 0; i < currentSequence.Count; i++)
        {
            yield return new WaitForSeconds(1f); // Delay between button activations
            PlayButton(currentSequence[i]);
        }

        // Enable player input after playing the sequence
        playerInputEnabled = true;
    }

    void Update()
    {
        // Handle player input only when player input is enabled
        if (playerInputEnabled)
        {
            // Handle player input based on button clicks or input detection mechanism
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                HandlePlayerInput(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                HandlePlayerInput(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                HandlePlayerInput(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                HandlePlayerInput(4);
            }
        }
    }

    public void HandlePlayerInput(int buttonIndex)
    {
        // Add the player's button press to the sequence
        playerSequence.Add(buttonIndex);

        // Compare player's sequence with expected sequence
        CheckPlayerSequence();

        // Play the button sound effect
        switch (buttonIndex)
        {
            case 1:
                button1AudioSource.Play();
                break;
            case 2:
                button2AudioSource.Play();
                break;
            case 3:
                button3AudioSource.Play();
                break;
            case 4:
                button4AudioSource.Play();
                break;
        }
    }


    public void CheckPlayerSequence()
    {
        // Compare player's sequence with expected sequence
        if (playerSequence.Count == currentSequence.Count)
        {
            bool sequencesMatch = true;
            for (int i = 0; i < playerSequence.Count; i++)
            {
                if (playerSequence[i] != currentSequence[i])
                {
                    sequencesMatch = false;
                    break;
                }
            }

            if (sequencesMatch)
            {
                // Player's sequence matches the expected sequence, play win sound and call win method from GameManager
                CorrectSequence();
            }
            else
            {
                // Player's sequence does not match the expected sequence
                playerInputEnabled = false;
                WrongSequence();
            }
        }
    }

    public void GenerateSequence(int length)
    {
        currentSequence.Clear(); // Clear the existing sequence

        // Generate a random sequence of button indices
        for (int i = 0; i < length; i++)
        {
            int randomIndex = Random.Range(1, 5); // Generate a random index between 1 and 4
            currentSequence.Add(randomIndex); // Add the index to the sequence list
        }
    }

    private void PlayButton(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 1:
                StartCoroutine(ActivateButton(button1Color, button1, button1AudioSource));
                break;
            case 2:
                StartCoroutine(ActivateButton(button2Color, button2, button2AudioSource));
                break;
            case 3:
                StartCoroutine(ActivateButton(button3Color, button3, button3AudioSource));
                break;
            case 4:
                StartCoroutine(ActivateButton(button4Color, button4, button4AudioSource));
                break;
        }
    }

    private IEnumerator ActivateButton(GameObject coloredButton, GameObject button, AudioSource audioSource)
    {
        coloredButton.SetActive(true);
        button.GetComponent<SpriteRenderer>().enabled = false;

        // Play the button sound effect
        audioSource.Play();

        yield return new WaitForSeconds(0.5f); // Duration the colored button is active

        coloredButton.SetActive(false);
        button.GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator ReplaySequenceCoroutine()
    {
        // Reset button states
        ResetButton(button1, button1Color);
        ResetButton(button2, button2Color);
        ResetButton(button3, button3Color);
        ResetButton(button4, button4Color);

        // Reset player sequence
        playerSequence.Clear();

        // Play the sequence
        yield return StartCoroutine(PlaySequence());
    }

    private void ResetButton(GameObject button, GameObject coloredButton)
    {
        coloredButton.SetActive(false);
        button.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void WrongSequence()
    {
        failedAttempts++;

        Debug.Log("Wrong sequence. Try again.");

        // Play error sound
        errorSound.Play();

        // Check if the player has reached the maximum number of failed attempts
        if (failedAttempts >= 30)
        {
            Debug.Log("Max failed attempts reached. Game over.");
            playerInputEnabled = false;
            // Call your game over method here
        }
        else
        {
            // Disable player input while replaying the sequence
            playerInputEnabled = false;

            // Replay the sequence
            StartCoroutine(ReplaySequenceCoroutine());
        }
    }

    private void CorrectSequence()
    {
        Debug.Log("Correct sequence!");

        // Play win sound
        winSound.Play();

        // Disable player input after a correct sequence
        playerInputEnabled = false;

        // Call win method in game manager
        gameManager.SuccessfulSequence();
    }
}

