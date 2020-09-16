using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckButtonHandler : MonoBehaviour
{
    public void loadDeckScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
