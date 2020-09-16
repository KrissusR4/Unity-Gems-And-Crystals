using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using publishTest;

public class PlayerProfileAwakeHandler : MonoBehaviour
{
    public Text winsText;
    public Text lossesText;
    public Text rankText;
    public Text MMR;
    public InputField usernameInputField;
    public InputField nicknameInputField;
    public InputField passwordInputField;

    void Awake()
    {

        usernameInputField.text = Client.Instance.Me.Username;
        nicknameInputField.text = Client.Instance.Me.Nickname;
        passwordInputField.text = Client.Instance.Me.Password;

        winsText.text = Client.Instance.Me.WinNo.ToString();
        lossesText.text = Client.Instance.Me.LossNo.ToString();
        rankText.text = Client.Instance.Me.Rank.ToString();
        MMR.text = Client.Instance.Me.MMR.ToString();
    }
}
