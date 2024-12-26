using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector3 = UnityEngine.Vector3;

public class PlayerControlReceptor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] 
    private Player player;
    
    private bool isControlling;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isControlling)
            return;
        
        var destination = Camera.main!.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, Camera.main!.transform.position.z)).x - Camera.main!.transform.position.x * 2;
        
        player.MoveTo(destination);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isControlling = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isControlling = false;
    }
}
