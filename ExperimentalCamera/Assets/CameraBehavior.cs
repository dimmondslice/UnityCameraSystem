using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour
{
	public Transform mainTarget;

	public float maxDist;
	public Vector3 defaultOffset;

	private Transform camTrans;
	private Camera cam;
	private Vector3 camLocalPosLastFrame;

	// Use this for initialization
	void Start ()
	{
		cam = GetComponentInChildren<Camera>();
		camTrans = cam.transform;

		transform.position = mainTarget.TransformPoint(defaultOffset);
		transform.LookAt(mainTarget);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//camTrans.localPosition = camLocalPosLastFrame;

		//Vector3 ssp = cam.WorldToViewportPoint(mainTarget.position);
		//print(ssp);
		//if (ssp.x <= .2 || ssp.x >= .8f || ssp.y <= .4 || ssp.y >= .6)
		//{
		//	transform.LookAt(mainTarget);
		//	transform.position = Vector3.Lerp( transform.position, mainTarget.TransformPoint(defaultOffset),.1f);
		//}

		//camTrans.localPosition = Vector3.Lerp(camLocalPosLastFrame, Vector3.zero, .05f);
		//camLocalPosLastFrame = camTrans.localPosition;
		if (DetectIfObjectOutsideScreenSpaceBoundry(mainTarget, .8f, .2f))
		{
			transform.LookAt(mainTarget);
			GetComponent<Rigidbody>().velocity = mainTarget.GetComponent<Rigidbody>().velocity;
		}
	}

	private bool DetectIfObjectOutsideScreenSpaceBoundry( Transform trackedObject, float xBoundry, float yBoundry )
	{
		Vector3 objInLocalSpace = trackedObject.position - camTrans.position;
		Vector3 objInHorizontalViewPlane = Vector3.ProjectOnPlane(objInLocalSpace, camTrans.up);
		Vector3 objInVerticalViewPlane = Vector3.ProjectOnPlane(objInLocalSpace, camTrans.right);
		float xAngle = Vector3.Angle(transform.forward, objInHorizontalViewPlane);
		float yAngle = Vector3.Angle(transform.forward, objInVerticalViewPlane);

		float xRatio = xAngle / (cam.fieldOfView / 2);
		float yRatio = yAngle / (cam.fieldOfView / 2);

		//print("xRatio:" + xRatio + "     yRatio:" + yRatio);
		//Debug.DrawRay(transform.position, transform.forward);
		//Debug.DrawRay(transform.position, objInLocalSpace, Color.magenta);

		if (xRatio > xBoundry)
			return true;
		else if (yRatio > yBoundry)
			return true;
		else return false;
	}
}