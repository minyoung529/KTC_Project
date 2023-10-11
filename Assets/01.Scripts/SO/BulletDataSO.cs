using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BulletDataSO")]
public class BulletDataSO : ScriptableObject
{
	public string gunName;
	public int ammo;
	public float reloadTime;
	public float speed;
}