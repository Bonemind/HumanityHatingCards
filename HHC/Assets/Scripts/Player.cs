using UnityEngine;
using System.Collections.Generic;

public class Player {
    /// <summary>
    /// The name of the player
    /// </summary>
    public string playerName;

    /// <summary>
    /// The hand the player has
    /// </summary>
    private List<Card> hand = new List<Card>();

    public GameObject HandDisplayPanel;

    public GameObject WhiteCardPrefab;

    /// <summary>
    /// Adds a card to the players hand
    /// </summary>
    /// <param name="card">The card to add</param>
    public void AddCard(Card card)
    {
        hand.Add(card);
        UpdateHandDisplay();
    }

    /// <summary>
    /// Removes a card from a players hand
    /// </summary>
    /// <param name="card">The card to remove</param>
    public void RemoveCard(Card card)
    {
        hand.Remove(card);
        UpdateHandDisplay();
    }

    /// <summary>
    /// Returns the hand of the player
    /// </summary>
    /// <returns>The player's hand</returns>
    public List<Card> GetHand()
    {
        return hand;
    }

    private void UpdateHandDisplay()
    {

        if (HandDisplayPanel == null)
        {
            Debug.Log("Handdisplaypanel null");
            return;
        }
        foreach (Transform child in HandDisplayPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Card c in hand)
        {
            GameObject go = (GameObject) GameObject.Instantiate(WhiteCardPrefab);
            go.GetComponent<CardWrapper>().card = c;
            go.transform.SetParent(HandDisplayPanel.transform);
        }

    }
}
