using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class QuestionMarkMenu : MonoBehaviour
{
    public GameObject questionMark;  
    public GameObject menu3D;        
    public Camera mainCamera;       
    public Vector3 questionMarkOffset = new Vector3(1.0f, -1.0f, 2.0f); // Offset pentru semnul de întrebare
    public Vector3 menuOffset = new Vector3(1.0f, -0.5f, 2.5f);         // Offset pentru meniu

    void Start()
    {
        if (!mainCamera) mainCamera = Camera.main;

        if (menu3D) menu3D.SetActive(false);
    }

    void Update()
    {
        PositionRelativeToCamera(questionMark, questionMarkOffset);

        if (menu3D.activeSelf) 
        {
            PositionRelativeToCamera(menu3D, menuOffset);
        }
    }

    void PositionRelativeToCamera(GameObject obj, Vector3 offset)
    {
        if (obj && mainCamera)
        {
            obj.transform.position = mainCamera.transform.position + mainCamera.transform.TransformVector(offset);

            obj.transform.LookAt(mainCamera.transform.position);
            obj.transform.Rotate(0, 180, 0); 
        }
    }

    public void ToggleMenu()
    {
        if (menu3D)
        {
          
            menu3D.SetActive(!menu3D.activeSelf);
        }
    }
}
