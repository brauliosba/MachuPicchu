using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject warningPanel;
    public GameObject textPanel;
    public Text text;
    public GameObject imagePanel;
    public Image image;
    public GameObject questionPanel;
    public List<Sprite> sprites = new List<Sprite>();
    public Button afirmacion;
    public Button negacion;
    private AudioSource audioSource;
    private bool previousResponse;
    private int cont = 0;

    public void panelControl(string type, Vector3 position)
    {
        switch (type)
        {
            case "sound":
                audioSource = GetComponent<AudioSource>();
                audioSource.Play();
                break;
            case "text":
                textPanel.transform.position = position + new Vector3(0, 2f, 13.0f);
                textPanel.SetActive(true);
                break;
            case "image":
                if (cont >= sprites.Count)
                {
                    cont = 0;
                }
                image.sprite = sprites[cont];
                imagePanel.transform.position = position + new Vector3(0, 1.8f, 13.0f);
                imagePanel.SetActive(true);
                cont++;
                break;
            case "question":
                questionPanel.transform.position = position + new Vector3(0, 4f, 10.0f);
                questionPanel.SetActive(true);
                break;
            case "response":
                if (previousResponse)
                {
                    text.text = "Nos alegramos que así sea.";
                }
                else
                {
                    text.text = "Por favor comantanos como podemos mejorar.";
                }
                textPanel.transform.position = position + new Vector3(0, 3f, 15.0f);
                textPanel.SetActive(true);
                break;
            case "warning":
                warningPanel.transform.position = position + new Vector3(0, 1.5f, 3.0f);
                warningPanel.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void HandleAnswer(bool response)
    {
        previousResponse = response;
        questionPanel.SetActive(false);
    }
}
