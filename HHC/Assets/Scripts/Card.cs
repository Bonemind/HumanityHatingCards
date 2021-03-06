﻿using UnityEngine;
using System.Collections;
using System;

public class Card {
    /// <summary>
    /// The unique cardcast id
    /// </summary>
    public string id;

    /// <summary>
    /// The actual text of the card
    /// after every entry there should be a blank
    /// </summary>
    public string[] text;

    /// <summary>
    /// The creation timestamp
    /// </summary>
    public DateTime created_at;

    /// <summary>
    /// Formats this cards text for display
    /// </summary>
    /// <returns>The formatted cardtext</returns>
    public string GetText()
    {
        return string.Join("____", text);
    }
}
