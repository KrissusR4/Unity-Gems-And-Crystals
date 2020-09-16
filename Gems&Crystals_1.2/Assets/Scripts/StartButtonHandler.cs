using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using publishTest;

public class StartButtonHandler : MonoBehaviour
{
    public void loadStartingGameScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
