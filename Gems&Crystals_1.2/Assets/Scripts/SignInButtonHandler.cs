using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using publishTest;

public class SignInButtonHandler : MonoBehaviour
{
    public void loadMenuScene(int sceneIndex)
    {
        GameObject parentPanel = this.transform.parent.gameObject;
        InputField usernameInputField = parentPanel.transform.Find("UsernameIF").GetComponentInParent<InputField>();
        InputField passwordInputField = parentPanel.transform.Find("PasswordIF").GetComponentInParent<InputField>();
        string usernameBase = usernameInputField.text.ToString();
        string passwordBase = passwordInputField.text.ToString();
        Debug.Log(usernameInputField.text.ToString());
        Client.Instance.Login(usernameBase, passwordBase);
        if (Client.Instance.Me != null)
        {
            Debug.Log(Client.Instance.Me.Nickname);
            SceneManager.LoadScene(sceneIndex);
        }
        
    }
}
