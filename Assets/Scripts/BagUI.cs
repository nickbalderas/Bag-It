using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagUI : MonoBehaviour
{
    public TextMeshProUGUI capacity;
    public TextMeshProUGUI lastItemMass;
    public TextMeshProUGUI numberOfItems;
    public TextMeshProUGUI itemValue;
    public TextMeshProUGUI bagMultiplier;
    public TextMeshProUGUI bagPenalty;
    public TextMeshProUGUI bagScore;
    public GameObject notification;
    public Image notificationBackground;
    public TextMeshProUGUI notificationText;

    private bool _isNotificationOn;
    private float _notificationDuration;

    private void Update()
    {
        if (_isNotificationOn && _notificationDuration > 0)
        {
            _notificationDuration -= Time.deltaTime;
        }
        else
        {
            notification.SetActive(false);
            _isNotificationOn = false;
        }
    }

    public void HandleNotification(Color color, string message, float duration)
    {
        notification.SetActive(true);
        _isNotificationOn = true;
        _notificationDuration = duration;
        notificationBackground.color = color;
        notificationText.text = message;
    }
}
