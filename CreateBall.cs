using UnityEngine;
using System.Collections;

public class CreateBall : MonoBehaviour {
	public static CreateBall Instance;
	public GameObject[] m_shootBall = new GameObject[2];//发射几个
	public GameObject[] m_ballStyle = new GameObject[8];//样式
	public int m_layerMaxBallNum = 8;

	//泡泡的小点
	public GameObject m_point;
	public struct ball
	{
		public GameObject pointobject;
		public GameObject ballobject;
	}
	public ball[,] m_ball;//球的数组
	public int m_centerx = Config.m_x/2;
	public int m_centery = Config.m_y/2;
	public GameObject m_ballcenter;
	private int m_x = Config.m_x;//列
	private int m_y = Config.m_y;//行
	public float m_ballRadius = 0.19f;

	private GameObject m_ballObject;
	// Use this for initialization
	void Start () {
		Instance = this;

		m_ball = new ball[m_x, m_y];
		initLayer (7);

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
	//创建中心点
	/*
	 * 初始化当前层的层数以及球数
	*/
	public void initLayer(int m_layer)
	{
		int m_startLayer = m_layer;
		m_layerMaxBallNum = m_layer;
		if (m_layer > 4 && m_layer <= 7) {
			m_startLayer = 4;
			m_layerMaxBallNum = m_layer;
		}
		if (m_layer > 7) {
			m_startLayer = 5;
			m_layerMaxBallNum = 7;
		}
		//清空数组
		for (int j = 0; j < m_y; j++) {
			for (int i = 0; i < m_x; i++) {
				if (m_ball [i, j].ballobject != null) {
					Destroy (m_ball [i, j].ballobject);
					m_ball [i, j].ballobject = null;
				}
				if (m_ball [i, j].pointobject != null) {
					Destroy (m_ball [i, j].pointobject);
					m_ball [i, j].pointobject = null;
				}
			}
		}
		//建立小球生成的定位点
		for (int j = 0; j < m_y; j++) {
			float offset = 0f;
			if (j % 2 == 1) {
				offset = m_ballRadius;
			}
			for (int i = 0; i < m_x; i++) {
				m_ball [i, j].pointobject = Instantiate (m_point, new Vector3 (i * 2 * m_ballRadius + offset, j * 0.329f, 0), Quaternion.identity)as GameObject;
				m_ball [i, j].ballobject = null;
			}
		
		}
		//将中心点设置为其他点的父节点
		for (int j = 0; j < m_y; j++) {
			for (int i = 0; i < m_x; i++) {
				if (i != m_centerx || j != m_centery) {
					m_ball [i, j].pointobject.transform.parent = m_ball [m_centerx, m_centery].pointobject.transform;
				}
			}
		}
		//建立中心球
		m_ball[m_centerx,m_centery].ballobject = Instantiate(m_ballcenter,m_ball[m_centerx,m_centery].pointobject.transform.position,Quaternion.identity) as GameObject;
		m_ball [m_centerx, m_centery].ballobject.SetActive (false);
		m_ball [m_centerx, m_centery].pointobject.transform.parent = m_ball [m_centerx, m_centery].ballobject.transform;

		//创建随机泡泡
		for (int j = 0; j < m_y; j++) {
			for (int i = 0; i < m_x; i++) {
				if (Vector3.Distance (m_ball [m_centerx, m_centery].pointobject.transform.position, m_ball [i, j].pointobject.transform.position) <= (m_startLayer * 2 * m_ballRadius + 0.01f)) {
					if (j == m_centery && i == m_centerx) {
					
					} else {
						m_ballObject = m_ballStyle [Random.Range (0, m_layerMaxBallNum)];
						m_ball [i, j].ballobject = Instantiate (m_ballObject,m_ball[i,j].pointobject.transform.position,Quaternion.identity)as GameObject;
						m_ball [i, j].ballobject.GetComponent<Rigidbody2D> ().isKinematic = true;
						m_ball [i, j].ballobject.transform.parent = m_ball [m_centerx, m_centery].pointobject.transform;
						m_ball [i, j].ballobject.SetActive (false);
					}
					m_ball [i, j].ballobject.tag = Config.staticBall;
					m_ball [i, j].ballobject.GetComponent<Collider2D> ().isTrigger = true;//为了停止后面的球
				}
			}
		}

		//设置中心球
		m_ball[m_centerx,m_centery].ballobject.SetActive(true);

		for (int k = 0; k < m_layerMaxBallNum; k++) {
			for (int j = 0; j < m_y; j++) {
				for (int i = 0; i < m_x; i++) {
					if (m_ball [i, j].ballobject != null) {
						float l = 1.0f + (float)k * 0.1f;
						if (m_ball [i, j].ballobject.GetComponent<Rigidbody2D> ().mass == l) {
							m_ball [i, j].ballobject.SetActive (true);
						}
					}
				}
			}
		}
		m_ball [m_centerx, m_centery].ballobject.GetComponent<Rigidbody2D> ().AddTorque (m_layer * 300f);
	}

}
