using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewDeckButtonHandler : MonoBehaviour
{ 
    public void newDeck()
    {
        //GameObject parentPanel = this.transform.parent.gameObject;
        //RectTransform DeckModPanel = parentPanel.transform.Find("DeckModPanel").GetComponentInParent<RectTransform>();
        DeckPanel.gameObject.SetActive(true);

    }
    public RectTransform DeckPanel;

    public void Awake()
    {
        DeckPanel.gameObject.SetActive(false);
    }
}
