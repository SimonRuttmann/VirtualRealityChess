using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VrSchachMenu : MonoBehaviour
{

	[SerializeField] private GameObject canvas;
	[SerializeField] private Button startButton;
	[SerializeField] private Text startText;
	[SerializeField] private Button neustartButton;
	[SerializeField] private Text neustartText;
	[SerializeField] private Button beendenButton;
	[SerializeField] private Text beendenText;
	private bool isActive = false;
	
	// Start is called before the first frame update
	public void showUI()
    {
		canvas.SetActive(true);
		isActive = true;
    }

	public void setupUI()
    {
		//isActive = !isActive;
		if (isActive) hideUI();
		else showUI();
    }
	public void hideUI()
    {
		canvas.SetActive(false);
		isActive = false;
    }


	public void beendeSpiel()
    {
		Application.Quit();
    }
}



