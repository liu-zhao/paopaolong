using UnityEngine;
using System.Collections;

public class BallProperty : MonoBehaviour {
	public int m_state; //状态值 0:表示什么也不做 1:出生 2:死亡 3:未加分死亡 4:旋转
	private GameObject m_gameObject; //当前球
	private bool m_state03 = false;//只执行一次的标志
	private Vector3 m_targetvect3;
	private float m_timer = 0;



	// Use this for initialization
	void Start () {
		m_gameObject = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_state == 3) {
			loseBall ();
		}
	}
	/* 当前球为未加分死亡球时，设置目标球将收到重力影响isKinematic = false，初始化重力，并且设置当前球会越来越小
	 * 在2.5s以后消失
	*/
	void loseBall(){
		//未加分死亡
		if (!m_state03) {
			//初始化
			m_state03 = true;
			m_gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			m_gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0.5f;
			m_gameObject.GetComponent<Collider2D> ().isTrigger = true;
			m_gameObject.tag = Config.loseBall;
			m_targetvect3 = new Vector3 (0.01f, 0.01f, 0.01f);

			//发随机力
			Vector2 tempvect2 = new Vector2(Random.Range(-360,360),Random.Range(-360,360));
			m_gameObject.GetComponent<Rigidbody2D> ().AddForce (tempvect2 *0.8f);

		}
		//从小变大
		m_gameObject.transform.localScale = Vector3.Lerp(m_gameObject.transform.localScale,m_targetvect3,Time.deltaTime*0.5f);
		m_timer += Time.deltaTime;
		if (m_timer >= 2.5f) {
			if(m_gameObject.GetComponent<Rigidbody2D>().mass ==20){
				//CreateBall.Instance.m
			}
			m_state = 0;
			m_timer = 0;
			Destroy (m_gameObject);
		}
	}
}
