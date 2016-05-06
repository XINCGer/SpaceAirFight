/*
ProjectName: 太空大战
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/05/01
*/
using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/SuperEnemy")]
public class SuperEnemy : Enemy
{
	public Transform m_rocket;
	//用来控制发射子弹的时间间隔
	protected float m_fireTimer = 2;
	//用来指向主角的飞船
	protected Transform m_player;
	//Awake继承自MonoBehaviour，它会在游戏体实例化的时候执行一次（先于start）
	//使用FindGameObjectWithTag函数获得主角的游戏体实例
	void Awake ()
	{
		GameObject obj = GameObject.FindGameObjectWithTag ("Player");
		if (obj != null) {
			m_player = obj.transform;
		}
	}

	protected override void UpdateMove ()
	{
		m_fireTimer -= Time.deltaTime;
		if (m_fireTimer <= 0) {
			m_fireTimer = 2;
			if (m_player != null) {
				Vector3 relativePos = m_transform.position - m_player.position;
				//Quaternion.LookRotation使子弹在初始化时朝向主角的方向
				Instantiate (m_rocket, m_transform.position, Quaternion.LookRotation (relativePos));
			}
		}
		//前进
		m_transform.Translate (new Vector3 (0, 0, -m_speed * Time.deltaTime));
	}
}
