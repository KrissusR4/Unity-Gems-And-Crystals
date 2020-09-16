using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsBackToMenuButtonHandler : MonoBehaviour
{
    public void loadOptionsMenuScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
