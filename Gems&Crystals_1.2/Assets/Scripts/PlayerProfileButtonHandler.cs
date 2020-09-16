using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProfileButtonHandler : MonoBehaviour
{
    public void loadPlayerProfileScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
