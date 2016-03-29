using UnityEngine;
using System.Collections;

public class ShooterController : MonoBehaviour {
	public static ShooterController Instance;//保证唯一性
	public float m_speed = 2f;//移动速度
	public bool m_shooter = false;//是否可以开跑
	public GameObject m_explosion;//爆炸效果
	private GameObject m_wallBottom;//找到底墙

	private Transform m_transform;//炮身
	private Transform m_childform;//炮管
	private Transform m_ppoint;//炮口

	private Vector2 m_transformpos;
	private Vector2 m_createPos;
	private Vector2 m_shootV3;

	// Use this for initialization
	void Start () {
		Instance = this;
		m_transform = this.transform;
		m_childform = m_transform.FindChild ("shooter");
		m_ppoint = m_childform.FindChild ("Point_p");
		m_transformpos = m_transform.position;
		m_wallBottom = GameObject.FindGameObjectWithTag (Config.wallBottom);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tarPos = Input.mousePosition;
		Vector3 objPos = Camera.main.WorldToScreenPoint (transform.position);//把当前炮的为止转换成3D坐标
		Vector3 direction = tarPos - objPos; //向量减法 把鼠标的位置指向炮的位置
		direction.z = 0f;
		direction = direction.normalized;

		if (direction.y >= 0.3f && Time.timeScale > 0 && tarPos.y / Screen.height <= 0.85f) {
			transform.up = direction;

			if((Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space))&&m_shooter)
			{

				m_wallBottom.GetComponent<Collider2D> ().isTrigger = true;//让底墙不参与碰撞 在ballstop里设置碰撞
				//发射球
				GameObject m_ballObject = CreateBall.Instance.m_shootBall[0];//得到球 从CreateBall这个类

				m_createPos = m_ppoint.position;//得到炮口位置
				m_ballObject.transform.position = m_ppoint.position;//把球移动到炮口 

				m_shootV3 = m_createPos - m_transformpos;//向量减法， 得到炮身到炮口
				m_ballObject.GetComponent<Rigidbody2D> ().isKinematic = false;

				m_ballObject.GetComponent<Rigidbody2D> ().velocity = m_shootV3 * m_speed;
				m_ballObject.GetComponent<Collider2D> ().isTrigger = false;

				m_ballObject.AddComponent<BallStop> (); //添加球停止的属性
				m_shooter = false;//防止多次发射
			}
		}
	}
}
