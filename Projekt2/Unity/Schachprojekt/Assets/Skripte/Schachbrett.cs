using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Schachbrett : MonoBehaviour
{
    [SerializeField] private Transform EffektiverStartpunktUntenLinks;
    [SerializeField] private float Feldgroesse;

    public const int GesFeldGroesse = 8;

    private Figur[,] grid;
    private Figur gewaehlteFigur;
    private SchachManager schachManager;

    private FeldAuswahlErsteller feldAuswahlErsteller; //anpassen


    public Vector3 RelativePositionZumSchachbrettfeld(Vector2Int position)
    {
        return EffektiverStartpunktUntenLinks.position + new Vector3(position.x * Feldgroesse, 0f, position.y * Feldgroesse);
    }

    //Implementieren
    public bool EnthältFigur(Figur figur)
    {

        return false;
    }
    /////////////////////////////////////////



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
        Vector2Int coords = CalculateCoordsFromPosition(inputPosition);
        Figur figur = GetPieceOnSquare(coords);
        if (gewaehlteFigur)
        {
            if (figur != null && gewaehlteFigur == figur)
                DeselectFigur();
            else if (figur != null && gewaehlteFigur != figur && schachManager.IstTeamzug(figur.figurFarbe))
                WahleFigur(figur);
            else if (gewaehlteFigur.BewegungMoeglichZu(coords))
                OnSelectedPieceMoved(coords, gewaehlteFigur);
        }
        else
        {
            if (figur != null && schachManager.IstTeamzug(figur.figurFarbe))
                WahleFigur(figur);
        }
    }
    private void WahleFigur(Figur figur)
    {
        schachManager.RemoveMovesEnablingAttakOnPieceOfType<Koenig>(figur);
        gewaehlteFigur = figur;
        List<Vector2Int> auswahl = gewaehlteFigur.Bewegungsmöglichkeiten;
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
        }
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
        Figur figur = GetPieceOnSquare(coords);
        if (figur && !gewaehlteFigur.IstGleichesTeam(figur))
        {
            TakePiece(figur);
        }
    }
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