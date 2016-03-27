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

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == Config.wall || other.gameObject.tag == Config.wallBottom) 
		{
			m_hitWall++;
			m_wallBottom.GetComponent<Collider2D> ().isTrigger = false;//可以碰撞
			if (m_hitWall == 6) 
			{
				GetComponent<Collider2D> ().isTrigger = true;
				Destroy (GetComponent<BallStop> ());
				GetComponent<BallProperty> ().m_state = 3;//设置当前球为 未加分死亡球 
				createShootBall();
			}
		}
	}
	void createShootBall()
	{
		CreateBall.Instance.m_shootBall [0] = CreateBall.Instance.m_shootBall [1];
		CreateBall.Instance.m_shootBall [0].transform.position = m_shootPos.position;
		Vector3 shootPos = GameObject.Find ("Point").transform.position;
		CreateBall.Instance.m_shootBall [1] = Instantiate (CreateBall.Instance.m_ballStyle [Random.Range (0, CreateBall.Instance.m_layerMaxBallNum)], shootPos, Quaternion.identity) as GameObject;
		CreateBall.Instance.m_shootBall [1].GetComponent<Rigidbody2D> ().isKinematic = true;
		ShooterController.Instance.m_shooter = true;
	}
}
