using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CancelGameButtonHandler : MonoBehaviour
{
    public void loadMenuScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
