using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupObject : MonoBehaviour
{
	GameObject mainCamera;
	public bool carrying;
	[SerializeField]
	GameObject carriedObject;
	public float distance;
	public float smooth;
	public float rotateSpeed;
	public bool stopRotation = false;
	public GameObject[] gameObjects;
	// Use this for initialization
	void Start()
	{
		mainCamera = GameObject.FindWithTag("MainCamera");
	}

	// Update is called once per frame
	void Update()
	{
		if (carrying)
		{
			carry(carriedObject);
			carriedObject.GetComponent<Animator>().enabled = true;
			if (stopRotation == true)
			{
				carriedObject.GetComponent<Animator>().SetBool("isTouch", false);
				stopRotation = false;
			}
            else
            {
				carriedObject.GetComponent<Animator>().SetBool("isTouch", true);
			}
			checkDrop();
			//rotateObject();
		}
		else
		{
			pickup();
		}
	}
    void carry(GameObject o)
	{
		o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
		//o.transform.rotation = Quaternion.identity;
	}

	public void pickup()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				Pickupable p = hit.collider.GetComponent<Pickupable>();
				if (p != null)
				{
					carrying = true;
					carriedObject = p.gameObject;
					//p.gameObject.rigidbody.isKinematic = true;
					p.gameObject.GetComponent<Rigidbody>().useGravity = false;
					
				}
			}
			
		}
	}

	void checkDrop()
	{
		//if (Input.GetButton("Fire1"))
		//{
		//	dropObject();
		//}
	}

	public void dropObject()
	{
		carrying = false;
		//carriedObject.gameObject.rigidbody.isKinematic = false;
		carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
		carriedObject.SetActive(false);
		GameObject.Find("Snap").SetActive(false);
		carriedObject = null;
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Snapper")
        {
			if(carrying == false) {
				gameObjects[0].SetActive(true);
				FindObjectOfType<Movement>().enabled = false;
				FindObjectOfType<Movement>().GetComponentInChildren<Animator>().enabled = false;
				StartCoroutine(WaitFor());
			}

		}
        if (other.tag == "Snapper1")
        {
			GameObject.Find("Snap").SetActive(true);
		}
    }
	
	IEnumerator WaitFor()
    {
		yield return new WaitForSeconds(1f);
		gameObjects[0].SetActive(false);
		gameObjects[1].SetActive(true);
		
	}

}
