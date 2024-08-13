using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project
{
    public class TitleScreenScript : MonoBehaviour
    {
        public void LoadGameScene()
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    }
}
