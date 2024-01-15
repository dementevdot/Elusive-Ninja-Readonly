using System.Collections;
using Agava.YandexGames;
using Shared;

namespace Start
{
    public class InitYandexSDK : Initializable
    {
        private IEnumerator Start()
        {
            #if !UNITY_EDITOR
            yield return YandexGamesSdk.Initialize(() => Inited?.Invoke());
            yield break;
            #endif

            Inited?.Invoke();
            yield return null;
        }
    }
}