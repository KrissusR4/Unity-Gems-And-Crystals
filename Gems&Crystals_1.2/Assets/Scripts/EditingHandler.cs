using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using publishTest;
using System;

public class EditingHandler : MonoBehaviour
{
    public GameObject YourDecksContainer;
    public GameObject prefabDN;
    GameObject prefabDeckName;

    public GameObject OwnedCardsContainer;
    public GameObject prefabOC;
    GameObject prefabOwnedCard;
    public RectTransform DeckPanel;
    List<Card> newDeckCards = new List<Card>();
    void Awake()
    {
        

        Client.Instance.GetUserDecks(Client.Instance.Me.IdUser);
        Client.Instance.GetUserCards();
        for (int i = 0; i < Client.Instance.MyDecks.Count; i++)
        {
            prefabDeckName = Instantiate(prefabDN, transform.position, Quaternion.identity) as GameObject;
            prefabDeckName.transform.Find("Text").GetComponent<Text>().text = Client.Instance.MyDecks[i].Name.ToString();
            prefabDeckName.transform.SetParent(YourDecksContainer.transform, false);
        }

        for (int i = 0; i < Client.Instance.OwnedCards.Count; i++)
        {
            Debug.Log(Client.Instance.OwnedCards[i].Name);
            prefabOwnedCard = Instantiate(prefabOC, (transform.position), Quaternion.identity) as GameObject;
            prefabOwnedCard.transform.Find("Text").GetComponent<Text>().text = Client.Instance.OwnedCards[i].Name;
            prefabOwnedCard.transform.SetParent(OwnedCardsContainer.transform, false);
        }
    }
    public void newDeck()
    {
        //GameObject parentPanel = this.transform.parent.gameObject;
        //RectTransform DeckModPanel = parentPanel.transform.Find("DeckModPanel").GetComponentInParent<RectTransform>();
        DeckPanel.gameObject.SetActive(true);
        foreach (Transform child in YourDecksContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void AddCardHandler(GameObject card)
    {
        newDeckCards.Add(Client.Instance.OwnedCards[int.Parse(card.transform.Find("CardId").GetComponent<Text>().text)]);
        prefabDeckName.transform.Find("Text").GetComponent<Text>().text = card.transform.Find("Text").GetComponent<Text>().text;
        prefabOwnedCard.transform.SetParent(OwnedCardsContainer.transform, false);
    }
 
}
