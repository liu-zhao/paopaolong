using UnityEngine;
using System.Collections;
/*
 * the ball shine when it near the wall
*/
public class BallShine : MonoBehaviour {


	public GameObject m_ballShine;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag (Config.staticBall)) {
			GameObject tempeffect = Instantiate(m_ballShine,other.transform.position,Quaternion.identity)as  GameObject;
			tempeffect.transform.parent = other.transform;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag (Config.ballShine)) {
			Destroy (other.gameObject);
		}
	}
}
