using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadANewLevel(buildIndex:"Level_1");
        }
        
    }

    public void LoadANewLevel(string buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
