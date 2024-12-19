using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PinConnector : MonoBehaviour
{
    public GameObject stringPrefab;
    public Transform stringParent;
    private GameObject firstSelectedPin = null;
    private Vector3 originalScale;

    private List<StringConnection> stringConnections = new List<StringConnection>();

    public void OnPinSelected(GameObject selectedPin)
    {
        if (firstSelectedPin == null)
        {
            firstSelectedPin = selectedPin;
            originalScale = firstSelectedPin.transform.localScale;
            firstSelectedPin.transform.localScale = originalScale * 1.4f;
        }
        else
        {
            firstSelectedPin.transform.localScale = originalScale;

            if (firstSelectedPin == selectedPin)
            {
                firstSelectedPin = null;
                return;
            }

            if (!ConnectionExists(firstSelectedPin, selectedPin))
            {
                GameObject newString = CreateStringBetweenPins(firstSelectedPin.transform.position, selectedPin.transform.position);
                stringConnections.Add(new StringConnection(firstSelectedPin, selectedPin, newString));
                SetupStringInteraction(newString);

                firstSelectedPin = null;
            }
        }
    }

    private GameObject CreateStringBetweenPins(Vector3 startPoint, Vector3 endPoint)
    {
        GameObject newString = Instantiate(stringPrefab);
        newString.name = $"String{stringConnections.Count}";

        if (stringParent != null)
        {
            newString.transform.SetParent(stringParent);
        }

        Vector3 midpoint = (startPoint + endPoint) / 2;
        Vector3 direction = endPoint - startPoint;

        float distance = direction.magnitude;

        Vector3 currentScale = newString.transform.localScale;
        currentScale.y = distance / 2;
        newString.transform.localScale = currentScale;

        newString.transform.position = midpoint;

        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
        newString.transform.rotation = rotation;

        return newString;
    }

    private void SetupStringInteraction(GameObject stringObject)
    {
        XRSimpleInteractable interactable = stringObject.GetComponent<XRSimpleInteractable>();
        if (interactable == null)
        {
            interactable = stringObject.AddComponent<XRSimpleInteractable>();
        }

        interactable.selectEntered.RemoveAllListeners();
        interactable.selectEntered.AddListener(interactor =>
        {
            RemoveString(stringObject);
        });
    }

    private bool ConnectionExists(GameObject pin1, GameObject pin2)
    {
        foreach (var connection in stringConnections)
        {
            if (connection.Matches(pin1, pin2))
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveString(GameObject stringObject)
    {
        StringConnection connectionToRemove = null;
        foreach (var connection in stringConnections)
        {
            if (connection.StringObject == stringObject)
            {
                connectionToRemove = connection;
                break;
            }
        }

        if (connectionToRemove != null)
        {
            Destroy(connectionToRemove.StringObject);
            stringConnections.Remove(connectionToRemove);
        }
    }
}
