using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springer : Figur
{
	Vector2Int[] offsets = new Vector2Int[]
	{
		new Vector2Int(2, 1),
		new Vector2Int(2, -1),
		new Vector2Int(1, 2),
		new Vector2Int(1, -2),
		new Vector2Int(-2, 1),
		new Vector2Int(-2, -1),
		new Vector2Int(-1, 2),
		new Vector2Int(-1, -2),
	};

	public override List<Vector2Int> WaehleMoeglicheFelder()
	{
		Bewegungsmöglichkeiten.Clear();

		for (int i = 0; i < offsets.Length; i++)
		{
			Vector2Int nextCoords = position + offsets[i];
			Figur piece = schachbrett.GetPieceOnSquare(nextCoords);
			if (!schachbrett.CheckIfCoordinatesAreOnBoard(nextCoords))
				continue;
			if (piece == null || !piece.IstGleichesTeam(this))
				AddBewegungsmoeglichkeit(nextCoords);
		}
		return Bewegungsmöglichkeiten;
	}
}