using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "CreatePlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    public int hp;

    public int SetHp(int hp) {
        if (hp <= 0) hp = 0;
        this.hp = hp;
        return this.hp;
    }

    public int GetHp() {
        return this.hp;
    }

    public void Reset() {
        this.hp = 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
