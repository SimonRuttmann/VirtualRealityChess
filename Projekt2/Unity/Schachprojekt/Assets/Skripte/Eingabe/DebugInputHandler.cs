using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInputHandler : MonoBehaviour, IInputHandler
{
    public void VerarbeiteInput(Vector3 inputPosition, GameObject gewaehltesObjekt, Action onClick)
    {
        Debug.Log(string.Format("Clicked object {0} in position {1} with callback {2}",
            gewaehltesObjekt != null ? gewaehltesObjekt.name.ToString() : "null",
            inputPosition,
            (onClick != null)));
    }
}