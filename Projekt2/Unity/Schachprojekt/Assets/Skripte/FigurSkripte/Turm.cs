using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turm : Figur
{
    private Vector2Int[] directions = new Vector2Int[] { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };
    public override List<Vector2Int> WaehleMoeglicheFelder()
    {
        Bewegungsmöglichkeiten.Clear();

        float range = Schachbrett.GesFeldGroesse;
        foreach (var direction in directions)
        {
            for (int i = 1; i <= range; i++)
            {
                Vector2Int nextCoords = position + direction * i;
                Figur piece = schachbrett.GetPieceOnSquare(nextCoords);
                if (!schachbrett.CheckIfCoordinatesAreOnBoard(nextCoords))
                    break;
                if (piece == null)
                    AddBewegungsmoeglichkeit(nextCoords);
                else if (!piece.IstGleichesTeam(this))
                {
                    AddBewegungsmoeglichkeit(nextCoords);
                    break;
                }
                else if (piece.IstGleichesTeam(this))
                    break;
            }
        }
        return Bewegungsmöglichkeiten;
    }


}