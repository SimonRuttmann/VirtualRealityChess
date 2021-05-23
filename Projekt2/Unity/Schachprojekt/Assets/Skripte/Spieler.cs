using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Vermutung: Entweder zur Speicherung der Klasse oder Inspektor
public class Spieler
{
    public FigurFarbe farbe;
    public Schachbrett schachbrett;
    public List<Figur> aktiveFiguren;
    // Start is called before the first frame update
    
    public Spieler(FigurFarbe farbe, Schachbrett schachbrett) 
    {
        aktiveFiguren = new List<Figur>();
        this.schachbrett = schachbrett;
        this.farbe = farbe;
    }

    public void AddFigur(Figur figur)
    {
        if (!aktiveFiguren.Contains(figur))
        {
            aktiveFiguren.Add(figur);
        }
    }

    public void RemoveFigur(Figur figur)
    {
        if (aktiveFiguren.Contains(figur))
        {
            aktiveFiguren.Remove(figur);
        }
    }

    * Wie weit soll unser Backend gehen, diese Methode ist nur notwendig wenn wir von
     * unserem ursprünglichen Plan abweichen die möglichen Züge beim Klick auf eine Figur durchzuführen
    public void GeneriereAlleMöglichenZüge()
    {
        foreach(var figur in aktiveFiguren)
        {
            if (schachbrett.EnthältFigur(figur))
            {
                Figur.
            }
        }
    }
    */
}
