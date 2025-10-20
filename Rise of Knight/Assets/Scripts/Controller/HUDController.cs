using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private Player player;
    private Spider spider;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image spiderHealthBar;
    [SerializeField] private Text currentGoldText;

    void Start()
    {
        player = FindObjectOfType<Player>();
        spider = FindObjectOfType<Spider>();
        healthBar.fillAmount = player.MaxHealth;
        spiderHealthBar.fillAmount = spider.MaxHealth;
    }

    void Update()
    {
        healthBar.fillAmount = player.Health;
        spiderHealthBar.fillAmount = spider.Health;
        currentGoldText.text = "x" + player.CurrentGold.ToString();
    }
}
