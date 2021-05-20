using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dieses Skript wird allen Bauern Figuren zugeordnet
//Es impelmentiert alle Event-Handler (Anfassen) und berechnet sich selbst 
public class BauerSkript : MonoBehaviour
{
    public Figurenverwaltung Figurenverwaltung;
    public GameObject Bauer;
    public Point Position;
    public Rigidbody Rb;


    //Diese Methode wird von Figurenverwaltung zu
    //Beginn des Spiels augerufen, um seine Position zu setzen
    public void SetPosition(Point position, float hoehe)
    {
       
        Position = position;
        Bauer.transform.position = new Vector3(position.GetX(), position.GetY(), hoehe);
      //  position.GetX ...; 
      //  position.GetY ... ;
    }


    public void Angriff()
    {
        this.Bauer.GetComponent<Animator>().SetTrigger("ConditionBauerAngriff");
    }

    public void Bewegung()
    {
        this.Bauer.GetComponent<Animator>().SetTrigger("ConditionBauerBewegung");
    }

    public void Idle()
    {
        this.Bauer.GetComponent<Animator>().SetTrigger("ConditionBauerIdle");
    }

    public void Sterbe()
    {
        this.Bauer.GetComponent<Animator>().SetTrigger("ConditionBauerSterbe");
    }

    

    // Start is called before the first frame update
    void Start()
    {
        this.Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
