using UnityEngine;

public class SpawnHelpPlatform : MonoBehaviour
{
    [SerializeField] private GameObject _platforma;
    [SerializeField] private Transform _spawnPlatformPoint;

    [SerializeField] private float _startScale = 0.34f;
    [SerializeField] private float _maxScale = 3;

    private float _scaleUpPlatformInSecond = 0.34f;

    private GameObject _pref;

    private void Start()
    {
        SetStartParametr();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) SpawnPlatform();
        if (Input.GetMouseButton(0)) ScaleUpPlatform();
    }

    private void SetStartParametr()
    {
        _scaleUpPlatformInSecond = _startScale;
    }

    private void SpawnPlatform()
    {
        _pref = Instantiate(_platforma, _spawnPlatformPoint.position, Quaternion.Euler(0, 0, 90));
        SetStartParametr();
    }

    private void ScaleUpPlatform()
    {
        if (_scaleUpPlatformInSecond <= _maxScale)
            _scaleUpPlatformInSecond += Time.deltaTime;

        _pref.transform.localScale = new Vector3(_scaleUpPlatformInSecond, 0.31f, 1f);
    }
}
