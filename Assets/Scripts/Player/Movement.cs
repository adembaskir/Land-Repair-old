using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float swipeSpeed;
    public float moveSpeed;
    private Camera cam;
    public Animator anim;
    public Transform transform2;
    #region SlowMotion
    public float slowMotionTimeScale;

    private float startTimeScale;
    private float startFixedDeltaTime;
    #endregion
    PickupObject po;
    public GameObject particle;
    void Start()
    {
        cam = Camera.main;

       StartCoroutine(WaitForTutorial());
       startTimeScale = Time.timeScale = 1;
       startFixedDeltaTime = Time.fixedDeltaTime;
       po = GetComponent<PickupObject>();
       
    }
    void Update()
    {

        StartSlowMo();
        if (transform2.position.y <= -1)
        {
            StopSlowMo();
        }

        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            moveSpeed = 5;
            swipeSpeed = 5;
            FindObjectOfType<Movement>().GetComponentInChildren<Animator>().enabled = true;
            StopSlowMo();
            Move();
            GetComponent<PickupObject>().stopRotation = true;
            GetComponent<PickupObject>().gameObjects[2].SetActive(false);
            GetComponent<PickupObject>().gameObjects[3].SetActive(false);
            GetComponent<PickupObject>().gameObjects[4].SetActive(false);
            GetComponent<PickupObject>().gameObjects[5].SetActive(false);
           
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StartSlowMo();
        }
        if (po.carrying == true)
        {
            anim.SetBool("isCarrying", true);
        }
        else
        {
            anim.SetBool("isCarrying", false);
           
        }
       
    }
    private void Move()
    {
      
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.transform.localPosition.z;

        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            Vector3 hitVec = hit.point;
            hitVec.y = transform.localPosition.y;
            hitVec.z = transform.localPosition.z;

            transform.localPosition = Vector3.MoveTowards(transform.position, hitVec, Time.deltaTime * swipeSpeed);
        }

    }

    public void StartSlowMo()
    {
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimeScale;
    }
    public void StopSlowMo()
    {
        
        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
           
            FindObjectOfType<Movement>().GetComponentInChildren<Animator>().SetTrigger("isWin");
            Instantiate(particle, other.gameObject.transform.position, Quaternion.identity);
            FindObjectOfType<Movement>().GetComponent<Movement>().enabled = false;
            FindObjectOfType<PickupObject>().gameObjects[6].SetActive(true);
            StartCoroutine(WinWait());
        }
        if(other.tag == "FallCheck")
        {
            FindObjectOfType<PickupObject>().gameObjects[0].SetActive(true);
            FindObjectOfType<Movement>().enabled = false;
            FindObjectOfType<Movement>().GetComponentInChildren<Animator>().enabled = false;
            Time.timeScale = 0;
            StartCoroutine(FallWait());
            

        }
    }
    public IEnumerator WinWait()
    {
        yield return new WaitForSecondsRealtime(1f);
        FindObjectOfType<PickupObject>().gameObjects[6].SetActive(false);
        FindObjectOfType<PickupObject>().gameObjects[7].SetActive(true);
    }
    public IEnumerator WaitForTutorial()
    {
        moveSpeed = 0;
        swipeSpeed = 0;
        FindObjectOfType<Movement>().GetComponentInChildren<Animator>().enabled = false;
        yield return new WaitForSeconds(0.5f);
    }
    public IEnumerator FallWait()
    {
        yield return new WaitForSecondsRealtime(1f);
        FindObjectOfType<PickupObject>().gameObjects[0].SetActive(false);
        FindObjectOfType<PickupObject>().gameObjects[1].SetActive(true);
       
    }
}
