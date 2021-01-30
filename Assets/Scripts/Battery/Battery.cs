using UnityEngine;

public class Battery : IntEventInvoker
{
    private BatteryPickedEvent batteryPickedEvent;

    private void Start()
    {
        batteryPickedEvent = new BatteryPickedEvent();
        unityEvents.Add(EventName.BatteryPicked, batteryPickedEvent);
        EventManager.AddInvoker(EventName.BatteryPicked, this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == CustomTag.Player.ToString())
        {
            batteryPickedEvent.Invoke(0);
            gameObject.SetActive(false);
        }
    }
}
