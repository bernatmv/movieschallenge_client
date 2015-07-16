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
		protected bool showRetry = true;
		protected bool showGoBack = true;
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
				// if finished correctly
				if (req.State == HTTPRequestStates.Finished) {
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
			if (showRetry && showGoBack) {
				_dispatcher.Dispatch ("api_error_retry_and_back");
			} 
			else {
				if (showRetry) {
					_dispatcher.Dispatch("api_error_retry");
				}
				else {
					_dispatcher.Dispatch("api_error_back");
				}
			}
		}
	}
}
