using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class RagdollPush : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Rigidbody> _ragdollRigidbodies;
    [SerializeField] private float _force;

    private void Awake()
    {
        Activate();
    }

    private async void Activate()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        _animator.enabled = false;
        _ragdollRigidbodies.ForEach(rb => rb.isKinematic = false);
        AddForce();
    }

    private async void AddForce()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));

        var index = Random.Range(0, _ragdollRigidbodies.Count);
        _ragdollRigidbodies[index].AddForce(Vector3.up * _force, ForceMode.Impulse);
        AddForce();
    }
}