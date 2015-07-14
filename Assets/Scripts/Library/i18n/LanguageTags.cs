using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace com.lovelydog
{
	public class LanguageTags {

		public Dictionary<string, Dictionary<string, string>> tags = new Dictionary<string, Dictionary<string, string>>();

		public LanguageTags() {
			tags.Add (
				"ES", 
				new Dictionary<string, string> {
					{"TEST_1", "Test numero 1"},
					{"TEST_2", "Test numero 2"}
				}
			);
			tags.Add (
				"EN", 
				new Dictionary<string, string> {
					{"TEST_1", "Test # 1"},
					{"TEST_2", "Test # 2"}
				}
			);
		}
	}
}
