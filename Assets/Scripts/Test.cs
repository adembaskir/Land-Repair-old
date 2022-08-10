using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "False")
        {
            FindObjectOfType<PickupObject>().gameObjects[0].SetActive(true);
            FindObjectOfType<Movement>().enabled = false;
            FindObjectOfType<Movement>().GetComponentInChildren<Animator>().enabled = false;
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f); 
        FindObjectOfType<PickupObject>().gameObjects[0].SetActive(false);
        FindObjectOfType<PickupObject>().gameObjects[1].SetActive(true);
    }
}
