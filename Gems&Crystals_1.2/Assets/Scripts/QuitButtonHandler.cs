using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonHandler : MonoBehaviour
{
    public void quitApp()
    {
        Debug.Log("Quit App!");
        Application.Quit();
    }
}
