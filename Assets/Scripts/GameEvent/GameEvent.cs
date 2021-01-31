﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
	private List<GameEventListener> listeners = new List<GameEventListener>();

	/// <summary>
	/// Call OnEventRaised method from all listeners subscribed to this event.
	/// </summary>
	[ContextMenu("Raise")]
	public void Raise()
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
		{
			listeners[i].OnEventRaised();
		}
	}

	public void RegisterListener(GameEventListener listener)
	{
		listeners.Add(listener);
	}

	public void UnregisterListener(GameEventListener listener)
	{
		listeners.Remove(listener);
	}
}
