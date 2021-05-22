using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dieses Skript wird dem SceneController zugewiesen
//Es ist verantwortlich für alle Berechnungen, welche in Zusammenhang
//mit anderen Figuren getätigt werden müssen

public class Figurenverwaltung : MonoBehaviour
{
    public SchachbrettAufstellung schachBrett;
    public GameObject W_bauer1;
    public GameObject W_bauer2;
    public GameObject W_bauer3;
    public GameObject W_bauer4;
    public GameObject W_bauer5;
    public GameObject W_bauer6;
    public GameObject W_bauer7;
    public GameObject W_bauer8;
    public GameObject W_koenig;
    public GameObject W_dame;
    public GameObject W_springer1;
    public GameObject W_springer2;
    public GameObject W_laeufer1;
    public GameObject W_laeufer2;
    public GameObject W_turm1;
    public GameObject W_turm2;

    public GameObject S_bauer1;
    public GameObject S_bauer2;
    public GameObject S_bauer3;
    public GameObject S_bauer4;
    public GameObject S_bauer5;
    public GameObject S_bauer6;
    public GameObject S_bauer7;
    public GameObject S_bauer8;
    public GameObject S_koenig;
    public GameObject S_dame;
    public GameObject S_springer1;
    public GameObject S_springer2;
    public GameObject S_laeufer1;
    public GameObject S_laeufer2;
    public GameObject S_turm1;
    public GameObject S_turm2;

    public GameObject[] WeisseListe = new GameObject[16];
    public GameObject[] SchwarzeListe = new GameObject[16];

    public BauerSkript BauerSkript;

    //      1   2   3   4   5   6   7   8
    //      
    //  a   0   1   2   3   4   5   6   7
    //  b   8   9   10  11  12  12  13  14 
    //  c   15  16  17  18  19  20  21  22 
    //  d   ...
    //  e   ...
    //  f   ...
    //  g   ...
    //  h   ...                             63




    // 0 =>  x = 0; y = 0; z = 0;
    // 1 =>  x = 0 +50; y = 0; z = 0
    // 15 => x = 0; y = 2*50; z= 0

    public GameObject[] Spielfeld = new GameObject[64];
    
    //Pseudo Konstruktor
    public void Init()
    {
        WeisseListe[0] = W_bauer1;
        WeisseListe[1] = W_bauer2;
        WeisseListe[2] = W_bauer3;
        WeisseListe[3] = W_bauer4;
        WeisseListe[4] = W_bauer5;
        WeisseListe[5] = W_bauer6;
        WeisseListe[6] = W_bauer7;
        WeisseListe[7] = W_bauer8;
        WeisseListe[8] = W_koenig; ;
        WeisseListe[9] = W_dame;
        WeisseListe[10] = W_springer1;
        WeisseListe[11] = W_springer2;
        WeisseListe[12] = W_laeufer1;
        WeisseListe[13] = W_laeufer2;
        WeisseListe[14] = W_turm1;
        WeisseListe[15] = W_turm2;

        SchwarzeListe[0] = S_bauer1;
        SchwarzeListe[1] = S_bauer2;
        SchwarzeListe[2] = S_bauer3;
        SchwarzeListe[3] = S_bauer4;
        SchwarzeListe[4] = S_bauer5;
        SchwarzeListe[5] = S_bauer6;
        SchwarzeListe[6] = S_bauer7;
        SchwarzeListe[7] = S_bauer8;
        SchwarzeListe[8] = S_koenig; ;
        SchwarzeListe[9] = S_dame;
        SchwarzeListe[10] = S_springer1;
        SchwarzeListe[11] = S_springer2;
        SchwarzeListe[12] = S_laeufer1;
        SchwarzeListe[13] = S_laeufer2;
        SchwarzeListe[14] = S_turm1;
        SchwarzeListe[15] = S_turm2;

    }

    public void Neuplazierung()
    {
        Spielfeld[0] = S_turm1;
        Spielfeld[1] = S_springer1;
        Spielfeld[2] = S_laeufer1;
        Spielfeld[3] = S_dame;
        Spielfeld[4] = S_koenig;
        Spielfeld[5] = S_laeufer2;
        Spielfeld[6] = S_springer2;
        Spielfeld[7] = S_turm2;
        



        for ( int i = 0; i < Spielfeld.Length; i++)
        {

        }
    }
    
    
    // Bauer1 schlägt anderen bauer2
    // Bauer1 angriff
    // Bauer2 sterbe
    // Bauer1 bewege
   
    

    //liste weise
    //liste schwarz

    public int abc = 0;
    public void KanoneAnimation()
    {

        this.W_bauer1.GetComponent<BauerSkript>().Angriff();
        

    }


}

public class Point{
    public float x;
    public float y;
    public float GetX()
    {
        return x;
    }

    public float GetY()
    {
        return y;
    }

    public void SetXY(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

}

public class Spielfeld{

}