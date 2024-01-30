using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;

public class TouchController : MonoBehaviour
{

    static int nObjetos = 0;
    static List<GameObject> listaObjetos = new List<GameObject>();

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nObjetos = listaObjetos.Count;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            if (aRRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {

                listaObjetos.Add(gameObject);
                nObjetos = listaObjetos.Count;
                Destroy(gameObject);

            }


        }


    }





}
