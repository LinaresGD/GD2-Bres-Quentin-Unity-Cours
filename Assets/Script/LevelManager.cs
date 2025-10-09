using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadANewLevel(buildIndex:1);
        }
        
    }

    public void LoadANewLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
