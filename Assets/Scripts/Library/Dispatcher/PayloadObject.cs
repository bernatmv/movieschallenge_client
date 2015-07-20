using UnityEngine;
using System;

// Developed by Lovely Dog Studio
namespace com.lovelydog {
	
	public class PayloadObject : UnityEngine.Object {
		
		public Action action;
		public int intPayload;
		public float floatPayload;
		public string stringPayload;
		public bool boolPayload;
		
		public PayloadObject (Action newAction) {
			action = newAction;
		}
		
		public PayloadObject (int newIntPayload) {
			intPayload = newIntPayload;
		}
		
		public PayloadObject (float newFloatPayload) {
			floatPayload = newFloatPayload;
		}
		
		public PayloadObject (string newStringPayload) {
			stringPayload = newStringPayload;
		}

		public PayloadObject (bool newBoolPayload) {
			boolPayload = newBoolPayload;
		}
	}
}