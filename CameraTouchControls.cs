using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class CameraTouchControls : MonoBehaviour
{
    public float speed = 0.1f;
    public float pinchZoomSpeed = 0.5f;
    public float easeSpeed, camResetTimer, camResetTimerOG;
    public float minX, maxX, minY, maxY, maxZoomOut, minZoomIn;
    public bool easeW, easeE, easeN, easeS, resetCam, easeToClosest;
    public GameObject[] columns;
    public Vector3 camPos, camPosOG, camLerpBackToPos;
    Vector3 closestColumn; //I think I'm going to go with this - When you end the touchphase, the camera will find the closest column and lerp to that.
    // Use this for initialization
    void Start()
    {
        columns = GameObject.FindGameObjectsWithTag("Column");
        camPosOG = Camera.main.transform.position;
        camPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            camResetTimer -= Time.deltaTime;
            if (camResetTimer <= 0)
            {
                resetCam = true;
            }
        }
        camPos = Camera.main.transform.position;

        if (!resetCam)
        {

            //pan camera logic:

            //if(Input.touchCount>0 &&Input.GetTouch(0).phase == TouchPhase.Began)
            //{
            //    Vector3 pos = Input.GetTouch(0).position;
            //    camLerpBackToPos = Camera.main.ScreenToWorldPoint(pos);
            //}
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (SceneManager.GetActiveScene().buildIndex == 0 || (SceneManager.GetActiveScene().buildIndex > 0 && !GameManager.GM.players[0].GetComponent<Player>().buttonMoving && GameManager.GM.players[0].GetComponent<Player>().onTopOfPlatform)) //if it's the map screen OR if it's another screen AND you're not moving //~~!This last check is a workaround.  checking for jumping being true didn't work, so i'm checking instead for player 1 being on the platform.  probably not the best.
                {
                    //additional check for if joystick is being used:
                    if (SceneManager.GetActiveScene().buildIndex == 0 ||!GameManager.GM.oneHandControls || (GameManager.GM.oneHandControls && SceneManager.GetActiveScene().buildIndex > 0 && !GameObject.Find("MobileJoystick").GetComponent<Joystick>().beingTouched))
                    {
                        Camera.main.GetComponent<CameraScript>().enabled = false; //temporarily turn off the camera script (which is always looking for the midpoint.

                        camResetTimer = camResetTimerOG;
                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                        //~Do I want x-axis movement?  It may be cool to sprawl the map out instead of just being up/down like most games.
                        //if ((camPos.x > minX && camPos.x < maxX) && (camPos.y >= minY && camPos.y < maxY))
                        //{
                        Camera.main.transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

                        //}

                    }
                }
            }

            //ease the camera back to where it should be:
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    if (columns[0] == null) //if the script lost the columns (it does during scene transfer)
                    {
                        columns = GameObject.FindGameObjectsWithTag("Column");
                    }
                    closestColumn = columns[0].transform.position;

                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (Vector3.Distance(camPos, columns[i].transform.position) < Vector3.Distance(camPos, closestColumn))
                        {
                            closestColumn = columns[i].transform.position;
                        }
                    }
                    if (camPos.y < minY)
                    {
                        easeS = true;
                    }
                    if (camPos.y > maxY)
                    {
                        easeN = true;
                    }
                }
                else
                {
                    closestColumn = Camera.main.GetComponent<CameraScript>().GetMidpoint();
                }
                //Debug.Log(closestColumn);
                easeToClosest = true;
                //if (camPos.x < minX)
                //{
                //    easeW = true;
                //}
                //if (camPos.x > maxX)
                //{
                //    easeE = true;
                //}



                Camera.main.GetComponent<CameraScript>().enabled = true; //turn regular camera script back on.

            }
            if (easeToClosest)
            {
                Camera.main.transform.position = Vector3.Lerp(camPos, new Vector3(closestColumn.x, camPos.y, -10), easeSpeed * Time.deltaTime);
                if (Vector3.Distance(camPos, new Vector3(closestColumn.x, camPos.y, -10)) < .2f)
                {
                    easeToClosest = false;
                }

            }
            //VVSecond TryVV
            //~~This does not account for two of them being true (i.e. in corners).  The camera can get caught in the corners atm.
            //if (easeW)
            //{
            //    Camera.main.transform.position = Vector2.Lerp(camPos, new Vector2(minX, camLerpBackToPos.y), easeSpeed * Time.deltaTime);
            //    if (Vector3.Distance(camPos, new Vector3(minX, camLerpBackToPos.y)) <.2f)
            //    {
            //        easeW = false;
            //    }
            //}
            //if (easeE)
            //{
            //    Camera.main.transform.position = Vector2.Lerp(camPos, new Vector2(maxX, camLerpBackToPos.y), easeSpeed * Time.deltaTime);
            //    if (Vector3.Distance(camPos, new Vector2(maxX, camLerpBackToPos.y)) < .2f)
            //    {
            //        easeE = false;
            //    }
            //}
            //if (easeS)
            //{
            //    Camera.main.transform.position = Vector2.Lerp(camPos, new Vector2(camLerpBackToPos.x, minY), easeSpeed * Time.deltaTime);
            //    if (Vector3.Distance(camPos, new Vector2(camLerpBackToPos.x, minY)) < .2f)
            //    {
            //        easeS = false;
            //    }
            //}
            //if (easeN)
            //{
            //    Camera.main.transform.position = Vector2.Lerp(camPos, new Vector2(camLerpBackToPos.x, maxY), easeSpeed * Time.deltaTime);
            //    if (Vector3.Distance(camPos, new Vector2(camLerpBackToPos.x, maxY)) < .2f)
            //    {
            //        easeN = false;
            //    }
            //}

            //VVFirst TryVV
            //if (easeW)
            //{
            //    Camera.main.transform.position = Vector2.Lerp(camPos, new Vector2(minX + 0.5f, camPos.y), easeSpeed * Time.deltaTime);
            //    if(camPos.x > minX)
            //    {
            //        easeW = false;
            //    }
            //}
            //if (easeE)
            //{
            //    Camera.main.transform.position = Vector2.Lerp(camPos, new Vector2(maxX - 0.5f, camPos.y), easeSpeed * Time.deltaTime);
            //    if(camPos.x < maxX)
            //    {
            //        easeE = false;
            //    }
            //}
            if (easeS)
            {
                Camera.main.transform.position = Vector3.Lerp(camPos, new Vector3(closestColumn.x, minY + 0.5f, -10), easeSpeed * Time.deltaTime);
                if (camPos.y > minY)
                {
                    easeS = false;
                }
            }
            if (easeN)
            {
                Camera.main.transform.position = Vector3.Lerp(camPos, new Vector3(closestColumn.x, maxY - 0.5f, -10), easeSpeed * Time.deltaTime);
                if (camPos.y < maxY)
                {
                    easeN = false;
                }
            }
            //!! this catch all led to problems with being off on two sides at once.  (it would ease in one side, then turn all of them false)
            //if ((camPos.x > minX && camPos.x < maxX) && (camPos.y >= minY && camPos.y < maxY))
            //{
            //    easeE = false;
            //    easeW = false;
            //    easeN = false;
            //    easeS = false;
            //}
        }
        else//reset the cam's position
        {
            //this looks like repetitive trash buti had some issues with changing camposOG between levels and when going back to map.
            if (SceneManager.GetActiveScene().buildIndex > 0)
            {
                Camera.main.transform.position = Vector3.Lerp(camPos, Camera.main.GetComponent<CameraScript>().GetMidpoint(), easeSpeed * Time.deltaTime);
                if (Vector3.Distance(camPos, Camera.main.GetComponent<CameraScript>().GetMidpoint()) < 1f)
                {
                    resetCam = false;
                    camResetTimer = camResetTimerOG;

                }
            }
            else
            {
                Camera.main.transform.position = Vector3.Lerp(camPos, camPosOG, easeSpeed * Time.deltaTime);
                if (Vector3.Distance(camPos, camPosOG) < 1f)
                {
                    resetCam = false;
                    camResetTimer = camResetTimerOG;

                }
            }
        }

        //Pinch to zoom, from: https://unity3d.com/learn/tutorials/topics/mobile-touch/pinch-zoom
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (Camera.main.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                //Don't pickup pinch input if the player is moving
                if (SceneManager.GetActiveScene().buildIndex == 0 || (SceneManager.GetActiveScene().buildIndex > 0 && !GameManager.GM.players[0].GetComponent<Player>().buttonMoving && !GameManager.GM.players[0].GetComponent<Player>().jumping)) //if it's the map screen OR if it's another screen AND you're not moving
                {
                    Camera.main.orthographicSize += deltaMagnitudeDiff * pinchZoomSpeed;
                }
                // Make sure the orthographic size never drops below zero.
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, minZoomIn); //make the camera's size the maxiumum between its size and most zoomed in.
                Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize, maxZoomOut);
            }
        }
    }

    public void ResetTimer()
    {
        camResetTimer = camResetTimerOG;

    }
}

