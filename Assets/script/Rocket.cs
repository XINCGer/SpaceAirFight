/*
ProjectName: 太空大战
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/05/01
*/
using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/Rocket")]
public class Rocket : MonoBehaviour {

	//子弹飞行速度
	public float m_speed =10;
	//生存时间
	public float m_liveTime=1;
	//威力
	public float m_power=1.0f;
	protected Transform m_transform;
	// Use this for initialization
	void Start () {
		m_transform = this.transform;
		Destroy (this.gameObject,m_liveTime);
	}
	
	// Update is called once per frame
	void Update () {
		m_transform.Translate (new Vector3(0,0,-m_speed*Time.deltaTime));
	}
	void OnTriggerEnter(Collider other){
		//如果子弹碰撞到敌人，则销毁自己
		if (other.tag.CompareTo ("Enemy") != 0)
			return;
		Destroy (this.gameObject);
	}
}
