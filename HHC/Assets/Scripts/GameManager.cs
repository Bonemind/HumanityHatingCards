using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The prefab for black cards
    /// </summary>
    public GameObject BlackCardPrefab;

    /// <summary>
    /// The prefab for white cards
    /// </summary>
    public GameObject WhiteCardPrefab;

    /// <summary>
    /// The list of decks to play with
    /// Expected to contain cardcast deckcodes
    /// </summary>
    public List<string> DeckList;

    /// <summary>
    /// The deck loaded
    /// </summary>
    private Deck deck;

    /// <summary>
    /// The current list of players
    /// </summary>
    public List<Player> playerList;

    /// <summary>
    /// The index of the card czar player
    /// </summary>
    public int cardCzarIndex;

    /// <summary>
    /// The current gamestate
    /// </summary>
    public GameState currState;

    /// <summary>
    /// The gameobject representing the current black card
    /// </summary>
    public GameObject BlackCardObject;

    /// <summary>
    /// The number of cards each player should play
    /// </summary>
    public int CardsToPlay;

    /// <summary>
    /// The list of cards played by players, and the player who played them
    /// </summary>
    private Dictionary<Player, List<Card>> playedCards;

    /// <summary>
    /// A dictionary holding the player scores
    /// </summary>
    private Dictionary<Player, int> playerScores;

    /// <summary>
    /// The amount of cards a player starts with
    /// </summary>
    public int StartingCardAmount;

    /// <summary>
    /// The panel that will display the current hand of the player
    /// </summary>
    public GameObject DisplayCardPanel;

    /// <summary>
    /// The panel to display the hand of the player in
    /// </summary>
    public GameObject HandPanel;

    public void Start()
    {
        playedCards = new Dictionary<Player, List<Card>>();
        playerScores = new Dictionary<Player, int>();
        deck = new Deck();
        playerList = new List<Player>();

        //Debug stuff
        //TODO: Remove
        addDebugPlayers();
        playerList[1].HandDisplayPanel = HandPanel;
        playerList[1].WhiteCardPrefab = WhiteCardPrefab;

        cardCzarIndex = 0;
        currState = GameState.PLAYING_CARDS;
        LoadSelectedDecks();
        Debug.Log("Black:" + deck.calls);
        Debug.Log("White:" + deck.responses);
        InitializeRoundStage();
        foreach (Player player in playerList)
        {
            playerScores.Add(player, 0);
        }
    }

    /// <summary>
    /// Loads all selected decks
    /// </summary>
    public void LoadSelectedDecks()
    {
        foreach (string deckCode in DeckList)
        {
            Deck currDeck = DeckLoader.LoadDeck(deckCode);
            deck.calls.AddRange(currDeck.calls);
            deck.responses.AddRange(currDeck.responses);
            Debug.Log("Loaded deck " + deckCode);
        }

        foreach (Player p in playerList)
        {
            DealCardsToPlayer(p, StartingCardAmount);
        }
    }

    /// <summary>
    /// Adds some debug player objects
    /// TODO: Remove
    /// </summary>
    private void addDebugPlayers()
    {
        Player p1 = new Player();
        Player p2 = new Player();
        Player p3 = new Player();
        Player p4 = new Player();
        p1.playerName = "p1";
        p2.playerName = "p2";
        p3.playerName = "p3";
        p4.playerName = "p4";
        playerList.Add(p1);
        playerList.Add(p2);
        playerList.Add(p3);
        playerList.Add(p4);
 
    }

    /// <summary>
    /// Plays all cards a player can play
    /// TODO: remove
    /// </summary>
    private void PlayAllCards()
    {
        foreach (Player p in playerList)
        {
            if (p == playerList[cardCzarIndex])
            {
                continue;
            }
            PlayCard(p, p.GetHand()[Random.Range(0, p.GetHand().Count)]);
        }
    }

    /// <summary>
    /// Called on update
    /// Currently only contains debug functions
    /// </summary>
    public void Update()
    {
        //Display a random black card
        if (Input.GetKeyDown(KeyCode.Z))
        {
            InitializeRoundStage();
        }
        //Display a random black card
        if (Input.GetKeyDown(KeyCode.X))
        {
            InitializeWinnerStage();
        }
        //Display a random black card
        if (Input.GetKeyDown(KeyCode.C))
        {
            InitializeDisplayingWinnerStage();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayAllCards();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SelectWinningCard(playedCards[playerList[0]][0]);
        }

        //Load deck with number NZ6R5 (Cards against humanity base deck)
        if (Input.GetKeyDown(KeyCode.D))
        {
            deck = DeckLoader.LoadDeck("NZ6R5");
        }
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200f, 50f), currState.ToString());
        string scores = "";
        foreach (KeyValuePair<Player, int> kv in playerScores)
        {
            scores += kv.Key.playerName + " -- " + kv.Value + "  |  ";
        }
        GUI.Label(new Rect(0, 50f, 500f, 50f), scores);
    }

    /// <summary>
    /// Handles initialization of a round
    /// </summary>
    public void InitializeRoundStage()
    {
        playedCards.Clear();
        cardCzarIndex = (cardCzarIndex + 1) % playerList.Count;
        Card currBlackCard = deck.calls[Random.Range(0, deck.calls.Count)];
        deck.calls.Remove(currBlackCard);
        if (BlackCardObject != null)
        {
            BlackCardObject.GetComponentInChildren<UnityEngine.UI.Text>().text = currBlackCard.GetText();
        }

        //Clean up cards of previous round
        foreach (Transform child in DisplayCardPanel.transform)
        {
            Destroy(child.gameObject);
        }

        //We need to play 1 less than the number of entries in a card
        CardsToPlay = Mathf.Max(currBlackCard.text.Length - 1, 1);

        currState = GameState.PLAYING_CARDS;
    }

    /// <summary>
    /// Handles the initialization of a winner stage
    /// </summary>
    public void InitializeWinnerStage() 
    {
        currState = GameState.SELECTING_WINNER;
        DisplayCards();
    }

    /// <summary>
    /// Handles the displaying of a winner stage
    /// </summary>
    public void InitializeDisplayingWinnerStage()
    {
        currState = GameState.DISPLAYING_WINNER;
        foreach (Player player in playerList)
        {
            if (player == playerList[cardCzarIndex])
            {
                continue;
            }
            DealCardsToPlayer(player, CardsToPlay);
        }
        Invoke("InitializeRoundStage", 8f);
    }

    /// <summary>
    /// Deals random white cards to players
    /// </summary>
    /// <param name="player">The player to deal cards to</param>
    /// <param name="cardsToDeal">The number of cards to deal</param>
    public void DealCardsToPlayer(Player player, int cardsToDeal)
    {
        for (int i = 0; i < cardsToDeal; i++)
        {
            Card randomCard = deck.responses[Random.Range(0, deck.responses.Count)];
            player.AddCard(randomCard);
            deck.responses.Remove(randomCard);
        }
    }

    /// <summary>
    /// Announces to the game manager that a player wants to play a card
    /// </summary>
    /// <param name="cardPlayer">The player playing the card</param>
    /// <param name="playedCard">The card that was played</param>
    public void PlayCard(Player cardPlayer, Card playedCard)
    {
        //If we are not playing cards, playing one doesn't make sense
        if (currState != GameState.PLAYING_CARDS)
        {
            return;
        }

        //Add the card to the list of cards played by the player this round
        List<Card> cards;
        playedCards.TryGetValue(cardPlayer, out cards);
        if (cards == null)
        {
            cards = new List<Card>();
            cards.Add(playedCard);
            playedCards.Add(cardPlayer, cards);
        }
        else
        {
            cards.Add(playedCard);
        }
        cardPlayer.RemoveCard(playedCard);

        //If not all players have played a card, the round cannot end yet
        //Ignore the card czar in this calculation
        if (playedCards.Count < playerList.Count - 1)
        {
            return;
        }

        //Check if every player has played all cards
        foreach (KeyValuePair<Player, List<Card>> playedCardPair in playedCards)
        {
            if (playedCardPair.Value.Count < CardsToPlay)
            {
                return;
            }
        }

        InitializeWinnerStage();
    }

    /// <summary>
    /// Displays the cards that were played
    /// </summary>
    public void DisplayCards()
    {
        foreach (KeyValuePair<Player, List<Card>> playedPair in playedCards)
        {
            foreach (Card c in playedPair.Value)
            {
                GameObject whiteCard = (GameObject)Instantiate(WhiteCardPrefab);
                whiteCard.GetComponent<CardWrapper>().card = c;
                whiteCard.transform.SetParent(DisplayCardPanel.transform);
            }
        }
    }

    /// <summary>
    /// Looks for the player that played a card
    /// </summary>
    /// <param name="card">The card to find the player of</param>
    /// <returns>The player, or null if none was found</returns>
    public Player FindPlayerForCard(Card card)
    {
        foreach (KeyValuePair<Player, List<Card>> pair in playedCards)
        {
            foreach (Card c in pair.Value)
            {
                if (c == card)
                {
                    return pair.Key;
                }

            }
        }
        return null;
    }

    /// <summary>
    /// Selects a winning card and adds 1 to a player's score
    /// </summary>
    /// <param name="card">The winning card</param>
    public void SelectWinningCard(Card card)
    {
        if (currState != GameState.SELECTING_WINNER)
        {
            return;
        }

        Player winner = FindPlayerForCard(card);
        playerScores[winner] = playerScores[winner] + 1;
        InitializeDisplayingWinnerStage();
    }
}
