using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [Header("아이템 설정")]
    public ItemType itemType;      // 어떤 효과인지
    public float duration = 5f;    // 지속시간 (초)
    public float value = 1.5f;     // 배율 (속도/점프용) → 무적은 무시됨

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))   // Player 태그 필수!
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                switch (itemType)
                {
                    case ItemType.Invincible:
                        player.Invincibility(duration);
                        break;

                    case ItemType.SpeedBoost:
                        player.SpeedBoost(value, duration);
                        break;

                    case ItemType.JumpBoost:
                        player.JumpBoost(value, duration);
                        break;
                }

                // 효과음, 파티클 등 추가하고 싶으면 여기서 호출
                Destroy(gameObject);   // 아이템 사라짐
            }
        }
    }
}