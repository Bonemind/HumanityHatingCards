using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject BlackCardPrefab;
    public GameObject WhiteCardPrefab;
	// Use this for initialization
    private Deck deck;
	void Start () {
        deck = DeckLoader.LoadDeck("H249F");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Card c = deck.calls[Random.RandomRange(0, deck.calls.Count)];
            GameObject currCard = (GameObject) GameObject.Instantiate(BlackCardPrefab);
            currCard.GetComponent<TextMeshWrapper>();

        }
	
	}
}
