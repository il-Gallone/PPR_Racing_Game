using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    public enum barType { HP, NRG };

    public PlayerController targetPlayer;
    public Slider bar;
    public barType type;

    // Start is called before the first frame update
    void Start()
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
        }
    }
}
