// Cristian Pop - https://boxophobic.com/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Boxophobic.Utils
{
    public partial class BoxophobicUtils
    {
        public static class ShaderUtils
        {
            public static void SetKeywordsByInts(Material material, List<string> features)
            {
                for (int i = 0; i < features.Count; i++)
                {
                    if (material.GetInt(features[i]) == 0)
                    {
                        material.DisableKeyword(features[i] + "_ON");
                    }
                    else
                    {
                        material.EnableKeyword(features[i] + "_ON");
                    }

                   //Debug.Log(features[i] + " " + material.GetInt(features[i]));
                }
            }

            public static List<string> GetShaderFeatures(Material material)
            {
                List<string> features = new List<string>();

                var propsCount = ShaderUtil.GetPropertyCount(material.shader);

                for (int i = 0; i < propsCount; i++)
                {
                    var propName = ShaderUtil.GetPropertyName(material.shader, i);

                    if (propName.ToUpper() == propName)
                    {
                        if (features.Contains(propName) == false)
                        {
                            features.Add(propName);
                            //Debug.Log(propName);
                        }
                    }
                }

                return features;
            }
        }
    }    
}
