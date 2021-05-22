using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Schachbrett : MonoBehaviour
{
    [SerializeField] private Transform EffektiverStartpunktUntenLinks;
    [SerializeField] private float Feldgroesse;

    public Vector3 RelativePositionZumSchachbrettfeld(Vector2Int position)
    {
        return EffektiverStartpunktUntenLinks.position + new Vector3(position.x * Feldgroesse, 0f, position.y * Feldgroesse);
    }
}

