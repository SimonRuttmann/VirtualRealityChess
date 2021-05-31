using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessUIManager : MonoBehaviour
{
	[SerializeField] private GameObject teamanzeige;
	[SerializeField] private Text teamtext;
	[SerializeField] private GameObject neustartanzeige;
//	[SerializeField] private Button restartButton;
	[SerializeField] private Text neustarttext;
	[SerializeField] private GameObject fadenkreuz;

	internal void HideUI()
	{
		teamanzeige.SetActive(true);
		neustartanzeige.SetActive(false);
		fadenkreuz.SetActive(true);
	}

	internal void SetTeamanzeige(FigurFarbe farbe)
    {
		if (farbe == FigurFarbe.weiss) teamtext.text = "Team Weiß ist an der Reihe";
		else teamtext.text = "Team Schwarz ist an der Reihe";
    }
	internal void OnGameFinished(string winner)
	{
		fadenkreuz.SetActive(false);
		teamanzeige.SetActive(false);
		neustartanzeige.SetActive(true);
		//finishText.text = string.Format("Team {0} hat gewonnen!", winner);
		neustarttext.text = "Spieler " + winner + "hat gewonnen!\n Drücke X um das Spiel neu zu starten.";
	}
}
