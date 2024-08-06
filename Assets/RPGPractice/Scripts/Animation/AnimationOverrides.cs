using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG
{
    public class AnimationOverrides : MonoBehaviour
    {
        [SerializeField] private GameObject character = null;
        [SerializeField] private SO_AnimationType[] soAnimationTypeArray = null;

        private Dictionary<AnimationClip, SO_AnimationType> animationTypeDictionaryByAnimation;
        private Dictionary<string, SO_AnimationType> animationTypeDictionaryByCompositeAttibuteKey;

        private void Start()
        {
            animationTypeDictionaryByAnimation = new Dictionary<AnimationClip, SO_AnimationType>();

            foreach (SO_AnimationType item in soAnimationTypeArray)
            {
                animationTypeDictionaryByAnimation.Add(item.animationClip, item);
            }


            animationTypeDictionaryByCompositeAttibuteKey = new Dictionary<string, SO_AnimationType>();

            foreach (SO_AnimationType item in soAnimationTypeArray)
            {
                string key = item.characterPart.ToString() + item.partVariantColour.ToString() + item.partVariantType.ToString() + item.animationName.ToString();
                animationTypeDictionaryByCompositeAttibuteKey.Add(key, item);
            }

        }



        public void ApplyCharacterCustomStationParameters(List<CharacterAttibute> characterAttibutesList)
        {

            //loop through all character attributes and set  the animation override controller for each
            foreach (CharacterAttibute characterAttibute in characterAttibutesList)
            {
                Animator currentAnimator = null;

                List<KeyValuePair<AnimationClip, AnimationClip>> animsKeyvValuePairList = new List<KeyValuePair<AnimationClip, AnimationClip>>();

                string animationSOAssetName = characterAttibute.characterPart.ToString();

                Animator[] animatorArray = character.GetComponentsInChildren<Animator>();

                foreach (Animator animator in animatorArray)
                {
                    if (animator.name == animationSOAssetName)
                    {
                        currentAnimator = animator;
                        break;
                    }
                }


                //get current animations for animator 
                AnimatorOverrideController aoc = new AnimatorOverrideController(currentAnimator.runtimeAnimatorController);
                List<AnimationClip> animationsList = new List<AnimationClip>(aoc.animationClips);

                foreach (AnimationClip animationClip in animationsList)
                {
                    SO_AnimationType so_AnimationType;
                    bool foundAnimation = animationTypeDictionaryByAnimation.TryGetValue(animationClip, out so_AnimationType);
                    if (foundAnimation)
                    {
                        string key = characterAttibute.characterPart.ToString() + characterAttibute.partVariantColour.ToString()
                            + characterAttibute.partVariantType.ToString() + so_AnimationType.animationName.ToString();

                        SO_AnimationType swapSO_AnimationType;
                        bool foundSwapAnimation = animationTypeDictionaryByCompositeAttibuteKey.TryGetValue(key, out swapSO_AnimationType);

                        if (foundAnimation)
                        {
                            AnimationClip swapAnimationClip = swapSO_AnimationType.animationClip;

                            animsKeyvValuePairList.Add(new KeyValuePair<AnimationClip, AnimationClip>(animationClip,swapAnimationClip));
                        }
                    }
                }

                //Apply
                aoc.ApplyOverrides(animsKeyvValuePairList);
                currentAnimator.runtimeAnimatorController = aoc;
            }
        }



    }
}
