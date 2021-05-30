using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessUIManager : MonoBehaviour
{
	[SerializeField] private GameObject teamanzeige;
	[SerializeField] private Text teamtext;
	[SerializeField] private GameObject UIParent;
	[SerializeField] private Button restartButton;
	[SerializeField] private Text finishText;
	[SerializeField] private GameObject fadenkreuz;

	internal void HideUI()
	{
		teamanzeige.SetActive(true);
		UIParent.SetActive(false);
		fadenkreuz.SetActive(true);
	}

	internal void SetTeamanzeige(FigurFarbe farbe)
    {
		if (farbe == FigurFarbe.weiss) teamtext.text = "Team Weiﬂ ist an der Reihe";
		else teamtext.text = "Team Schwarz ist an der Reihe";
    }
	internal void OnGameFinished(string winner)
	{
		fadenkreuz.SetActive(false);
		teamanzeige.SetActive(false);
		UIParent.SetActive(true);
		//finishText.text = string.Format("Team {0} hat gewonnen!", winner);
		finishText.text = "Spieler " + winner + "hat gewonnen!";
	}
}
