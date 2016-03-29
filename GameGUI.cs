using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
	public static GameGUI Instance;
	public int m_score;
	public int m_scoretotal1;

	public GameObject m_addScoreNumberF;
	public GameObject[] m_addScoreNumber2 = new GameObject[10];
	/*******
	 * Create Score UI
	 * 
	*********/
	private Object[] m_redNumber;//read the RedNumber textures from the file
	private Vector2 m_scoreXY;//the x,y axiy about the score
	public Transform m_scoretransfrom;//display the socre

	// Use this for initialization
	void Start () {
		Instance = this;
		m_redNumber = Resources.LoadAll ("RedNumber/");
	}
	
	// Update is called once per frame
	void Update () {
	

	}
	void OnGUI()
	{
		m_scoreXY = vect3to2 (m_scoretransfrom.position);
		drawScoreNumber (m_scoreXY.x, m_scoreXY.y, m_score, m_redNumber);
	}
	public void addScore(int point)
	{
		m_score += point;
	}
	//convert vector3 to vector2, 3D->2D screen
	public Vector2 vect3to2(Vector3 position)
	{
		Camera cameramain = Camera.main;
		Vector2 vect2 = cameramain.WorldToScreenPoint (position);
		vect2 = new Vector2 (vect2.x, Screen.height - vect2.y);
		return vect2;
	}
	public void drawScoreNumber(float x,float y,int point,Object[] images)
	{
		char[] chars = point.ToString().ToCharArray();
		Texture2D tex = (Texture2D)images [0];
		//Screen.height/Config.m_screenHeight to calculate the rate for setting the scale of the pictures.
		float width = tex.width*(Screen.height/Config.m_screenHeight)*1f;
		float height = tex.height*(Screen.height/Config.m_screenHeight)*1f;
		x = Screen.width / 2 - (chars.Length * width / 2);
		foreach (char s in chars) {
			int i = int.Parse (s.ToString ());
			GUI.DrawTexture (new Rect (x, y, width, height), (Texture2D)images [i]);
			x += width;
		}
	}
}
