using TMPro;
using UnityEngine;

public class CommentMove : MonoBehaviour
{
    private RectTransform rectTransform;

    private TMP_Text text;

    private float speed;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<TMP_Text>();
    }

    public void Initialize(CommentData data)
    {
        text.text = data.comment;

        speed = Random.Range(data.minSpeed, data.maxSpeed);
    }

    // Update is called once per frame
    private void Update()
    {
        rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;
    }
}
