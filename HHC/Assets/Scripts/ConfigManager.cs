using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;

public class ConfigManager
{

    /// <summary>
    /// The path of the config
    /// </summary>
    private static string configPath = Application.persistentDataPath + "/" + "config.json";

    /// <summary>
    /// Private config cariable containing the actual config object
    /// </summary>
    private static Dictionary<string, string> _values;

    /// <summary>
    /// Accessor for the config object
    /// </summary>
    public static Dictionary<string, string> values
    {
        get
        {

            if (_values == null)
            {
                LoadConfig();
            }
            return _values;
        }
    }

    /// <summary>
    /// Loads the config file
    /// </summary>
    private static void LoadConfig()
    {
        try
        {
            string configText = File.ReadAllText(configPath);
            _values = JsonConvert.DeserializeObject<Dictionary<string, string>>(configText);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            Debug.Log("Failed to read config, bailing!");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }

    /// <summary>
    /// Saves the config file
    /// </summary>
    public static void SaveConfig() 
    {
        string json = JsonConvert.SerializeObject(_values);
        File.WriteAllText(configPath, json);
    }

    /// <summary>
    /// Gets a value from a keyname
    /// Will return an empty string if the key is not found
    /// </summary>
    /// <param name="keyName">The name to fetch the value of</param>
    /// <returns>The value belonging to that key</returns>
    public static string GetValue(string keyName)
    {
        string outVal = "";
        ConfigManager.values.TryGetValue(keyName, out outVal);
        return outVal;
    }
}
