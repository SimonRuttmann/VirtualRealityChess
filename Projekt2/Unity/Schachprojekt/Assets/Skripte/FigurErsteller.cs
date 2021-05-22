using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Erstellt eine Figur
public class FigurErsteller : MonoBehaviour
{
    //Dieses Array beinhaltet alle unserer Figuren in schwarzer und weißer Version
    [SerializeField] private GameObject[] Modellarray; 

    //Dieses Dictionary ist einfach nur für einfachen Zugriff vorhanden. Zugriff über String statt Index
    private Dictionary<string, GameObject> ModellDictionary = new Dictionary<string, GameObject>();

    // Alle Modelle dem Dictionary hinzufügen
    private void Awake()
    {
        this.AddModelleZumDictionary();
    }
    private void AddModelleZumDictionary()
    {
        foreach (GameObject modell in Modellarray)
        {
            String modellName = modell.GetComponent<Figur>().ToString();        // Key:  LaeuferSchwarz (Laeufer)       Value: LaeuferSchwarz (UnityEngine.GameObject) 
            ModellDictionary.Add(modellName, modell);
        }
        
    }
    // Turm (Turm)
    //ErstelleFigur("schwarzerBauer") -> Sucht sich das Modell/Prefab und instanziiert es -> !Neues Objekt in der Szene!
    public GameObject ErstelleFigur(Type figurtyp)
    {
        GameObject modell = ModellDictionary[figurtyp.ToString()];             //"Bauer1" -> Bauer1Pref

        if (modell)
        {
            GameObject richtigeFigur = Instantiate(modell);          // "Baut modell"
            return richtigeFigur;
        }
        return null;
    }

}