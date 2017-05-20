using UnityEngine;

public class ControlSceneGame : MonoBehaviour
{
    public UniversityRunGame game;

    private void Awake()
    {
        // load stuff?
        game.Begin();
    }

    private void Update()
    {

    }
}