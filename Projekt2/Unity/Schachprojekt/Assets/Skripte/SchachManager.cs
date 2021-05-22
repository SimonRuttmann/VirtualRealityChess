using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(FigurErsteller))]
public class SchachManager : MonoBehaviour
{
    // Hier wird das Skriptobjekt im Editor hinzugefügt
    [SerializeField] private SchachbrettAufstellung Startkonfiguration;
    
    //Schachfeld hinzufügen
    [SerializeField] private Schachbrett schachbrett;

    private FigurErsteller FigurErsteller;

    //FigurErsteller ist ein Singleton -> Objekt kann über GetComponent erhalten werden
    private void Awake()
    {
        if (this.FigurErsteller == null)
        {
            this.FigurErsteller = GetComponent<FigurErsteller>();
        }
        else
        {
            Debug.Log("FigurErsteller ist bereits erstellt!");
        }
    }

  
    //neues Spiel
    private void Start()
    {

        for (int i = 0; i < this.Startkonfiguration.GetFigurenAnzahl(); i++)
        {
            Vector2Int xyPosition = Startkonfiguration.Get_XY_VonAufstellungsFigur(i);
            FigurFarbe figurfarbe = Startkonfiguration.Get_Farbe_VonAufstellungsFigur(i);
            string figurtypS = Startkonfiguration.Get_Name_VonAufstellungsFigur(i);
            
            //Debug.Log("Typeof");

           // Debug.Log("figurtypS: " + figurtypS);
           // Debug.Log("GetType Figur: " + Type.GetType("Figur"));
           // Debug.Log("GetType Turm toString: " + Type.GetType("Turm").ToString());
           // Debug.Log("GetType Turm: " +Type.GetType("Turm"));

            
            Figur neueFigur = this.FigurErsteller.ErstelleFigur(figurtypS).GetComponent<Figur>();
            neueFigur.GebeFigurdaten(xyPosition, figurfarbe, schachbrett);
        }
    }


}

