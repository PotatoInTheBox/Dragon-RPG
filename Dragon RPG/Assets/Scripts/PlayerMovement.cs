using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkFieldRadius = 0.2f;

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

    bool isInDirectMode = false; // TODO make static later

    Vector3 playerToClickPoint = Vector3.zero;

    //this is purely experimental
    [SerializeField] GameObject enemy = null;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;

    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        //G for gamepad.
        if (Input.GetKeyDown(KeyCode.G)) //TODO allow player to map later
        {
            isInDirectMode = !isInDirectMode; //toggle
        }

        if (isInDirectMode)
        {
            ProcessDirectMovement();
        };
        if(!isInDirectMode)
        {
            ProcessMouseMovement();
        };
        
    }

    private void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

        m_Character.Move(m_Move, false, false);
    }

    private void ProcessMouseMovement()
    {
        // TODO stop the character because it keeps running from the position changing when it shouldn't
        if (Input.GetMouseButton(0))
        {
            print("Cursor raycast hit" + cameraRaycaster.hit.collider.gameObject.name.ToString());
            switch (cameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;     // So not set in default case
                    playerToClickPoint = currentClickTarget - transform.position;
                    print(currentClickTarget);
                    if (playerToClickPoint.magnitude >= walkFieldRadius)
                    {
                        m_Character.Move(playerToClickPoint, false, false);
                    }
                    else
                    {
                        m_Character.Move(Vector3.zero, false, false);
                    }
                    break;
                case Layer.Enemy:
                    print("This is an enemy.");
                    break;
                default:
                    print("ERROR!!!! Unexpected layer found");
                    return;
            }

        }
        if (enemy != null)
        {
            enemy.transform.position = cameraRaycaster.hit.point;
            //emy.transform.position = playerToClickPoint + transform.position;
        }
    }
}

