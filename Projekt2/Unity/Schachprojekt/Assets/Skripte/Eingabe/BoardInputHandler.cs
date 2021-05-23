using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Schachbrett))]
public class BoardInputHandler : MonoBehaviour, IInputHandler
{
    private Schachbrett board;

    private void Awake()
    {
        board = GetComponent<Schachbrett>();
    }

    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick)
    {
        board.OnSquareSelected(inputPosition);
    }
}