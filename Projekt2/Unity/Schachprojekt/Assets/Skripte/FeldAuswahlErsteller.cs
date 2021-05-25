using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeldAuswahlErsteller : MonoBehaviour
{
    //[SerializeField] private Material freeSquareMaterial;
    //[SerializeField] private Material enemySquareMaterial;
    [SerializeField] private GameObject selectorPrefab;
    private List<GameObject> instantiatedSelectors = new List<GameObject>();

    public void ZeigeAuswahl(Dictionary<Vector3, bool> FeldDaten)
    {
       // Debug.Log("zeige Auswahl im FeldAuswahlErsteller aufgerufen");
        ClearSelection();
        //Richtiger absoluter wert
        foreach (var data in FeldDaten)
        {
           
            //GameObject selector = Instantiate(selectorPrefab, data.Key, Quaternion.identity);
            GameObject selector = Instantiate(selectorPrefab);
            selector.transform.position = data.Key;

            
       //     Debug.Log("Instanziiere: " + selector);
            instantiatedSelectors.Add(selector);
            /*
            foreach (var setter in selector.GetComponentsInChildren<MaterialSetter>())
            {
                setter.SetSingleMaterial(data.Value ? freeSquareMaterial : enemySquareMaterial);
            }
            */
        }
    }

    public void ClearSelection()
    {
        for (int i = 0; i < instantiatedSelectors.Count; i++)
        {
            Destroy(instantiatedSelectors[i]);
        }
    }
}