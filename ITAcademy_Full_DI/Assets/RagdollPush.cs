using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

public class RagdollPush : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Rigidbody> _ragdollRigidbodies;

    [SerializeField] private float _force;

    //[SerializeField] private GameObject _prefab; BRUTE FORCE
    [SerializeField] private AssetReference _reference;

    private void Awake()
    {
        AddPrefab();
        Activate();
    }

    private async void AddPrefab()
    {
        //Instantiate(_prefab); BRUTE FORCE

        //var obj = Resources.Load<GameObject>("Prefab"); RESOURCES
        //var gameObj = Instantiate(obj);
        
        var handle = await Addressables.LoadAssetAsync<GameObject>(_reference).Task;
        var gameObj = Instantiate(handle);

        await Task.Delay(TimeSpan.FromSeconds(2));

        var path = Application.streamingAssetsPath;
        var pathToImage = Path.Combine(path, "test.png");
        var bytes = File.ReadAllBytes(pathToImage);
        var materialMainTexture = new Texture2D(300, 300);
        materialMainTexture.LoadImage(bytes);
        gameObj.GetComponent<MeshRenderer>().material.mainTexture = materialMainTexture;
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