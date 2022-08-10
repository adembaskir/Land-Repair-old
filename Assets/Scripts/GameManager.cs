using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;
        if (buildIndex >= 1)
        {
            FindObjectOfType<PickupObject>().GetComponent<PickupObject>().gameObjects[2].SetActive(false);
            FindObjectOfType<PickupObject>().GetComponent<PickupObject>().gameObjects[3].SetActive(false);
            FindObjectOfType<PickupObject>().GetComponent<PickupObject>().gameObjects[4].SetActive(false);
            FindObjectOfType<PickupObject>().GetComponent<PickupObject>().gameObjects[5].SetActive(false);
            FindObjectOfType<PickupObject>().GetComponent<PickupObject>().gameObjects[6].SetActive(false);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }
}
