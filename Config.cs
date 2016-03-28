using UnityEngine;
using System.Collections;

public class Config : MonoBehaviour {
	public static Config Instance;
	public const int m_x = 29;
	public const int m_y = 27;


	public const string wall = "Wall";
	public const string wallBottom = "WallBottom";
	public const string loseBall = "LoseBall";
	public const string staticBall = "StaticBall";


	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
