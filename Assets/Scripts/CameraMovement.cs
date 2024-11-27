using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    
    
    
    private Camera cam;
    private Vector3 rotatePos;
    private Vector3 oldPos;
    private Ray ray;
    private bool hasRotated;
    private Vector2 mousePos;
    private Mouse mouse;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        
        mouse = Mouse.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = cam.ViewportPointToRay(cam.ScreenToViewportPoint(Input.mousePosition));
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                rotatePos = hit.point;
                oldPos = transform.position;
                mousePos = Input.mousePosition;
                Cursor.visible = false;
            }

        }
        
        Debug.DrawRay(ray.origin, ray.direction * 1000);
        
        if (Input.GetMouseButton(1))
        {
            hasRotated = true;
            float mod = Time.deltaTime * 300f;
            
            Vector2 mouseMovement = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if (mouseMovement.y > 0 && transform.rotation.eulerAngles.x >= 89) return; //Doesn't move if we're going too high angled
            if (mouseMovement.y < 0 && Vector3.Angle(Vector3.up, transform.position - rotatePos) >= 85) return; //Doesn't move if we are going under the ground
            
            transform.RotateAround(rotatePos, Vector3.up, mouseMovement.x * mod);
            transform.RotateAround(rotatePos, transform.right, mouseMovement.y  * mod);
            //transform.Rotate(new Vector3(mouseMovement.x * mod, mouseMovement.y * mod));
        }
        else if (hasRotated)
        {
            Cursor.visible = true;
            hasRotated = false;
            mouse.WarpCursorPosition(mousePos);
        }

        Vector3 forward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized * Input.GetAxisRaw("Vertical");
        Vector3 right = new Vector3(transform.right.x, 0, transform.right.z).normalized * Input.GetAxisRaw("Horizontal");
        Vector3 up = Vector3.up * (Input.GetAxisRaw("Mouse ScrollWheel") * 15);
        Vector3 movement = (forward + right + up).normalized * (Time.deltaTime * 60f);
        
        transform.Translate(movement, Space.World);
        
        
    }
}
