using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// FPS Controlelr
/// </summary>
public class SchachBehaviourSkript : MonoBehaviour
{
    public Figurenverwaltung figurenverwaltung;
    // Start is called before the first frame update
    void Start()
    {
        figurenverwaltung.abc = 524;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(figurenverwaltung.abc);
        //if ( Input.GetKeyPressed(X))
        figurenverwaltung.KanoneAnimation();
    }
}
