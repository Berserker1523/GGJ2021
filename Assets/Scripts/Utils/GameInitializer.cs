using UnityEngine;

/// <summary>
/// Initializes static classes of the game
/// </summary>
public class GameInitializer : MonoBehaviour
{
	private void Awake()
    {
        ConfigurationUtils.Initialize();
        EventManager.Initialize();
    }
}