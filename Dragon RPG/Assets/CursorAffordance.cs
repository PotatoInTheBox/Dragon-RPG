using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D attackCursor = null;
    [SerializeField] Texture2D questionCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);

    CameraRaycaster CameraRaycaster;

	// Use this for initialization
	void Start () {
        CameraRaycaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void Update () {
        /*switch(CameraRaycaster.layerHit)
        {
            case Layer.Enemy:
                Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
                break;
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            
        } */
        Texture2D CCursor = null;
        switch(CameraRaycaster.layerHit)
        {
            case Layer.Enemy:
                CCursor = attackCursor;
                break;
            case Layer.Walkable:
                CCursor = walkCursor;
                break;
            case Layer.RaycastEndStop:
                CCursor = questionCursor;
                break;

        }
        Cursor.SetCursor(CCursor, cursorHotspot, CursorMode.Auto);

    }
}
