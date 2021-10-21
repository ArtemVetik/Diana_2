using System;

namespace Diana2.Castomization
{
    public static class SkinDataExtention
    {
        public static string GetSkinKey(this SkinData data)
        {
            return data.Name.Split('/')[0];
        }
    }
}