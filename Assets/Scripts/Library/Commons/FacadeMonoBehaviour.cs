using UnityEngine;
using System.Collections;
using com.lovelydog.events;

namespace com.lovelydog.movieschallenge
{
	public class FacadeMonoBehaviour : MonoBehaviour {
		// instantiate dispatcher
		protected Dispatcher<Game.Events> _dispatcher = Dispatcher<Game.Events>.Instance;
		// instantiate world object
		protected World _world = World.Instance;
	}
}