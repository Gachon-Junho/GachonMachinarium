using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] 
    private RectTransform rect;
    
    [SerializeField] 
    private Sprite itemIcon;

    [SerializeField] 
    private Image image;

    [SerializeField] 
    private GameObject puzzlePiece;

    private bool dragStarted;

    private Vector3 initialPosition;

    private PuzzlePiece draggingPuzzle;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = rect.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStarted = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingPuzzle == null)
            return;

        initialPosition = Camera.main!.ScreenToWorldPoint(Input.mousePosition + 
                                                          new Vector3(0, 0, Camera.main!.transform.position.z) - 
                                                          Camera.main!.transform.position * 2);
        
        draggingPuzzle.transform.position = Camera.main!.ScreenToWorldPoint(Input.mousePosition + 
                                                                             new Vector3(0, 0, Camera.main!.transform.position.z) - 
                                                                             Camera.main!.transform.position * 2);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"{name} drag stopped");

        dragStarted = false;
        draggingPuzzle = null;

        // TODO: 지정된 위치에 퍼즐이 배치되었자면 인벤토리 아이템 삭제, 그렇지 않다면 퍼즐 삭제 (macOS 애니메이션 참조)
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        // TODO: 호버시 하이하이트 효과 (ex: 알파 조절)
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        // TODO: 이때부터는 UI영역을 벗어난 것으로 판단하고 인스턴스화

        if (dragStarted && draggingPuzzle == null)
        {
            draggingPuzzle = Instantiate(puzzlePiece).GetComponent<PuzzlePiece>();
            draggingPuzzle.transform.position = Camera.main!.ScreenToWorldPoint(Input.mousePosition + 
                new Vector3(0, 0, Camera.main!.transform.position.z) - 
                Camera.main!.transform.position * 2);
        }
    }
}
