using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckBackToMenuButtonHandler : MonoBehaviour
{
    public void loadDeckMenuScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
