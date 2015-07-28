using UnityEngine;
using UnityEngine.UI;
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

		public static void fadeInPanel (MonoBehaviour context, CanvasGroup canvas, float aTime, Action callback) {
			context.StartCoroutine (Utils.fadePanel (canvas, aTime, () => {
				Utils.showCanvas(canvas);
				callback();
			}, 1f));
		}

		public static void fadeOutPanel (MonoBehaviour context, CanvasGroup canvas, float aTime, Action callback) {
			context.StartCoroutine (Utils.fadePanel (canvas, aTime, () => {
				Utils.hideCanvas(canvas);
				callback();
			}, 0f));
		}
		
		public static IEnumerator fadePanel (CanvasGroup canvas, float aTime, Action callback, float newAlpha)
		{
			float alpha = canvas.alpha;
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
				canvas.alpha = Mathf.Lerp(alpha, newAlpha, t);
				yield return null;
			}
			// execute callback
			callback ();
			// finish coroutine
			yield return null;
		}

		public void loadScene(string scene) {
			// reset dispatcher
			_dispatcher.Reset ();
			// load level
			Application.LoadLevel(scene);
		}

		public void reloadScene() {
			// reset dispatcher
			_dispatcher.Reset ();
			// load level
			Application.LoadLevel(Application.loadedLevel);
		}
		
		public static void delayAction(MonoBehaviour context, Action callback, float seconds) {
			context.StartCoroutine (Utils.delayActionCoroutine (callback, seconds));
		}

		public static IEnumerator delayActionCoroutine(Action callback, float seconds) {
			yield return new WaitForSeconds (seconds);
			// execute callback
			callback ();
			// finish coroutine
			yield return null;
		}

		public static string limitString(string str, int limit) {
			return (str.Length > limit) ? str.Substring(0, (limit - 1)) + "..." : str;
		}

		public static void hideCanvas(CanvasGroup canvas) {
			if (canvas != null) {
				canvas.interactable = false;
				canvas.blocksRaycasts = false;
				canvas.alpha = 0f;
			}
		}

		public static void showCanvas(CanvasGroup canvas) {
			if (canvas != null) {
				canvas.alpha = 1f;
				canvas.interactable = true;
				canvas.blocksRaycasts = true;
			}
		}

		public static void openWebSite(string uri) {
			Application.OpenURL(uri);
		}

		public static string replaceFirst(string text, string search, string replace)
		{
			int pos = text.IndexOf(search);
			if (pos < 0)
			{
				return text;
			}
			return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
		}
	}
}
