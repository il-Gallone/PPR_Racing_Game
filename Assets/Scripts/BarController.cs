using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    public enum barType { HP, NRG, SHLD };

    public PlayerController targetPlayer;
    public Slider bar;
    public barType type;

    public float delay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetMaxValues", delay);
    }

    void SetMaxValues()
    {
        switch (type)
        {
            case barType.HP:
                {
                    bar.maxValue = targetPlayer.maxHP;
                    break;
                }
            case barType.NRG:
                {
                    bar.maxValue = targetPlayer.maxEnergy;
                    break;
                }
            case barType.SHLD:
                {
                    if (GameManager.instance.stats.currentModuleID == "Shield Generator")
                    {
                        bar.maxValue = 20;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case barType.HP:
                {
                    bar.value = targetPlayer.HP;
                    break;
                }
            case barType.NRG:
                {
                    bar.value = targetPlayer.energy;
                    break;
                }
            case barType.SHLD:
                {
                    if (GameManager.instance.stats.currentModuleID == "Shield Generator")
                    {
                        bar.value = targetPlayer.moduleResource;
                    }
                    break;
                }
        }
    }
}
