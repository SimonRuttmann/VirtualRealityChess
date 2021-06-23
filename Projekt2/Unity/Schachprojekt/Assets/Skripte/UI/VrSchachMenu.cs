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
	[SerializeField] private Button switchButton;
	[SerializeField] private Text switchText;
	
	[SerializeField] private GameObject walkingPlayer;
	[SerializeField] private GameObject teleportPlayer;
	private Verfolger verfolgerScript;
	private bool menuIsActive = false;
	private bool teleportPlayerIsActive = true;

    // Start is called before the first frame update
    public void Awake()
    {
		verfolgerScript = gameObject.GetComponent<Verfolger>();
    }

    public void showUI()
    {
		canvas.SetActive(true);
		menuIsActive = true;
    }

	public void setupUI()
    {
		//isActive = !isActive;
		if (menuIsActive) hideUI();
		else showUI();
    }
	public void hideUI()
    {
		canvas.SetActive(false);
		menuIsActive = false;
    }

	public void wechsleBewegungsmodus()
    {
		StartCoroutine(WechseleModusCoRoutine());
	}

	IEnumerator WechseleModusCoRoutine()
    {
		if (teleportPlayerIsActive)
		{
			teleportPlayer.SetActive(false);
			yield return new WaitForSeconds(0.0f);
			walkingPlayer.SetActive(true);

			verfolgerScript.changeTarget(false);
			teleportPlayerIsActive = false;
		}
		else
		{
			walkingPlayer.SetActive(false);
			yield return new WaitForSeconds(0.0f);
			teleportPlayer.SetActive(true);

			verfolgerScript.changeTarget(true);
			teleportPlayerIsActive = true;
		}
	}

	public void beendeSpiel()
    {
		Application.Quit();
    }
}



