using UnityEngine;
using System.Collections;

public class NumberProperty : MonoBehaviour {
	public int m_state;//状态值 0:表示什么也不做 1:出生 2:死亡 3:未加分死亡 4:旋转
	private float m_colorA = 255;//透明度
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (m_state == 1 && (gameObject != null)) {
		//从小变大
			gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale,new Vector3(2,2,2),Time.deltaTime* 2f);
			m_colorA = Mathf.Lerp (m_colorA, 0, Time.deltaTime * 10);//从不透明变到透明
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, m_colorA);
		}
	}
}
