/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    private static ConfigurationData configurationData;

    #region Properties

    public static float PlayerMovementSpeed
    {
        get { return configurationData.PlayerMovementSpeed; }
    }

    public static float LanternTotalBattery
    {
        get { return configurationData.LanternTotalBattery; }
    }

    public static float BatterySpawnSeconds
    {
        get { return configurationData.BatterySpawnSeconds; }
    }

    #endregion

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        configurationData = new ConfigurationData();
    }
}
