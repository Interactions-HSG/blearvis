using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{

    public ThunderboardHandlerList thunderboardHandlerList;

    [Header("Yolo Frame Dimensions")]
    public bool NewYoloFrameDimensions;
    public int YoloFrameWidth;
    public int YoloFrameHeight;

    [Header("Simulated Camera to get 3d points of 2d points from Yolov4.")]
    [Tooltip("Simulates camera from HL2. From ARETT.")]
    public Camera HL2Camera;
    public RenderTexture HL2CameraRenderTexture;


    private bool _positionIsChangingRightNow;
    private DateTime _lastOffsetUpdateTime;
    private bool _cameraIsMovingTooMuch;
    private Camera _mainCamera;
    private Vector3 _lastCameraPosition;
    private Quaternion _lastCameraRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Pixel width :" + HL2Camera.pixelWidth + " Pixel height : " + HL2Camera.pixelHeight);
        NewYoloFrameDimensions = false;
        _mainCamera = Camera.main;
        _lastCameraPosition = _mainCamera.transform.position;
        _lastCameraRotation = _mainCamera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (NewYoloFrameDimensions)
        {
            Debug.Log($"Yolo Pixel width : {YoloFrameWidth} Yolo Pixel height : {YoloFrameHeight}");
            HL2CameraRenderTexture.width = YoloFrameWidth;
            HL2CameraRenderTexture.height = YoloFrameHeight;
            Debug.Log($"Camera Pixel width : {HL2Camera.pixelWidth} Camera Pixel height : {HL2Camera.pixelHeight}");
            NewYoloFrameDimensions = false;
        }
        
        

        
    }

    /// <summary>
    /// Check whether the camera (aka user's head) is moving too much with respect to the last frame.
    /// Then NO new offset is set to prevent the InfoBox to move with the users FoV.
    /// Otherise the InfoBox might not stay close to the corresponding object.
    /// </summary>
    /// <returns>true, if camera moves more than the configured thresholds</returns>
    public bool CameraIsMovingTooMuch()
    {
        var isMovingTooMuch = false;
        var log = "----- CameraIsMovingTooMuch -----";
        var distanceToLastPosition = Vector3.Distance(_lastCameraPosition, _mainCamera.transform.position);
        log += $"\ndistanceToLastPosition: {distanceToLastPosition}";

        if (distanceToLastPosition > 0.007)
        {
            isMovingTooMuch = true;
        }
        _lastCameraPosition = _mainCamera.transform.position;
        log += $"\nisMoving: {isMovingTooMuch}";

        var last  = Quaternion.identity * Quaternion.Inverse(_lastCameraRotation);
        log += $"\nlast rotation: {last.eulerAngles}";
        var current = Quaternion.identity * Quaternion.Inverse(_mainCamera.transform.rotation);
        log += $"\ncurrent rotation: {current.eulerAngles}";
        var diff = current * Quaternion.Inverse(last);
        var diffX = Mathf.Abs(last.eulerAngles.x - current.eulerAngles.x);
        var diffY = Mathf.Abs(last.eulerAngles.y - current.eulerAngles.y);
        log += $"\ndiffX: {diffX}";
        log += $"\ndiffY: {diffY}";
        log += $"\nrotation diff: {diff.eulerAngles.ToString()}";

        if (diffX > 3 || diffY > 3 )
        {
            isMovingTooMuch = true;
        }
        _lastCameraRotation = _mainCamera.transform.rotation;
        log += $"\nisMoving: {isMovingTooMuch}";
        log += "\n----- CameraIsMovingTooMuch ----- end ---";
        Debug.Log(log);
        return isMovingTooMuch;
    }


    /// <summary>
    /// Calculate a new position from an angle.
    /// </summary>
    /// <param name="angle"></param>
    /// <returns>calculated position in world space</returns>
    public Vector3 CalculateLocalOffsetFromAngle(float angle)
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
        return newPosition;
    }


    /// <summary>
    /// Converts a screen space vector2 to a world space vector2 by raycasting.
    /// </summary>
    /// <param name="newPos"></param>
    /// <returns></returns>
    public Vector3 ScreenToCameraPointThroughRaycast(Vector2 newPos)
    {
        /* ----------------- ↓ PROBABLY NOT needed anymore ↓ ------------------------------- */
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
        if (thunderboardHandlerList.pointMode == "app")
        {
            raycastDirection = pworld;
        }
        else if (thunderboardHandlerList.pointMode == "cameraPos")
        {
            raycastDirection = plusCamPos;
        }
        else
        {
            raycastDirection = newPosinWorldCoord;
        }
        //log += $"\nraycastStart: {raycastDirection}";

        /* ----------------- ↑ PROBABLY NOT needed anymore ↑ ------------------------------- */


        /* ----------------- ↓ ALL WE need ↓ ------------------------------- */


        // Yolo coordinates: (0,0) top left (640,360) bottom right
        // HL2camera coordinates: (0,0) bottom left (640,360) top right
        var newPosYFlipped = new Vector2(newPos.x, HL2Camera.pixelHeight - newPos.y);
        log += $"\nnewPosYFlipped: {newPosYFlipped}";

        Ray ray = HL2Camera.ScreenPointToRay(newPosYFlipped);
        var newWorldPos = HL2Camera.ScreenToWorldPoint(newPosYFlipped);
        var newViewportPos = HL2Camera.ScreenToViewportPoint(newPosYFlipped);
        log += $"\nnewWorldPos HL2camera: {newWorldPos}";
        log += $"\nnewViewportPos HL2camera: {newViewportPos}";
        //Debug.DrawRay(oworld, raycastDirection);
        //if (Physics.Raycast(oworld, raycastDirection, out RaycastHit hit))
        //if (Physics.Raycast(raycastDirection, oworld, out RaycastHit hit))
        //if (Physics.Raycast(raycastStart, cameraTransform.forward, out RaycastHit hit))
        //if (Physics.Raycast(pworld, tc.TransformDirection(Vector3.forward), out RaycastHit hit))
        //if (Physics.Raycast(pworld, Vector3.forward, out RaycastHit hit))
        if (Physics.Raycast(ray, out RaycastHit hit, 10))
        {
            // we need the hitpoint in local camer space, to set the local offset of the hologram
            var newInCamLocalSpace = HL2Camera.transform.InverseTransformPoint(hit.point);
            log += $"\nhit point: {hit.point}";
            log += $"\nnewInCamLocalSpace: {newInCamLocalSpace}";
            log += "\n--- ScreenToWorldPointRaycast --- end ---";

            Debug.Log(log);
            
            //var newVector = new Vector3(newWorldPos.x, newWorldPos.y, hit.point.z);
            return newInCamLocalSpace;
        }
    
        log += "\n--- ScreenToWorldPointRaycast --- end ---";
        Debug.Log(log);
        return new Vector3(0, 0, 0);

    }


    /// <summary>
    /// Check if the local offset should be updated. If yes, start MoveLocalOffset().
    /// Currently it is only updated, if the saved data is not older than 3 seconds.
    /// </summary>
    /// <param name="tbh"></param>
    /// <param name="orbital"></param>
    /// <param name="billboard"></param>
    public void UpdateLocalOffsetSensorFusion(ThunderboardHandler tbh, Orbital orbital, Billboard billboard)
    {

        if (_positionIsChangingRightNow)
        {
            return;
        }
        string log = "---------- UpdateLocalOffset ------------ start ---";
        var lastYoloOffset = tbh.LastYoloOffset.offset;
        var lastYoloReceivedTime = tbh.LastYoloOffset.timestamp;
        log += $"\nlastYoloOffset: {lastYoloOffset}";
        log += $"\nlastYoloTime: {lastYoloReceivedTime}";

        var lastAoAOffset = tbh.LastAoAOffset.offset;
        var lastAoATime = tbh.LastAoAOffset.timestamp;
        log += $"\nlastAoAOffset: {lastAoAOffset}";
        log += $"\nlastAoATime: {lastAoATime}";

        var curOffset = tbh.TBCurrentLocalOffsetInWorld;
        log += $"\ncurOffset: {curOffset}";
        var newXOffset = curOffset.x;
        var newYOffset = curOffset.y;
        var newZOffset = curOffset.z;


        var lastYoloReceivedTimeDiffSeconds = (DateTime.UtcNow - lastYoloReceivedTime).TotalSeconds;
        var lastAoAReceivedTimeDiffSeconds = (DateTime.UtcNow - lastAoATime).TotalSeconds;
        var lastOffsetUpdateTimeDiff = (DateTime.UtcNow - _lastOffsetUpdateTime).TotalSeconds;
        log += $"\nlastYoloTime: {lastYoloReceivedTime.ToString("HH:mm:ss:fff")}";
        log += $"\nlastAoATime: {lastAoATime.ToString("HH:mm:ss:fff")}";
        log += $"\nnow: {DateTime.UtcNow.ToString("HH:mm:ss:fff")}";
        log += $"\nlastYoloTimeDiffSeconds: {lastYoloReceivedTimeDiffSeconds}";
        log += $"\nlastAoATimeDiffSeconds: {lastAoAReceivedTimeDiffSeconds}";
        log += $"\nlastOffsetUpdateTimeDiff: {lastOffsetUpdateTimeDiff}";



        // ---- TEMPORAL THRESHOLD ---
        // - the last time the offset was changed needs to be older than 1 second
        // - the last received offset need to be younger than 3 seconds to be considered
        var lastYoloTimeDiffs = (lastOffsetUpdateTimeDiff > 1) && (lastYoloReceivedTimeDiffSeconds < 3);
        var lastAoATimeDiffs = (lastOffsetUpdateTimeDiff > 1) && (lastAoAReceivedTimeDiffSeconds < 3);

        if (lastYoloTimeDiffs  && lastAoATimeDiffs)
        {
            log += "\nUpdating offset from both";
            // we assume that the offset from Yolo is more accurate
            newXOffset = Mathf.Lerp(lastYoloOffset.x, lastAoAOffset.x, 0.3f);
            newYOffset = lastYoloOffset.y;
            newZOffset = Mathf.Lerp(lastYoloOffset.z, lastAoAOffset.z, 0.3f);

        } else if (lastYoloTimeDiffs && (lastAoAReceivedTimeDiffSeconds > 3) )
        {
            log += "\nUpdating offset from Yolo";
            newXOffset = lastYoloOffset.x;
            newYOffset = lastYoloOffset.y;
            newZOffset = lastYoloOffset.z;
        } else if ((lastYoloReceivedTimeDiffSeconds > 3) && lastAoATimeDiffs)
        {
            log += "\nUpdating offset from AoA";
            newXOffset = lastAoAOffset.x;
            newYOffset = 0;
            newZOffset = lastAoAOffset.z;
        }

        var newOffset = new Vector3(newXOffset, newYOffset, newZOffset);
        log += $"\nnewOffset: {newOffset}";


        // ---- SPATIAL THRESHOLD ---
        // the new offset vector needs to be at least 0.2 different to the current offset vector
        var offsetDiff = Vector3.Distance(curOffset, newOffset);
        log += $"\noffsetDiff: {offsetDiff}";
        if (offsetDiff > 0.2f)
        {
            log += "\nOffset has changed.";
            StartCoroutine(MoveLocalOffset(orbital, curOffset, newOffset, billboard));
            tbh.TBCurrentLocalOffsetInWorld = newOffset;
            tbh.SetOffsetText();
            tbh.SetAngleText();
          
           
        } else
        {
            log += "\nOffset has not changed. Not updating.";
        }
        log += "\n---------- UpdateLocalOffset ------------ end ---";
        Debug.Log(log);

    }



    // https://gamedev.stackexchange.com/a/121324 CC BY-SA 3.0
    public IEnumerator MoveLocalOffset(Orbital orbital, Vector3 from, Vector3 to, Billboard billboard)
    {
        _positionIsChangingRightNow = true;
        float t = 0f;
        //Debug.Log("in move");
        while (t < 1f)
        {
            t += 3f * Time.deltaTime;
            orbital.LocalOffset = Vector3.Lerp(from, to, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        billboard.enabled = true;
        _positionIsChangingRightNow = false;
        orbital.enabled = false;
        _lastOffsetUpdateTime = DateTime.UtcNow;
    }



    /// <summary>
    /// DEPRECATED.
    /// </summary>
    /// <param name="tbT"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public Quaternion CalculateRotationFromAngle(Transform tbT, float angle)
    {
        // negate because +90 is on left and -90 is on right of receiver

        tbT.LookAt(Camera.main.transform.position, Camera.main.transform.up);
        var newYRotation = tbT.rotation.y;

        if (angle < 0)
        {
            newYRotation = -newYRotation;
        }
        var rotation = new Quaternion(0, newYRotation, 0, 1);




        if (Camera.main.transform.position.z < 0)
        {
            Debug.Log($"Camera.main.transform.position.z: {Camera.main.transform.position.z}");
            angle = 180 - angle;
        }
        //Quaternion.Euler(0, angle, 0);
        return Quaternion.Euler(0, angle, 0);
    }
}
