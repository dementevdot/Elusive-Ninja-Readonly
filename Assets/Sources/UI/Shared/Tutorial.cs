using Global;
using UnityEngine;

namespace UI.Shared
{
    public class Tutorial : MonoBehaviour
    {
        private void Awake()
        {
            if (PlayerPrefsService.IsNew.Value == false) Destroy(gameObject);
        }
    }
}
