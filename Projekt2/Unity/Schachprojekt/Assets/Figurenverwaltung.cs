using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figurenverwaltung : MonoBehaviour
{
    public GameObject kanone;
 //liste weise
 //liste schwarz

    public int abc = 0;
    public void KanoneAnimation()
    {
        kanone.GetComponent<Animator>().SetTrigger("ConditionTrigger");

    }


}
