using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPotion : MonoBehaviour
{
    public AudioClip spawnPotionSound;
    public List<GameObject> listOfPotions;

    public void SpawnSpecificPotion(string potionID)
    {
        for (int i = 0; i < listOfPotions.Count; i++)
        {
            if (potionID == listOfPotions[i].GetComponent<Potion>()._potionID)
            {
                Instantiate(listOfPotions[i], transform.position, transform.rotation);
                TempAudioManager.instance.PlaySoundEffect(spawnPotionSound);
            }
        }
    }
}
