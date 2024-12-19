using UnityEngine;

public class StringConnection
{
    public GameObject StartPin { get; set; }
    public GameObject EndPin { get; set; }
    public GameObject StringObject { get; set; }

    public StringConnection(GameObject startPin, GameObject endPin, GameObject stringObject)
    {
        StartPin = startPin;
        EndPin = endPin;
        StringObject = stringObject;
    }

    public bool Matches(GameObject pin1, GameObject pin2)
    {
        return (StartPin == pin1 && EndPin == pin2) || (StartPin == pin2 && EndPin == pin1);
    }

    public bool HasPin(GameObject pin)
    {
        return StartPin == pin || EndPin == pin;
    }
}
