using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour
{
	public float speed;
	public float rotateSpeed;

	public Transform moveRelativeToObj;

	Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	void FixedUpdate ()
	{
		//Vector3 walkDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized * speed;
		//walkDir.y = rb.velocity.y;
		//rb.velocity = transform.TransformDirection(walkDir);

		//transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotateSpeed);

		Vector3 inputVec = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

		Vector3 relativeSpaceInputVec = moveRelativeToObj.TransformDirection(inputVec);

		relativeSpaceInputVec.y = 0f;
		

		if (inputVec != Vector3.zero)
		{
			transform.forward = Vector3.Lerp(transform.forward, relativeSpaceInputVec, .2f);
		}

		rb.velocity = relativeSpaceInputVec * speed;

		//rb.velocity = transform.forward * relativeSpaceInputVec.magnitude * Mathf.Sign(Input.GetAxis("Vertical")) * speed;
	}
}
