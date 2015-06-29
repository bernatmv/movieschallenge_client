using UnityEngine;
using System.Collections;

// Developed by Lovely Dog Studio
namespace com.lovelydog.events {

	public class Event : IEvent {		
		public Event () {}
	}

	public class Event<TParam1, TParam2, TParam3> : Event {

		TParam1 _param1;
		TParam2 _param2;
		TParam3 _param3;

		public Event() {}

		public Event (TParam1 param1, TParam2 param2, TParam3 param3) {
			_param1 = param1;
			_param2 = param2;
			_param3 = param3;
		}

		public TParam1 GetFirstParam () {
			return _param1;
		}

		public TParam2 GetSecondParam () {
			return _param2;
		}

		public TParam3 GetThirdParam () {
			return _param3;
		}

		public Event<TParam1, TParam2, TParam3> SetFirstParam (TParam1 value) {
			_param1 = value;
			return this;
		}
		
		public Event<TParam1, TParam2, TParam3> SetSecondParam (TParam2 value) {
			_param2 = value;
			return this;
		}
		
		public Event<TParam1, TParam2, TParam3> SetThirdParam (TParam3 value) {
			_param3 = value;
			return this;
		}
	}

	public class Event<TParam1, TParam2> : Event {
		
		TParam1 _param1;
		TParam2 _param2;

		public Event() {}
		
		public Event (TParam1 param1, TParam2 param2) {
			_param1 = param1;
			_param2 = param2;
		}

		public TParam1 GetFirstParam () {
			return _param1;
		}
		
		public TParam2 GetSecondParam () {
			return _param2;
		}

		public Event<TParam1, TParam2> SetFirstParam (TParam1 value) {
			_param1 = value;
			return this;
		}
		
		public Event<TParam1, TParam2> SetSecondParam (TParam2 value) {
			_param2 = value;
			return this;
		}
	}

	public class Event<TParam1> : Event {
		
		TParam1 _param1;

		public Event() {}
		
		public Event (TParam1 param1) {
			_param1 = param1;
		}

		public TParam1 GetFirstParam () {
			return _param1;
		}

		public Event<TParam1> SetFirstParam (TParam1 value) {
			_param1 = value;
			return this;
		}
	}
}