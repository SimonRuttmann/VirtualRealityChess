using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bauer : Figur
{
    public override List<Vector2Int> WaehleMoeglicheFelder()
    {
        Bewegungsmöglichkeiten.Clear();
        Bewegungsmöglichkeiten.Add(position + new Vector2Int(0, 1));
        return Bewegungsmöglichkeiten;
    }
    /*
    public override List<Vector2Int> WaehleMoeglicheFelder()
    {
        Bewegungsmöglichkeiten.Clear();

        Vector2Int direction = figurFarbe == FigurFarbe.weiss ? Vector2Int.up : Vector2Int.down;
        float range = WurdeBewegt ? 1 : 2;
        for (int i = 1; i <= range; i++)
        {
            Vector2Int nextCoords = position + direction * i;
            Figur piece = schachbrett.GetPieceOnSquare(nextCoords);
            if (!schachbrett.CheckIfCoordinatesAreOnBoard(nextCoords))
                break;
            if (piece == null)
                AddBewegungsmoeglichkeit(nextCoords);
            else
                break;
        }

        Vector2Int[] takeDirections = new Vector2Int[] { new Vector2Int(1, direction.y), new Vector2Int(-1, direction.y) };
        for (int i = 0; i < takeDirections.Length; i++)
        {
            Vector2Int nextCoords = position + takeDirections[i];
            Figur piece = schachbrett.GetPieceOnSquare(nextCoords);
            if (!schachbrett.CheckIfCoordinatesAreOnBoard(nextCoords))
                continue;
            if (piece != null && !piece.IstGleichesTeam(this))
            {
                AddBewegungsmoeglichkeit(nextCoords);
            }
        }
        return Bewegungsmöglichkeiten;
    }

    public override void BewegeFigur(Vector2Int coords)
    {
        base.BewegeFigur(coords);
        CheckPromotion();
    }

    private void CheckPromotion()
    {
        int endOfBoardYCoord = figurFarbe == FigurFarbe.weiss ? Schachbrett.GesFeldGroesse - 1 : 0;
        if (position.y == endOfBoardYCoord)
        {
            schachbrett.PromotePiece(this);
        }
    }
    */
}