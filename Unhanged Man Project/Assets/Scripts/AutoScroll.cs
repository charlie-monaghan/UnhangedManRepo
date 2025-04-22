using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    RectTransform content;
    public float scrollSpeed = 20f;
    public float maxHeight = 1000f;
    public float minHeight = -50f;
    bool isScrollingUpwards = true;

    // Update is called once per frame
    void Start()
    {
        content = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isScrollingUpwards == true) //if scrolling up is true
        {
            if (content.anchoredPosition.y <= maxHeight)
            {
                //Debug.Log("Scrolling up!");
                content.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime; // scrolls content up

                if (content.anchoredPosition.y >= maxHeight)
                {
                    isScrollingUpwards = false;

                }
            }
        }
        else { //scrolling up is false
            if (content.anchoredPosition.y >= minHeight)
            {
                //Debug.Log("Scrolling down!");
                content.anchoredPosition -= Vector2.up * scrollSpeed * Time.deltaTime; // scrolls content down

                if (content.anchoredPosition.y <= minHeight)
                {
                    isScrollingUpwards = true;
                }
            }
        }
    }
}
