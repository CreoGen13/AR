using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder2 : MonoBehaviour
{
    public GameObject cube;
    public GameObject go;

    public GameObject Target3;
    public GameObject Cylinder3;
    public GameObject CylinderCurrent;

    private int x = 2;
    private bool wall2Active = false;

    MeshRenderer meshCurrent;
    MeshRenderer meshOther;

    private GameObject go1;
    private GameObject[,] masCube;

    private Vector3 positionCurrent;
    private Vector3 positionOther;
    void Start()
    {
        go1 = Instantiate(go, transform.position, Quaternion.identity) as GameObject;
        meshCurrent = CylinderCurrent.GetComponent<MeshRenderer>();
        meshOther = Cylinder3.GetComponent<MeshRenderer>();
    }
    void Update()
    {
        float delta = Mathf.Sqrt(Mathf.Pow(transform.position.x - Target3.transform.position.x, 2) + Mathf.Pow(transform.position.z - Target3.transform.position.z, 2));
        if (delta <= x)
        {
            if (!wall2Active)
            {
                Operator.CreateWall(Target3.transform, transform, ref masCube, go1, cube);
                wall2Active = true;
            }
            if (wall2Active)
            {
                float rattle1 = Operator.Rattling(positionCurrent, transform.position);
                float rattle2 = Operator.Rattling(positionOther, Target3.transform.position);
                if (rattle1 > 0.03 || rattle2 > 0.03)
                {
                    Operator.UpdateWall(Target3.transform, transform, ref masCube);

                    positionCurrent = transform.position;
                    positionOther = Target3.transform.position;
                }
            }
        }
        if (delta > x || !meshCurrent.enabled || !meshOther.enabled)
        {
            Operator.DeleteWall(go1);
            wall2Active = false;
        }
    }
}
