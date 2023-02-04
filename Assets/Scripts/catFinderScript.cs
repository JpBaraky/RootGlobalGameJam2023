using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catFinderScript : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    public Vector3 targetPosition;
    public Transform pointerTransform;
    public GameObject catMom;
   


    private void Update() {

        targetPosition = catMom.transform.position;
        
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z= 0f;
        Vector3 dir = (toPosition- fromPosition).normalized;


        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
        Debug.Log(isOffScreen);
        if(isOffScreen) {
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if(cappedTargetScreenPosition.x <= 0) {
                cappedTargetScreenPosition.x = 0f;
            }
            if(cappedTargetScreenPosition.x >= Screen.width) {
                cappedTargetScreenPosition.x = Screen.width;
            }
            if(cappedTargetScreenPosition.y <= 0) {
                cappedTargetScreenPosition.y = 0f;
            }
            if(cappedTargetScreenPosition.y >= Screen.height) {
                cappedTargetScreenPosition.y = Screen.height;
            }
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerTransform.position = pointerWorldPosition;
            pointerTransform.localPosition = new Vector3(pointerTransform.localPosition.x,pointerTransform.localPosition.y,0f);
        } else {
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            pointerTransform.position = pointerWorldPosition;
            pointerTransform.localPosition = new Vector3(pointerTransform.localPosition.x,pointerTransform.localPosition.y,0f);

        }
    }
}
