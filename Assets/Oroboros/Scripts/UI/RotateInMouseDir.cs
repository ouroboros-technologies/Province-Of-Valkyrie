using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RotateInMouseDir : MonoBehaviour
{
    Vector3 mouseDirection;
    Vector3 mouseAxis;
    Vector3 prevMouseAxis;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        mouseAxis = new Vector3(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"), 0);
        prevMouseAxis = mouseAxis;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseAxis = new Vector2(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        mouseAxis.Normalize();
        Color temp = image.color;
        if (float.IsNaN(Mathf.Atan(mouseAxis.y / mouseAxis.x)) || mouseAxis.magnitude < 0.9f)
        {
            temp.a = 0f;
            image.color = temp;
        }
        else
        {
            temp.a = 1f;
            image.color = temp;
            Vector3 rotation = new Vector3(0, 0, (Mathf.Rad2Deg * Mathf.Atan2(mouseAxis.y, mouseAxis.x)) - 270);
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
