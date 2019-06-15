using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

	public string worldMenu;
	public GameObject gameOverScreen;
	private Board board;
	public Text txtPoints;

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board> ();

	}
	// Update is called once per frame
	void Update () {
		//if (board.GetComponent<Board>().moves ==  0) 
		//{
		//	gameOverScreen.SetActive (true);
		//	txtPoints.text= " " + board.score;
		//}
	}
		
	public void toWorldMenu()
	{
		SceneManager.LoadScene (worldMenu);
	}
}

