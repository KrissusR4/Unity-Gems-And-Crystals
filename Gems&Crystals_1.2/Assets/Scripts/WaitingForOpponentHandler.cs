using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using publishTest;
using System;

public class WaitingForOpponentHandler : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(5);
        Client.Instance.GetUserDecks(Client.Instance.Me.IdUser);
        Client.Instance.Me.ActiveDeck = Client.Instance.MyDecks[0];
        Client.Instance.StartMatchMaking();
        SceneManager.LoadScene(7);
    }
}
