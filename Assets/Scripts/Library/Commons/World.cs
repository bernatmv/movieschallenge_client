using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Developed by Lovely Dog Studios
namespace com.lovelydog.movieschallenge 
{
	public sealed class World {

		// private properties
		private static readonly World _instance = new World();
		// public world properties

		// private methods

		// public methods
		public static World Instance {
			get { return _instance; }
		}
	}
}