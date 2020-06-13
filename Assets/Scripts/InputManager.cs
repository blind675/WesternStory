using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public static bool moveForward;
	public static bool moveBackwards;
	public static bool rotateCCW;
	public static bool rotateCW;
	public static bool fire;

	// TODO: move all the keyboard press events here

	public void StartMoveForward ()
	{
		moveForward = true;
	}

	public void StopMoveForward ()
	{
		moveForward = false;
	}

	public void StartMoveBackwards ()
	{
		moveBackwards = true;
	}

	public void StopMoveBackwards ()
	{
		moveBackwards = false;
	}

	public void StartRotateCW ()
	{
		rotateCW = true;
	}

	public void StopRotateCW ()
	{
		rotateCW = false;
	}

	public void StartRotateCCW ()
	{
		rotateCCW = true;
	}

	public void StopRotateCCW ()
	{
		rotateCCW = false;
	}

	public void StartFire ()
	{
		//Debug.Log ("Start Fire");
		fire = true;
	}

	public void StopFire ()
	{
		//Debug.Log ("Stop Fire");
		fire = false;
	}

}
