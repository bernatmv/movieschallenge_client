using UnityEngine;
using System;

// Developed by Lovely Dog Studio
namespace com.lovelydog {
	
	public class CallbackObject : UnityEngine.Object {

		public Action action;

		public CallbackObject (Action newAction) {
			action = newAction;
		}
	}
}