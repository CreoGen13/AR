using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutines : MonoBehaviour
{
    public GameObject shard;
    public GameObject go;

    private float radius = 0.4f;
    private float power = 10;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.transform.tag == "WallPart")
                {
                    Vector3 explosionPos = hit.point;
                    Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
                    foreach (Collider hited in colliders)
                    {
                        if (hited.tag == "WallPart")
                        {
                            GameObject go = Instantiate(shard, hited.transform.position, hited.transform.rotation);
                            go.gameObject.GetComponent<MeshRenderer>().material.color = hited.gameObject.GetComponent<MeshRenderer>().material.color;
                            hited.gameObject.SetActive(false);
                        }
                    }
                    colliders = Physics.OverlapSphere(explosionPos, radius);

                    GameObject go1 = Instantiate(go, hit.transform.position, Camera.main.transform.rotation);

                    //explosionPos = Camera.main.transform.InverseTransformDirection(Camera.main.transform.TransformDirection(explosionPos) - new Vector3(0, 0, 0.3f));
                    explosionPos = go1.transform.position - go1.transform.TransformDirection(new Vector3(0, 0, 0.3f));
                    Destroy(go1);

                    foreach (Collider hited in colliders)
                    {
                        if (hited.tag == "Shard")
                        {
                            Rigidbody rb = hited.GetComponent<Rigidbody>();
                            rb.AddExplosionForce(power, explosionPos, radius, 0, ForceMode.Impulse);
                        }
                    }
                }
            }
        }
    }
}
