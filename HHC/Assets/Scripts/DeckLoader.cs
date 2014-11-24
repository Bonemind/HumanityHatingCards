using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

public class DeckLoader : MonoBehaviour {
    /// <summary>
    /// Loads a deck from a given deck code
    /// </summary>
    /// <param name="deckCode">The cardcast deck code</param>
    /// <returns>The parsed deck</returns>
    public static Deck LoadDeck(string deckCode)
    {
        string deckJson = LoadJsonFromApi(ConfigManager.GetValue("CardListUrl").Replace("<DECK_CODE>", deckCode));
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
