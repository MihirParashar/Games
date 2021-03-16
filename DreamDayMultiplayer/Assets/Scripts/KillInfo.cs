using UnityEngine;

public class KillInfo
{
    public Player playerKiller;
    public Player playerThatDied;

    //Creating a constructor for this class.
    public KillInfo(Player _playerKiller, Player _playerThatDied) {
        playerKiller = _playerKiller;
        playerThatDied = _playerThatDied;
    }
}
