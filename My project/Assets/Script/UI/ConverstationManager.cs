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

    [Header("文字送り速度")]
    [SerializeField] private float characterForwardingSpeed = 0.0f;

    private bool isConverstationActive = false;

    private void Start()
    {
        converstationUI.SetActive(false);
    }

    private void Update()
    {
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
    }
}
