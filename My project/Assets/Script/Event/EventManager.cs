using UnityEngine;

public enum EventType
{
    BattleEvent,    // 戦闘イベント
    NonCombat          // 非戦闘イベント
}

public class EventManager : MonoBehaviour
{
    [SerializeField] private BattleEvent battleEvent;

    public EventType GetRandomEventType()
    {
        int randomIndex=Random.Range(0,System.Enum.GetValues(typeof(EventType)).Length);

        return (EventType)randomIndex;
    }

    public void EventDecision()
    {
        EventType eventType= GetRandomEventType();

        switch (eventType)
        {
            case EventType.BattleEvent:
                Debug.Log("戦闘イベント開始");
                battleEvent.AppearEnemy();
                break;

            case EventType.NonCombat:
                Debug.Log("非戦闘イベント開始");
                break;
        }
    }
}
