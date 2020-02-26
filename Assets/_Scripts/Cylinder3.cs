using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder3 : MonoBehaviour
{
    public GameObject cube;
    public GameObject go;

    public GameObject Target1;
    public GameObject Cylinder1;
    public GameObject CylinderCurrent;

    private int x = 2;
    private bool wall3Active = false;

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
        meshOther = Cylinder1.GetComponent<MeshRenderer>();
    }
    void Update()
    {
        float delta = Mathf.Sqrt(Mathf.Pow(transform.position.x - Target1.transform.position.x, 2) + Mathf.Pow(transform.position.z - Target1.transform.position.z, 2));
        if (delta <= x)
        {
            if (!wall3Active)
            {
                Operator.CreateWall(Target1.transform, transform, ref masCube, go1, cube);
                wall3Active = true;
            }
            if (wall3Active)
            {
                float rattle1 = Operator.Rattling(positionCurrent, transform.position);
                float rattle2 = Operator.Rattling(positionOther, Target1.transform.position);
                if (rattle1 > 0.03 || rattle2 > 0.03)
                {
                    Operator.UpdateWall(Target1.transform, transform, ref masCube);

                    positionCurrent = transform.position;
                    positionOther = Target1.transform.position;
                }
            }
        }
        if (delta > x || !meshCurrent.enabled || !meshOther.enabled)
        {
            Operator.DeleteWall(go1);
            wall3Active = false;
        }
    }
}
