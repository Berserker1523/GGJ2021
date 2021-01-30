public class PlayerHealth : IntEventInvoker
{
    private int lifes;
    private GameOverEvent gameOverEvent;

    private void Start()
    {
        lifes = ConfigurationUtils.PlayerLifes;
        EventManager.AddListener(EventName.DamageReceived, TakeDamage);

        gameOverEvent = new GameOverEvent();
        unityEvents.Add(EventName.GameOver, gameOverEvent);
        EventManager.AddInvoker(EventName.GameOver, this);
    }

    private void TakeDamage(int unused)
    {
        if(lifes == 0)
        {
            gameOverEvent.Invoke(0);
            return;
        }

        lifes -= 1;
    }
}
