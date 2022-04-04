using UnityEngine;
using UnityEditor;

public class StandardInterpolatedGUI : ShaderGUI
{
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        MaterialProperty albedoA = FindProperty("_MainTexA", properties);
        MaterialProperty colorA = FindProperty("_ColorA", properties);
        MaterialProperty albedoB = FindProperty("_MainTexB", properties);
        MaterialProperty colorB = FindProperty("_ColorB", properties);
        MaterialProperty burn = FindProperty("_BurnMap", properties);
        MaterialProperty burnFactor = FindProperty("_BurnFactor", properties);
        materialEditor.TexturePropertySingleLine(new GUIContent(albedoA.displayName), albedoA, colorA);
        materialEditor.TexturePropertySingleLine(new GUIContent(albedoB.displayName), albedoB, colorB);
        materialEditor.TexturePropertySingleLine(new GUIContent(burn.displayName), burn, burnFactor);

        EditorGUILayout.LabelField("");
        MaterialProperty metallic = FindProperty("_MetallicMap", properties);
        MaterialProperty factorA = FindProperty("_MetallicA", properties);
        MaterialProperty factorB = FindProperty("_MetallicB", properties);
        MaterialProperty smoothnessA = FindProperty("_GlossinessA", properties);
        MaterialProperty smoothnessB = FindProperty("_GlossinessB", properties);
        materialEditor.TexturePropertySingleLine(new GUIContent(metallic.displayName), metallic);
        materialEditor.RangeProperty(factorA, "FactorA");
        materialEditor.RangeProperty(factorB, "FactorB");
        materialEditor.RangeProperty(smoothnessA, "SmoothnessA");
        materialEditor.RangeProperty(smoothnessB, "SmoothnessB");

        EditorGUILayout.LabelField("");
        MaterialProperty normal = FindProperty("_BumpMap", properties);
        MaterialProperty scaleA = FindProperty("_BumpScaleA", properties);
        MaterialProperty scaleB = FindProperty("_BumpScaleB", properties);
        materialEditor.TexturePropertySingleLine(new GUIContent(normal.displayName), normal);
        materialEditor.FloatProperty(scaleA, "ScaleA");
        materialEditor.FloatProperty(scaleB, "ScaleB");

        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("");
        MaterialProperty position = FindProperty("_Position", properties);
        materialEditor.RangeProperty(position, "Position");
    }
}
