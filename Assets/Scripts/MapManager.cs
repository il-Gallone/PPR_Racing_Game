using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static string sceneToLoad;
    public static int targetedFaction;

    public void LoadSceneByName()
    {
        switch (targetedFaction)
        {
            case 1:
                {
                    GameManager.instance.stats.faction1Favour -= 5;
                    GameManager.instance.stats.faction2Favour -= 2;
                    GameManager.instance.stats.faction3Favour += 3;
                    GameManager.instance.stats.faction4Favour += 1;
                    GameManager.instance.stats.faction5Favour += 2;
                    break;
                }
            case 2:
                {
                    GameManager.instance.stats.faction1Favour -= 2;
                    GameManager.instance.stats.faction2Favour -= 5;
                    GameManager.instance.stats.faction3Favour += 1;
                    GameManager.instance.stats.faction4Favour += 3;
                    GameManager.instance.stats.faction5Favour += 2;
                    break;
                }
            case 3:
                {
                    GameManager.instance.stats.faction1Favour += 3;
                    GameManager.instance.stats.faction2Favour += 1;
                    GameManager.instance.stats.faction3Favour -= 5;
                    GameManager.instance.stats.faction4Favour -= 2;
                    GameManager.instance.stats.faction5Favour += 2;
                    break;
                }
            case 4:
                {
                    GameManager.instance.stats.faction1Favour += 1;
                    GameManager.instance.stats.faction2Favour += 3;
                    GameManager.instance.stats.faction3Favour -= 2;
                    GameManager.instance.stats.faction4Favour -= 5;
                    GameManager.instance.stats.faction5Favour += 2;
                    break;
                }
            case 5:
                {
                    GameManager.instance.stats.faction1Favour += 1;
                    GameManager.instance.stats.faction2Favour += 1;
                    GameManager.instance.stats.faction3Favour += 1;
                    GameManager.instance.stats.faction4Favour += 1;
                    GameManager.instance.stats.faction5Favour -= 5;
                    break;
                }
        }
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadSceneByIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
