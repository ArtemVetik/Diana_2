using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diana2.Castomization
{
    public class SkinListView : MonoBehaviour
    {
        [SerializeField] private SkinPresenter _template;
        [SerializeField] private Transform _container;

        public IEnumerable<SkinPresenter> Render(IEnumerable<SkinData> datas)
        {
            var instSkins = new List<SkinPresenter>();

            foreach (var data in datas)
            {
                var inst = Instantiate(_template, _container);
                inst.Render(data);

                instSkins.Add(inst);
            }

            return instSkins;
        }
    }
}