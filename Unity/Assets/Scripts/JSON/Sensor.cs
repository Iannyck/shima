using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sensor
{
    public string name;
    public string type;
    public Vector3 position;
    public Quaternion rotation;
    public List<Sensor> children;
}
