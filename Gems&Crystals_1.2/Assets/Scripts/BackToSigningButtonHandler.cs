using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToSigningButtonHandler : MonoBehaviour
{
    public void loadSignInScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
