using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfile
{
    public int textureIndex;
    public int hairIndex;
    public int eyebrowsIndex;
    public int eyesIndex;
    public int noseIndex;
    public int mouthIndex;
}

public class CharacterGenerator : MonoBehaviour
{
    public static CharacterGenerator instance;

    private Texture2D[] _bodyTextures;

    [SerializeField]
    private SkinnedMeshRenderer _renderer;
    [SerializeField]
    private MeshRenderer _earsRenderer;
    [SerializeField]
    private GameObject _hairParent;
    [SerializeField]
    private GameObject _eyebrowsParent;
    [SerializeField]
    private GameObject _eyesParent;
    [SerializeField]
    private GameObject _noseParent;
    [SerializeField]
    private GameObject _mouthParent;

    private void Awake()
    {
        this._bodyTextures = Resources.LoadAll<Texture2D>("BodyTextures");

        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            this.GenerateRandomCharacter();
        }
    }

    public void LoadCharacterFromPlayerPrefs()
    {
        int bodyTextureIndex = PlayerPrefs.GetInt("body", -1);
        if (bodyTextureIndex <= 0)
        {
            this.GenerateRandomCharacter();
            return;
        }

        this._renderer.material.mainTexture = this._bodyTextures[bodyTextureIndex];
        this._earsRenderer.material.mainTexture = this._bodyTextures[bodyTextureIndex];

        this.EnableSelectedChild(this._hairParent, PlayerPrefs.GetInt("hair", 0));
        this.EnableSelectedChild(this._eyebrowsParent, PlayerPrefs.GetInt("eyebrows", 0));
        this.EnableSelectedChild(this._eyesParent, PlayerPrefs.GetInt("eyes", 0));
        this.EnableSelectedChild(this._noseParent, PlayerPrefs.GetInt("nose", 0));
        this.EnableSelectedChild(this._mouthParent, PlayerPrefs.GetInt("mouth", 0));
    }

    public void LoadCharacterFromProfile(CharacterProfile profile)
    {
        this._renderer.material.mainTexture = this._bodyTextures[profile.textureIndex];
        this._earsRenderer.material.mainTexture = this._bodyTextures[profile.textureIndex];

        this.EnableSelectedChild(this._hairParent, profile.hairIndex);
        this.EnableSelectedChild(this._eyebrowsParent, profile.eyebrowsIndex);
        this.EnableSelectedChild(this._eyesParent, profile.eyesIndex);
        this.EnableSelectedChild(this._noseParent, profile.noseIndex);
        this.EnableSelectedChild(this._mouthParent, profile.mouthIndex);
    }

    public void GenerateRandomCharacter()
    {
        int bodyIndex = Random.Range(0, this._bodyTextures.Length);
        int hairIndex = Random.Range(0, this._hairParent.transform.childCount);
        int eyebrowsIndex = Random.Range(0, this._eyebrowsParent.transform.childCount);
        int eyesIndex = Random.Range(0, this._eyesParent.transform.childCount);
        int noseIndex = Random.Range(0, this._noseParent.transform.childCount);
        int mouthIndex = Random.Range(0, this._mouthParent.transform.childCount);

        PlayerPrefs.SetInt("body", bodyIndex);
        PlayerPrefs.SetInt("hair", hairIndex);
        PlayerPrefs.SetInt("eyebrows", eyebrowsIndex);
        PlayerPrefs.SetInt("eyes", eyesIndex);
        PlayerPrefs.SetInt("nose", noseIndex);
        PlayerPrefs.SetInt("mouth", mouthIndex);

        this._renderer.material.mainTexture = this._bodyTextures[bodyIndex];
        this._earsRenderer.material.mainTexture = this._bodyTextures[bodyIndex];

        this.EnableSelectedChild(this._hairParent, hairIndex);
        this.EnableSelectedChild(this._eyebrowsParent, eyebrowsIndex);
        this.EnableSelectedChild(this._eyesParent, eyesIndex);
        this.EnableSelectedChild(this._noseParent, noseIndex);
        this.EnableSelectedChild(this._mouthParent, mouthIndex);
    }

    private void EnableSelectedChild(GameObject targetParent, int childIndex)
    {
        //Disable all children first
        for (int i = 0; i < targetParent.transform.childCount; i++)
        {
            targetParent.transform.GetChild(i).gameObject.SetActive(false);
        }
    
        targetParent.transform.GetChild(childIndex).gameObject.SetActive(true);
    }
}
