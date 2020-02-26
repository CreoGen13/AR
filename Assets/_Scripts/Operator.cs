using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : MonoBehaviour
{
    public static void CreateWall(Transform other, Transform current, ref GameObject[,] masCube, GameObject go, GameObject cube)
    {
        float delta = Mathf.Sqrt(Mathf.Pow(other.transform.InverseTransformDirection(current.position).x - other.transform.InverseTransformDirection(other.position).x, 2) + Mathf.Pow(other.transform.InverseTransformDirection(current.position).z - other.transform.InverseTransformDirection(other.position).z, 2));
        float height = (delta - 1f) * 10;

        float colorPercent = 1 - (height / 13);
        Color color = new Color(1, colorPercent, colorPercent, 1);

        float signSin = Mathf.Sign(other.transform.InverseTransformDirection(current.position).x - other.transform.InverseTransformDirection(other.position).x);
        float signCos = Mathf.Sign(other.transform.InverseTransformPoint(other.position).x - other.transform.InverseTransformPoint(current.position).x);

        float sin = (other.transform.InverseTransformDirection(other.position).z - other.transform.InverseTransformDirection(current.position).z) / delta;
        sin = signSin * Mathf.Sin(Mathf.Asin(signSin * sin));

        float cos = Mathf.Sqrt(1 - Mathf.Pow(sin, 2));
        cos = signCos * cos;

        masCube = new GameObject[32, 20];

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 32; j++)
            {
                if (j < height + 2)
                {
                    masCube[j, i] = Instantiate(cube, other.transform.TransformPoint(-((j / 10f) + 0.45f) * cos, 0.05f + (i / 10f), -((j / 10f) + 0.55f) * sin), other.transform.rotation * Quaternion.LookRotation(other.transform.InverseTransformDirection(current.position - other.position), new Vector3(0, other.transform.InverseTransformDirection(other.position).y, 0)), go.transform);
                    masCube[j, i].GetComponent<MeshRenderer>().material.color = color;
                    masCube[j, i].SetActive(true);
                }
                else
                {
                    masCube[j, i] = Instantiate(cube, other.transform.TransformPoint(-((j / 10f) + 0.45f) * cos, 0.05f + (i / 10f), -((j / 10f) + 0.55f) * sin), other.transform.rotation * Quaternion.LookRotation(other.transform.InverseTransformDirection(current.position - other.position), new Vector3(0, other.transform.InverseTransformDirection(other.position).y, 0)), go.transform);
                    masCube[j, i].GetComponent<MeshRenderer>().material.color = color;
                    masCube[j, i].SetActive(false);
                }
            }
        }
    }
    public static void UpdateWall(Transform other, Transform current, ref GameObject[,] masCube)
    {
        float delta = Mathf.Sqrt(Mathf.Pow(other.transform.InverseTransformDirection(current.position).x - other.transform.InverseTransformDirection(other.position).x, 2) + Mathf.Pow(other.transform.InverseTransformDirection(current.position).z - other.transform.InverseTransformDirection(other.position).z, 2));
        float height = (delta - 1f) * 10;

        float colorPercent = 1 - (height / 13);
        Color color = new Color(1, colorPercent, colorPercent, 1);

        float signSin = Mathf.Sign(other.transform.InverseTransformDirection(current.position).x - other.transform.InverseTransformDirection(other.position).x);
        float signCos = Mathf.Sign(other.transform.InverseTransformPoint(other.position).x - other.transform.InverseTransformPoint(current.position).x);

        float sin = (other.transform.InverseTransformDirection(other.position).z - other.transform.InverseTransformDirection(current.position).z) / delta;
        sin = signSin * Mathf.Sin(Mathf.Asin(signSin * sin));

        float cos = Mathf.Sqrt(1 - Mathf.Pow(sin, 2));
        cos = signCos * cos;

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 32; j++)
            {
                if (j < height + 2)
                {
                    Quaternion q = Quaternion.LookRotation(other.transform.InverseTransformDirection(current.position - other.position), new Vector3(0, other.transform.InverseTransformDirection(other.position).y, 0));

                    masCube[j, i].transform.position = other.transform.TransformPoint(-((j / 10f) + 0.45f) * cos, 0.05f + (i / 10f), -((j / 10f) + 0.55f) * sin);
                    masCube[j, i].transform.rotation = other.transform.rotation * q;

                    masCube[j, i].GetComponent<MeshRenderer>().material.color = color;
                    masCube[j, i].SetActive(true);
                }
                else
                    masCube[j, i].SetActive(false);
            }
        }
    }
    public static void DeleteWall(GameObject go)
    {
        foreach (Transform child in go.transform)
            Destroy(child.gameObject);
    }
    public static float Concut(float x)
    {
        return ((int)(x * 10)) / 10f;
    }
    public static Vector3 Concut(Vector3 vector)
    {
        return new Vector3(Concut(vector.x), Concut(vector.y), Concut(vector.z));
    }
    public static float Rattling(Vector3 vector1, Vector3 vector2)
    {
        float[] mas = new float[3];//{ Mathf.Abs(vector1.x - vector2.x) , Mathf.Abs(vector1.y - vector2.y), Mathf.Abs(vector1.z - vector2.z) };
        mas[0] = Mathf.Abs(vector1.x - vector2.x);
        mas[1] = Mathf.Abs(vector1.y - vector2.y);
        mas[2] = Mathf.Abs(vector1.z - vector2.z);
        return Mathf.Max(mas);
    }
}