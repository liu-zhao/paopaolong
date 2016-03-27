using UnityEngine;
using System.Collections;

public class Config : MonoBehaviour {
	public static Config Instance;

	public const string wall = "Wall";
	public const string wallBottom = "WallBottom";
	public const string loseBall = "LoseBall";
	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
