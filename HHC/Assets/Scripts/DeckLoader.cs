using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

public class DeckLoader : MonoBehaviour {
    /// <summary>
    /// Path of the decks
    /// </summary>
    private static string DeckPath = Application.persistentDataPath + "/Decks";

    /// <summary>
    /// Loads a deck from a given deck code
    /// </summary>
    /// <param name="deckCode">The cardcast deck code</param>
    /// <returns>The parsed deck</returns>
    public static Deck LoadDeck(string deckCode)
    {
        if (!Directory.Exists(DeckPath))
        {
            Directory.CreateDirectory(DeckPath);
        }
        string currDeckPath = DeckPath + "/" + deckCode;
        if (!Directory.Exists(currDeckPath))
        {
            Directory.CreateDirectory(currDeckPath);
            string cardsJson = LoadJsonFromApi(ConfigManager.GetValue("CardListUrl").Replace("<DECK_CODE>", deckCode));
            string deckInfoJson = LoadJsonFromApi(ConfigManager.GetValue("DeckInfoUrl").Replace("<DECK_CODE>", deckCode));
            File.WriteAllText(currDeckPath + "/cards.json", cardsJson);
            File.WriteAllText(currDeckPath + "/deck.json", deckInfoJson);
        }
        string deckJson = File.ReadAllText(currDeckPath + "/cards.json");
        Debug.Log(deckJson);
        Debug.Log("Loaded deck json");
        Deck deck = JsonConvert.DeserializeObject<Deck>(deckJson);
        Debug.Log("Parsed json");
        return deck;
    }
    

    /// <summary>
    /// Loads json from the passed url
    /// </summary>
    /// <param name="url">The url to load data from</param>
    /// <returns>String containing the contents of the url</returns>
    public static string LoadJsonFromApi(string url)
    {
        WWW webreq = new WWW(url);
        while (!webreq.isDone);
        return webreq.text;
    }
}
