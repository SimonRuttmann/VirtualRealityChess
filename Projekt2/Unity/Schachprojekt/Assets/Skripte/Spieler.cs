using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * 
 * The main reason to use System.Serializable for most users is so that your class and variables will show up in the inspector.
    If you added a public List of BaseTest to a component, you would be able to add and configure instances of BaseTest right in the inspector, with all that configured data available in the list at runtime. Otherwise you don't need to make your data classes Serializable.

 */
[System.Serializable] //Vermutung: Entweder zur Speicherung der Klasse oder Inspektor
public class Spieler
{
    public FigurFarbe Farbe;
    public Schachbrett Schachbrett;
    //Alle Figuren eines Spieler, welche sich auf dem Spielfeld befinden
    public List<Figur> AktiveFiguren;
    // Start is called before the first frame update

    //VORSICHT KONSTRUKTOR!
    public Spieler(FigurFarbe farbe, Schachbrett schachbrett)
    {
        AktiveFiguren = new List<Figur>();
        this.Schachbrett = schachbrett;
        this.Farbe = farbe;
    }

    public void AddFigur(Figur figur)
    {
        if (!AktiveFiguren.Contains(figur))
        {
            AktiveFiguren.Add(figur);
        }
    }

    public void RemoveFigur(Figur figur)
    {
        if (AktiveFiguren.Contains(figur))
        {
            AktiveFiguren.Remove(figur);
        }
    }

    /* Wie weit soll unser Backend gehen, diese Methode ist nur notwendig wenn wir von
       * unserem ursprünglichen Plan abweichen die möglichen Züge beim Klick auf eine Figur durchzuführen
       */
      public void GeneriereAlleMoeglichenZuege()
      {
          foreach(var figur in AktiveFiguren)
          {
              if (Schachbrett.EnthältFigur(figur))
              {
                figur.WaehleMoeglicheFelder();
              }
          }
      }

	public Figur[] GetPieceAtackingOppositePiceOfType<T>() where T : Figur
	{
		return AktiveFiguren.Where(p => p.IsAttackingPieceOfType<T>()).ToArray();
	}

	public Figur[] GetPiecesOfType<T>() where T : Figur
	{
		return AktiveFiguren.Where(p => p is T).ToArray();
	}

	public void RemoveMovesEnablingAttakOnPieceOfType<T>(Spieler opponent, Figur selectedPiece) where T : Figur
	{
		List<Vector2Int> coordsToRemove = new List<Vector2Int>();

		coordsToRemove.Clear();
		foreach (var coords in selectedPiece.Bewegungsmöglichkeiten)
		{
			Figur pieceOnCoords = this.Schachbrett.GetPieceOnSquare(coords);
			Schachbrett.UpdateBoardOnPieceMove(coords, selectedPiece.position, selectedPiece, null);
			opponent.GeneriereAlleMoeglichenZuege();
			if (opponent.CheckIfIsAttacigPiece<T>())
				coordsToRemove.Add(coords);
			Schachbrett.UpdateBoardOnPieceMove(selectedPiece.position, coords, selectedPiece, pieceOnCoords);
		}
		foreach (var coords in coordsToRemove)
		{
			selectedPiece.Bewegungsmöglichkeiten.Remove(coords);
		}

	}

	internal bool CheckIfIsAttacigPiece<T>() where T : Figur
	{
		foreach (var piece in AktiveFiguren)
		{
			if (Schachbrett.HasPiece(piece) && piece.IsAttackingPieceOfType<T>())
				return true;
		}
		return false;
	}

	public bool CanHidePieceFromAttack<T>(Spieler opponent) where T : Figur
	{
		foreach (var piece in AktiveFiguren)
		{
			foreach (var coords in piece.Bewegungsmöglichkeiten)
			{
				Figur pieceOnCoords = Schachbrett.GetPieceOnSquare(coords);
				Schachbrett.UpdateBoardOnPieceMove(coords, piece.position, piece, null);
				opponent.GeneriereAlleMoeglichenZuege();
				if (!opponent.CheckIfIsAttacigPiece<T>())
				{
					Schachbrett.UpdateBoardOnPieceMove(piece.position, coords, piece, pieceOnCoords);
					return true;
				}
				Schachbrett.UpdateBoardOnPieceMove(piece.position, coords, piece, pieceOnCoords);
			}
		}
		return false;
	}

	internal void OnGameRestarted()
	{
		AktiveFiguren.Clear();
	}




}

