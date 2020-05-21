using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*自動ドアプログラム
 * 初めの位置を記録　変数:startPos
 * ２秒待つ 変数waitTime
 * ２秒かけて特定の場所まで閉じる 変数:closeTime,endPos
 * ２秒かけて開く変数:openTime
 * 繰り返す
 */
public class DoorMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float startTime, intervalTime, closeTime, openTime;
    private Vector3 startPos;
    [SerializeField]
    private Vector3 endPos;
    private float nowTime;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.localPosition;
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Move() {
        yield return new WaitForSeconds(startTime);
        while (true) {
            yield return Open();
            yield return new WaitForSeconds(intervalTime);
            yield return Close();
            yield return new WaitForSeconds(intervalTime);
        }
    }

    IEnumerator Open() {
        float diff = 0, rate = 0;
        nowTime = Time.timeSinceLevelLoad;
        while (rate <= 1.0f) {
            diff = Time.timeSinceLevelLoad - nowTime;
            rate = diff / openTime;
            this.transform.localPosition = Vector3.Lerp(startPos, endPos, rate);
            yield return null;
        }
    }
    IEnumerator Close() {
        float diff = 0, rate = 0;
        nowTime = Time.timeSinceLevelLoad;
        while (rate <= 1.0f) {
            diff = Time.timeSinceLevelLoad - nowTime;
            rate = diff / closeTime;
            this.transform.localPosition = Vector3.Lerp(endPos, startPos, rate);
            yield return null;
        }
    }

}
