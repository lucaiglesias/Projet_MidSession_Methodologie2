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
        username.text = GameManager.Instance.characterData.username;
        maxHp.text = "" + GameManager.Instance.characterData.MaxHealth;
        Gold.text = "" + GameManager.Instance.characterData.Gold;
        PowerAttack.text = "" + GameManager.Instance.characterData.PowerAttack;
        MonstersKilled.text = "" + GameManager.Instance.characterData.MonstersKilled;
        GameOver.text = "" + GameManager.Instance.characterData.GameOver;
    }
}
