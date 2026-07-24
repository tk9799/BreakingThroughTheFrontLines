using UnityEngine;

public class EventUIManager : MonoBehaviour
{
    public static EventUIManager Instance;

    [SerializeField] private GameObject panel;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenPanel(NonCombatEventData data)
    {
        panel.SetActive(true);

        if (data.eventType == NonCombatEventKinds.RandomStatusReinforcemont)
        {

        }
        else
        {

        }
    }
}
