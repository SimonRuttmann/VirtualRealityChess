using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//[RequireComponent(typeof(FigurErsteller))]
public class SchachManager : MonoBehaviour
{
    private enum Spielzustand
    {
        Start, Spiel, Fertig
    }

    // Hier wird das Skriptobjekt im Editor hinzugefügt
    [SerializeField] private SchachbrettAufstellung Startkonfiguration;
    
    //Schachfeld hinzufügen
    [SerializeField] private Schachbrett schachbrett;

    [SerializeField] private ChessUIManager SchachUIManager;

    private FigurErsteller FigurErsteller;
    private Spieler WeisserSpieler;
    private Spieler SchwarzerSpieler;
    private Spieler AktiverSpieler;
    private Spielzustand spielzustand;


    //FigurErsteller ist ein Singleton -> Objekt kann über GetComponent erhalten werden
    private void Awake()
    {
        //Abhängigkeiten
        this.FigurErsteller = GetComponent<FigurErsteller>();
        ErstelleSpieler();
        /*
        if (this.FigurErsteller == null)
        {
            this.FigurErsteller = GetComponent<FigurErsteller>();
        }
        else
        {
            Debug.Log("FigurErsteller ist bereits erstellt!");
        }
        */
    }

    private void ErstelleSpieler()
    {
        this.WeisserSpieler = new Spieler(FigurFarbe.weiss, schachbrett);
        this.SchwarzerSpieler = new Spieler(FigurFarbe.schwarz, schachbrett);
    }

    private void StartNewGame()
    {
        this.spielzustand = Spielzustand.Start;
       // this.SchachUIManager.HideUI();
        schachbrett.SetzeAbhaengigkeiten(this);

        //"Create Pieces From Layout"
        this.ErstelleFigurenVonAufstellung(Startkonfiguration);
        AktiverSpieler = WeisserSpieler;
        ErstelleAlleSpielerZuege(AktiverSpieler);
        this.spielzustand = Spielzustand.Spiel;
    }

    //neues Spiel
    private void Start()
    {
        StartNewGame();
    }

    private void ErstelleFigurenVonAufstellung(SchachbrettAufstellung schachbrettAufstellung)
    {
        for (int i = 0; i < schachbrettAufstellung.GetFigurenAnzahl(); i++)
        {
            //Daten der Figur abrufen
            Vector2Int xyPosition = schachbrettAufstellung.Get_XY_VonAufstellungsFigur(i);
            FigurFarbe figurfarbe = schachbrettAufstellung.Get_Farbe_VonAufstellungsFigur(i);
            string figurtypS = schachbrettAufstellung.Get_Name_VonAufstellungsFigur(i);

            //Figur erstellen, instanziieren und dem Spieler hinzufügen 
            ErstelleFigurUndInitialisiere(xyPosition, figurfarbe, figurtypS);
        }
    }

    public void ErstelleFigurUndInitialisiere(Vector2Int xyPosition, FigurFarbe figurfarbe, string figurtypS)
    {
        Figur neueFigur = this.FigurErsteller.ErstelleFigur(figurtypS).GetComponent<Figur>();
        neueFigur.SetzeFigurdaten(xyPosition, figurfarbe, schachbrett);
        
        //Setzt die Figur auf das Schachfeld
        this.schachbrett.SetPieceOnBoard(xyPosition, neueFigur);

        //Figur dem Spieler hinzufügen
        Spieler aktuellerSpieler;
        if (figurfarbe == FigurFarbe.weiss) { aktuellerSpieler = WeisserSpieler; }
        else { aktuellerSpieler = SchwarzerSpieler; }
        aktuellerSpieler.AddFigur(neueFigur);
    }

    private void ErstelleAlleSpielerZuege(Spieler spieler)
    {
        spieler.GeneriereAlleMoeglichenZuege();
    }

    public bool IstTeamzug(FigurFarbe farbe)
    {
        return AktiverSpieler.Farbe == farbe;
    }
    
    public void BeendeZug()
    {
        //Spielerzüge vom aktuellen Spieler ermitteln
        ErstelleAlleSpielerZuege(AktiverSpieler);
        
        //Spielerzüge vom Gegner ermitteln
        ErstelleAlleSpielerZuege(GegnerVonSpieler(AktiverSpieler));

        if (IstSpielVorbei()) { BeendeSpiel(); }
        else { WechlseAktivesTeam(); }
    }


    internal bool LaueftSpiel()
    {
        return spielzustand == Spielzustand.Spiel;
    }

    private bool IstSpielVorbei()
    {
        Figur[] koenigsbedroher = AktiverSpieler.GetPieceAtackingOppositePiceOfType<Koenig>();
        if (koenigsbedroher.Length > 0)
        {
            Spieler gegnerischerSpieler = GegnerVonSpieler(AktiverSpieler);
            Figur angegriffenerKoenig = gegnerischerSpieler.GetPiecesOfType<Koenig>().FirstOrDefault();
            gegnerischerSpieler.RemoveMovesEnablingAttakOnPieceOfType<Koenig>(AktiverSpieler, angegriffenerKoenig);

            int moeglicheKoenigszuege = angegriffenerKoenig.Bewegungsmöglichkeiten.Count;
            if (moeglicheKoenigszuege == 0)
            {
                bool koenigDeckbar = gegnerischerSpieler.CanHidePieceFromAttack<Koenig>(AktiverSpieler);
                if (!koenigDeckbar)
                    return true;
            }
        }
        return false;
    }

    private void BeendeSpiel()
    {
        spielzustand = Spielzustand.Fertig;
        this.SchachUIManager.OnGameFinished(AktiverSpieler.Farbe.ToString());
    }

    private void ZerstoereFiguren()
    {
        WeisserSpieler.AktiveFiguren.ForEach(p => Destroy(p.gameObject));
        SchwarzerSpieler.AktiveFiguren.ForEach(p => Destroy(p.gameObject));
    }

    private void WechlseAktivesTeam()
    {
        if (AktiverSpieler == WeisserSpieler) { AktiverSpieler = SchwarzerSpieler;  }
        else {                                  AktiverSpieler = WeisserSpieler;    }
    }

    private Spieler GegnerVonSpieler(Spieler spieler)
    {
        Spieler aktuellerGegner;
        if (spieler == WeisserSpieler) { aktuellerGegner = SchwarzerSpieler; }
        else { aktuellerGegner = WeisserSpieler; }

        return aktuellerGegner;
    }

    internal void OnPieceRemoved(Figur figur)
    {
        Spieler figurBesitzer = (figur.figurFarbe == FigurFarbe.weiss) ? WeisserSpieler : SchwarzerSpieler;
        figurBesitzer.RemoveFigur(figur);
    }

    internal void RemoveMovesEnablingAttakOnPieceOfType<T>(Figur figur) where T : Figur
    {
        AktiverSpieler.RemoveMovesEnablingAttakOnPieceOfType<T>(GegnerVonSpieler(AktiverSpieler), figur);
    }

    public void RestartGame()
    {
        ZerstoereFiguren();
        schachbrett.OnGameRestarted();
        WeisserSpieler.OnGameRestarted();
        SchwarzerSpieler.OnGameRestarted();
        StartNewGame();
    }
}

