using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG
{
    [Serializable]
    public struct CharacterAttibute
    {
        public CharacterPartAnimator characterPart;
        public PartVariantColour partVariantColour;
        public PartVariantType partVariantType;

        public CharacterAttibute(CharacterPartAnimator characterPart, PartVariantColour partVariantColour, PartVariantType partVariantType)
        {
            this.characterPart = characterPart;
            this.partVariantColour = partVariantColour;
            this.partVariantType = partVariantType;
        }

    }
}
