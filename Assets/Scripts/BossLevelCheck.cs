using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelCheck : MonoBehaviour
{
    public int bossThreshold = 10; // how many levels need to be completed before boss unlocks

    // Start is called before the first frame update
    void Start()
    {
        // boss level unlocks after specified number of levels completed
        if (GameManager.instance.stats.levelsCompleted >= bossThreshold)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    public void LoadBossLevel()
    {
        GameManager.instance.levelLimits = 25;
        SceneManager.LoadScene("BossLevel");
    }
}
