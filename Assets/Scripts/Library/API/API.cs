using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using com.lovelydog.movieschallenge;
using BestHTTP;
using LitJson;

namespace com.lovelydog
{
	public class API {

		protected string _host = Properties.API;
		protected Dispatcher<UnityEngine.Object> _dispatcher = Dispatcher<UnityEngine.Object>.Instance;
		public bool showRetry = true;
		public bool showGoBack = true;
		public HTTPRequest request;

		public API() {}
		public API(string action, Action<HTTPRequest, HTTPResponse> callback) {
			CreateRequest (action, callback);
		}

		public void Get(string action, Action<HTTPRequest, HTTPResponse> callback) {
			CreateRequest (action, callback);
		}

		public void Post(string action, Action<HTTPRequest, HTTPResponse> callback) {
			CreateRequest (action, callback, HTTPMethods.Post);
		}

		public void Send() {
			Debug.Log ("Begin call");
			request.Send ();
		}
		
		public void AddField(string param, string value) {
			request.AddField (param, value);
		}

		protected void AddOptions() {
			request.ConnectTimeout = System.TimeSpan.FromSeconds (Properties.connectTimeout);
			request.Timeout = System.TimeSpan.FromSeconds (Properties.timeout);
			request.DisableCache = true;
		}

		protected void CreateRequest(string action, Action<HTTPRequest, HTTPResponse> callback, HTTPMethods methodType = HTTPMethods.Get) {
			request = new HTTPRequest (new Uri (_host + action), methodType, (HTTPRequest req, HTTPResponse res) => {
				Debug.Log ("End call");
				Debug.Log (res.DataAsText);
				// if finished correctly
				if (req.State == HTTPRequestStates.Finished) {
					ClearError();
					callback(req, res);
				}
				// if not show an error with a retry and/or a go back to main menu
				else {
					//TODO: log to analytics
					Debug.Log (req.State);
					ShowError();
				}
			});
			// add options
			AddOptions ();
		}

		protected void ShowError() {
			// create a callback object wrapper to be passed in the dispatch
			CallbackObject callback = new CallbackObject (Send);
			// dispatch the appropiated event
			if (showRetry && showGoBack) {
				_dispatcher.Dispatch ("api_error_retry_and_back", callback);
			} 
			else {
				if (showRetry) {
					_dispatcher.Dispatch("api_error_retry", callback);
				}
				else {
					_dispatcher.Dispatch("api_error_back");
				}
			}
		}

		protected void ClearError() {
			_dispatcher.Dispatch("api_error_reset");
		}
	}
}
