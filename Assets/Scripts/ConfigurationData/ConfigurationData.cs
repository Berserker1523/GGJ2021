using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    private const string ConfigurationDataFileName = "ConfigurationData.csv";
    private Dictionary<ConfigurationDataValueName, float> values = new Dictionary<ConfigurationDataValueName, float>();

    #endregion

    #region Properties

    public float PlayerMovementSpeed
    {
        get { return values[ConfigurationDataValueName.PlayerMovementSpeed]; }
    }

    public float LanternTotalBattery
    {
        get { return values[ConfigurationDataValueName.LanternTotalBattery]; }
    }

    public float BatterySpawnSeconds
    {
        get { return values[ConfigurationDataValueName.BatterySpawnSeconds]; }
    }

    public int PlayerLifes 
    {
        get { return (int)values[ConfigurationDataValueName.PlayerLifes]; }    
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        StreamReader file = null;
        try
        {
            file = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));

            string currentLine = file.ReadLine();
            while (currentLine != null)
            {
                string[] tokens = currentLine.Split(',');
                ConfigurationDataValueName valueName = 
                    (ConfigurationDataValueName)Enum.Parse(typeof(ConfigurationDataValueName), tokens[0]);
                values.Add(valueName, float.Parse(tokens[1]));
                currentLine = file.ReadLine();
            }

        }
        catch(Exception e)
        {
            Debug.LogError(e);
            SetDefaultValues();
        }
        finally
        {
            if(file != null)
            {
                file.Close();
            }
        }
    }

    #endregion

    #region Methods

    private void SetDefaultValues()
    {
        values.Clear();
        //
        //Set up for WebGL Build with final values
        //
        values.Add(ConfigurationDataValueName.PlayerMovementSpeed, 5);
        values.Add(ConfigurationDataValueName.LanternTotalBattery, 10);
        values.Add(ConfigurationDataValueName.BatterySpawnSeconds, 5);
        values.Add(ConfigurationDataValueName.PlayerLifes, 3);
    }

    #endregion
}
