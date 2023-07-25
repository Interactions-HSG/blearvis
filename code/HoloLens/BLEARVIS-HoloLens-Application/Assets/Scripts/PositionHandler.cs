using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{

    public ThunderboardHandlerList thunderboardHandlerList;
   
    public GameObject BBoxCorner;
    public GameObject BBoxCornerWorld;

    [Header("Yolo Frame Dimensions")]
    public bool NewYoloFrameDimensions;
    public int YoloFrameWidth;
    public int YoloFrameHeight;

    [Header("Simulated Camera to get 3d points of 2d points from Yolov4.")]
    [Tooltip("Simulates camera from HL2. From ARETT.")]
    public Camera HL2Camera;
    public RenderTexture HL2CameraRenderTexture;

    [Header("Parameters to detect camera movement")]
    public float MaxAngularVelocity = 0.25f;
    public float MaxTranslationVelocity = 0.25f;

    [Header("Parameters for position change")]
    public float MaxSecondsSinceLastUpdate = 3;
    public float MinSecondsSinceLastUpdate = 0.5f;
    public float LerpBetweenYoloAndAoAOffset = 0.3f;
    public float MinDiffToLastOffset = 1f;

    private bool _positionIsChangingRightNow;
    private DateTime _lastOffsetUpdateTime;
    private bool _cameraIsMovingTooMuch;
    private Camera _mainCamera;
    private Vector3 _lastCameraPosition;
    private Quaternion _lastCameraRotation;
    public int RaycastCounter = 0;

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

        if (distanceToLastPosition > 0.03)
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
        var diffX360 = Mathf.Abs((360 + last.eulerAngles.x) - current.eulerAngles.x);
        var diffY = Mathf.Abs(last.eulerAngles.y - current.eulerAngles.y);
        var diffY360 = Mathf.Abs((360 + last.eulerAngles.y) - current.eulerAngles.y);
        log += $"\ndiffX: {diffX} / {diffX360}";
        log += $"\ndiffY: {diffY} / {diffY360}";
        log += $"\nrotation diff: {diff.eulerAngles.ToString()}";

        if (Mathf.Min(diffX, diffX360) > 5 || Mathf.Min(diffY, diffY360) > 5 )
        {
            isMovingTooMuch = true;
        }
        // _lastCameraRotation = _mainCamera.transform.rotation;
        log += $"\nisMoving: {isMovingTooMuch}";
        log += $"\n_cameraIsMovingTooMuch: {_cameraIsMovingTooMuch}";
        log += "\n----- CameraIsMovingTooMuch ----- end ---";
        
        Debug.Log(log);

        return _cameraIsMovingTooMuch;
    }


    /// <summary>
    /// Calculate a new position from an angle.
    /// </summary>
    /// <param name="angle"></param>
    /// <returns>calculated position in world space</returns>
    public Vector3 CalculateLocalOffsetFromAngle(float angle)
    {

        var newPosition = new Vector3(0, 0, 0);
        string log = "--- CalculatePositionFromAngle --";
        log += $"\nangle: {angle}";
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

        // x- and z-value. keeps the info box on a circle with radius 1 around the HL2.
        var newXCircle = Mathf.Cos(Mathf.Deg2Rad * (angle + 90));
        var newZCircle = Mathf.Sin(Mathf.Deg2Rad * (angle + 90));
        // (1,1,1)

        newPosition.x = newXCircle;
        newPosition.z = newZCircle;
        log += $"\nnewPosition: {newPosition}";

        log += "\n--- CalculatePositionFromAngle --- end ---";
        Debug.Log(log);
        return newPosition;
    }

    /// <summary>
    /// Updates the ThunderboardInfoBox's orbital.localOffset.z based on a raycast in the 
    /// center of the bounding box coordinates from YOLO.
    /// </summary>
    public Vector3 CalculateNewOffsetFromYoloStaticDevice(Vector2 BBoxPixelTopLeft, Vector2 BBoxPixelBottomRight)
    {
        string log = $"--- CalculateNewOffsetFromYolo ---";

        // do a Raycast in each corner of the BoundingBox and the center.
        // choose the smalles Z -> possibly closest point of object to user
        Vector2 centerBBox = Vector2.Lerp(BBoxPixelTopLeft, BBoxPixelBottomRight, 0.5f);

        var newOffsetFromRayCenter = ScreenToCameraPointThroughRaycast(centerBBox, $"C_{RaycastCounter}");
        // from the corners of the bbox go more towards the center. might make hitting the object more likely
        var tl = Vector2.Lerp(BBoxPixelTopLeft, centerBBox, 0.4f);
        var newOffsetFromRayTopLeft = ScreenToCameraPointThroughRaycast(tl, $"TL_{RaycastCounter}");
        //
        var br = Vector2.Lerp(BBoxPixelBottomRight, centerBBox, 0.4f);
        var NewOffsetFromRayBottomRight = ScreenToCameraPointThroughRaycast(br, $"BR_{RaycastCounter}");

        var topRight = new Vector2(BBoxPixelBottomRight.x, BBoxPixelTopLeft.y);

        topRight = Vector2.Lerp(topRight, centerBBox, 0.4f);
        var newOffsetFromRayTopRight = ScreenToCameraPointThroughRaycast(topRight, $"TR_{RaycastCounter}");

        var bottomLeft = new Vector2(BBoxPixelTopLeft.x, BBoxPixelBottomRight.y);
        bottomLeft = Vector2.Lerp(bottomLeft, centerBBox, 0.4f);
        var newOffsetFromRayBottomLeft = ScreenToCameraPointThroughRaycast(bottomLeft, $"BL_{RaycastCounter}");
        RaycastCounter++;


        var meanX = (newOffsetFromRayCenter.x + newOffsetFromRayTopLeft.x + NewOffsetFromRayBottomRight.x +
           newOffsetFromRayTopRight.x + newOffsetFromRayBottomLeft.x) / 5f;

        var maxY = Mathf.Max(newOffsetFromRayCenter.y, newOffsetFromRayTopLeft.y, NewOffsetFromRayBottomRight.y,
           newOffsetFromRayTopRight.y, newOffsetFromRayBottomLeft.y);

        var minY = Mathf.Min(newOffsetFromRayCenter.y, newOffsetFromRayTopLeft.y, NewOffsetFromRayBottomRight.y,
           newOffsetFromRayTopRight.y, newOffsetFromRayBottomLeft.y);

        // get the z-value that is closest to the HL2 that is not 0
        var zs = new[] {newOffsetFromRayCenter.z, newOffsetFromRayTopLeft.z, NewOffsetFromRayBottomRight.z,
            newOffsetFromRayTopRight.z, newOffsetFromRayBottomLeft.z};
        float closestZ = zs.Where(z => z != 0).DefaultIfEmpty().Min();
        

        var bboxHeight = Mathf.Abs(maxY - minY);
        log += $"\nbboxHeight: {bboxHeight}";


        // keep the InfoBox at least 0.7m away from the user
        closestZ = (closestZ <= 0.7f) ? 0.7f : closestZ;

        // +(bboxHeight / 2)
        var newLocalOffset = new Vector3(meanX, maxY + bboxHeight / 2, closestZ);
        log += $"\nnewLocalOffset: {meanX}, {maxY}, {closestZ}";
        log += $"\nnewLocalOffset + y-bbox: {newLocalOffset}";

        log += $"\n--- CalculateNewOffsetFromYolo --- end ---";
        Debug.Log(log);

        return newLocalOffset;
    }

    /// <summary>
    /// Converts a screen space vector2 to a world space vector2 by raycasting.
    /// </summary>
    /// <param name="newPos"></param>
    /// <returns></returns>
    public Vector3 ScreenToCameraPointThroughRaycast(Vector2 newPos, string label = "")
    {
        string log = "--- ScreenToCameraPointThroughRaycast ---";

        /* ----------------- ↓ ALL WE need ↓ ------------------------------- */


        // Yolo coordinates: (0,0) top left (1280,720) bottom right
        // HL2camera coordinates: (0,0) bottom left (1280,720) top right
        var newPosYFlipped = new Vector2(newPos.x, HL2Camera.pixelHeight - newPos.y);
        log += $"\nnewPosYFlipped: {newPosYFlipped}";

        Ray ray = HL2Camera.ScreenPointToRay(newPosYFlipped);
        var newWorldPos = HL2Camera.ScreenToWorldPoint(newPosYFlipped);
        var newViewportPos = HL2Camera.ScreenToViewportPoint(newPosYFlipped);
        log += $"\nnewWorldPos HL2camera: {newWorldPos}";
        log += $"\nnewViewportPos HL2camera: {newViewportPos}";
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // we need the hitpoint in local camera space, to set the local offset of the hologram relative to the user's head
            var newInCamLocalSpace = HL2Camera.transform.InverseTransformPoint(hit.point);
            log += $"\nhit point: {hit.point}";
            log += $"\nnewInCamLocalSpace: {newInCamLocalSpace}";
            log += "\n--- ScreenToWorldPointRaycast --- end ---";
            // if  a label is set, create boxes where the raycast hit.point is
            if (label != "")
            {
                //CreateBBoxCorner(newInCamLocalSpace, label);
                //CreateBBoxCornerWorld(hit.point, label);
            }
            
            Debug.Log(log);
            return newInCamLocalSpace;
        }
    
        log += "\n--- ScreenToWorldPointRaycast --- end ---";
        Debug.Log(log);

        // fallback if ray doesn't hit anthing
        return new Vector3(0, 0, 0);

    }

    private void CreateBBoxCorner(Vector3 pos, string label)
    {
        var newPrefabInstance = Instantiate(BBoxCorner, new Vector3(0, 0, 0), Quaternion.identity);
        newPrefabInstance.transform.localRotation = Quaternion.identity;
        newPrefabInstance.transform.localScale = new Vector3(0.02f, 0.02f, 0.01f);
        newPrefabInstance.GetComponent<Orbital>().LocalOffset = pos;
        newPrefabInstance.GetComponentInChildren<TextMeshPro>().text = label;
        newPrefabInstance.SetActive(true);
        GameObject.Destroy(newPrefabInstance, 5);
    }

    private void CreateBBoxCornerWorld(Vector3 pos, string label)
    {
        var newPrefabInstance = Instantiate(BBoxCornerWorld, pos, Quaternion.identity);
        newPrefabInstance.transform.localRotation = Quaternion.identity;
        newPrefabInstance.transform.localScale = new Vector3(0.04f, 0.02f, 0.01f);
        newPrefabInstance.GetComponentInChildren<TextMeshPro>().text = label + "_w";
        newPrefabInstance.SetActive(true);
        GameObject.Destroy(newPrefabInstance, 5);
    }


    public void UpdateLocalOffsetStaticObject(ThunderboardHandler tbh, Orbital orbital, Billboard billboard)
    {
        string log = "---------- UpdateLocalOffsetStaticObject ------------ start ---";

        if (tbh.NumberOfYoloCoordinatesReceived == 5 && tbh.NumberOfAoAsReceived == 5)
        {
            var lastYoloOffset = tbh.LastYoloOffset.offset;
            Vector3 avgYoloOffset = new Vector3(
              tbh.Last5YoloOffsets.Average(x => x.x),
              tbh.Last5YoloOffsets.Average(x => x.y),
              tbh.Last5YoloOffsets.Average(x => x.z));


            Vector3 avgAoAOffset = new Vector3(
             tbh.Last5YoloOffsets.Average(x => x.x),
             tbh.Last5YoloOffsets.Average(x => x.y),
             tbh.Last5YoloOffsets.Average(x => x.z));


            var combinationOfAvgs = new Vector3(Mathf.Lerp(avgYoloOffset.x, avgAoAOffset.x, 0.3f), avgYoloOffset.y, Mathf.Lerp(avgYoloOffset.z, avgAoAOffset.z, 0.3f));


            var curOffset = TBsCurrentPositionInCameraSpace(tbh);

            var dist = Vector3.Distance(curOffset, combinationOfAvgs);
            log += $"\nYolo average after 5 vectors: {dist}";

            if (dist > 0.5)
            {
                log += $"Yolo: distance to average is > 0.5. Updating position";
                StartCoroutine(MoveLocalOffset(orbital, curOffset, combinationOfAvgs, billboard));
                tbh.ThunderboardInfoBox.SetActive(true);
                tbh.TBCurrentLocalOffsetInWorld = avgYoloOffset;
                tbh.SetOffsetText();
                
            }
            
        } else
        {
            UpdateLocalOffsetSensorFusion(tbh, orbital, billboard);
        }
        log += "---------- UpdateLocalOffsetStaticObject ------------ end ---";
        Debug.Log(log);

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
            Debug.Log("Position is changing right now. Not updating.");
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

        var curOffset = TBsCurrentPositionInCameraSpace(tbh); ;
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
        // - the last time the offset was changed needs to be older than *MinSecondsSinceLastUpdate* second
        // - the last received offset need to be younger than *MaxSecondsSinceLastUpdate* seconds to be considered
        // - for Yolo: the time when the frame was processed by yolo should be less than 0.5s ago
        var TimeDiffSinceYoloFrameWasProcessed = (DateTime.UtcNow - tbh.LastYoloFrameProcessedAtTime).TotalSeconds < 0.5f;
        var lastYoloTimeDiffs = TimeDiffSinceYoloFrameWasProcessed && (lastOffsetUpdateTimeDiff > MinSecondsSinceLastUpdate) && (lastYoloReceivedTimeDiffSeconds < MaxSecondsSinceLastUpdate);
        var lastAoATimeDiffs = (lastOffsetUpdateTimeDiff > MinSecondsSinceLastUpdate) && (lastAoAReceivedTimeDiffSeconds < MaxSecondsSinceLastUpdate);


        log += $"\ntime when frame was processed by yolo: {tbh.LastYoloFrameProcessedAtTime}";
        log += $"\nnow: {DateTime.UtcNow}";
        log += $"\ndiff since frame in yolo in ms: {(DateTime.UtcNow - tbh.LastYoloFrameProcessedAtTime).TotalMilliseconds}";

        // if (lastYoloTimeDiffs  && lastAoATimeDiffs)
        if ((lastYoloReceivedTimeDiffSeconds < MaxSecondsSinceLastUpdate) && (lastAoAReceivedTimeDiffSeconds < MaxSecondsSinceLastUpdate))
        {
            log += "\nUpdating offset from both";
            // we assume that the offset from Yolo is more accurate
            var xInterpolated = Mathf.Lerp(lastYoloOffset.x, lastAoAOffset.x, LerpBetweenYoloAndAoAOffset);
            newXOffset = xInterpolated;
            newYOffset = lastYoloOffset.y;
            var yInterpolated = Mathf.Lerp(lastYoloOffset.z, lastAoAOffset.z, LerpBetweenYoloAndAoAOffset);
            newZOffset = yInterpolated;
            log += $"\ninterpolated, x: {xInterpolated}, y: {yInterpolated}";
        } else if ((lastYoloReceivedTimeDiffSeconds < MaxSecondsSinceLastUpdate) && (lastAoAReceivedTimeDiffSeconds > MaxSecondsSinceLastUpdate))
        {
            log += "\nUpdating offset from Yolo";
            newXOffset = lastYoloOffset.x;
            newYOffset = lastYoloOffset.y;
            newZOffset = lastYoloOffset.z;
        } else if ((lastYoloReceivedTimeDiffSeconds > MaxSecondsSinceLastUpdate) && (lastAoAReceivedTimeDiffSeconds<MaxSecondsSinceLastUpdate))
        {
            log += "\nUpdating offset from AoA";
            newXOffset = lastAoAOffset.x;
            newYOffset = 0f;
            newZOffset = lastAoAOffset.z;
        }
        else
        {
            log += "\nTime thresholds are not satisfied. Not updating.";
            Debug.Log(log);
            return;
        }

        var newOffset = new Vector3(newXOffset, newYOffset, newZOffset);
        newOffset.z = (newOffset.z < 0.5f) ? 0.5f : newOffset.z;
        log += $"\nnewOffset: {newOffset}";
        if (newOffset.sqrMagnitude == 0)
        {
            log += "\nnewOffset's length is 0 aka position (0,0,0). Not continuing.";
            Debug.Log(log);
            return;
        }


        // ---- SPATIAL THRESHOLD ---
        // the new offset vector needs to be at least 0.2*0.2 different to the current offset vector
        Vector3 offsetDiff = curOffset - newOffset;
        float sqrLen = offsetDiff.sqrMagnitude;

        log += $"\noffsetDiff: {sqrLen}";
        //if (offsetDiff > 0.2f)
        if (sqrLen > MinDiffToLastOffset)
        {
            log += "\nOffset has changed enough. Updating.";
            StartCoroutine(MoveLocalOffset(orbital, curOffset, newOffset, billboard));
            tbh.ThunderboardInfoBox.SetActive(true);
            tbh.TBCurrentLocalOffsetInWorld = newOffset;
            tbh.SetOffsetText();
            tbh.SetAngleText();
        } else
        {
            log += "\nOffset has not changed enough. Not updating.";
        }
        log += "\n---------- UpdateLocalOffset ------------ end ---";
        Debug.Log(log);

    }



    // https://gamedev.stackexchange.com/a/121324 CC BY-SA 3.0
    public IEnumerator MoveLocalOffset(Orbital orbital, Vector3 from, Vector3 to, Billboard billboard)
    {
        _positionIsChangingRightNow = true;
        float t = 0f;
        Debug.Log("in move");
        Debug.Log($"before move: orbital from: {from}");
        Debug.Log($"before move: orbital to: {to}");
        while (t < 1f)
        {
            t += Time.deltaTime;
            orbital.LocalOffset = Vector3.Lerp(from, to, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        billboard.enabled = true;
        _positionIsChangingRightNow = false;
        orbital.enabled = false;
        
        Debug.Log($"after move: orbital localOffset: {orbital.LocalOffset}");
        
        _lastOffsetUpdateTime = DateTime.UtcNow;
    }


    /// <summary>
    /// Calculates the TB's InfoBox's current position in camera space. 
    /// This allows a comparison with the current/new offset which is in the same space.
    /// </summary>
    /// <param name="tbh"></param>
    /// <returns>The position of the InfoBox in camera space.</returns>
    public Vector3 TBsCurrentPositionInCameraSpace(ThunderboardHandler tbh)
    {
        var tbhPosition = tbh.ThunderboardInfoBox.transform.position;
        var curPositionInCamLocalSpace = HL2Camera.transform.InverseTransformPoint(tbhPosition);
        return curPositionInCamLocalSpace;
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
