using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpKeys", menuName = "PowerUpKeys")]
public class PowerUpKeys : ScriptableObject
{
    [SerializeField] private KeyCode speedUp;
    public KeyCode SpeedUp => speedUp;

    [SerializeField] private KeyCode protectFromCollision;
    public KeyCode ProtectFromCollision => protectFromCollision;


    [SerializeField] private KeyCode moveThrowWalls;
    public KeyCode MoveThrowWalls => moveThrowWalls;

    [SerializeField] private KeyCode smallerTheOtherPlayer;
    public KeyCode SmallerTheOtherPlayer => smallerTheOtherPlayer;
}
