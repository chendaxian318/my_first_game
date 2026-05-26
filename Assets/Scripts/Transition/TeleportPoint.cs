using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO sceneToGo;

    public Vector3 PositionToGo;
    public void TriggerAction()
    {
        loadEventSO.RaiseLoadRequestEvent(sceneToGo,PositionToGo,true);
    }
}
