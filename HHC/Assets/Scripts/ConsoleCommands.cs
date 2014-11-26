using UnityEngine;
using System.Collections.Generic;


public class ConsoleCommands : MonoBehaviour {

    /// <summary>
    /// Initialization
    /// </summary>
	public void Start () {
        ConsoleCommandsRepository repo = ConsoleCommandsRepository.Instance;
        repo.RegisterCommand("show_czar", ShowCzar);
        repo.RegisterCommand("list_players", ListPlayers);
        repo.RegisterCommand("show_gamestate", ShowGameState);
        repo.RegisterCommand("display_playerhands", DisplayPlayerHands);
	}

    /// <summary>
    /// Displays the current card czar
    /// </summary>
    /// <param name="args"></param>
    public void ShowCzar(params string[] args)
    {
        Debug.Log(GameObject.Find("GameManager").GetComponent<GameManager>().cardCzarIndex);
    }

    /// <summary>
    /// Lists the players currently in the game
    /// </summary>
    /// <param name="args"></param>
    public void ListPlayers(params string[] args)
    {
        List<Player> players = GameObject.Find("GameManager").GetComponent<GameManager>().playerList;
        Debug.Log("Playercount: " + players.Count);
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log(i + ": " + players[i].playerName);
        }
    }

    /// <summary>
    /// Displays the current gamestate
    /// </summary>
    /// <param name="args"></param>
    public void ShowGameState(params string[] args)
    {
        Debug.Log(GameObject.Find("GameManager").GetComponent<GameManager>().currState);
    }

    /// <summary>
    /// Displays the hands of all players
    /// </summary>
    /// <param name="args"></param>
    public void DisplayPlayerHands(params string[] args)
    {
        foreach (Player p in GameObject.Find("GameManager").GetComponent<GameManager>().playerList)
        {
            Debug.Log(p.playerName);
            foreach (Card c in p.GetHand())
            {
                Debug.Log(c.GetText());
            }
        }
    }
}
