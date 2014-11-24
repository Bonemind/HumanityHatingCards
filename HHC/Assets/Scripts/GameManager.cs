using UnityEngine;
using System.Collections;
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
    /// The deck loaded
    /// </summary>
    private Deck deck;

    /// <summary>
    /// Called on update
    /// Currently only contains debug functions
    /// </summary>
    public void Update()
    {
        //Display a random black card
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Card c = deck.calls[Random.Range(0, deck.calls.Count)];
            GameObject currCard = (GameObject)GameObject.Instantiate(BlackCardPrefab);
            currCard.GetComponentInChildren<TextMeshWrapper>().Text = string.Join("_____", c.text);
        }

        //Load deck with number NZ6R5 (Cards agains humanity base deck)
        if (Input.GetKeyDown(KeyCode.D))
        {
            deck = DeckLoader.LoadDeck("NZ6R5");
        }

    }
}
