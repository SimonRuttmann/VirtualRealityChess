using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInputReciever : InputReciever
{
    private Vector3 clickPosition;
    void Update()
    {
         //https://www.youtube.com/watch?time_continue=208&v=_yf5vzZ2sYE&feature=emb_logo&ab_channel=InfallibleCode
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           // Debug.Log("Received Input" + GetComponent<GameObject>().ToString());
            if (Physics.Raycast(ray, out hit))
            {
                clickPosition = hit.point;
                OnInputRecieved();
            }
        }
    }

    public override void OnInputRecieved()
    {
        //Debug.Log("Klick erhalten: " + clickPosition); 
        foreach (var handler in inputHandlers)
        {
            //Debug.Log("Process" + clickPosition);
            handler.ProcessInput(clickPosition, null, null);
        }
    }
}