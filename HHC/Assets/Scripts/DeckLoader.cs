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
        string deckJson = LoadJsonFromApi("https://api.cardcastgame.com/v1/decks/" + deckCode + "/cards");
        Deck deck = JsonConvert.DeserializeObject<Deck>(deckJson);
        return deck;
    }
    

    /// <summary>
    /// Loads json from the passed url
    /// </summary>
    /// <param name="url">The url to load data from</param>
    /// <returns>String containing the contents of the url</returns>
    public static string LoadJsonFromApi(string url)
    {
        ServicePointManager.ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;

        WebRequest webRequest = WebRequest.Create(url);
        WebResponse webResponse = webRequest.GetResponse();
        Stream dataStream = webResponse.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string deckJson = reader.ReadToEnd();
        reader.Close();
        webResponse.Close();
        return deckJson;
    }
}
