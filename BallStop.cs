using UnityEngine;
using System.Collections;

public class BallStop : MonoBehaviour {
	private int m_hitWall = 0;
	private GameObject m_wallBottom;
	private Transform m_shootPos;
	// Use this for initialization
	void Start () {
		m_shootPos = GameObject.FindGameObjectWithTag ("Player").transform;
		m_wallBottom = GameObject.FindGameObjectWithTag (Config.wallBottom);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onCollisionEnter2D(Collider2D other)
	{
		
	}
}
