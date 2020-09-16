using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using publishTest;

public class StartingGameHandler : MonoBehaviour
{
    public Text OppName;
    public Text OppLifePoints;
    public Text OppGems;
    public Text OppCrystals;
    public Text MyName;
    public Text MyLifePoints;
    public Text MyGems;
    public Text MyCrystals;
    public Text WhosPlaying;
    public Text OppDeckCount;
    public Text MyDeckCount;
    public RectTransform MyHand;
    public RectTransform MyTable;
    public GameObject prefabC;
    public GameObject imagesList;
    GameObject prefabCard;

    void Start()
    {

        if (State.Instance.PlayerOne.IdUser == Client.Instance.Me.IdUser)
        {
            OppName.text = State.Instance.PlayerTwo.Nickname;
            OppLifePoints.text = State.Instance.playerTwoLifePoints.ToString();
            MyName.text = State.Instance.PlayerOne.Nickname;
            MyLifePoints.text = State.Instance.playerOneLifePoints.ToString();
            if(State.Instance.plOnMove == Client.Instance.Me.IdUser)
            {
                MyGems.text = State.Instance.playerGems.ToString();
                MyCrystals.text = State.Instance.playerCrystals.ToString();
                WhosPlaying.text = Client.Instance.Me.Nickname;
                OppGems.text = "?";
                OppCrystals.text = "?";
            }
            else
            {
                MyGems.text = "?";
                MyCrystals.text = "?";
                WhosPlaying.text = State.Instance.PlayerTwo.Nickname;
                OppGems.text = State.Instance.playerGems.ToString();
                OppCrystals.text = State.Instance.playerCrystals.ToString();
            }
            OppDeckCount.text = State.Instance.playerTwoDeck.Count.ToString();
            MyDeckCount.text = State.Instance.playerOneDeck.Count.ToString();
            foreach (Card card in State.Instance.playerOneHand)
            {
                //Instantiate(prefabC, MyHand.transform.position, Quaternion.identity);
                //prefabC.transform.Find("GemText").GetComponent<Text>().text = card.GemsCost.ToString();
                //prefabC.transform.Find("CrystalText").GetComponent<Text>().text = card.CrystalsCost.ToString();
                //prefabC.transform.Find("AttackText").GetComponent<Text>().text = card.Attack.ToString();
                //prefabC.transform.Find("ShieldText").GetComponent<Text>().text = card.Shield.ToString();
                //prefabC.transform.Find("HealthText").GetComponent<Text>().text = card.Health.ToString();
                //prefabC.transform.Find("Description").GetComponent<Text>().text = card.Description.ToString();
                //prefabC.transform.SetParent(MyHand.transform);

                prefabCard = Instantiate(prefabC, transform.position, Quaternion.identity) as GameObject;
                prefabCard.transform.Find("GemText").GetComponent<Text>().text = card.GemsCost.ToString();
                prefabCard.transform.Find("CrystalText").GetComponent<Text>().text = card.CrystalsCost.ToString();
                prefabCard.transform.Find("AttackText").GetComponent<Text>().text = card.Attack.ToString();
                prefabCard.transform.Find("ShieldText").GetComponent<Text>().text = card.Shield.ToString();
                prefabCard.transform.Find("HealthText").GetComponent<Text>().text = card.Health.ToString();
                prefabCard.transform.Find("Description").GetComponent<Text>().text = card.Description.ToString();
                prefabCard.transform.Find("Picture").GetComponent<Image>().sprite = imagesList.transform.Find(card.IdCard.ToString()).GetComponent<SpriteRenderer>().sprite;
                // prefabCard.transform.Find("Picture").GetComponent<Image>().sprite = Resources.Load<Sprite>(card.IdCard.ToString());
                prefabCard.transform.SetParent(MyHand.transform);
            }
        }
        else
        {
            MyName.text = State.Instance.PlayerTwo.Nickname;
            MyLifePoints.text = State.Instance.playerTwoLifePoints.ToString();
            MyGems.text = "8";
            MyCrystals.text = "0";
            OppName.text = State.Instance.PlayerOne.Nickname;
            OppLifePoints.text = State.Instance.playerOneLifePoints.ToString();
            OppGems.text = State.Instance.playerGems.ToString();
            OppCrystals.text = State.Instance.playerCrystals.ToString();
            if (State.Instance.plOnMove == Client.Instance.Me.IdUser)
            {
                MyGems.text = State.Instance.playerGems.ToString();
                MyCrystals.text = State.Instance.playerCrystals.ToString();
                WhosPlaying.text = Client.Instance.Me.Nickname;
                OppGems.text = "?";
                OppCrystals.text = "?";
            }
            else
            {
                MyGems.text = "?";
                MyCrystals.text = "?";
                WhosPlaying.text = State.Instance.PlayerTwo.Nickname;
                OppGems.text = State.Instance.playerGems.ToString();
                OppCrystals.text = State.Instance.playerCrystals.ToString();
            }
            OppDeckCount.text = State.Instance.playerTwoDeck.Count.ToString();
            MyDeckCount.text = State.Instance.playerOneDeck.Count.ToString();
            foreach (Card card in State.Instance.playerOneHand)
            {
                //Instantiate(prefabC, MyHand.transform.position, Quaternion.identity);
                //prefabC.transform.Find("GemText").GetComponent<Text>().text = card.GemsCost.ToString();
                //prefabC.transform.Find("CrystalText").GetComponent<Text>().text = card.CrystalsCost.ToString();
                //prefabC.transform.Find("AttackText").GetComponent<Text>().text = card.Attack.ToString();
                //prefabC.transform.Find("ShieldText").GetComponent<Text>().text = card.Shield.ToString();
                //prefabC.transform.Find("HealthText").GetComponent<Text>().text = card.Health.ToString();
                //prefabC.transform.Find("Description").GetComponent<Text>().text = card.Description.ToString();
                //prefabC.transform.SetParent(MyHand.transform);


                prefabCard = Instantiate(prefabC,transform.position, Quaternion.identity) as GameObject;
                prefabCard.transform.Find("GemText").GetComponent<Text>().text = card.GemsCost.ToString();
                prefabCard.transform.Find("CrystalText").GetComponent<Text>().text = card.CrystalsCost.ToString();
                prefabCard.transform.Find("AttackText").GetComponent<Text>().text = card.Attack.ToString();
                prefabCard.transform.Find("ShieldText").GetComponent<Text>().text = card.Shield.ToString();
                prefabCard.transform.Find("HealthText").GetComponent<Text>().text = card.Health.ToString();
                prefabCard.transform.Find("Description").GetComponent<Text>().text = card.Description.ToString();
                prefabCard.transform.Find("Picture").GetComponent<Image>().sprite = imagesList.transform.Find(card.IdCard.ToString()).GetComponent<SpriteRenderer>().sprite;
                // prefabCard.transform.Find("Picture").GetComponent<Image>().sprite = Resources.Load<Sprite>(card.IdCard.ToString());
                prefabCard.transform.SetParent(MyHand.transform);
            }
        }
    }
}
