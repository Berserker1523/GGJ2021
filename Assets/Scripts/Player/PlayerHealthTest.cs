public class PlayerHealthTest : IntEventInvoker
{
    private CountdownTimer takeDamage;
    private DamageReceivedEvent dm;

    private void Start()
    {
        dm = new DamageReceivedEvent();
        unityEvents.Add(EventName.DamageReceived, dm);
        EventManager.AddInvoker(EventName.DamageReceived, this);

        takeDamage = gameObject.AddComponent<CountdownTimer>();
        takeDamage.AddTimerFinishedListener(hit);
        takeDamage.Duration = 11f;
        takeDamage.Run();
    }

    private void hit()
    {
        dm.Invoke(0);
    }
}
