using UnityEngine;

public class CommentManager : MonoBehaviour
{
    [SerializeField] private CommentTable commentTable;

    [SerializeField] private GameObject commentPrefab;

    [SerializeField] private RectTransform[] lanePoints;

    [SerializeField] private RectTransform commentParent;

    private float timer = 0.0f;

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
        // コメントをランダムに選ぶ
        int index = Random.Range(0, commentTable.comments.Count);

        // コメントを流す座標をランダムに選ぶ
        int laneIndex=Random.Range(0, lanePoints.Length);

        // 流す座標を取得
        RectTransform lane = lanePoints[laneIndex];

        // 表示するコメントを取得
        CommentData data = commentTable.comments[index];

        // コメントオブジェクト取得
        GameObject obj = Instantiate(commentPrefab, lane);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;

        CommentMove move = obj.GetComponent<CommentMove>();

        // コメントを動かす処理を呼び出す
        move.Initialize(data);
    }
}
