using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMassive : MonoBehaviour
{

    public void InitSuperMassive(float posX, float posY, int size, int grav) {
        PointEffector2D gravityEffector = GetComponent<PointEffector2D>();
        gravityEffector.forceMagnitude = grav;
        this.transform.position = new Vector3(posX, posY, 0);
        this.transform.localScale = new Vector3(size*10, size*10, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
