using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Developed by Lovely Dog Studio
namespace com.lovelydog {

	public sealed class Dispatcher<T> {

		// properties
		private static readonly Dispatcher<T> _instance = new Dispatcher<T>();
		private Dictionary<string, List<Action<T>>> _signal = new Dictionary<string, List<Action<T>>>();

		// private methods
		private Dispatcher () {}		

		// public methods
		public static Dispatcher<T> Instance {
			get { return _instance; }
		}

		public void Dispatch(string eventId, T eventData = default(T)) {
			Debug.Log (eventId);
			List<Action<T>> list;
			if (_signal.TryGetValue (eventId, out list)) {
				foreach (Action<T> callback in list) {
					callback (eventData);
				}
			}
		}

		public void AddListener(string eventId, Action<T> callback) {
			List<Action<T>> list;
			if (!_signal.TryGetValue (eventId, out list)) {
				list = new List<Action<T>>();
			}
			list.Add(callback);
			_signal[eventId] = list;
		}

		public void RemoveListener(string eventId, Action<T> callback) {
			List<Action<T>> list;
			if (_signal.TryGetValue (eventId, out list)) {
				Action<T> itemToRemove = list.SingleOrDefault(e => e == callback);
				if (itemToRemove != null) {
					list.Remove(itemToRemove);
					_signal[eventId] = list;
				}
			}
		}
		
		public void Reset () {
			_signal = new Dictionary<string, List<Action<T>>>();
		}

		public void Noop () {
			// noop
		}
	}
}