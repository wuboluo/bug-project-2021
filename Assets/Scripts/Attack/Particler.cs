using UnityEngine;

public class Particler : MonoBehaviour
{
    public ParticleSystem par;

    private void Start()
    {
        var particle = GetComponent<ParticleSystem>();
        var mainModule = particle.main;
        mainModule.loop = false;
        mainModule.stopAction = ParticleSystemStopAction.Callback;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) transform.localScale = par.transform.localScale;
    }

    public void OnParticleSystemStopped()
    {
        Debug.Log("粒子停止");
    }
}