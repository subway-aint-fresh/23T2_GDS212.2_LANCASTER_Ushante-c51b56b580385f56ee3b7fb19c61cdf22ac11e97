using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public float timeLimit = 60f;       // Time limit set in seconds
    private float currentTime;          // Time remaining
    public TextMeshProUGUI timerText;   // Reference to the TMP time text

    public GameObject exitButton;       // Reference to the exit button on the popup template
    public GameObject sendButton;       // Reference to the send button on the email prefab
    private Collider2D send_Collider;   // Collider on the send button

    public int popupsInScene = 1;
    [SerializeField] private int popupsClosed; // Counter for how many pop-ups have been closed

    public GameObject winScreen;        // Reference to Win screen
    public GameObject loseScreen;       // Reference to lose screen

    private int correctSelections;
    private int wrongSelections;

    public AudioSource timeTicking;

    public GameObject findImageWindow;  // Reference to the find image puzzle
    public GameObject simonSaysWindow;

    private bool playerInputEnabled = false; // Flag to control player input for ImageSelection

    private void Start()
    {
        StartTimer();
        send_Collider = sendButton.GetComponent<Collider2D>();

        // Set find image pop-up to active
        //findImageWindow.SetActive(true);
    }

    private void Update()
    {
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            GameFailed();
        }
    }

    // Timer
    public void StartTimer()
    {
        currentTime = timeLimit;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Make sure the timer text updates properly through a check
        if (minutes <= 0 && seconds <= 0)
        {
            timerText.text = "00:00";
        }
        else
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Find Image Puzzle management

    // Method responsible for checking if the image selections are correct
    public void CheckImageSelections()
    {
        Debug.Log("CheckImageSelections method called");
        ImageSelection[] imageSelections = FindObjectsOfType<ImageSelection>();

        correctSelections = 0;
        wrongSelections = 0;

        foreach (ImageSelection imageSelection in imageSelections)
        {
            if (imageSelection.IsSelected)
            {
                if (imageSelection.gameObject.layer == LayerMask.NameToLayer("CorrectImage"))
                {
                    correctSelections++;
                }
                else if (imageSelection.gameObject.layer == LayerMask.NameToLayer("WrongImage"))
                {
                    wrongSelections++;
                }
            }
        }

        Debug.Log("Correct Selections: " + correctSelections);
        Debug.Log("Wrong Selections: " + wrongSelections);

        if (correctSelections >= 6 && wrongSelections <= 0)
        {
            // Trigger a method in the GameManager to handle the successful selection of 6 correct images.
            SuccessfulImageSelection();
        }
    }

    // Method responsible for increasing popups closed counter, to be called by exit button
    public void ClosedPopup()
    {
        // Increase counter for popups closed
        if (popupsClosed < popupsInScene)
        {
            popupsClosed++;

            // Set the box collider active instead
            send_Collider.enabled = true;
        }
    }

    // Method for Simon Says Win
    public void SuccessfulSequence()
    {
        Debug.Log("Enabling find image window");
        // Set simon says window to inactive
        simonSaysWindow.SetActive(false);

        // Enable input on find image window
        playerInputEnabled = true;
    }

    // Method for Find Image Win
    private void SuccessfulImageSelection()
    {
        // Set exit button to active
        Debug.Log("pop up done");

        // Enable exit button
        exitButton.SetActive(true);
    }

    // Ends the game and throws up a game over window 
    private void GameFailed()
    {
        currentTime = 0f;
        timeTicking.Stop();
        loseScreen.SetActive(true);
    }

    // Ends the game and throws up a game win window
    public void GameWin()
    {
        currentTime = 0f;
        timeTicking.Stop();

        // add a check to see that all of the pop-ups have been closed
        // if the counter == int set for amount of pop-ups in scene, execute
        if (popupsClosed == popupsInScene)
        {
            winScreen.SetActive(true);
        }
    }

    // Loads home screen scene
    public void HomeScreen()
    {
        SceneManager.LoadScene("HomeScreen");
    }

    // Method to check if player input is enabled for ImageSelection
    public bool IsPlayerInputEnabled()
    {
        return playerInputEnabled;
    }
}

