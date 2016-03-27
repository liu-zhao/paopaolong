using UnityEngine;
using System.Collections;

public class CreateBall : MonoBehaviour {
	public static CreateBall Instance;
	public GameObject[] m_shootBall = new GameObject[2];//发射几个
	public GameObject[] m_ballStyle = new GameObject[8];//样式
	public int m_layerMaxBallNum = 8;

	private GameObject m_ballObject;
	// Use this for initialization
	void Start () {
		Instance = this;
		Vector3 shootPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		m_ballObject = m_ballStyle [Random.Range (0, m_layerMaxBallNum)];
		m_shootBall [0] = Instantiate (m_ballObject, shootPos, Quaternion.identity) as GameObject;

		shootPos = GameObject.Find ("Point").transform.position;
		m_ballObject = m_ballStyle [Random.Range (0, m_layerMaxBallNum)];
		m_shootBall [1] = Instantiate (m_ballObject, shootPos, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
