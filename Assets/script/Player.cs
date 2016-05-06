/*
ProjectName: 太空大战
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/05/01
*/
using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour
{
	public float m_speed = 1;
	float m_rocketRate = 0;
	protected Transform m_transform;
	public Transform m_rocket;
	public float m_life=3;
	//声音
	public AudioClip m_shootClip;
	//声音源
	protected AudioSource m_audio;
	//爆炸特效
	public Transform m_explosionFX;

	//目标位置
	protected Vector3 m_targetPos;
	//鼠标射线碰撞层
	public LayerMask m_inputMask;
	// Use this for initialization
	void Start ()
	{
		m_transform = this.transform;
		m_audio = this.GetComponent<AudioSource>();
		//初始化目标位置
		m_targetPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		MoveTo ();
		//纵向移动距离
		float movev = 0;
		//水平移动距离
		float moveh = 0;
		//按上键
		if (Input.GetKey (KeyCode.UpArrow)) {
			movev -= m_speed * Time.deltaTime;
		}
		//按下键
		if (Input.GetKey (KeyCode.DownArrow)) {
			movev += m_speed * Time.deltaTime;
		}
		//按左键
		if (Input.GetKey (KeyCode.LeftArrow)) {
			moveh += m_speed * Time.deltaTime;
		}
		//按右键
		if (Input.GetKey (KeyCode.RightArrow)) {
			moveh -= m_speed * Time.deltaTime;
		}
		//移动
		m_transform.Translate (new Vector3 (moveh, 0, movev));
		m_rocketRate -= Time.deltaTime;
		if (m_rocketRate <= 0) {
			m_rocketRate = 0.1f;
			//按空格或者鼠标左键发射子弹
			if (Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0)) {
				Instantiate (m_rocket, m_transform.position, m_transform.rotation);
				//播放声音
				m_audio.PlayOneShot(m_shootClip);
			}
		}


	}
	void OnTriggerEnter(Collider other){
		//和除了自己的子弹以外的精灵发生碰撞的时候都会损失生命
		if (other.tag.CompareTo ("PlayerRocket") != 0) {
			m_life-=1;
			if(m_life<=0){
				//爆炸特效
				Instantiate(m_explosionFX,m_transform.position,Quaternion.identity);
				Destroy(this.gameObject);
			}
		}
	}

	void MoveTo(){
		if (Input.GetMouseButton (0)) {
			//获得鼠标屏幕位置
			Vector3 ms = Input.mousePosition;
			//将屏幕位置转为射线
			Ray ray = Camera.main.ScreenPointToRay(ms);
			//用来记录射线碰撞信息
			RaycastHit hitinfo;
			//产生射线
			bool iscast = Physics.Raycast(ray,out hitinfo,1000,m_inputMask);
			if(iscast){
				//如果击中目标，记录射线碰撞点
				m_targetPos=hitinfo.point;
			}
		}
		//使用vector3提供的MoveTowards函数，获得向目标移动的位置
		Vector3 pos = Vector3.MoveTowards (this.m_transform.position,m_targetPos,m_speed*Time.deltaTime);
		//更新当前位置
		this.m_transform.position = pos;
	}
}
