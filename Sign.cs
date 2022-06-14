using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] GameObject textObj;
    [SerializeField] string signText;
    bool started;

    void Start()
    {
        started = false;
        text = textObj.GetComponent<TextMeshProUGUI>();
        text.text = "";
        textObj.SetActive(false);
    }
    public void Activate()
    {
        textObj.SetActive(true);
        if (!started)
        {
            StartCoroutine("PlayText");
        }
    }
    IEnumerator PlayText()
    {
        started = true;
        foreach (char c in signText)
        {
            text.text += c;
            yield return new WaitForSeconds(0.04f);
        }
        started = false;
    }

    public void Deactivate()
    {
        StopCoroutine("PlayText");
        started = false;
        text.text = "";
        textObj.SetActive(false);
    }
}
