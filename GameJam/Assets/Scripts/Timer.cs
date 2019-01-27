using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int hh, mm, ss;
    private string hhs, mms, sss;
    public bool ShowTime = false;

    void Update() {
        // счёт от 0 до 59 секунд
        if (Mathf.RoundToInt(Time.timeSinceLevelLoad) < 60) {
            ss = Mathf.RoundToInt(Time.timeSinceLevelLoad);   //время в секундах типа int
            hhs = "00";
            mms = "00";
            sss = ss.ToString();
            if (ss < 10)                                      // если секунд меньще 10 - добавим leading zero
                sss = "0" + ss.ToString();
        }

        // счёт от 1 до 59 минут
        if (Mathf.RoundToInt(Time.timeSinceLevelLoad) > 60 && Mathf.RoundToInt(Time.timeSinceLevelLoad) < 3600) {
            hhs = "00";
            mm = (Mathf.RoundToInt(Time.timeSinceLevelLoad) / 60);
            ss = (Mathf.RoundToInt(Time.timeSinceLevelLoad) % 60);
            mms = mm.ToString();
            sss = ss.ToString();
            if (mm < 10)                                      // если минут меньще 10 - добавим leading zero
                mms = "0" + mm.ToString();
            if (ss < 10)                                      // если секунд меньще 10 - добавим leading zero
                sss = "0" + ss.ToString();
        }

        // счёт более часа
        if (Mathf.RoundToInt(Time.timeSinceLevelLoad) > 3600) {
            hh = (Mathf.RoundToInt(Time.timeSinceLevelLoad) / 3600); ;
            mm = (Mathf.RoundToInt(Time.timeSinceLevelLoad) % 3600 / 60);
            ss = (Mathf.RoundToInt(Time.timeSinceLevelLoad) % 3600 % 60);
            hhs = hh.ToString();
            mms = mm.ToString();
            sss = ss.ToString();
            if (hh < 10)                                      // если часов меньще 10 - добавим leading zero
                hhs = "0" + hh.ToString();
            if (mm < 10)                                      // если минут меньще 10 - добавим leading zero
                mms = "0" + mm.ToString();
            if (ss < 10)                                      // если секунд меньще 10 - добавим leading zero
                sss = "0" + ss.ToString();
        }
        //GetComponent<GUIText>().text = hhs + ":" + mms + ":" + sss;
        GetComponent<TextMeshProUGUI>().text = mms + ":" + sss;
    }
}
