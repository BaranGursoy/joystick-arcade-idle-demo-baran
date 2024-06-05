using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MineRespawner : MonoBehaviour
{
    [FormerlySerializedAs("mineObject")] [SerializeField] private GameObject mineModelObject;
    [SerializeField] private TextMeshPro respawnerTimerTMP;

    private Mine _mine;

    public void RespawnMine(float respawnTime, Mine mine)
    {
        _mine = mine;
        
        mineModelObject.SetActive(false);
        gameObject.SetActive(true);
        StartCoroutine(RespawnMineCoroutine(respawnTime));
    }

    private IEnumerator RespawnMineCoroutine(float respawnTime)
    {
        float passedTimeForCoroutine = respawnTime;
        while (passedTimeForCoroutine > 0f)
        {
            int timeForText = Mathf.CeilToInt(passedTimeForCoroutine);
            respawnerTimerTMP.text = timeForText.ToString();
            passedTimeForCoroutine -= Time.deltaTime;
            yield return null;
        }
        
        respawnerTimerTMP.text = "0";

        yield return new WaitForSeconds(0.1f);
        
        ResetMineObject();
        gameObject.SetActive(false);
    }

    private void ResetMineObject()
    {
        if(_mine)
        {
            _mine.ResetMine();
        }   
        
        mineModelObject.SetActive(true);
    }
}
