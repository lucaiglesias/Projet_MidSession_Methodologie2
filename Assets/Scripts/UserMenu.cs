using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserMenu : MonoBehaviour
{

    [SerializeField] private TMP_Text username;
    [SerializeField] private TMP_Text maxHp;
    [SerializeField] private TMP_Text Gold;
    [SerializeField] private TMP_Text PowerAttack;
    [SerializeField] private TMP_Text MonstersKilled;
    [SerializeField] private TMP_Text GameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        username.text = GameManager.Instance.userData.username;
        maxHp.text = ""+ GameManager.Instance.userData.MaxHealth;
        Gold.text = "" + GameManager.Instance.userData.Gold;
        PowerAttack.text = "" + GameManager.Instance.userData.PowerAttack;
        MonstersKilled.text = "" + GameManager.Instance.userData.MonstersKilled;
        GameOver.text = ""+ GameManager.Instance.userData.GameOver;
    }
}
