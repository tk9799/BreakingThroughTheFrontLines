using System.Collections;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ConverstationEvent
{
    //セリフ
    public string[] sentences;
}

public class ConverstationManager : MonoBehaviour
{
    // テキスト表示
    public TMP_Text converstationText;

    // UI全体
    public GameObject converstationUI;

    // 会話データ
    public ConverstationEvent[] converstationEvents;

    // 現在の会話イベント
    private int currentDialogueIndex = 0;

    // 現在のセリフ
    private int currentSentenceIndex = 0;

    // 文字表示中フラグ
    private bool isTyping = false;

    // 会話を終了するかの判定
    private bool isConverstationFinished = false;

    [Header("文字送り速度")]
    [SerializeField] private float characterForwardingSpeed = 0.0f;

    private bool isConverstationActive = false;

    [Header("会話終了までプレイヤーの移動を禁止するためにアタッチするクラス")]
    [SerializeField] private PlayerMove playerMove = null;

    [Header("会話終了まで敵がスポーンしないようにするためにアタッチするクラス")]
    [SerializeField] private SpawnEnemy spawnEnemy = null;

    private void Start()
    {
        converstationUI.SetActive(false);
    }

    private void Update()
    {
        // 会話がすべて終了している場合
        if (isConverstationFinished)
        {
            gameObject.SetActive(false);

            //処理を終えて何もしない
            //return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (!isConverstationActive)
            {
                StartConverstation();
            }
            else
            {
                NextSentence();
            }
        }
    }

    private void StartConverstation()
    {
        isConverstationActive = true;
        converstationUI.SetActive(true);

        // プレイヤーの移動を禁止
        playerMove.enabled = false;

        // 敵のスポーンを禁止
        spawnEnemy.enabled = false;

        currentDialogueIndex = 0;
        currentSentenceIndex = 0;

        StartCoroutine(TypeSentence(converstationEvents[currentDialogueIndex].
            sentences[currentSentenceIndex]));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        converstationText.text = "";
        // **\n を正しく改行に変換** 
        sentence = sentence.Replace("\\n", "\n");
        foreach (char letter in sentence.ToCharArray())
        {
            converstationText.text += letter;

            // 文字送り速度 
            yield return new WaitForSeconds(characterForwardingSpeed);
        }
        isTyping = false;
    }
    private void NextSentence() 
    {
        if (isTyping) 
        {
            StopAllCoroutines();
            converstationText.text = converstationEvents[currentDialogueIndex].
                sentences[currentSentenceIndex].Replace("\\n", "\n"); 
            isTyping = false; 
        }
        else 
        {
            currentSentenceIndex++; 
            if (currentSentenceIndex < converstationEvents[currentDialogueIndex].
                sentences.Length) 
            {
                StartCoroutine(TypeSentence(converstationEvents[currentDialogueIndex].
                    sentences[currentSentenceIndex])); 
            }
            else 
            { 
                currentSentenceIndex = 0;
                currentDialogueIndex++; 
                if (currentDialogueIndex < converstationEvents.Length) 
                {
                    StartCoroutine(TypeSentence(converstationEvents[currentDialogueIndex].
                        sentences[currentSentenceIndex])); 
                }
                else 
                {
                    CloseConvert();
                }
            }
        }
    }

    void CloseConvert() 
    {
        converstationUI.SetActive(false);
        isConverstationActive = false;

        // プレイヤーの操作ができるようにする
        playerMove.enabled = true;

        // 敵のスポーン開始
        spawnEnemy.enabled = true;

        if(playerMove.enabled&&spawnEnemy.enabled)
        {
            // すべての会話が終了した
            isConverstationFinished = true;
        }
    }
}
