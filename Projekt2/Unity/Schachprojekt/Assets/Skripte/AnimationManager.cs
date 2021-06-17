using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NICHT ANFASSEN
public class AnimationManager : MonoBehaviour
{
    public enum Animationtrigger { Nichts, Sterben, Angriff, Idle, Loeschen, Bewegen, Drehen}
    private Figur angreifendeFig;
    private Figur sterbendeFig;

    private Animationtrigger animationFigAngreifend = Animationtrigger.Nichts;
    private Animationtrigger animationFigSterbend = Animationtrigger.Nichts;

    private Figur bewegteFigur;
    private Vector2Int koordinaten;
    private Animationtrigger animationBewegteFigur = Animationtrigger.Nichts;


    private Figur gedrehteFigur1;
    private float drehung1;
    private Animationtrigger animationDreheFigur1 = Animationtrigger.Nichts;

    private Figur gedrehteFigur2;
    private float drehung2;
    private Animationtrigger animationDreheFigur2 = Animationtrigger.Nichts;

    public void CleanesLoeschen(float time, Figur figur)
    {
        StartCoroutine(CleanerLoeschenverwalter(time, figur));
    }

    IEnumerator CleanerLoeschenverwalter(float time, Figur figur)
    {
       
        yield return new WaitForSeconds(time);

        Destroy(figur.gameObject);

    }

    public void BewegeFigur(float time, Figur figur, Vector2Int koordianten)
    {
        StartCoroutine(Bewegungsverwalter(time, figur, koordianten));
    }

    IEnumerator Bewegungsverwalter(float time, Figur figur, Vector2Int koord)
    {
        yield return new WaitForSeconds(time);
        this.koordinaten = koord;
        this.bewegteFigur = figur;
        this.animationBewegteFigur = Animationtrigger.Bewegen;

    }



    public void StartAnimation(float time, Figur angreifendeFigur, Figur sterbendeFigur, Animationtrigger animationtrigger)
    {
        StartCoroutine(Animationsverwalter(time, angreifendeFigur, sterbendeFigur, animationtrigger));
    }

    
    public void DreheFigur1(float time, Figur gedrehteFigur, float drehung)
    {
        StartCoroutine(Drehungsverwalter(time, gedrehteFigur, drehung, true));
    }

    public void DreheFigur2(float time, Figur gedrehteFigur, float drehung)
    {
        StartCoroutine(Drehungsverwalter(time, gedrehteFigur, drehung, false));
    }

    IEnumerator Drehungsverwalter(float time, Figur gedrehteFigur, float drehung, bool isFirst)
    {
        yield return new WaitForSeconds(time);
        if (isFirst)
        {
            this.drehung1 = drehung;
            this.gedrehteFigur1 = gedrehteFigur;
            this.animationDreheFigur1 = Animationtrigger.Drehen;
        }
        else
        {
            this.drehung2 = drehung;
            this.gedrehteFigur2 = gedrehteFigur;
            this.animationDreheFigur2 = Animationtrigger.Drehen;
        }
    }


    IEnumerator Animationsverwalter(float time, Figur angreifendeFigur, Figur sterbendeFigur, Animationtrigger animationtrigger)
    {
        yield return new WaitForSeconds(time);

        if (angreifendeFigur != null)
        {
            this.angreifendeFig = angreifendeFigur;
            this.animationFigAngreifend = animationtrigger;
        }
        if (sterbendeFigur != null)
        {
            this.sterbendeFig = sterbendeFigur;
            this.animationFigSterbend = animationtrigger;
        }
       

    }


    public void Update()
    {
        
        switch (this.animationFigAngreifend)
        {
            case Animationtrigger.Nichts:   break;
            case Animationtrigger.Angriff:  this.animationFigAngreifend = Animationtrigger.Nichts; this.angreifendeFig.AngriffAnimation(); Debug.Log("Angriffsfigur Angriff"); break;
            case Animationtrigger.Idle:     this.animationFigAngreifend = Animationtrigger.Nichts; this.angreifendeFig.IdleAnimation(); Debug.Log("Angriffsfigur idle"); break;
            case Animationtrigger.Sterben:  this.animationFigAngreifend = Animationtrigger.Nichts; this.angreifendeFig.SterbeAnimation(); Debug.Log("Angriffsfigur sterben"); break;
            case Animationtrigger.Loeschen: this.animationFigAngreifend = Animationtrigger.Nichts; Destroy(this.angreifendeFig.gameObject); Debug.Log("Angriffsfigur loeschen"); break;
        }
  
        switch (this.animationFigSterbend)
        {
            case Animationtrigger.Nichts:   break;
            case Animationtrigger.Angriff:  this.animationFigSterbend = Animationtrigger.Nichts; this.sterbendeFig.AngriffAnimation(); Debug.Log("Sterbefigur Angriff"); break;
            case Animationtrigger.Idle:     this.animationFigSterbend = Animationtrigger.Nichts; this.sterbendeFig.IdleAnimation(); Debug.Log("Sterbefigur idle");     break;
            case Animationtrigger.Sterben:  this.animationFigSterbend = Animationtrigger.Nichts; this.sterbendeFig.SterbeAnimation(); Debug.Log("Sterbefigur sterben");    break;
            case Animationtrigger.Loeschen: this.animationFigSterbend = Animationtrigger.Nichts; Destroy(this.sterbendeFig.gameObject); Debug.Log("Sterbefigur loeschen");    break;
        }

        switch(this.animationBewegteFigur)
        {
            case Animationtrigger.Nichts:   break;
            case Animationtrigger.Bewegen:  this.animationBewegteFigur = Animationtrigger.Nichts; this.bewegteFigur.BewegeFigur(this.koordinaten); Debug.Log("Bewegefigur bewege"); break;
        }
        switch (this.animationDreheFigur1)
        {
            case Animationtrigger.Nichts:   break;
            case Animationtrigger.Drehen:   this.animationDreheFigur1 = Animationtrigger.Nichts; this.gedrehteFigur1.DreheFigur(drehung1);            break;
        }
        switch (this.animationDreheFigur2)
        {
            case Animationtrigger.Nichts: break;
            case Animationtrigger.Drehen: this.animationDreheFigur2 = Animationtrigger.Nichts; this.gedrehteFigur2.DreheFigur(drehung2); break;
        }

    }

    public void StartEndAnimation(float time, Figur angreifendeFigur, Figur sterbendeFigur, Animationtrigger animationtrigger)
    {
        StartCoroutine(Endanimationsverwalter(time, angreifendeFigur, sterbendeFigur, animationtrigger));
    }



    IEnumerator Endanimationsverwalter(float time, Figur angreifendeFigur, Figur sterbendeFigur, Animationtrigger animationtrigger)
    {
        sterbendeFigur.SterbeAnimation();
        yield return new WaitForSeconds(time);

        Destroy(sterbendeFigur.gameObject);

    }




}
