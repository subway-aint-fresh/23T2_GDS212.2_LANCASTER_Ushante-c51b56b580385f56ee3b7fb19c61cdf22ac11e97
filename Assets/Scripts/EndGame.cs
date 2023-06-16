using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    //end screen
   // public GameObject endScreenTitle;

    //responsible for ending game on click
    public void SendEmail()
    {
        //on click the end game screen is thrown up
        //endScreenTitle.SetActive(true);
        Debug.Log("game ended");

    }
}
