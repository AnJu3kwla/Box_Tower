using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour {

	public GameObject box_Prefab;

	/*void Start(){
		SpawnBox();
	} 
	*/

	public void SpawnBox(){
		GameObject box_Obj=Instantiate(box_Prefab);

		Vector3 temp=transform.position;
		temp.z=0f;
		box_Obj.transform.position=temp;
	}
}
