using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour {

	public static GamePlayController instance;

	public BoxSpawner box_Spawner;

	[HideInInspector]
	public BoxScript currentBox;

	public CameraFollow cameraScript;
	private int moveCount;
	
	void Awake(){
		if(instance==null)
			instance=this;
	}
	// Use this for initialization
	void Start () {
		box_Spawner.SpawnBox();
	}
	
	// Update is called once per frame
	void Update () {
		DetectInput();
	}

	void DetectInput(){
		if(Input.GetMouseButtonDown(0)){
			currentBox.DropBox();
		}
	}

	public void SpawnNewBox(){
		Invoke("NewBox",2f);
	}

	void NewBox(){
		box_Spawner.SpawnBox();
	}

	public void MoveCamera(){
		moveCount++;
		if(moveCount == 3){
			moveCount = 0;
			cameraScript.targetPos.y += 2f;
		}
	}

	public void RestartGame(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(
		UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
	}
}
