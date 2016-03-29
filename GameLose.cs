using UnityEngine;
using System.Collections;

public class GameLose : MonoBehaviour {

	private bool m_lose;//to judge if the game is over.
	private Transform m_shoot;//when gameover the shooter can't shoot any more;
	private Object[] m_redNumber;
	private Vector2 m_scoreXY;
	public Transform m_socreTransform;
	public Transform m_gameOverTransform;

	void Awake()
	{
		Application.targetFrameRate = 45;//enhancing the ability change the refurbish rate to 45 (lower)
	}


	// Use this for initialization
	void Start () {
		m_lose = false;
		m_shoot = GameObject.FindWithTag ("Player").transform;
		m_redNumber = Resources.LoadAll ("RedNumber/");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag( Config.staticBall) && other.gameObject != null) {
			other.gameObject.transform.localScale = new Vector3 (2f, 2f, 0);
			Time.timeScale = 0;
			m_lose = true;
		}
	}
	void OnGUI()
	{
		if (m_lose) {
			m_shoot.GetComponent<ShooterController> ().enabled = false;
			m_gameOverTransform.gameObject.SetActive (true);

			m_scoreXY = GameGUI.Instance.vect3to2 (m_socreTransform.position);
			drawImageNumber(m_scoreXY.x,m_scoreXY.y,GameGUI.Instance.m_score,m_redNumber);
		}
	}
	void drawImageNumber(float x,float y,int point,Object[]images)
	{
		char[] chars = point.ToString ().ToCharArray ();
		Texture2D tex = (Texture2D)images [0];
		float width = tex.width * (Screen.height / Config.m_screenHeight) * 1f;
		float height = tex.height * (Screen.height / Config.m_screenHeight) * 1f;
		//x = Screen.width / 2 - (chars.Length * width / 2);
		foreach (char s in chars) {
			int i = int.Parse (s.ToString ());
			GUI.DrawTexture (new Rect (x, y, width, height), (Texture2D)images [i]);
			x += width; 
		}
	}
}
