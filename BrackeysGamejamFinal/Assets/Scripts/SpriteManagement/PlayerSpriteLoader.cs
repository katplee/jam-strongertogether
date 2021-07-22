﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSpriteLoader : MonoBehaviour
{
    #region Animation
    public List<Sprite> Sprites { get; set; }
    private AnimationClip animClip;
    private float animKeyFrameRate = 5;
    private Animator animator;

    private EditorCurveBinding spriteBinding = new EditorCurveBinding();
    #endregion

    #region Avatar/Sprite
    private SpriteRenderer spriteRenderer;
    #endregion

    private void Start()
    {
        //pass the enemy sprite loader to the sprite manager
        SpriteManager.Instance.AssignPlayerSpriteLoader(this);
        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    public void GenerateAnimClip()
    {
        animClip = new AnimationClip();
        animClip.frameRate = 20; // fps

        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "m_Sprite";

        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[Sprites.Count];

        for (int i = 0; i < (Sprites.Count); i++)
        {
            spriteKeyFrames[i] = new ObjectReferenceKeyframe();

            if (i == Sprites.Count - 1)
            {
                spriteKeyFrames[i].time = spriteKeyFrames[i - 1].time + (8 / animClip.frameRate);
            }
            else
            {
                spriteKeyFrames[i].time = (i / animClip.frameRate) * animKeyFrameRate;
            }
            spriteKeyFrames[i].value = Sprites[i];
        }

        AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, spriteKeyFrames);

        AssetDatabase.CreateAsset(animClip, "Assets/Animations/Enemy/PlayerAttackReady.anim");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        SetAnimation();
    }

    private void SetAnimation()
    {
        animator.SetBool("animReady", true);
    }

    public void SetAvatar()
    {
        spriteRenderer.sprite = Sprites[0];
    }

    public void GenerateList()
    {
        Sprites = new List<Sprite>();
    }

    private void SubscribeEvents()
    {
        //SpriteManager.OnTransferComplete += GenerateAnimClip;
        //SpriteManager.OnTransferComplete += SetAvatar;
    }

    private void UnsubscribeEvents()
    {
        //SpriteManager.OnTransferComplete -= GenerateAnimClip;
        //SpriteManager.OnTransferComplete -= SetAvatar;
    }
}


