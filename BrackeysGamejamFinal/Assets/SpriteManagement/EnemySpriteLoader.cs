using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpriteLoader : MonoBehaviour
{
    public Sprite[] sprites;
    private AnimationClip animClip;

    private EditorCurveBinding spriteBinding = new EditorCurveBinding();

    private void Start()
    {
        animClip = new AnimationClip();
        animClip.frameRate = 20; // fps

        spriteBinding.type = typeof(SpriteRenderer);
        //spriteBinding.path = "";
        //spriteBinding.propertyName = "m_Sprite";

        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[sprites.Length];

        for (int i = 0; i < (sprites.Length); i++)
        {
            spriteKeyFrames[i] = new ObjectReferenceKeyframe();
            spriteKeyFrames[i].time = i / animClip.frameRate;
            spriteKeyFrames[i].value = sprites[i];
        }

        AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, spriteKeyFrames);

        AssetDatabase.CreateAsset(animClip, "Assets/Animations/Enemy/Sample.anim");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}


