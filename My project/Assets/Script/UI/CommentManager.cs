using UnityEngine;

public class CommentManager : MonoBehaviour
{
    [SerializeField] private CommentTable commentTable;

    [SerializeField] private GameObject commentPrefab;

    [SerializeField] private RectTransform parent;

    [SerializeField] private float timer = 0.0f;

    [SerializeField] private float interval = 0.0f;

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            CreateComment();

            timer = 0.0f;
        }
    }

    private void CreateComment()
    {
        int index = Random.Range(0, commentTable.comments.Count);

        CommentData data = commentTable.comments[index];

        GameObject obj = Instantiate(commentPrefab, parent);

        CommentMove move = obj.GetComponent<CommentMove>();

        move.Initialize(data);
    }
}
