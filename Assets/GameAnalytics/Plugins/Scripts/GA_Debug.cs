﻿/// <summary>
/// This class handles error and exception messages, and makes sure they are added to the Quality category 
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GameAnalyticsSDK
{
	public static class GA_Debug
	{
		public static bool SubmitErrors = true;
		public static int MaxErrorCount = 10;
		
		private static int _errorCount = 0;

		private static bool _showLogOnGUI = false;
		public static List<string> Messages;
		
		/// <summary>
		/// If SubmitErrors is enabled on the GA object this makes sure that any exceptions or errors are submitted to the GA server
		/// </summary>
		/// <param name="logString">
		/// The message <see cref="System.String"/>
		/// </param>
		/// <param name="stackTrace">
		/// The exception stack trace <see cref="System.String"/>
		/// </param>
		/// <param name="type">
		/// The type of the log message (we only submit errors and exceptions to the GA server) <see cref="LogType"/>
		/// </param>
		public static void HandleLog(string logString, string stackTrace, LogType type)
		{
			//Only used for testing
			if (_showLogOnGUI)
			{
				if (Messages == null)
				{
					Messages = new List<string>();
				}
				Messages.Add(logString);
			}

			//We only submit exceptions and errors
			if (SubmitErrors && _errorCount < MaxErrorCount && type != LogType.Log)
			{
				_errorCount++;
				
				string lString = logString.Replace('"', '\'').Replace('\n', ' ').Replace('\r', ' ');
				string sTrace = stackTrace.Replace('"', '\'').Replace('\n', ' ').Replace('\r', ' ');
				
				SubmitError(lString + " " + sTrace, type);
			}
		}
		
		private static void SubmitError(string message, LogType type)
		{
			GA_Error.GAErrorSeverity severity = GA_Error.GAErrorSeverity.GAErrorSeverityInfo;

			switch (type)
			{
			case LogType.Assert:
				severity = GA_Error.GAErrorSeverity.GAErrorSeverityInfo;
				break;
			case LogType.Error:
				severity = GA_Error.GAErrorSeverity.GAErrorSeverityError;
				break;
			case LogType.Exception:
				severity = GA_Error.GAErrorSeverity.GAErrorSeverityCritical;
				break;
			case LogType.Log:
				severity = GA_Error.GAErrorSeverity.GAErrorSeverityDebug;
				break;
			case LogType.Warning:
				severity = GA_Error.GAErrorSeverity.GAErrorSeverityWarning;
				break;
			}

			GA_Error.NewEvent(severity, message);
		}
		
		/// <summary>
		/// Used only for testing purposes
		/// </summary>
		public static void EnabledLog ()
		{
			_showLogOnGUI = true;
		}
	}
}