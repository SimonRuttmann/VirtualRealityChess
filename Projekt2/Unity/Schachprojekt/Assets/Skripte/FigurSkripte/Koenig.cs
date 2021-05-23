
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koenig : Figur
{

    Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(-1, 1),
        new Vector2Int(0, 1),
        new Vector2Int(1, 1),
        new Vector2Int(-1, 0),
        new Vector2Int(1, 0),
        new Vector2Int(-1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(1, -1),
    };

    private Figur leftRook;
    private Figur rightRook;

    private Vector2Int leftCastlingMove;
    private Vector2Int rightCastlingMove;

    public override List<Vector2Int> WaehleMoeglicheFelder()
    {
        Bewegungsmöglichkeiten.Clear();
        AssignStandardMoves();
        AssignCastlingMoves();
        return Bewegungsmöglichkeiten;

    }

    private void AssignCastlingMoves()
    {
        leftCastlingMove = new Vector2Int(-1, -1);
        rightCastlingMove = new Vector2Int(-1, -1);
        if (!WurdeBewegt)
        {
            leftRook = GetPieceInDirection<Turm>(figurFarbe, Vector2Int.left);
            if (leftRook && !leftRook.WurdeBewegt)
            {
                leftCastlingMove = position + Vector2Int.left * 2;
                Bewegungsmöglichkeiten.Add(leftCastlingMove);
            }
            rightRook = GetPieceInDirection<Turm>(figurFarbe, Vector2Int.right);
            if (rightRook && !rightRook.WurdeBewegt)
            {
                rightCastlingMove = position + Vector2Int.right * 2;
                Bewegungsmöglichkeiten.Add(rightCastlingMove);
            }
        }
    }

    private Figur GetPieceInDirection<T>(FigurFarbe team, Vector2Int direction)
    {
        for (int i = 1; i <= Schachbrett.GesFeldGroesse; i++)
        {
            Vector2Int nextCoords = position + direction * i;
            Figur piece = schachbrett.GetPieceOnSquare(nextCoords);
            if (!schachbrett.CheckIfCoordinatesAreOnBoard(nextCoords))
                return null;
            if (piece != null)
            {
                if (piece.figurFarbe != team || !(piece is T))
                    return null;
                else if (piece.figurFarbe == team && piece is T)
                    return piece;
            }
        }
        return null;
    }

    private void AssignStandardMoves()
    {
        float range = 1;
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
    }

    public override void BewegeFigur(Vector2Int coords)
    {
        base.BewegeFigur(coords);
        if (coords == leftCastlingMove)
        {
            schachbrett.UpdateBoardOnPieceMove(coords + Vector2Int.right, leftRook.position, leftRook, null);
            leftRook.BewegeFigur(coords + Vector2Int.right);
        }
        else if (coords == rightCastlingMove)
        {
            schachbrett.UpdateBoardOnPieceMove(coords + Vector2Int.left, rightRook.position, rightRook, null);
            rightRook.BewegeFigur(coords + Vector2Int.left);
        }
    }

}