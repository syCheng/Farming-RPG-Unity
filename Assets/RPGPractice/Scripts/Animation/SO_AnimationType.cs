using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG
{
    [CreateAssetMenu(fileName = "so_AnimationType",menuName = "SO/Animation/Animation Type")]
    public class SO_AnimationType : ScriptableObject
    {
        public AnimationClip animationClip;
        public AnimationName animationName;
        public CharacterPartAnimator characterPart;
        public PartVariantColour partVariantColour;
        public PartVariantType partVariantType;
    }
}
