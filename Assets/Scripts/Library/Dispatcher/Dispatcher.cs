using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Developed by Lovely Dog Studio
namespace com.lovelydog.events {

	public sealed class Dispatcher<T> : IDispatcher<T> {

		// properties
		private static readonly Dispatcher<T> _instance = new Dispatcher<T>();
		private Dictionary<T, List<Action<IEvent>>> _signal = new Dictionary<T, List<Action<IEvent>>>();
		private Event _nullEvent = new Event();

		// private methods
		private Dispatcher () {}		

		private void DispatchEvent (T eventId, IEvent eventData) {
			List<Action<IEvent>> list;
			if (_signal.TryGetValue (eventId, out list)) {
				foreach (Action<IEvent> callback in list) {
					callback (eventData);
				}
			}
		}

		// public methods
		public static Dispatcher<T> Instance {
			get { return _instance; }
		}

		public void Dispatch(T eventId, IEvent eventData = null) {
			if (eventData == null) {
				eventData = _nullEvent;
			}
			DispatchEvent (eventId, eventData);
		}

		public void AddListener(T eventId, Action<IEvent> callback) {
			List<Action<IEvent>> list;
			if (!_signal.TryGetValue (eventId, out list)) {
				list = new List<Action<IEvent>>();
			}
			list.Add(callback);
			_signal[eventId] = list;
		}

		public void RemoveListener(T eventId, Action<IEvent> callback) {
			List<Action<IEvent>> list;
			if (_signal.TryGetValue (eventId, out list)) {
				Action<IEvent> itemToRemove = list.SingleOrDefault(e => e == callback);
				if (itemToRemove != null) {
					list.Remove(itemToRemove);
					_signal[eventId] = list;
				}
			}
		}
		
		public void Reset () {
			_signal = new Dictionary<T, List<Action<IEvent>>>();
		}
	}
}