using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{

    public ThunderboardHandlerList thunderboardHandlerListScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Calculate a new position from an angle.
    /// </summary>
    /// <param name="angle"></param>
    /// <returns>calculated position in world space</returns>
    public Vector3 CalculatePositionFromAngle(float angle)
    {

        var newPosition = new Vector3(0, 0, 1);
        string log = "--- CalculatePositionFromAngle --";
        // tan(angle) = x/y
        // -> x = tan(angle) * y
        // z = 1
        // a = angleFloat
        // b = 90°
        /*
         *     x
         *  ________
         *  |b     /
         *  |     /
         *  |    /          
         * z|   /
         *  |  /
         *  |a/
         *  |/ 
         */
        // only x-value:
        // negate because +90 is on left and -90 is on right of receiver
        //var newX = Mathf.Tan(Mathf.Deg2Rad * angle);

        // x- and z-value. keeps the info box on a circle around the HL2.
        var newXCircle = Mathf.Cos(Mathf.Deg2Rad * (angle + 90));
        var newZCircle = Mathf.Sin(Mathf.Deg2Rad * (angle + 90));
        // (1,1,1)

        newPosition.x = newXCircle;
        newPosition.z = newZCircle;
        log += $"\nnewPosition: {newPosition}";

        var curCameraPosition = Camera.main.transform.position;
        log += $"\ncurCameraPosition: {curCameraPosition}";

        var curCameraRotation = Camera.main.transform.rotation;
        log += $"\ncurCameraRotation: {curCameraRotation}";
        Vector3 cameraForward = curCameraRotation * Vector3.forward;

        var newPosPlusCam = newPosition + curCameraPosition;
        log += $"\nnewPosition +camPos: {newPosPlusCam}";

        var newPosPlusCamTimesRot = cameraForward + newPosPlusCam;
        log += $"\nnewPosition +camPos * camRot: {newPosPlusCamTimesRot}";

        newPosPlusCamTimesRot.z = newZCircle;
        /*
        // If the Camera (Head) rotates, the InforBox needs to be "on the other side"
        if (curCameraRotation.z < 0)
        {
            newPosPlusCamTimesRot.z = -newZCircle;
        }
        newPosPlusCam.z = newZCircle;
        //*/

        log += "\n--- CalculatePositionFromAngle --- end ---";
        Debug.Log(log);
        return newPosPlusCamTimesRot;
    }


    /// <summary>
    /// Converts a screen space vector2 to a world space vector2 by raycasting.
    /// </summary>
    /// <param name="newPos"></param>
    /// <returns></returns>
    public Vector3 ScreenToWorldPointRaycast(Vector2 newPos)
    {
        var cameraTransform = Camera.main.transform;
        string log = "--- ScreenToWorldPointRaycast ---";

        //1280x720
        // following this paper "Augmented Reality Controlled Smart Wheelchair Using Dynamic Signifiers for Affordance Representation"
        // chacon-quesada2019
        // https://doi.org/10.1109/IROS40897.2019.8968290
        var newX = (2 * newPos.x / 1280f) - 1;
        var newY = (-2 * newPos.y / 720) + 1;
        // pixel in normalised device coordinates (ndc)
        var pndc = new Vector4(newX, newY, 1, 1);
        log += $"\nin normalised device coordinates: {pndc}";
        var Mproj = Camera.main.projectionMatrix;

        // position in the camera coordinate system
        //var pc =  Mproj.inverse.MultiplyVector(pndc);
        var Mproji = Mproj.inverse;
        var pc = Mproji.MultiplyPoint(pndc);
        log += $"\nin camera coordinates: {pc}";

        var Mworld = Camera.main.cameraToWorldMatrix;
        // position in application coordinate system (aka world coordinates)
        var pworld = Mworld.MultiplyPoint(pc);
        log += $"\nin app coordinates: {pworld}";

        // position of the camera in the camera coordinate system
        var ocam = new Vector4(0, 0, 0, 1);
        // position of the camera in the world coordinate system
        var oworld = Mworld.MultiplyPoint(ocam);
        log += $"\norigin in world coordinates: {oworld}";



        Vector3 newPosinWorldCoord = Camera.main.ScreenToWorldPoint(newPos);
        log += $"\nresult from ScreenToWorldPoint: {newPosinWorldCoord}";




        var curCameraPosition = Camera.main.transform.position;
        log += $"\ncurCameraPosition: {curCameraPosition}";

        var plusCamPos = newPosinWorldCoord + curCameraPosition;
        log += $"\nplusCamPos: {plusCamPos}";



        Vector3 raycastDirection = new Vector3(0, 0, 0);
        if (thunderboardHandlerListScript.pointMode == "app")
        {
            raycastDirection = pworld;
        }
        else if (thunderboardHandlerListScript.pointMode == "cameraPos")
        {
            raycastDirection = plusCamPos;
        }
        else
        {
            raycastDirection = newPosinWorldCoord;
        }
        log += $"\nraycastStart: {raycastDirection}";

        Ray ray = Camera.main.ScreenPointToRay(newPos);
        Debug.DrawRay(oworld, raycastDirection);
        if (Physics.Raycast(oworld, raycastDirection, out RaycastHit hit))
        //if (Physics.Raycast(raycastDirection, oworld, out RaycastHit hit))
        //if (Physics.Raycast(raycastStart, cameraTransform.forward, out RaycastHit hit))
        //if (Physics.Raycast(pworld, tc.TransformDirection(Vector3.forward), out RaycastHit hit))
        //if (Physics.Raycast(pworld, Vector3.forward, out RaycastHit hit))
        //if (Physics.Raycast(ray, out RaycastHit hit))
        {
            log += $"\nhit point: {hit.point}";
            log += "\n--- ScreenToWorldPointRaycast --- end ---";

            Debug.Log(log);
            return hit.point;
        }
        /*
            if (xyPlane.Raycast(mouseRay, out float distance))
        {
            Vector3 hitPoint = mouseRay.GetPoint(distance);
            return hitPoint;
        }
        */
        log += "\n--- ScreenToWorldPointRaycast --- end ---";
        Debug.Log(log);
        return new Vector3(0, 0, 0);

    }
}
