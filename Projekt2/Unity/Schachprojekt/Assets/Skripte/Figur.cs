using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Figur : MonoBehaviour
{
	// Das Schachbrett
	public Schachbrett schachbrett;

	// Das aktuell belegte Feld
	public Vector2Int position;

	// Die Farbe der Figur
	public FigurFarbe figurFarbe;


	public bool WurdeBewegt; // Rochade & Bauern
	public List<Vector2Int> Bewegungsm�glichkeiten;

	//private IObjectTweener tweener;

	public abstract List<Vector2Int> WaehleMoeglicheFelder();

	// Pseudo Konstruktor
	private void Awake()
	{

		Bewegungsm�glichkeiten = new List<Vector2Int>();
		WurdeBewegt = false;
	}

	// Aufruf um der Figur alle Daten hinzuzuf�gen nachdem sie erstellt wurde
	public void SetzeFigurdaten(Vector2Int position, FigurFarbe team, Schachbrett schachbrett)
	{
		this.figurFarbe = team;
		this.position = position;
		this.schachbrett = schachbrett;

		//Figur entsprechende Position hinzuf�gen
		transform.position = this.schachbrett.RelativePositionZumSchachbrettfeld(position);
	}


	public bool IstGleichesTeam(Figur figur) { return this.figurFarbe == figur.figurFarbe; }
	public bool BewegungMoeglichZu(Vector2Int position) { return Bewegungsm�glichkeiten.Contains(position); }
	protected void AddBewegungsmoeglichkeit(Vector2Int position) { Bewegungsm�glichkeiten.Add(position); }

	public virtual void BewegeFigur(Vector2Int coords)
	{
		Vector3 targetPosition = schachbrett.CalculatePositionFromCoords(coords);
		position = coords;
		WurdeBewegt = true;
		//tweener.MoveTo(transform, targetPosition);
	}

	//Unbekannt ob es wichtig ist welcher Figurtyp angegriffen wird
	public bool IsAttackingPieceOfType<T>() where T : Figur
	{
		foreach (var feld in Bewegungsm�glichkeiten)
		{
			if (schachbrett.GetPieceOnSquare(feld) is T)
				return true;
		}
		return false;
	}

}