using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemInfo Info;
    
    private bool dragging;

    private void Update()
    {
    }

    public bool OnItemDropped()
    {
        // TODO: 현재 마우스 위치에서 시작한 레이에 스냅포인트 존재 여부 확인
        
        return false;
    }
}