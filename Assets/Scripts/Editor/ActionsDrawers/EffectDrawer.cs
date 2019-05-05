using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDrawer {
    public virtual void Draw(ref BaseEffect pBaseEffect)
    {
        // write the current effect ability draw ui here in derivateds class
    }
    public static void DrawTypeList(ref BaseEffect pBaseEffect, BaseEffect.EFFECT_TYPE pEffectType)
    {
        switch (pEffectType)
        {
            case BaseEffect.EFFECT_TYPE.BLOCK:
               BlockEffectDrawer tBlockEffectDrawer = new BlockEffectDrawer();

                tBlockEffectDrawer.Draw(ref pBaseEffect);
                break;
            case BaseEffect.EFFECT_TYPE.CROSS:
                CrossEffectDrawer tCrossEffectDrawer = new CrossEffectDrawer();

                tCrossEffectDrawer.Draw(ref pBaseEffect);
                break;
            case BaseEffect.EFFECT_TYPE.DESTROY_BY_TYPE:
                DestroyByTypeEffectDrawer tDestroyByTypeEffectDrawer = new DestroyByTypeEffectDrawer();

                tDestroyByTypeEffectDrawer.Draw(ref pBaseEffect);
                break;
        }
    }
}
