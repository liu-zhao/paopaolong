﻿using UnityEngine;
using System.Collections;

public class BallStop : MonoBehaviour {
	private int m_hitWall = 0;
	private GameObject m_wallBottom;//找到底墙
	private Transform m_shootPos;//炮台位置

	//发射出来的球停靠
	private bool m_trigger = false;// 保证trigger只被触发一次
	private Transform m_transform;
	//用来存放发射球停靠位置的点的值的结构
	struct xy
	{
		public int x;
		public int y;
	}
	//用来存放发射球停靠位置的点的值
	private xy m_xy;
	//获取预设值
	private int m_x = Config.m_x;
	private int m_y = Config.m_y;

	//用来实现三个相同颜色的球掉落的数据结构
	private ArrayList m_listA = new ArrayList();//放入要检查的泡泡
	private ArrayList m_listB = new ArrayList();//放入要消除的泡泡
	private Stack m_stackA = new Stack ();//过渡栈

	// Use this for initialization
	void Start () {
		m_transform = this.transform;
		m_shootPos = GameObject.FindGameObjectWithTag ("Player").transform;
		m_wallBottom = GameObject.FindGameObjectWithTag (Config.wallBottom);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
	 * 发射的球若碰撞6次墙则成为未加分死亡球 
	*/
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == Config.wall || other.gameObject.tag == Config.wallBottom) 
		{
			m_hitWall++;
			m_wallBottom.GetComponent<Collider2D> ().isTrigger = false;//可以碰撞
			if (m_hitWall == 6) 
			{
				GetComponent<Collider2D> ().isTrigger = true;
				Destroy (GetComponent<BallStop> ()); //在当前球上删除ballstop脚本
				GetComponent<BallProperty> ().m_state = 3;//设置当前球为 未加分死亡球 
				createShootBall();
			}
		}
	}
	/*
	 * 创建新的可以发射的球以及准备发生的球
	*/
	void createShootBall()
	{
		CreateBall.Instance.m_shootBall [0] = CreateBall.Instance.m_shootBall [1];
		CreateBall.Instance.m_shootBall [0].transform.position = m_shootPos.position;
		Vector3 shootPos = GameObject.Find ("Point").transform.position;
		CreateBall.Instance.m_shootBall [1] = Instantiate (CreateBall.Instance.m_ballStyle [Random.Range (0, CreateBall.Instance.m_layerMaxBallNum)], shootPos, Quaternion.identity) as GameObject;
		CreateBall.Instance.m_shootBall [1].GetComponent<Rigidbody2D> ().isKinematic = true;
		ShooterController.Instance.m_shooter = true;
	}
	/*
	 * 用来实现发射新球停靠在已经存在球旁边
	*/
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == Config.staticBall & !m_trigger) {
			m_trigger = true;
			m_wallBottom.GetComponent<Collider2D> ().isTrigger = false;
			Destroy(GetComponent<BallStop>());
			GetComponent<Collider2D> ().isTrigger = true;
			tag = Config.staticBall;
			m_xy = nearPoint (transform.position);//寻找最近停靠点 m_xy里面存放了距离最近的点的值
			transform.position = CreateBall.Instance.m_ball[m_xy.x,m_xy.y].pointobject.transform.position;//停在最近点上
			//设为中心点的子物体
			m_transform.parent = CreateBall.Instance.m_ball[CreateBall.Instance.m_centerx,CreateBall.Instance.m_centery].pointobject.transform;
			m_transform.GetComponent<Rigidbody2D> ().isKinematic = true;
			/*
			 * 当前球停靠在最近位置后，产生新的可以发射的球
			*/
			createShootBall ();//创建要发射的球

			/*
			 * 要实现同颜色相消
			*/
			//将颜色相同的球放入listA中
			for (int j = 0; j < m_y; j++) {
				for (int i = 0; i < m_x; i++) {
					if (CreateBall.Instance.m_ball [i, j].ballobject != null) {
						if (GetComponent<Rigidbody2D> ().mass == CreateBall.Instance.m_ball [i, j].ballobject.GetComponent<Rigidbody2D> ().mass) {
							xy t_xy;
							t_xy.x = i;
							t_xy.y = j;
							m_listA.Add (t_xy);
						}
					}
				}
			}
			//把当前球写入m_ball
			CreateBall.Instance.m_ball [m_xy.x, m_xy.y].ballobject = this.gameObject;
			//求得与现有球相同的所有类型球 判断两圆是否相切
			all_tangency (m_xy);


			//如果listB.Count大于3表示有三个相同颜色的球  可以消除
			if (m_listB.Count >= 3) {
			
				for (int i = 0; i < m_listB.Count; i++) {
					xy t_xy = (xy)m_listB [i];
					drop (t_xy);//消除
				}
			}

		}
	}
	/*
	 * 用来寻找最近停靠点
	 */
	xy nearPoint(Vector3 point)
	{
		float mylen = 100f;
		xy m_nearPoint = new xy ();
		for (int j = 0; j < m_y; j++) {
			for (int i = 0; i < m_x; i++) {
				if (CreateBall.Instance.m_ball [i, j].ballobject == null) {
					float templen = Vector3.Distance (point, CreateBall.Instance.m_ball [i, j].pointobject.transform.position);
					if (templen < mylen) {
						mylen = templen;
						m_nearPoint.x = i;
						m_nearPoint.y = j;
					}

				}
			}
		}
		return m_nearPoint;
	}
	//判断两圆是否相切
	public bool tangency(Vector3 vect1,float radius1,Vector3 vect2,float radius2)
	{
		//0.01f 是误差 1%的误差
		return (Vector3.Distance (vect1, vect2) < (radius1 + radius2 + radius1 * 0.01f + radius2 * 0.01f));
	}
	//判断是否都相切
	void all_tangency (xy t_xy){
		m_stackA.Push (t_xy);
		xy judgxy;
		xy tempxy;
		while (m_stackA.Count > 0) {
			judgxy =(xy) m_stackA.Pop ();
			for (int i = 0; i < m_listA.Count; i++) {
				if (m_listA[i] != null) {
					
					tempxy = (xy)m_listA [i];
					if (tangency (CreateBall.Instance.m_ball [judgxy.x, judgxy.y].ballobject.transform.position, CreateBall.Instance.m_ballRadius, CreateBall.Instance.m_ball [tempxy.x, tempxy.y].ballobject.transform.position, CreateBall.Instance.m_ballRadius)) {
						m_stackA.Push (tempxy);
						m_listA[i] = null;
					}
						
				}
			}
			m_listB.Add (judgxy);
		}
	}
	//消除功能
	void drop (xy t_xy)
	{
		GameObject m_b = CreateBall.Instance.m_ball [t_xy.x, t_xy.y].ballobject;
		m_b.GetComponent<Rigidbody2D> ().isKinematic = false;
		m_b.GetComponent<Rigidbody2D> ().gravityScale = 1.0f;
		m_b.GetComponent<Collider2D> ().isTrigger = false;
		m_b.tag = Config.loseBall;
		Destroy (m_b, 2);
		m_b = null;

	}
}
