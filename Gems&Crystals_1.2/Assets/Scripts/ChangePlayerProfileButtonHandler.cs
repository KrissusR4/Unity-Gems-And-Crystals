using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using publishTest;

public class ChangePlayerProfileButtonHandler : MonoBehaviour
{
    public void changePlayerProfile()
    {
        GameObject parentPanel = this.transform.parent.gameObject;
        InputField usernameInputField = parentPanel.transform.Find("UsernameIF").GetComponentInParent<InputField>();
        InputField nicknameInputField = parentPanel.transform.Find("NicknameIF").GetComponentInParent<InputField>();
        InputField passwordInputField = parentPanel.transform.Find("PasswordIF").GetComponentInParent<InputField>();
        string usernameDB = usernameInputField.text.ToString();
        string nicknameDB = nicknameInputField.text.ToString();
        string passwordDB = passwordInputField.text.ToString();

        SendUser usr = new SendUser
        {
            IdUser = Client.Instance.Me.IdUser,
            Username = usernameDB,
            Nickname = nicknameDB,
            Password = passwordDB,
            WinNo = Client.Instance.Me.WinNo,
            LossNo = Client.Instance.Me.LossNo,
            Rank = Client.Instance.Me.Rank,
            MMR = Client.Instance.Me.MMR,
            Region = Client.Instance.Me.Region,
            Avatar = Client.Instance.Me.Avatar,
            MyDecksList = Client.Instance.Me.MyDecksList,
            OwnedCardsList = Client.Instance.Me.OwnedCardsList,

        };

        Client.Instance.Me.Username = usernameDB;
        Client.Instance.Me.Nickname = nicknameDB;
        Client.Instance.Me.Password = passwordDB;

        Client.Instance.PutUser(usr);
    }

}
