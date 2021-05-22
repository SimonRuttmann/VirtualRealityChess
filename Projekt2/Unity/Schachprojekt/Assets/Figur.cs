using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Figur : MonoBehaviour
{
	// Das Schachbrett
	public Schachbrett Schachbrett;

	// Das aktuell belegte Feld
	public Vector2Int Position;

	// Die Farbe der Figur
	public FigurFarbe FigurFarbe;


	public bool WurdeBewegt; // Rochade & Bauern
	public List<Vector2Int> Bewegungsmöglichkeiten;

	//private IObjectTweener tweener;

	public abstract List<Vector2Int> SelectAvaliableSquares();

	// Pseudo Konstruktor
	private void Awake()
	{
		Bewegungsmöglichkeiten = new List<Vector2Int>();
		WurdeBewegt = false;
	}

	// Aufruf um der Figur alle Daten hinzuzufügen nachdem sie erstellt wurde
	public void GebeFigurdaten(Vector2Int position, FigurFarbe team, Schachbrett schachbrett)
	{
		this.FigurFarbe = team;
		Position = position;
		this.Schachbrett = schachbrett;

		//Figur entsprechende Position hinzufügen
		transform.position = Schachbrett.RelativePositionZumSchachbrettfeld(position);
	}


	public bool IstGleichesTeam(Figur figur) { return this.FigurFarbe == figur.FigurFarbe; }
	public bool BewegungMoeglichZu(Vector2Int position) { return Bewegungsmöglichkeiten.Contains(position); }
	protected void AddBewegungsmoeglichkeit(Vector2Int position) { Bewegungsmöglichkeiten.Add(position); }

	public virtual void MovePiece(Vector2Int coords)
	{

	}

}
	
