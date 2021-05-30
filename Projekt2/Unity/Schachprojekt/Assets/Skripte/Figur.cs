using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Figur : MonoBehaviour
{
	//IdleTrigger
	//AngriffTrigger
	//SterbeTrigger

	private Quaternion drehgradStart;
	private Quaternion drehgradEnde;
	private float timeCount = 0.0f;
	private bool drehungAktiv;
	public void DreheFigur(float drehung)
	{
		this.drehgradEnde = Quaternion.Euler(0, drehung, 0);
		//this.drehgradStart = Quaternion.LookRotation(transform.forward);
		//	this.drehgradStart = Quaternion.LookRotation(transform.localRotation);
		this.drehgradStart = transform.localRotation;
		this.drehungAktiv = true;
		timeCount = 0;
		//Debug.Log(timeCount);
		//Debug.Log(Time.deltaTime);
		//Debug.Log(drehgradStart.eulerAngles.y);
		//Debug.Log(drehgradEnde.eulerAngles.y);
	}
    public void Update()
    {

		if (transform.rotation != this.drehgradEnde && drehungAktiv)
		{
		//	Debug.Log("Update");
		//	Debug.Log(timeCount);
		//	Debug.Log(drehgradStart.eulerAngles.y);
		//	Debug.Log(drehgradEnde.eulerAngles.y);
			transform.rotation = Quaternion.Slerp(this.drehgradStart, this.drehgradEnde, timeCount);
			timeCount = timeCount + Time.deltaTime;
		}
        else
        {
			//this.drehungAktiv = false;
        }
		if (transform.rotation == this.drehgradEnde)
        {
			this.drehungAktiv = false;
        }
	}

    public Animator animator;
	public void IdleAnimation()
    {
		//Debug.Log("Idle Ausgeführt");
		animator.SetTrigger("IdleTrigger");
    }

	public void SterbeAnimation()
    {
		//Debug.Log("Sterbe Ausgeführt");
		animator.SetTrigger("SterbeTrigger");
    }

	public void AngriffAnimation()
    {
		//Debug.Log("Angriff ausgeführt");
		animator.SetTrigger("AngriffTrigger");
    }



	// Das Schachbrett
	public Schachbrett schachbrett;

	// Das aktuell belegte Feld
	public Vector2Int position;

	// Die Farbe der Figur
	public FigurFarbe figurFarbe;


	public bool WurdeBewegt; // Rochade & Bauern
	public List<Vector2Int> Bewegungsmöglichkeiten;

	private IObjectTweener tweener;

	public abstract List<Vector2Int> WaehleMoeglicheFelder();

	// Pseudo Konstruktor
	private void Awake()
	{
		this.animator = GetComponent<Animator>();
		tweener = GetComponent<IObjectTweener>();
		//Debug.Log("Erhalte Tweener: " + tweener);
		Bewegungsmöglichkeiten = new List<Vector2Int>();
		WurdeBewegt = false;
	}

	// Aufruf um der Figur alle Daten hinzuzufügen nachdem sie erstellt wurde
	public void SetzeFigurdaten(Vector2Int position, FigurFarbe team, Schachbrett schachbrett)
	{
		this.figurFarbe = team;
		this.position = position;
		this.schachbrett = schachbrett;

		//Figur entsprechende Position hinzufügen
		transform.position = this.schachbrett.RelativePositionZumSchachbrettfeld(position);

		if (this.figurFarbe == FigurFarbe.weiss)
        {
			//transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
			//(Vector3.up)
			transform.Rotate(0, 180, 0); 
		}
	}


	public bool IstGleichesTeam(Figur figur) { return this.figurFarbe == figur.figurFarbe; }
	public bool BewegungMoeglichZu(Vector2Int position) { return Bewegungsmöglichkeiten.Contains(position); }
	protected void AddBewegungsmoeglichkeit(Vector2Int position) { Bewegungsmöglichkeiten.Add(position); }

	public virtual void BewegeFigur(Vector2Int coords)
	{
		Vector3 targetPosition = schachbrett.CalculatePositionFromCoords(coords);
		position = coords;
		WurdeBewegt = true;
		tweener.MoveTo(transform, targetPosition);
	}

	//Unbekannt ob es wichtig ist welcher Figurtyp angegriffen wird
	public bool IsAttackingPieceOfType<T>() where T : Figur
	{
		foreach (var feld in Bewegungsmöglichkeiten)
		{
			if (schachbrett.GetPieceOnSquare(feld) is T)
				return true;
		}
		return false;
	}

}