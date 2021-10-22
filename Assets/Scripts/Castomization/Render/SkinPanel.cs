using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diana2.Castomization
{
    public class SkinPanel : MonoBehaviour
    {
        [SerializeField] private SkinListView _skinListView;
        [SerializeField] private SkinDataBase _dataBase;

        public IEnumerable<SkinPresenter> Data { get; private set; }

        public void Render(string skinKey)
        {
            var renderSkins = new List<SkinData>();

            foreach (var data in _dataBase.Data)
                if (skinKey == data.GetSkinKey())
                    renderSkins.Add(data);

            Data = _skinListView.Render(renderSkins);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
