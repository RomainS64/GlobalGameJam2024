using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] 
    private CinemachineVirtualCamera defaultCamera;

    [SerializeField] 
    private CinemachineSmoothPath trailZoom;

    [SerializeField] private Transform defaultCameraTarget;
    [SerializeField] private Transform zoomCameraTarget;
    [SerializeField] private Vector3 zoomCameraTargetOffset;

    public float smoothRate = 3f;
    private CinemachineTrackedDolly cinemachineZoomTrackedDolly;

    private IEnumerator currentEnumerator;

    void Awake()
    {
        defaultCamera.LookAt = defaultCameraTarget;
        trailZoom.m_Waypoints[0].position = defaultCamera.transform.position;
        trailZoom.m_Waypoints[trailZoom.m_Waypoints.Length-1].position = defaultCameraTarget.position;
        cinemachineZoomTrackedDolly = defaultCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        cinemachineZoomTrackedDolly.m_PathPosition = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            ZoomIn();
        }
        
        if (Input.GetKey(KeyCode.R))
        {
            ZoomOut();
        }
    }

    public void ZoomIn()
    {
        if (currentEnumerator != null)
        {
           StopCoroutine("EnumeratorZoomIn");
        }

        currentEnumerator = EnumeratorZoomIn();
        StartCoroutine("EnumeratorZoomIn");
    }

    public void ZoomOut()
    {
        if (currentEnumerator != null)
        {
            StopCoroutine("EnumeratorZoomOut");
        }

        currentEnumerator = EnumeratorZoomOut();
        StartCoroutine("EnumeratorZoomOut");
    }

    IEnumerator EnumeratorZoomIn()
    {
        defaultCamera.LookAt = zoomCameraTarget;
        trailZoom.m_Waypoints[trailZoom.m_Waypoints.Length - 1].position = zoomCameraTarget.position + zoomCameraTargetOffset;

        float waypointsLenght = (float)trailZoom.m_Waypoints.Length;
        float alpha = 0;
        while (alpha < waypointsLenght)
        {
            alpha += .1f * smoothRate * Time.deltaTime;
            cinemachineZoomTrackedDolly.m_PathPosition = alpha;
            yield return null;
        }
    }

    IEnumerator EnumeratorZoomOut()
    {
        defaultCamera.LookAt = defaultCameraTarget;
        float alpha = (float)trailZoom.m_Waypoints.Length;

        while (alpha > 0)
        {
            alpha -= .1f * smoothRate * Time.deltaTime;
            cinemachineZoomTrackedDolly.m_PathPosition = alpha;
            yield return null;
        }
    }
}
