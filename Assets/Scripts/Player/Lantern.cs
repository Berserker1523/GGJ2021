using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    private float lanternTotalBattery;
    private bool canTurnOn = true;

    private Light2D light2D;
    private Collider2D lanternCollider;
    [HideInInspector]
    public CountdownTimer lanternBattery;

    private void Start()
    {
        lanternTotalBattery = ConfigurationUtils.LanternTotalBattery;

        light2D = GetComponent<Light2D>();
        lanternCollider = GetComponent<Collider2D>();

        lanternBattery = gameObject.AddComponent<CountdownTimer>();
        lanternBattery.AddTimerFinishedListener(TurnOff);
        lanternBattery.Duration = lanternTotalBattery;
        lanternBattery.Run();

        EventManager.AddListener(EventName.BatteryPicked, RechargeBattery);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && canTurnOn && lanternBattery.TimeLeft > 0)
        {
            if (lanternBattery.Running)
            {
                Pause();
            }
            else{
                UnPause();
            }
            
        }
    }

    private void Pause()
    {
        lanternBattery.Running = false;
        TurnOff();
    }

    private void UnPause()
    {
        TurnOn();
        lanternBattery.Running = true;
    }

    public void PauseDueToDamage()
    {
        canTurnOn = false;
        if (lanternBattery.Running)
        {
            Pause();
        }
    }

    public void UnPauseDueToDamage()
    {
        canTurnOn = true;
        if (lanternBattery.Running)
        {
            UnPause();
        }
    }

    private void TurnOn()
    {
        light2D.enabled = true;
        lanternCollider.enabled = true;
        AudioManager.Play(AudioClipName.DoubleLanternSwitch);
    }

    private void TurnOff()
    {
        light2D.enabled = false;
        lanternCollider.enabled = false;
        AudioManager.Play(AudioClipName.LanternSwitch);
    }

    private void RechargeBattery(int unused)
    {
        lanternBattery.Duration = lanternTotalBattery;
        lanternBattery.elapsedSeconds = 0;
    }
}
