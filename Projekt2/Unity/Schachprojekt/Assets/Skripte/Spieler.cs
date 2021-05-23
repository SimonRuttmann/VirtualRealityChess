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
     * unserem urspr�nglichen Plan abweichen die m�glichen Z�ge beim Klick auf eine Figur durchzuf�hren
    public void GeneriereAlleM�glichenZ�ge()
    {
        foreach(var figur in aktiveFiguren)
        {
            if (schachbrett.Enth�ltFigur(figur))
            {
                Figur.
            }
        }
    }
    */
}
