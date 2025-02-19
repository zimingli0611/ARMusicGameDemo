using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingCube : MonoBehaviour
{
    private float fallSpeed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        if (this.transform.position.y < GameManager.hitHeight - 1.0f)
        { 
            Destroy(this.gameObject);
        }
    }
}
