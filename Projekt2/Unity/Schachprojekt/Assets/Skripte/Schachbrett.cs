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
    private AnimationManager animationManager;

    public Vector3 RelativePositionZumSchachbrettfeld(Vector2Int position)
    {
        return EffektiverStartpunktUntenLinks.position + new Vector3(position.x * Feldgroesse, 0f, position.y * Feldgroesse);
    }

    protected virtual void Awake()
    {
        animationManager = GetComponent<AnimationManager>();
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
        if (this.blocker) return;
       // Debug.Log("Inputhandler hat OnSquareSelcted aufgerufen mit: " + inputPosition);
        Vector2Int coords = CalculateCoordsFromPosition(inputPosition);
        Figur figur = GetPieceOnSquare(coords);
      //  Debug.Log("Figur: " + figur + " erhalten");
        
        if (gewaehlteFigur)
        {
        //    Debug.Log("gewaehlte Figur:" + gewaehlteFigur + "und neuer Klick zur Position: " + coords);
        //    Debug.Log("Figur nicht null ->  Bewegung ist moeglich: " + gewaehlteFigur.BewegungMoeglichZu(coords) + " Befehl wird ausgeführt:");

            // Figur nochnamal anwählen
            if (figur != null && gewaehlteFigur == figur)
            {
           //     Debug.Log("Deselect");
                DeselectFigur();
            }

            //Wir hatten ne weise Figur ausgwählt und haben jetzt eine andere Ausgewählt
            else if (figur != null && gewaehlteFigur != figur && schachManager.IstTeamzug(figur.figurFarbe))
            {
             //   Debug.Log("neue Figur ausgewählt");
                figur.IdleAnimation();
                WahleFigur(figur);
            }

            //Figur ist ausgewählt und "Klick" auf bewegbares feld
            else if (gewaehlteFigur.BewegungMoeglichZu(coords))
           // else if (true)
                    {
               // Debug.Log("Figur wird bewegt zu: " + coords);
                OnSelectedPieceMoved(coords, gewaehlteFigur);
            }
        }
        else
        {
            //Debug.Log("Figur 1. mal angewählt");
          //  Debug.Log("figur: " + figur + "Farbe" + figur.figurFarbe + "ist Teamzug: " + schachManager.IstTeamzug(figur.figurFarbe));
            if (figur != null && schachManager.IstTeamzug(figur.figurFarbe))
            {
                figur.IdleAnimation();
                //     Debug.Log("Wähle Figur");
                WahleFigur(figur);
            }
           // Debug.Log("Figur ist null, oder der Teamzug ist nicht richtig");
        }
    }

    private void WahleFigur(Figur figur)
    {
        schachManager.RemoveMovesEnablingAttakOnPieceOfType<Koenig>(figur);
        gewaehlteFigur = figur;
      //  Debug.Log("Gewählte Figur zugewiesen: " + gewaehlteFigur);
        List<Vector2Int> auswahl = gewaehlteFigur.Bewegungsmöglichkeiten;
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
        Vector2Int pos = figur.position;
        TryToTakeOppositePiece(kooridanten);
        UpdateBoardOnPieceMove(kooridanten, pos, figur, null);
   //     gewaehlteFigur.BewegeFigur(kooridanten);
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

        // BewegeFigur() -> Richtig aufrufen 
        // Take pice braucht position der geschlagenden figur
        // 
    private void TryToTakeOppositePiece(Vector2Int coords)
    {
        //Gegnerische Figur
        Figur figur = GetPieceOnSquare(coords);
        if (figur && !gewaehlteFigur.IstGleichesTeam(figur))
        {
            StartKonflikt(gewaehlteFigur, figur, coords);
            TakePiece(figur);
        }
        else{
            gewaehlteFigur.BewegeFigur(coords);
        }

    }

 
    private void StartKonflikt(Figur angreifendeFigur, Figur geschlageneFigur, Vector2Int kooridanten)
    {
       
        double RotationspunktAngreifer;
        double RotationspunktVerteidiger;

        if (geschlageneFigur.position.y - angreifendeFigur.position.y == 0)
        {
          
             if (geschlageneFigur.position.x > angreifendeFigur.position.x) RotationspunktAngreifer = 90;
             else RotationspunktAngreifer = 270;
        }
        else
        {
            int gegenkathete = geschlageneFigur.position.x - angreifendeFigur.position.x;
            int ankathete = geschlageneFigur.position.y - angreifendeFigur.position.y;
     
            RotationspunktAngreifer = (180/Math.PI) *  Math.Atan((geschlageneFigur.position.x - angreifendeFigur.position.x) / (geschlageneFigur.position.y - angreifendeFigur.position.y));
         
        }

        RotationspunktVerteidiger =  RotationspunktAngreifer;

        if (angreifendeFigur.figurFarbe == FigurFarbe.weiss) { RotationspunktAngreifer = RotationspunktAngreifer - 180; }
        if (geschlageneFigur.figurFarbe == FigurFarbe.weiss) { RotationspunktVerteidiger = RotationspunktVerteidiger - 180; }
      
  
        //Bewegung Angreifer
        animationManager.DreheFigur1(0f, angreifendeFigur, (float)RotationspunktAngreifer);
        
        //Bewegung Verteidiger
        animationManager.DreheFigur2(0f, geschlageneFigur, (float)RotationspunktVerteidiger);

        //Angriff
        animationManager.StartAnimation(1f, angreifendeFigur, null, AnimationManager.Animationtrigger.Angriff);

        //Sterben
        animationManager.StartAnimation(1.5f, null, geschlageneFigur, AnimationManager.Animationtrigger.Sterben);

        //Loeschen
        animationManager.StartAnimation(4f, null, geschlageneFigur, AnimationManager.Animationtrigger.Loeschen);

        //Bewege Figur 1 auf Figur 2
        animationManager.BewegeFigur(5f, angreifendeFigur, kooridanten);

        //Drehe Figur zurück
        int back = 0;
        if (angreifendeFigur.figurFarbe == FigurFarbe.weiss) back = 180;

        animationManager.DreheFigur1(6f, angreifendeFigur,(float)back);
        this.BlockEingabe(11f); 
    }

    //Take Piece -> Übergebene Figur wird sterben
    private void TakePiece(Figur figur)
    {
        if (figur)
        {
            grid[figur.position.x, figur.position.y] = null;
            schachManager.OnPieceRemoved(figur);
           // Destroy(figur.gameObject); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
        }
    }

    public void BlockEingabe(float time)
    {
        this.blocker = true;
        StartCoroutine(EingabeblockerManager(time));
    }

    IEnumerator EingabeblockerManager(float time)
    {
        yield return new WaitForSeconds(time);
        this.blocker = false;

    }
    private bool blocker = false;

    public void PromotePiece(Figur figur)
    {
        string modell;
        if (figur.figurFarbe == FigurFarbe.weiss) modell = "DameWeiss";
        else
        {
            modell = "DameSchwarz";
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        TakePiece(figur);
        Destroy(figur.gameObject);
        schachManager.ErstelleFigurUndInitialisiere(figur.position, figur.figurFarbe, modell);      //DamePromote -> In Schachmanagerklasse -> If AktiverSPielr == weiser -> "DameWeiss" -> "DameSChwarz"
    }
   
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  
    internal void OnGameRestarted()
    {
        gewaehlteFigur = null;
        CreateGrid();
    }
}