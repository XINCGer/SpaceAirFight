/*
ProjectName: 太空大战
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/05/01
*/
using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/Enemy")]
public class Enemy : MonoBehaviour
{

	//移动速度
	public float m_speed = 1;
	//旋转速度
	protected float m_rotSpeed=30;
	public float m_life =10;
	protected Transform m_transform;
	//爆炸特效
	public Transform m_explosionFX;
	// Use this for initialization
	//分数
	public int m_point =10;
	void Start ()
	{
		m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateMove ();
	}

	protected virtual void UpdateMove ()
	{
		//左右移动
		float rx = Mathf.Sin (Time.time)*Time.deltaTime;
		//前进
		m_transform.Translate (new Vector3(rx,0,-m_speed*Time.deltaTime));
	}
	void OnTriggerEnter(Collider other){
		//判断是否是主角的子弹
		if (other.tag.CompareTo ("PlayerRocket") == 0) {
			//获得对方碰撞体的Rocket脚本组件
			Rocket rocket = other.GetComponent<Rocket> ();
			if (rocket != null) {
				m_life -= rocket.m_power;
				if (m_life <= 0) {
					GameManager.Instance.AddScore(m_point);
					//爆炸特效
					Instantiate(m_explosionFX,m_transform.position,Quaternion.identity);
					Destroy (this.gameObject);
				}
			}
		} 
		//判断主角是否与敌人发生碰撞
		else if (other.tag.CompareTo ("Player") == 0) {
			m_life = 0;
			//爆炸特效
			Instantiate(m_explosionFX,m_transform.position,Quaternion.identity);
			Destroy (this.gameObject);
		}
		//敌人飞出屏幕以外自动销毁
		else if (other.tag.CompareTo ("bound") == 0) {
			Debug.Log("==========");
			m_life=0;
			Destroy(this.gameObject);
		}
	}
}
