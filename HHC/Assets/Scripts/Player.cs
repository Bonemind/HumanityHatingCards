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

    /// <summary>
    /// Holds the panel that displays cards
    /// Since this is not a monobehaviour this should be set by the gamemanager 
    /// or some other object
    /// </summary>
    public GameObject HandDisplayPanel;

    /// <summary>
    /// Holds the white card prefab
    /// Still not a monobehaviour, see above
    /// </summary>
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

    /// <summary>
    /// Updates the planel containing the cards of a player
    /// </summary>
    private void UpdateHandDisplay()
    {
        if (HandDisplayPanel == null)
        {
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
