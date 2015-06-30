using UnityEngine;
using System.Collections;

// Developed by Lovely Dog Studio
namespace com.lovelydog.events {

	public class Event : IEvent {		
		public Event () {}
	}

	public class Event<TParam> : Event {
		
		TParam _param;

		public Event() {}
		
		public Event (TParam param) {
			_param = param;
		}

		public TParam GetFirstParam () {
			return _param;
		}

		public Event<TParam> SetParam (TParam value) {
			_param = value;
			return this;
		}
	}
}