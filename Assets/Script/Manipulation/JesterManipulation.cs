using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JesterManipulation : MonoBehaviour
{
    [SerializeField] private float distanceToFloor;
    [SerializeField] private float distanceToWalls;
    [SerializeField] private float distanceMinFromCamera;
    [SerializeField] private float lerpValue;
    [SerializeField] private float snapLerpValue;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float clickMaxDuration = 0.2f;
    private CameraManager cameraManager;
    private ClickableObject clickableObject;
    private Rigidbody rigidBody;
    private JesterEquipmentHandler jesterEquipmentHandler;
    private IEnumerator followMouseCoroutine;
    private float clickStartTime;

    private SnapPoint linkedSnapPoint = null;

    void Start()
    {
        clickableObject = GetComponent<ClickableObject>();
        rigidBody = GetComponent<Rigidbody>();
        cameraManager = FindObjectOfType<CameraManager>();
        jesterEquipmentHandler = GetComponent<JesterEquipmentHandler>();
        clickableObject.OnClickStart += OnJesterSelected;
        clickableObject.OnClickStop += OnJesterUnSelected;
    }

    private IEnumerator GoToSelection()
    {
        if (linkedSnapPoint != null)
        {
            linkedSnapPoint.IsHolded = false;
            linkedSnapPoint = null;
        }
        GameManager.Instance.IsInJesterSelection = false;
        cameraManager.ZoomIn();
        yield return new WaitForSeconds(3.2f);
        jesterEquipmentHandler.Rotate(180f);
        EquipmentManager.Instance.DisplayEquipmentUI(jesterEquipmentHandler);
    }
    private void OnJesterUnSelected()
    {
        if (Time.time - clickStartTime < clickMaxDuration)
        {
            ClickJester();
        }
        rigidBody.isKinematic = false;
        if (followMouseCoroutine != null)
        {
            StopCoroutine(followMouseCoroutine);
            followMouseCoroutine = null;
        }
        if (linkedSnapPoint != null)
        {
            transform.position = linkedSnapPoint.transform.position;
            StartCoroutine(GoToSelection());
        }
        AudioManager.Instance.PlaySongByTypeAndTag(((SVoice)jesterEquipmentHandler.voiceProperty.Info).Voice.ToString(),"Lache");

    }

    private void ClickJester()
    {
        AudioManager.Instance.PlaySongByTypeAndTag(((SVoice)jesterEquipmentHandler.voiceProperty.Info).Voice.ToString(),"Douleur");
    }
    IEnumerator FollowMouse()
    {
        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask ignoreLayer = LayerMask.NameToLayer("Jester");
            LayerMask snapLayer = LayerMask.NameToLayer("Snap");
            if (Physics.Raycast(ray,out hit,50f))
            {
                var layer = hit.transform.gameObject.layer;
                if (layer != ignoreLayer && layer != snapLayer)
                {
                    SetPosition(ray,hit);
                    if (linkedSnapPoint != null)
                    {
                        linkedSnapPoint.IsHolded = false;
                        linkedSnapPoint = null;
                    }
                }
                else if(layer == snapLayer)
                {
                    SnapPoint hitSnapPoint = hit.transform.GetComponent<SnapPoint>();
                    if (hitSnapPoint.IsHolded && linkedSnapPoint != hitSnapPoint)
                    {
                        SetPosition(ray,hit);
                    }
                    else
                    {
                        linkedSnapPoint = hitSnapPoint;
                        hitSnapPoint.IsHolded = true;
                        transform.position = Vector3.Lerp(transform.position,hit.transform.position,snapLerpValue);
                    }
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void SetPosition(Ray ray,RaycastHit hit)
    {
        var layer = hit.transform.gameObject.layer;
        Vector3 newPos = hit.point - (ray.direction.normalized)*(layer==LayerMask.NameToLayer("Ground")?distanceToFloor:distanceToWalls)+offset;
        float minZPos = Camera.main.transform.position.z + distanceMinFromCamera;
        newPos.z = newPos.z > minZPos ? newPos.z : minZPos;
        transform.position = Vector3.Lerp(transform.position,newPos,lerpValue);
    }
    private void OnJesterSelected()
    {
        AudioManager.Instance.PlaySongByTypeAndTag(((SVoice)jesterEquipmentHandler.voiceProperty.Info).Voice.ToString(),"Attrape");
        clickStartTime = Time.time;
        rigidBody.isKinematic = true;
        if (followMouseCoroutine != null)
        {
            StopCoroutine(followMouseCoroutine);
        }

        followMouseCoroutine = FollowMouse();
        StartCoroutine(followMouseCoroutine);
    }
}
