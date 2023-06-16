using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuessTheAnimalPopup : MonoBehaviour
{
    public Text animalNameText;
    public Image animalImage;
    public InputField guessInputField;
    public Button guessButton;
    public Button closeButton;

    private string[] animalNames = { "Dog", "Cat", "Elephant", "Lion" };
    private string currentAnimal;

    private void Start()
    {
        guessButton.onClick.AddListener(CheckGuess);
        closeButton.onClick.AddListener(ClosePopup);

        // Start the game
        ShowRandomAnimal();
    }

    private void ShowRandomAnimal()
    {
        // Randomly select an animal from the list
        int randomIndex = Random.Range(0, animalNames.Length);
        currentAnimal = animalNames[randomIndex];

        // Display the animal's name
        animalNameText.text = currentAnimal;

        // Load and display the animal's image (you need to have the images in your project's Assets/Resources folder)
        Sprite animalSprite = Resources.Load<Sprite>("AnimalImages/" + currentAnimal);
        animalImage.sprite = animalSprite;
    }

    private void CheckGuess()
    {
        string guess = guessInputField.text;

        if (guess.ToLower() == currentAnimal.ToLower())
        {
            Debug.Log("Correct guess! You win!");
            // Add your reward or game logic here

            // Show a new random animal for the next round
            ShowRandomAnimal();
        }
        else
        {
            Debug.Log("Wrong guess! Try again.");
        }

        // Clear the guess input field
        guessInputField.text = "";
    }

    private void ClosePopup()
    {
        // Close the popup
        gameObject.SetActive(false);
    }
}
