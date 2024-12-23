using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody rigidbody;
    
    [SerializeField] 
    private float moveSpeed;

    private float? requestedPosition = null;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        
        if (Input.GetMouseButton(0))
        {
            requestedPosition = Camera.main!.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main!.transform.position.z)).x;
        }
        
        if (!requestedPosition.HasValue)
            return;
        
        if (!Precision.AlmostEquals(requestedPosition.Value, transform.position.x))
        {
            // 뭔가 좌표가 이상해서 요청 위치의 부호를 바꿈
            var direction = Mathf.Sign(-requestedPosition.Value - transform.position.x);
            
            transform.Translate(new Vector3(moveSpeed * direction * Time.deltaTime, 0, 0));
        }
        else
        {
            requestedPosition = null;
        }
    }

    private void FixedUpdate()
    {
        // var mousePos = Input.mousePosition;
        //
        // if (Input.GetMouseButton(0))
        // {
        //     requestedPosition = Camera.main!.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main!.transform.position.z)).x;
        // }
        //
        // if (!requestedPosition.HasValue)
        //     return;
        //
        // if (!Precision.AlmostEquals(requestedPosition.Value, rigidbody.position.x))
        // {
        //     // 뭔가 좌표가 이상해서 요청 위치의 부호를 바꿈
        //     var direction = Mathf.Sign(-requestedPosition.Value - rigidbody.position.x);
        //     
        //     rigidbody.MovePosition(new Vector3(rigidbody.position.x + moveSpeed * direction * Time.fixedDeltaTime, rigidbody.position.y, rigidbody.position.z));
        // }
        // else
        // {
        //     requestedPosition = null;
        // }
    }
}
