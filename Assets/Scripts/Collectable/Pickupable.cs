using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public GameObject road;
    public GameObject myObject;
    Animator animator;
    public bool deneme;
    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        animator =GetComponent<Animator>();
       



    }
    public void Update()
    {
        if (deneme == true)
        {
            Vector3 angles = myObject.transform.rotation.eulerAngles;
            if (Vector3.Angle(Vector3.up, myObject.transform.up) >= 100 && Vector3.Angle(Vector3.up, myObject.transform.up) <= 180)
            {
                road.SetActive(true);
                FindObjectOfType<PickupObject>().dropObject();
                
            }
        }
    }
     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Snapper")
        {
            deneme = true;
            Instantiate(particle, other.gameObject.transform.position,Quaternion.identity);
        }
    }
}
