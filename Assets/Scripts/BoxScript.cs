using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour {

	private float min_X=-2.2f, max_X=2.2f;
	private bool canMove;
	private float moveSpeed=2f;

	private Rigidbody2D myBody;

	private bool gameOver;
	private bool ignoreCollision;
	private bool ignoreTrigger;

	void Awake(){
		myBody=GetComponent<Rigidbody2D>();
		myBody.gravityScale=0f;
	}
	// Use this for initialization
	void Start () {
		canMove=true;
		if(Random.Range(0,2)>0){
			moveSpeed *=-1f;
		}
		GamePlayController.instance.currentBox=this;
	}
	
	// Update is called once per frame
	void Update () {
		MoveBox();
	}

	void MoveBox(){
		if(canMove){
			Vector3 temp=transform.position;
			temp.x += moveSpeed*Time.deltaTime;	

			if(temp.x > max_X){
				moveSpeed*=-1f;
			}
			else if(temp.x < min_X){
				moveSpeed*=-1f;
			}
			transform.position=temp;
		}
	}

	public void DropBox(){
		canMove=false;
		myBody.gravityScale = Random.Range(2,4);
	}

	void Landed(){
		if(gameOver)
			return;

		ignoreCollision=true;
		ignoreTrigger=true;

		GamePlayController.instance.SpawnNewBox();
		GamePlayController.instance.MoveCamera();
	}

	void RestartGame(){
		GamePlayController.instance.RestartGame();
	}

	void OnCollisionEnter2D(Collision2D target){
		if(ignoreCollision)
			return;

		if(target.gameObject.tag=="Platform"){
			Invoke("Landed",2f);
			ignoreCollision=true;
		}

		if(target.gameObject.tag=="Box"){
			Invoke("Landed",2f);
			ignoreCollision=true;
		}
	}

	void OnTriggerEnter2D(Collider2D target){
		if(ignoreTrigger)
			return;
		
		if(target.tag == "GameOver"){
			CancelInvoke("Landed");
			gameOver=true;
			ignoreTrigger=true;

			Invoke("RestartGame",2f);
		}
	}
}
