using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    [HideInInspector] public float originalSpeed;
    [HideInInspector] public bool started = false;
    public List<Transform> Waypoints;
    private int previousPoint;
    private int currentPoint;
    private int nextPoint;
    public GameObject textPanel;
    public GameObject imagePanel;
    public GameObject questionPanel;
    public GameObject warningPanel;
    public GameObject panelManager;
    public GameObject pausePanel;

    private void Start()
    {
        disabelPanels();
        previousPoint = 0;
        currentPoint = 0;
        nextPoint = 1;
        originalSpeed = speed;
    }

    public void disabelPanels()
    {
        textPanel.SetActive(false);
        imagePanel.SetActive(false);
        questionPanel.SetActive(false);
        warningPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void disablePausePanel(){
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (started)
        {
            if (Input.GetKeyDown(KeyCode.W) && currentPoint != Waypoints.Count - 1)
            {
                disabelPanels();
                speed = originalSpeed;
                if (transform.position != Waypoints[currentPoint].position)
                {
                    if (nextPoint < currentPoint)
                    {
                        nextPoint = currentPoint;
                        currentPoint -= 1;
                    }
                    else if (nextPoint == currentPoint)
                    {
                        nextPoint = currentPoint + 1;
                    }
                }
                transform.position = Vector3.MoveTowards(transform.position, Waypoints[nextPoint].position, speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.W) && currentPoint != Waypoints.Count - 1)
            {
                if (transform.position != Waypoints[currentPoint].position)
                {
                    if (nextPoint < currentPoint)
                    {
                        nextPoint = currentPoint;
                        currentPoint -= 1;
                    }
                    else if (nextPoint == currentPoint)
                    {
                        nextPoint = currentPoint + 1;
                    }
                }
                transform.position = Vector3.MoveTowards(transform.position, Waypoints[nextPoint].position, speed * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.S) && currentPoint != 0)
            {
                disabelPanels();
                if (transform.position != Waypoints[currentPoint].position)
                {
                    if (nextPoint >= currentPoint)
                    {
                        nextPoint = currentPoint;
                    }
                    else
                    {
                        nextPoint = previousPoint;
                    }
                }
                else
                {
                    nextPoint = previousPoint;
                }
                transform.position = Vector3.MoveTowards(transform.position, Waypoints[nextPoint].position, speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (transform.position != Waypoints[currentPoint].position)
                {
                    if (nextPoint >= currentPoint)
                    {
                        nextPoint = currentPoint;
                    }
                    else
                    {
                        nextPoint = previousPoint;
                    }
                }
                else
                {
                    nextPoint = previousPoint;
                }
                transform.position = Vector3.MoveTowards(transform.position, Waypoints[nextPoint].position, speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                pausePanel.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WayPoint")
        {
            WayPoint temp = other.gameObject.GetComponent<WayPoint>();
            if (temp.name != "-")
            {
                currentPoint = int.Parse(temp.name);
                previousPoint = int.Parse(temp.previousPoint.name);
                nextPoint = int.Parse(temp.nextPoint.name);
            }
            panelManager.GetComponent<PanelManager>().panelControl(temp.type, temp.numberInteraction);
        }
    }
}

