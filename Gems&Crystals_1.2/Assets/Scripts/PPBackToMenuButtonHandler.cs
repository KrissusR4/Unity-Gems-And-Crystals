using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PPBackToMenuButtonHandler : MonoBehaviour
{
    public void loadPPMenuScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
