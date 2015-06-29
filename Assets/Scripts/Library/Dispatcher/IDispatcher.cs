using UnityEngine;
using System;
using System.Collections;

// Developed by Lovely Dog Studio
namespace com.lovelydog.events {

	public interface IDispatcher<T> {
		void Dispatch(T eventId, IEvent eventData);
		void AddListener(T eventId, Action<IEvent> callback);
	}
}