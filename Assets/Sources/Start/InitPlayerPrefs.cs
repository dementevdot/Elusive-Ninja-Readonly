using System.Collections;
using Global;
using Shared;
using UnityEngine;

namespace Start
{
    public class InitPlayerPrefs : Initializable
    {
        private IEnumerator Start()
        {
            while (PlayerPrefsService.IsNew == null) 
                yield return new WaitForEndOfFrame();

            Inited?.Invoke();
        }
    }
}