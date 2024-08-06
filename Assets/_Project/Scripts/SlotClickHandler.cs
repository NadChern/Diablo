using UnityEngine;
using UnityEngine.UI;

namespace _Project
{
   public class SlotColorHandler : MonoBehaviour
        {
            [SerializeField] private Image background;

            public void SetNormalColor()
            {
                if (background != null)
                {
                    background.color = new Color(0.6f, 0.3f, 0.1f, 1); // Brown color
                }
            }

            public void SetClickedColor()
            {
                if (background != null)
                {
                    background.color = new Color(0, 1, 0, 0.5f); // Green with transparency
                }
            }
        }
}

