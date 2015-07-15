using UnityEngine;
using System;
using System.Collections;

namespace com.lovelydog
{
	public class Utils {
		// instantiate dispatcher
		protected Dispatcher<UnityEngine.Object> _dispatcher = Dispatcher<UnityEngine.Object>.Instance;

		public static void ShuffleArray<T>(T[] arr) {
			for (int i = arr.Length - 1; i > 0; i--) {
				int r = UnityEngine.Random.Range(0, i);
				T tmp = arr[i];
				arr[i] = arr[r];
				arr[r] = tmp;
			}
		}
		
		public void loadScene(string scene) {
			// reset dispatcher
			_dispatcher.Reset ();
			// load level
			Application.LoadLevel(scene);
		}
	}
}
