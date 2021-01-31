using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
	[SerializeField] private GameEvent gameEvent;
	[SerializeField] private UnityEvent response;

	private void OnEnable()
	{
		gameEvent.RegisterListener(this);
	}

	private void OnDisable()
	{
		gameEvent.UnregisterListener(this);
	}

	/// <summary>
	/// Invokes an action selected in the editor when the game event is triggered.
	/// </summary>
	public void OnEventRaised()
	{
		response.Invoke();
	}
}

