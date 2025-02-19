using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDroppingCube : MonoBehaviour
{
    private float fallSpeed;

    // Start is called before the first frame update
    void Start()
    {
        fallSpeed = ARGameManager.dropSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        if (this.transform.position.y < ARGameManager.hitHeight - 0.05f)
        {
            Destroy(this.gameObject);
        }
    }
}
