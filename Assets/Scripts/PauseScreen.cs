using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseScreen : MonoBehaviour {

	public string worldMenu;

	private Board board;

	public GameObject thePauseScreen;
	public GameObject theConfirmationScreen;
	
	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			thePauseScreen.SetActive (true);
		}
	}

	private void OnMouseDown()
    {
		thePauseScreen.SetActive (true);
		//boardGO.SetActive(false);
	}


	public void Resume()
	{
		//boardGO.SetActive(true);
		thePauseScreen.SetActive (false);
	}

	public void toWorldMenu()
	{
		thePauseScreen.SetActive (false);
		theConfirmationScreen.SetActive (true);
	}

	public void Sim()
	{
		SceneManager.LoadScene (worldMenu);
	}

	public void Nao()
	{
		theConfirmationScreen.SetActive (false);
	}

	public void Sair()
	{
		
	}

}
