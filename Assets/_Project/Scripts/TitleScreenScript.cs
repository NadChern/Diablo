using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project
{
    public static class Constants
    {
        public const string GAME_SCENE_NAME = "SampleScene";
    }
    
    public class TitleScreenScript : MonoBehaviour
    {
        public void LoadGameScene()
        {
            SceneManager.LoadScene(Constants.GAME_SCENE_NAME, LoadSceneMode.Single);
        }
    }
}
