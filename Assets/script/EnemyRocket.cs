/*
ProjectName: 太空大战
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/05/01
*/
using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/EnemyRocket")]
public class EnemyRocket : Rocket {
	void OnTriggerEnter(Collider other){
		if (other.tag.CompareTo ("Player") != 0) 
		{
			return;
		}
		Destroy (this.gameObject);
	}
}
