using UnityEngine;

public class StairsController : MonoBehaviour
{
    public Transform leftController;
    public Transform rightController; 
    public float speed = 2f;
    public Vector3 stairsDirection = new Vector3(0, 1, 1); 

    private bool isOnStairs = false; 
    private Vector3 initialLeftControllerPosition; 
    private Vector3 initialRightControllerPosition; 
    private GameObject player; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered stairs");
            isOnStairs = true;
            player = other.gameObject;

            initialLeftControllerPosition = leftController.position;
            initialRightControllerPosition = rightController.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited stairs");
            isOnStairs = false;
            player = null; 
        }
    }

    private void Update()
    {
        if (isOnStairs && player != null)
        {
            float leftControllerMovement = leftController.position.y - initialLeftControllerPosition.y;
            float rightControllerMovement = rightController.position.y - initialRightControllerPosition.y;

            float averageMovement = (leftControllerMovement + rightControllerMovement) / 2;

            if (Mathf.Abs(averageMovement) > 0.01f) 
            {
                Vector3 moveDirection = stairsDirection.normalized * averageMovement;
                player.transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
            }
        }
    }
}
