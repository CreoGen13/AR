using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardBehaviour : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 1);
    }
    void Update()
    {
        float size = transform.localScale.x;
        transform.localScale -= new Vector3(size * 0.1f, size * 0.1f, size * 0.1f);
    }
}
