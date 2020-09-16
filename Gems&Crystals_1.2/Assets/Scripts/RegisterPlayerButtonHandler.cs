using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using publishTest;

public class RegisterPlayerButtonHandler : MonoBehaviour
{
    public void registerPlayerLoadSignInScene(int sceneIndex)
    {
        GameObject parentPanel = this.transform.parent.gameObject;
        InputField usernameInputField = parentPanel.transform.Find("UsernameIF").GetComponentInParent<InputField>();
        InputField nicknameInputField = parentPanel.transform.Find("NicknameIF").GetComponentInParent<InputField>();
        InputField passwordInputField = parentPanel.transform.Find("PasswordIF").GetComponentInParent<InputField>();
        string usernameBase = usernameInputField.text.ToString();
        string nicknameBase = nicknameInputField.text.ToString();
        string passwordBase = passwordInputField.text.ToString();
        if (usernameInputField.text.ToString() == "" || nicknameInputField.text.ToString() == "" || passwordInputField.text.ToString() == "")
        {
            Debug.Log("Informacije nisu unesene!");
        }
        else
        {
            SendUser usr = new SendUser
            {
                Username = usernameBase,
                Nickname = nicknameBase,
                Password = passwordBase,
                MMR = 100,
                Rank = "Unranked",
                Region = "EU",
                
            };
            Client.Instance.Register(usr);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
