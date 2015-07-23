using UnityEngine;
using System.Collections;

public class ConfirmationModel<T> : Object {
	
	public bool success;
	public string error;
	public T payload;	
}
