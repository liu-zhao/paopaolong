using UnityEngine;
using System.Collections;
/*
 * implement buttons' function in GameOver
*/
public class GameLose2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void m_restartPlay()
	{
		Time.timeScale = 1;
		Application.LoadLevel (Application.loadedLevelName);
	}
	public void m_exitGame()
	{
		Application.Quit ();
	}
}
