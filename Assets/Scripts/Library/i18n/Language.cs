using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace com.lovelydog
{
	public sealed class Language {
		// private properties
		private static readonly Language _instance = new Language();
		private LanguageTags languageTags = new LanguageTags();
		private string lang;
		private Dictionary<string, string> tags;

		// public methods
		public static Language Instance {
			get { return _instance; }
		}

		public Language() {
			lang = PlayerPrefs.GetString ("lang");
			validateLang ();
		}

		void validateLang() {
			if (string.IsNullOrEmpty (lang)) {
				lang = "en";
			}
			// get the appropiated language tags
			if (!languageTags.tags.TryGetValue (lang, out tags)) {
				tags = new Dictionary<string, string>();
			}
		}

		public void setLocale(string newLang) {
			lang = newLang;
			validateLang ();
			PlayerPrefs.SetString ("lang", lang);
			PlayerPrefs.Save();
		}

		public string getLocale() {
			return lang;
		}

		public string get(string tagName) {
			string tagValue;
			tags.TryGetValue (tagName, out tagValue);
			if (!string.IsNullOrEmpty (tagValue)) {
				return tagValue;
			} 
			else {
				return tagName;
			}
		}
	}
}
