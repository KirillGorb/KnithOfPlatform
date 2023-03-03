using UnityEngine;
using UnityEngine.Events;

public class TriggerControlerItem : MonoBehaviour
{
    [SerializeField] private UnityEvent EventOnTrggerEnterPlayer;//событие подборки
    [SerializeField] private UnityEvent EventOnTrggerEnter;//событие подборки

    public void DestroyObject() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out TriggerControler player))
            EventOnTrggerEnterPlayer?.Invoke();
        EventOnTrggerEnter?.Invoke();
    }
}