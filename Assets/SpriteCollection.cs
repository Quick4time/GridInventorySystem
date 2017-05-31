using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCollection : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    public Sprite GetSprite(int spriteIndex)
    {
        if (spriteIndex >= 0 && spriteIndex < sprites.Count)
        {
            return sprites[spriteIndex];
        }
        return null;
    }
    public Sprite GetSprite(string spriteName)
    {
        foreach(Sprite sprite in sprites)
        {
            if (sprite.name == spriteName)
            {
                return sprite;
            }
        }
        return null;
    }

}
