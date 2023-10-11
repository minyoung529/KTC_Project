using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GunDataSO")]
public class GunDataSO : ScriptableObject
{
	public string gunName;
	public int ammo;
	public float reloadTime;
	public float speed;
	public bool isReload;
}