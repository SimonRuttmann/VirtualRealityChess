using System;
using System.Collections.Generic;
using System.Text;

namespace Schachprojekt
{
    public class Figurenverwaltung
    {
        private Dictionary<string,Figur> MapAllerFiguren = new Dictionary<string, Figur>();
        private static Figurenverwaltung SpielerInstance, GegnerInstance;
        public void GetFigurPos()
        {

        }

        public Position[] GetFigurBewegePos(string id)
        {
            if (MapAllerFiguren.TryGetValue(id, out Figur figur))
            {
                return figur.CheckBewPos();
            }
            return null;
        }
        //  <<Initialisierung>> 
        public void Neuplatzierung() 
        {

        }
        //  <<Interne Methode>>
        private void FigurErstellen()
        {

        }
        private void FigurEntfernen(string id)
        {

        }
        private Figur SucheFigur(string id)
        {
            if (MapAllerFiguren.TryGetValue(id, out Figur figur))
            {
                return figur;
            }
            return null;
        }
        // <<Schachzüge>>
        public void Rochade(string koenigID,string turmid)
        {

        }
        public void BauerVerwandlung(string bauerid, string verwandlungsfigurid)
        {

        }
        public void Schlagefigur(string schlagendeFig,string geschlageneFig)
        {

        }
        public void BewegeFigur(string id, Position pop)
        {

        }

        // <<Singleton>>
        public Figurenverwaltung GetSpielerInstance()
        {
            if (SpielerInstance == null)
            {
                SpielerInstance = new Figurenverwaltung();
            }
            return SpielerInstance;
        }
        public Figurenverwaltung GetKiInstance()
        {
            if(GegnerInstance == null)
            {
                GegnerInstance = new Figurenverwaltung();
            }
            return GegnerInstance;
        }
    }
}
