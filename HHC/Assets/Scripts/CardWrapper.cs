using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Makes sure the text of a card gets updated when it is assigned a card
/// Also makes sure a card can be assigned to an object
/// </summary>
public class CardWrapper : MonoBehaviour {
    /// <summary>
    /// The text ui component to set the text of
    /// </summary>
    Text text;

    /// <summary>
    /// The card object
    /// </summary>
    private Card _card;

    /// <summary>
    /// Getter and setter for the card object of this gameobject
    /// </summary>
    public Card card
    {
        get
        {
            return _card;
        }
        set
        {
            this._card = value;
            if (this.text != null)
            {
                this.text.text = card.GetText();
            }
        }
    }
    
	// Use this for initialization
	void Start () {
        text = GetComponentInChildren<Text>();
        if (_card != null)
        {
            text.text = _card.GetText();
        }
	
	}

    public void Hello(bool a)
    {
        Debug.Log("Hello");
    }
}
