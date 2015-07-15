using UnityEngine;
using System;
using System.Collections;
using com.lovelydog;

namespace com.lovelydog.movieschallenge
{
	public class FacadeMonoBehaviour : MonoBehaviour {
		// instantiate singletons
		protected Dispatcher<UnityEngine.Object> _dispatcher = Dispatcher<UnityEngine.Object>.Instance;
		protected World _world = World.Instance;
		protected Language _i18n = Language.Instance;
		// instantiate properties
		protected Utils _utils = new Utils();

		public IEnumerator delayAction(Action callback, float seconds) {
			yield return new WaitForSeconds (seconds);
			callback ();
		}

		public static void ShuffleArray<T>(T[] arr) {
			for (int i = arr.Length - 1; i > 0; i--) {
				int r = UnityEngine.Random.Range(0, i);
				T tmp = arr[i];
				arr[i] = arr[r];
				arr[r] = tmp;
			}
		}
	}
}