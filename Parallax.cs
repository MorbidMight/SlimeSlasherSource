using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, width, startpos, startposy;
    public GameObject cam;
    public float parallaxEffect;
    public bool vertScroll = false;
    public float vertEffect = 1f;

    // Start is called before the first frame update
    void Start()
    {
        startposy = transform.position.y + 30;
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        width = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
        if (vertScroll)
        {
            float tempy = (cam.transform.position.y * (1 - vertEffect));
            float disty = (cam.transform.position.y * vertEffect);

            transform.position = new Vector3(transform.position.x, startposy + disty, transform.position.z);
            if (tempy > startposy + width)
            {
                startposy += width;
            }
            else if (tempy < startposy - width)
            {
                startposy -= width;
            }
        }



    }


}
