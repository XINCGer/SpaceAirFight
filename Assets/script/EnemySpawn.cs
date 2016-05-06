/*
ProjectName: 太空大战
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/05/01
*/
using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/EnemySpawn")]
public class EnemySpawn : MonoBehaviour {

	//敌人的prefab
	public Transform m_enemy;
	//生成敌人的时间间隔
	protected float m_timer =5;
	protected Transform m_transform;

	// Use this for initialization
	void Start () {
		m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		m_timer -= Time.deltaTime;
		if (m_timer <= 0) {
			m_timer=Random.value*15.0f;
			if(m_timer<5)
				m_timer=5;
			Instantiate(m_enemy,m_transform.position,Quaternion.identity);
		}
	}
	void OnDrawGizmos(){
		Gizmos.DrawIcon (transform.position,"item.png",true);
	}
}
