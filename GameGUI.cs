using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
	public static GameGUI Instance;
	public int m_score;
	public int m_scoretotal1;

	public GameObject m_addScoreNumberF;
	public GameObject[] m_addScoreNumber2 = new GameObject[10];
	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	

	}
	public void addScore(int point)
	{
		m_score += point;
	}
}
