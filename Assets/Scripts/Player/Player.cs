using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private float MoveSpeed;
    
    private float requestedPosition;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;
        
        requestedPosition = Camera.main!.ScreenToWorldPoint(Input.mousePosition).x;

        if (!Precision.AlmostEquals(requestedPosition, transform.position.x))
        {
            var direction = Mathf.Sign(requestedPosition - transform.position.x);
            
            transform.Translate(new Vector3(MoveSpeed * direction, transform.position.y, transform.position.z));
        }
    }
}
