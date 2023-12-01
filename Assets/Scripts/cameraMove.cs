using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    
    float valueToLerpX;
    float valueToLerpY;
    float speed;
    int displayNumber;
    Vector2Int resolution = new Vector2Int(640, 480);
    List<Vector2Int> resolutionList = new List<Vector2Int>();
    int resolutionIndex = 0;

    List<DisplayInfo> displayLayout = new List<DisplayInfo>();
    void Start()
    {
        Screen.GetDisplayLayout(displayLayout);
        Screen.SetResolution(resolution.x, resolution.y, false);
        speed = 0.075f;
        displayNumber = 0;
        resolutionList.Add(new Vector2Int(480, 270));
        resolutionList.Add(new Vector2Int(640, 480));
        resolutionList.Add(new Vector2Int(960, 540));
        resolutionList.Add(new Vector2Int(1920, 1080));


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            setRes();
            StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, 0, Screen.mainWindowPosition.y, 0, speed));
        }

        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            setRes();
            int xFinalPos = (displayLayout[displayNumber].width / 2) - (resolution.x / 2);
            StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, xFinalPos, Screen.mainWindowPosition.y, 0, speed));
        }

        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            setRes();
            int xFinalPos = (displayLayout[displayNumber].width) - (resolution.x);
            StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, xFinalPos, Screen.mainWindowPosition.y, 0, speed));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            setRes();
            int yFinalPos = (displayLayout[displayNumber].height / 2) - (resolution.y / 2);
            StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, 0, Screen.mainWindowPosition.y, yFinalPos, speed));
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            setRes();
            int xFinalPos = (displayLayout[displayNumber].width / 2) - (resolution.x / 2);
            int yFinalPos = (displayLayout[displayNumber].height / 2) - (resolution.y / 2);
            StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, xFinalPos, Screen.mainWindowPosition.y, yFinalPos, speed));
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            setRes();
            int xFinalPos = (displayLayout[displayNumber].width) - (resolution.x);
            int yFinalPos = (displayLayout[displayNumber].height / 2) - (resolution.y / 2);
            StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, xFinalPos, Screen.mainWindowPosition.y, yFinalPos, speed));
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            setRes();
            int yFinalPos = (displayLayout[displayNumber].height) - (resolution.y);
            StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, 0, Screen.mainWindowPosition.y, yFinalPos, speed));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            setRes();
            int xFinalPos = (displayLayout[displayNumber].width / 2) - (resolution.x / 2);
            int yFinalPos = (displayLayout[displayNumber].height) - (resolution.y);
            StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, xFinalPos, Screen.mainWindowPosition.y, yFinalPos, speed));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            setRes();
            int xFinalPos = (displayLayout[displayNumber].width) - (resolution.x);
            int yFinalPos = (displayLayout[displayNumber].height) - (resolution.y);
            StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, xFinalPos, Screen.mainWindowPosition.y, yFinalPos, speed));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            displayNumber += 1;
            if (displayNumber>=displayLayout.Count)
            {
                displayNumber = 0;
            }
            setRes();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            cycleResolution();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(LerpInfinity(1f));
        }
    }

    Vector2Int getFinalPos(float xPercent, float yPercent)
    {
        return new Vector2Int((int)((displayLayout[displayNumber].width *xPercent) - (resolution.x *xPercent)), (int)((displayLayout[displayNumber].height *yPercent) - (resolution.y *yPercent)));
    }

    void cycleResolution()
    {
        resolutionIndex += 1;
        if (resolutionIndex >= resolutionList.Count)
        {
            resolutionIndex = 0;
        }
        resolution = resolutionList[resolutionIndex];

        setRes();
        
    }

    IEnumerator LerpInfinity(float t)
    {
        //x positions: 0.33,0.3725, 0.415, 0.4575, 0.5,0.5425 0.585, 0.6275,0.67
        //y positions: 0.33, 0.38, 0.5, 0.62,0.67
        Vector2Int nextPos = getFinalPos(0.5f, 0.5f);
        StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, nextPos.x, Screen.mainWindowPosition.y, nextPos.y, t-0.01f));
        yield return new WaitForSeconds(t);

        nextPos = getFinalPos(0.4575f, 0.38f);
        StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, nextPos.x, Screen.mainWindowPosition.y, nextPos.y, t - 0.01f));
        yield return new WaitForSeconds(t);

        nextPos = getFinalPos(0.415f, 0.33f);
        StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, nextPos.x, Screen.mainWindowPosition.y, nextPos.y, t * .66f - 0.01f));
        yield return new WaitForSeconds(t*.66f);

        nextPos = getFinalPos(0.3725f, 0.38f);
        StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, nextPos.x, Screen.mainWindowPosition.y, nextPos.y, t * .66f - 0.01f));
        yield return new WaitForSeconds(t * .66f);

        nextPos = getFinalPos(0.33f, 0.5f);
        StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, nextPos.x, Screen.mainWindowPosition.y, nextPos.y, t - 0.01f));
        yield return new WaitForSeconds(t);

        nextPos = getFinalPos(0.3725f, 0.62f);
        StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, nextPos.x, Screen.mainWindowPosition.y, nextPos.y, t - 0.01f));
        yield return new WaitForSeconds(t);
        setRes();

        nextPos = getFinalPos(0.415f, 0.67f);
        StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, nextPos.x, Screen.mainWindowPosition.y, nextPos.y, t * .66f - 0.01f));
        yield return new WaitForSeconds(t * .66f);

        nextPos = getFinalPos(0.4575f, 0.62f);
        StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, nextPos.x, Screen.mainWindowPosition.y, nextPos.y, t * .66f - 0.01f));
        yield return new WaitForSeconds(t * .66f);

        nextPos = getFinalPos(0.5f, 0.5f);
        StartCoroutine(LerpWindow(Screen.mainWindowPosition.x, nextPos.x, Screen.mainWindowPosition.y, nextPos.y, t - 0.01f));
        yield return new WaitForSeconds(t);

        setRes();
    }

    void setRes()
    {
        if (resolutionIndex == resolutionList.Count - 1)
        {
            Screen.SetResolution(resolution.x, resolution.y, true);
        }
        else
        {
            Screen.SetResolution(resolution.x, resolution.y, false);
        }
    }

    IEnumerator LerpWindow(int startX, int endX, int startY, int endY, float t)
    {
        float timeElapsed = 0;
        float lerpDuration = t;
        while (timeElapsed < lerpDuration)
        {
            valueToLerpX= Mathf.Lerp(startX, endX, timeElapsed / lerpDuration);
            valueToLerpY = Mathf.Lerp(startY, endY, timeElapsed / lerpDuration);

            //Move window lerping
            Screen.MoveMainWindowTo(displayLayout[displayNumber], new Vector2Int((int)valueToLerpX, (int)valueToLerpY));

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        valueToLerpX = endX;
        valueToLerpY = endY;
        Screen.MoveMainWindowTo(displayLayout[displayNumber], new Vector2Int(endX, endY));
        setRes();
    }
}
