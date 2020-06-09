using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	// TODO: debug variables
	public float targetDistance;

	void Attack (object attackData)
	{

		object [] a = (object [])attackData;

		int attackDamage = (int)a [0];
		float maxAttackDistance = (float)a [1];

		RaycastHit Shot;
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out Shot)) {
			targetDistance = Shot.distance;

			//invoke enemy damage 
			if (targetDistance < maxAttackDistance) {
				Shot.transform.SendMessage ("EnemyHit", attackDamage, SendMessageOptions.DontRequireReceiver);
			}

			// TODO: blood splash maybe
		}
	}


}