using System;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public Action OnClickStart;
    public Action OnClickStop;
    private bool isHolding = false;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.IsInJesterSelection)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,100f,~LayerMask.NameToLayer("Snap"),QueryTriggerInteraction.Ignore))
            {
                
                if (hit.collider.gameObject == gameObject)
                {
                    OnClickStart?.Invoke();
                    isHolding = true;
                }
            }
        }
        else if(Input.GetMouseButtonUp(0) && isHolding)
        {
            isHolding = false;
            OnClickStop?.Invoke();
        }
    }
}