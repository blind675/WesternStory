using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float activateDistance = 15f;
	public float attackDistance = 10f;

	public int health = 20;

	void EnemyHit (int damageAmount)
	{
		health -= damageAmount;
	}
}
