using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent onNewSubject;
    public float lifeforce;
    public float lifeforceDefault;
    public float lifeforceMin;
    public float lifeforceMax;
    public float lifeforceInc;
    

    private void Start()
    {
        lifeforce = lifeforceDefault;
    }

    public void SubjectArrest()
    {
        // TODO
        Debug.Log("Subject arrested");
        NewSubject();
    }

    public void SubjectFree()
    {
        // TODO
        Debug.Log("Subject freed");
        NewSubject();
    }

    public void NewSubject()
    {
        Debug.Log("New subject");
        onNewSubject.Invoke();
    }

    public void SetLifeForce(float newLifeForce)
    {
        lifeforce = Mathf.Clamp(newLifeForce, lifeforceMin, lifeforceMax);
    }
}
