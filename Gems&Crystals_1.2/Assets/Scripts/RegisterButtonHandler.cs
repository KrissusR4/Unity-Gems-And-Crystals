using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using publishTest;

public class RegisterButtonHandler : MonoBehaviour
{
    public void loadRegisterScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
