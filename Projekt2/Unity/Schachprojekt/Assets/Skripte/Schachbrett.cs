using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schachbrett : MonoBehaviour
{
    [SerializeField] private Transform EffektiverStartpunktUntenLinks;
    [SerializeField] private float Feldgroesse;

    public const int GesFeldGroesse = 8;

    private Figur[,] grid;  //Start bei 1, 1
    private Figur gewaehlteFigur;
    private SchachManager schachManager;

    private FeldAuswahlErsteller feldAuswahlErsteller; //anpassen


    public Vector3 RelativePositionZumSchachbrettfeld(Vector2Int position)
    {
        return EffektiverStartpunktUntenLinks.position + new Vector3(position.x * Feldgroesse, 0f, position.y * Feldgroesse);
    }

    protected virtual void Awake()
    {
        feldAuswahlErsteller = GetComponent<FeldAuswahlErsteller>();
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Figur[GesFeldGroesse, GesFeldGroesse];
    }

    public void SetzeAbhaengigkeiten(SchachManager schachManager)
    {
        this.schachManager = schachManager;
    }

    public Vector3 CalculatePositionFromCoords(Vector2Int coords)
    {
        return EffektiverStartpunktUntenLinks.position + new Vector3(coords.x * Feldgroesse, 0f, coords.y * Feldgroesse);
    }

    private Vector2Int CalculateCoordsFromPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt(transform.InverseTransformPoint(inputPosition).x / Feldgroesse) + GesFeldGroesse / 2;
        int y = Mathf.FloorToInt(transform.InverseTransformPoint(inputPosition).z / Feldgroesse) + GesFeldGroesse / 2;
        return new Vector2Int(x, y);
    }

    public void OnSquareSelected(Vector3 inputPosition)
    {
       // Debug.Log("Inputhandler hat OnSquareSelcted aufgerufen mit: " + inputPosition);
        Vector2Int coords = CalculateCoordsFromPosition(inputPosition);
        Figur figur = GetPieceOnSquare(coords);
        Debug.Log("Figur: " + figur + " erhalten");
        
        if (gewaehlteFigur)
        {
        //    Debug.Log("gewaehlte Figur:" + gewaehlteFigur + "und neuer Klick zur Position: " + coords);
        //    Debug.Log("Figur nicht null ->  Bewegung ist moeglich: " + gewaehlteFigur.BewegungMoeglichZu(coords) + " Befehl wird ausgef�hrt:");

            // Figur nochnamal anw�hlen
            if (figur != null && gewaehlteFigur == figur)
            {
           //     Debug.Log("Deselect");
                DeselectFigur();
            }

            //Wir hatten ne weise Figur ausgw�hlt und haben jetzt eine andere Ausgew�hlt
            else if (figur != null && gewaehlteFigur != figur && schachManager.IstTeamzug(figur.figurFarbe))
            {
             //   Debug.Log("neue Figur ausgew�hlt");
                figur.IdleAnimation();
                WahleFigur(figur);
            }

            //Figur ist ausgew�hlt und "Klick" auf bewegbares feld
            else if (gewaehlteFigur.BewegungMoeglichZu(coords))
           // else if (true)
                    {
               // Debug.Log("Figur wird bewegt zu: " + coords);
                OnSelectedPieceMoved(coords, gewaehlteFigur);
            }
        }
        else
        {
            //Debug.Log("Figur 1. mal angew�hlt");
          //  Debug.Log("figur: " + figur + "Farbe" + figur.figurFarbe + "ist Teamzug: " + schachManager.IstTeamzug(figur.figurFarbe));
            if (figur != null && schachManager.IstTeamzug(figur.figurFarbe))
            {
                figur.IdleAnimation();
                //     Debug.Log("W�hle Figur");
                WahleFigur(figur);
            }
           // Debug.Log("Figur ist null, oder der Teamzug ist nicht richtig");
        }
    }

    private void WahleFigur(Figur figur)
    {
        schachManager.RemoveMovesEnablingAttakOnPieceOfType<Koenig>(figur);
        gewaehlteFigur = figur;
      //  Debug.Log("Gew�hlte Figur zugewiesen: " + gewaehlteFigur);
        List<Vector2Int> auswahl = gewaehlteFigur.Bewegungsm�glichkeiten;
    //    Debug.Log("zeige Auswahl: " + auswahl);
        //foreach (var a in auswahl)
       // {
         //   Debug.Log(a);
       // }
        ShowSelectionSquares(auswahl);    
    }

    private void ShowSelectionSquares(List<Vector2Int> auswahl)
    {
        Dictionary<Vector3, bool> squaresData = new Dictionary<Vector3, bool>();
        for (int i = 0; i < auswahl.Count; i++)
        {
            Vector3 position = CalculatePositionFromCoords(auswahl[i]);
            bool isSquareFree = GetPieceOnSquare(auswahl[i]) == null;
            squaresData.Add(position, isSquareFree);
           // Debug.Log("Feld mit Position " + position + " ist Frei: " + isSquareFree);
        }
       // Debug.Log("Squares Data: " + squaresData);
        feldAuswahlErsteller.ZeigeAuswahl(squaresData);
    }

    private void DeselectFigur()
    {
        gewaehlteFigur = null;
        feldAuswahlErsteller.ClearSelection();
    }

    private void OnSelectedPieceMoved(Vector2Int kooridanten, Figur figur)
    {
        TryToTakeOppositePiece(kooridanten);
        UpdateBoardOnPieceMove(kooridanten, figur.position, figur, null);
        gewaehlteFigur.BewegeFigur(kooridanten);
        DeselectFigur();
        BeendeZug();
    }
    private void BeendeZug()
    {
        schachManager.BeendeZug();
    }

    public void UpdateBoardOnPieceMove(Vector2Int newCoords, Vector2Int oldCoords, Figur neuFig, Figur altFig)
    {
        grid[oldCoords.x, oldCoords.y] = altFig;
        grid[newCoords.x, newCoords.y] = neuFig;
    }

    public Figur GetPieceOnSquare(Vector2Int coords)
    {
        if (CheckIfCoordinatesAreOnBoard(coords))
            return grid[coords.x, coords.y];
        return null;
    }

    public bool CheckIfCoordinatesAreOnBoard(Vector2Int coords)
    {
        if (coords.x < 0 || coords.y < 0 || coords.x >= GesFeldGroesse || coords.y >= GesFeldGroesse)
            return false;
        return true;
    }

    public bool HasPiece(Figur figur)
    {
        for (int i = 0; i < GesFeldGroesse; i++)
        {
            for (int j = 0; j < GesFeldGroesse; j++)
            {
                if (grid[i, j] == figur)
                    return true;
            }
        }
        return false;
    }

    public void SetPieceOnBoard(Vector2Int coords, Figur figur)
    {
        if (CheckIfCoordinatesAreOnBoard(coords))
            grid[coords.x, coords.y] = figur;
    }

    
    private void TryToTakeOppositePiece(Vector2Int coords)
    {
        //Gegnerische Figur
        Figur figur = GetPieceOnSquare(coords);
        if (figur && !gewaehlteFigur.IstGleichesTeam(figur))
        {
            StartKonflikt(gewaehlteFigur, figur);
            TakePiece(figur);
        }

    }

 
    private void StartKonflikt(Figur angreifendeFigur, Figur geschlageneFigur)
    {
        // P
        // A
        
        double linksRotAngreifer;
        if (geschlageneFigur.position.x - angreifendeFigur.position.x == 0)
        {
            if (geschlageneFigur.position.y > angreifendeFigur.position.y) linksRotAngreifer = 0;
            else linksRotAngreifer = -180;
        }
        else
        {
            linksRotAngreifer = Math.Atan((angreifendeFigur.position.y - angreifendeFigur.position.y) / (geschlageneFigur.position.x - angreifendeFigur.position.x));
        }

        if (geschlageneFigur.figurFarbe == FigurFarbe.schwarz) { linksRotAngreifer = linksRotAngreifer - 180; }
        geschlageneFigur.transform.Rotate(0, (float)linksRotAngreifer, 0);

        //"Es sagt einfach Unity stop, halt und warte" ~ Veronika Scheller, 25.05.2021, kurz vor der Heirat mit MU
        Debug.Log("WAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
        this.fig = this.gewaehlteFigur;
        
        
        StartCoroutine(Animationsverwalter(5f, gewaehlteFigur, null, Animationtrigger.Angriff));
        StartCoroutine(Animationsverwalter(10f, null, geschlageneFigur, Animationtrigger.Sterben));


        Debug.Log("WAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH2");

       
    }

    public Figur fig;


 
   
    private enum Animationtrigger { Nichts, Sterben, Angriff, Idle }
    private Figur angreifendeFig;
    private Figur strebendeFig;
    private Animationtrigger animationFigAngreifend;
    private Animationtrigger animationFigSterbend;

    IEnumerator Animationsverwalter(float time,Figur angreifendeFigur, Figur sterbendeFigur, Animationtrigger animationtrigger)
    {
        yield return new WaitForSeconds(time);

        if (angreifendeFigur != null)
        {
            this.angreifendeFig = angreifendeFigur;
            this.animationFigAngreifend = animationtrigger;
        }
        if (sterbendeFigur != null)
        {
            this.strebendeFig = sterbendeFigur;
            this.animationFigSterbend = animationtrigger;
        }
        
    }
  

    public void Update()
    {
        switch (this.animationFigAngreifend)
        {
            case Animationtrigger.Nichts:   break;
            case Animationtrigger.Angriff:  this.animationFigAngreifend = Animationtrigger.Nichts; this.angreifendeFig.AngriffAnimation();  break;
            case Animationtrigger.Idle:     this.animationFigAngreifend = Animationtrigger.Nichts; this.angreifendeFig.IdleAnimation();     break;
            case Animationtrigger.Sterben:  this.animationFigAngreifend = Animationtrigger.Nichts; this.angreifendeFig.SterbeAnimation();   break;
        }
        
        switch (this.animationFigSterbend)
        {
            case Animationtrigger.Nichts:   break;
            case Animationtrigger.Angriff:  this.animationFigSterbend = Animationtrigger.Nichts;    this.strebendeFig.AngriffAnimation();   break;
            case Animationtrigger.Idle:     this.animationFigSterbend = Animationtrigger.Nichts;    this.strebendeFig.IdleAnimation();      break;
            case Animationtrigger.Sterben:  this.animationFigSterbend = Animationtrigger.Nichts;    this.strebendeFig.SterbeAnimation();    break;
        }
    }



    //Take Piece -> �bergebene Figur wird sterben
    private void TakePiece(Figur figur)
    {
        if (figur)
        {
            grid[figur.position.x, figur.position.y] = null;
            schachManager.OnPieceRemoved(figur);
            Destroy(figur.gameObject);
        }
    }

    public void PromotePiece(Figur figur)
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        TakePiece(figur);
        schachManager.ErstelleFigurUndInitialisiere(figur.position, figur.figurFarbe, "Dame");      //DamePromote -> In Schachmanagerklasse -> If AktiverSPielr == weiser -> "DameWeiss" -> "DameSChwarz"
    }
   
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  
    internal void OnGameRestarted()
    {
        gewaehlteFigur = null;
        CreateGrid();
    }
}