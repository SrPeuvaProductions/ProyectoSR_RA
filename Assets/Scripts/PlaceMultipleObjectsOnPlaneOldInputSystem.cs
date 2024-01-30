using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// For tutorial video, see my YouTube channel: <seealso href="https://www.youtube.com/@xiennastudio">YouTube channel</seealso>
/// How to use this script:
/// - Add ARPlaneManager to XROrigin GameObject.
/// - Add ARRaycastManager to XROrigin GameObject.
/// - Attach this script to XROrigin GameObject.
/// - Add the prefab that will be spawned to the <see cref="placedPrefab"/>.
/// 
/// Touch to place the <see cref="placedPrefab"/> object on the touch position.
/// Will only placed the object if the touch position is on detected trackables.
/// Will spawn a new object on the touch position.
/// Using Unity old input system.
/// </summary>
[HelpURL("https://youtu.be/HkNVp04GOEI")]
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceMultipleObjectsOnPlaneOldInputSystem : MonoBehaviour
{
    /// <summary>
    /// The prefab that will be instantiated on touch.
    /// </summary>
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject placedPrefab;
    

    /// <summary>
    /// The instantiated object.
    /// </summary>
    GameObject spawnedObject;
    

    private int objectsPut;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // Check if there is existing touch.
        if (Input.touchCount == 0)
            return;

        // Store the current touch input.
        Touch touch = Input.GetTouch(0);

        // Check if the touch input just touched the screen.
        if (touch.phase == TouchPhase.Began)
        {
            // Check if the raycast hit any trackables.
            if (aRRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first hit means the closest.
                var hitPose = hits[0].pose;

                // Instantiated the prefab.
                if (objectsPut < 2)
                {
                    spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                    objectsPut++;

                }
            }

           

        }

        // To make the spawned object always look at the camera. Delete if not needed.
        Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
        lookPos.y = 0;
        spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
