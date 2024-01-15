using Global;
using UnityEngine;

namespace UI.Menu
{
    public class SkinCellFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _skinCellPrefab;

        private void Awake()
        {
            foreach (var pair in PlayerSkin.Skins)
                Instantiate(_skinCellPrefab, transform).GetComponent<SkinCell>().Init(pair.Key, pair.Value.Cost, pair.Value.Picture);
        }
    }
}
