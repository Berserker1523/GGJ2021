using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    private float lanternTotalBattery;
    
    private Light2D light2D;
    private Collider2D collider2D;
    private CountdownTimer lanternBatteryDuration;

    private void Start()
    {
        lanternTotalBattery = ConfigurationUtils.LanternTotalBattery;

        light2D = GetComponent<Light2D>();
        collider2D = GetComponent<Collider2D>();

        lanternBatteryDuration = gameObject.AddComponent<CountdownTimer>();
        lanternBatteryDuration.AddTimerFinishedListener(TurnOffLantern);
        TurnOnLantern();

        EventManager.AddListener(EventName.BatteryPicked, RechargeBattery);
    }

    /*private void Update()
    {
        Debug.Log(lanternBatteryDuration.TimeLeft);
    }*/

    private void TurnOnLantern()
    {
        
        light2D.enabled = true;
        collider2D.enabled = true;
        AudioManager.Play(AudioClipName.DoubleLanternSwitch);
        lanternBatteryDuration.Duration = lanternTotalBattery;
        lanternBatteryDuration.Run();
    }

    private void TurnOffLantern()
    {
        AudioManager.Play(AudioClipName.LanternSwitch);
        light2D.enabled = false;
        collider2D.enabled = false;
    }

    private void RechargeBattery(int unused)
    {
        TurnOnLantern();
    }
}
