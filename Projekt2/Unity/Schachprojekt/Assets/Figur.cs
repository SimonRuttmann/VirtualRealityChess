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
	public List<Vector2Int> Bewegungsm�glichkeiten;

	//private IObjectTweener tweener;

	public abstract List<Vector2Int> SelectAvaliableSquares();

	// Pseudo Konstruktor
	private void Awake()
	{
		Bewegungsm�glichkeiten = new List<Vector2Int>();
		WurdeBewegt = false;
	}

	// Aufruf um der Figur alle Daten hinzuzuf�gen nachdem sie erstellt wurde
	public void GebeFigurdaten(Vector2Int position, FigurFarbe team, Schachbrett schachbrett)
	{
		this.FigurFarbe = team;
		Position = position;
		this.Schachbrett = schachbrett;

		//Figur entsprechende Position hinzuf�gen
		transform.position = Schachbrett.RelativePositionZumSchachbrettfeld(position);
	}


	public bool IstGleichesTeam(Figur figur) { return this.FigurFarbe == figur.FigurFarbe; }
	public bool BewegungMoeglichZu(Vector2Int position) { return Bewegungsm�glichkeiten.Contains(position); }
	protected void AddBewegungsmoeglichkeit(Vector2Int position) { Bewegungsm�glichkeiten.Add(position); }

	public virtual void MovePiece(Vector2Int coords)
	{

	}

}
	
