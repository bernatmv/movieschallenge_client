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
				"es", 
				new Dictionary<string, string> {
					{"LOG_IN_TITLE", "Entra"},
					{"USERNAME_LABEL", "Nombre de usuario"},
					{"PASSWORD_LABEL", "Contraseña"},
					{"SUBMIT_BUTTON", "Enviar"},
					{"START_NEW_GAME_LABEL", "Empezar una nueva partida"},
					{"TURN_TEXT", "Turno"},
					{"LVL1_TITLE", "Novato"},
					{"USERNAME_PLACEHOLDER_ETC", "usuario..."},
					{"CHALLENGE_BUTTON", "Desafiar"},
					{"RANDOM_BUTTON", "Aleatorio"},
					{"OOPS_ERROR_TEXT", "¡Ups!\nParece que algo no ha funcionado como se esperaba :("},
					{"RETRY_BUTTON", "Reintentar"},
					{"GO_TO_MAIN_MENU_BUTTON", "Ir al menu de juegos"},
					{"LOADING_TEXT", "Enviando..."},
					{"ERROR_HINT_TEXT", "Este juego requiere una conexión a internet, porfavor verifica que estás conectado antes de reintentar. Gracias."},
					{"GO_MENU_BUTTON", "Menu"},
					{"TAP_ME_BUTTON", "¡Pícame!"},
					{"YOU_WON_TEXT", "¡Ganaste!"},
					{"YOU_LOST_TEXT", "¡Perdiste!"},
					{"CORRECT_MESSAGE", "CORRECTO!"},
					{"WRONG_MESSAGE", "¡MOOOOC!"},
					{"FINAL_ROUND_TITLE", "Ronda Final!"},
					{"FINAL_ROUND_HINT", "Contesta correctamente a 4 o más para ganar."},
					{"STAR_QUESTION_TITLE", "Jugando para completar:\n"},
					{"STAR_QUESTION_HINT", "Si contestas correctamente completarás la categoría. ¡Pero vigila! Si fallas, perderás todo el progreso en esta categoría."},
					{"CATEGORY_1", "Familiar"},
					{"CATEGORY_2", "Romántica"},
					{"CATEGORY_3", "SiFi y fantástico"},
					{"CATEGORY_4", "Humor"},
					{"CATEGORY_5", "Historia y acción"},
					{"CATEGORY_6", "Otras"},
					{"SPANISH_LOCALE_BUTTON", "Español"},
					{"ENGLISH_LOCALE_BUTTON", "Inglés"},
					{"CONTRIBUTE_BUTTON", "Contribuye"},
					{"CONTRIBUTE_TEXT", "¿Quieres que tus frases favoritas aparezcan en el juego?\n¡Envíanoslas y te añadiremos en los creditos!"},
				}
			);
			tags.Add (
				"en", 
				new Dictionary<string, string> {
					{"LOG_IN_TITLE", "Log in"},
					{"USERNAME_LABEL", "Username"},
					{"PASSWORD_LABEL", "Password"},
					{"SUBMIT_BUTTON", "Submit"},
					{"START_NEW_GAME_LABEL", "Start a new game"},
					{"TURN_TEXT", "Turn"},
					{"LVL1_TITLE", "Novice"},
					{"USERNAME_PLACEHOLDER_ETC", "username..."},
					{"CHALLENGE_BUTTON", "Challenge"},
					{"RANDOM_BUTTON", "Random"},
					{"OOPS_ERROR_TEXT", "Oops!\nSomething unexpected happened :("},
					{"RETRY_BUTTON", "Retry"},
					{"GO_TO_MAIN_MENU_BUTTON", "Go to the games menu"},
					{"LOADING_TEXT", "Loading..."},
					{"ERROR_HINT_TEXT", "Movies Challenge requieres an active internet connection, please check that you are online before retrying. Thank you."},
					{"GO_MENU_BUTTON", "Menu"},
					{"TAP_ME_BUTTON", "Tap me!"},
					{"YOU_WON_TEXT", "You won!"},
					{"YOU_LOST_TEXT", "You lost!"},
					{"CORRECT_MESSAGE", "CORRECT!"},
					{"WRONG_MESSAGE", "MOOOOC!"},
					{"FINAL_ROUND_TITLE", "Final Round!"},
					{"FINAL_ROUND_HINT", "Answer correctly 4 or more questions to win the game."},
					{"STAR_QUESTION_TITLE", "Playing to complete the category:\n"},
					{"STAR_QUESTION_HINT", "If you answer correctly you will complete this category, but be careful! If you choose the wrong answer, you will lose all progress on this category!"},
					{"CATEGORY_1", "Family"},
					{"CATEGORY_2", "Romantic"},
					{"CATEGORY_3", "SiFi & fantasy"},
					{"CATEGORY_4", "Humor"},
					{"CATEGORY_5", "History & action"},
					{"CATEGORY_6", "Other"},
					{"NOT_YOUR_TURN_TEXT", "Not your turn"},
					{"SPANISH_LOCALE_BUTTON", "Spanish"},
					{"ENGLISH_LOCALE_BUTTON", "English"},
					{"CONTRIBUTE_BUTTON", "Contribute"},
					{"CONTRIBUTE_TEXT", "Do you want your favorite movie quotes to appear on the game?\nSend them and we will add your name to the credits!"},
				}
			);
		}
	}
}
