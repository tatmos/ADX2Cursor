using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADX2CursorLoader : MonoBehaviour
{

    public string searchPath = "";

    public bool makeCueObject = true;
    public bool makeCursorObject = true;

    #region MyCueInfo
    public class MyCueInfo
    {
        public string name = "dummyCueSheet";
        public int id = 0;
        public string comment = "";
    }
    #endregion

    #region MyAcbInfo
    public class MyAcbInfo
    {
        public string name = "dummyCueSheet";
        public int id = 0;
        public string comment = "";
        public string acbPath = "dummyCueSheet.acb";
        public string awbPath = "dummyCueSheet_streamfiles.awb";
        public string atomGuid = "";
        public Dictionary<int, MyCueInfo> cueInfoList = new Dictionary<int, MyCueInfo>();

        public MyAcbInfo(string n, int inId, string com, string inAcbPath, string inAwbPath, string _guid)
        {
            this.name = n;
            this.id = inId;
            this.comment = com;
            this.acbPath = inAcbPath;
            this.awbPath = inAwbPath;
            this.atomGuid = _guid;
        }
    };
    #endregion

    MyAcbInfo myAcbInfo;
    List<MyAcbInfo> myAcbInfoList = new List<MyAcbInfo>();

    // Use this for initialization
    void Start()
    {
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetUp()
    {
        //  CRI Atom作成
        var criAtom = new GameObject("CRIAtom");
        criAtom.AddComponent<CriAtom>();

        //  ACFロード
        searchPath = Application.streamingAssetsPath;
        string acfPath = GetAcfPath();
        Debug.Log("Load ACF \"" + acfPath + "\"");
        CriAtomEx.RegisterAcf(null, acfPath);

        //  エフェクト系のため
        Debug.Log("AttachDspBusSetting \"" + "DspBusSetting_0" + "\"");
        CriAtom.AttachDspBusSetting("DspBusSetting_0");


        //  キューのオブジェクト作成
        GetAcbInfoList(false, searchPath);
        float x = 0;
        float z = 0;
        int acbNo = 0;
        foreach (MyAcbInfo acbInfo in myAcbInfoList)
        {
            //  再生のためキューシートロード
            CriAtom.AddCueSheet(acbInfo.name, acbInfo.acbPath, acbInfo.awbPath);

            if (makeCueObject)
            {
                int itemCount = 0;
                float y = 0;
                x = 0;
                z = 0;
                foreach (KeyValuePair<int, MyCueInfo> pair in acbInfo.cueInfoList)
                {
                    //Debug.Log (acbInfo.name + " " + pair.Key + " : " + pair.Value.name);

                    //  cube
                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    cube.transform.position = new Vector3(x + acbNo * 1.0f + this.gameObject.transform.localPosition.x,
                        y + this.gameObject.transform.localPosition.y,
                        z + this.gameObject.transform.localPosition.z);

                    Rigidbody rb = cube.AddComponent<Rigidbody>();
                    rb.useGravity = false;
                    cube.transform.parent = this.gameObject.transform;
                    ADX2PlayColor adx2PlayColor = cube.AddComponent<ADX2PlayColor>();
                    adx2PlayColor.SetColors(Color.white, Color.red);

                    if (acbNo % 2 == 1)
                    { 
                       adx2PlayColor.SetColors(Color.white, Color.blue);
                    }

                    //  textMesh
                    //GameObject go = new GameObject(pair.Value.name);
                    //go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    //go.transform.localPosition = new Vector3(0.0f, -0.1f, -0.1f);
                    //TextMesh tm = go.AddComponent<TextMesh>();
                    //tm.fontSize = 10;
                    //tm.text = /*acbinfo.name + " " + pair.key + " : " + */ pair.Value.name;
                    //go.transform.parent = cube.transform;


                    //  ADX2CursorInstCube
                    ADX2CursorInstCube adx2CursorInstTouch = cube.AddComponent<ADX2CursorInstCube>();

                    adx2CursorInstTouch.SetCue(acbInfo.name, pair.Value.name);

                    x += 0.12f;

                    itemCount++;
                    if (itemCount % 6 == 0)
                    {
                        x = 0;
                        y += 0.12f;
                        z += 0.12f/8f;
                    }
                }
                acbNo++;
            }
        }

        if (makeCursorObject)
        {
            //  ADX2カーソル作成
            var cursor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            cursor.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            cursor.name = "ADX2Cursor";
            var col = cursor.GetComponent<SphereCollider>();
            col.isTrigger = true;
            cursor.AddComponent<ADX2CursorColliderPlayStop>();
        }
    }

    #region kaiseki
    public void GetAcbInfoList(bool foreceReload, string searchPath)
    {
        int acbIndex = 0;
        {

            //if (UnityEditor.EditorApplication.isPlaying)
            {
                GetAcbInfoListCore(searchPath, ref acbIndex);
            }
        }
    }

    private void GetAcbInfoListCore(string searchPath, ref int acbIndex)
    {
        string[] files = System.IO.Directory.GetFiles(searchPath);
        foreach (string file in files)
        {
            if (System.IO.Path.GetExtension(file.Replace("\\", "/")) == ".acb")
            {

                MyAcbInfo myAcbInfo = new MyAcbInfo(System.IO.Path.GetFileNameWithoutExtension(file),
                                        acbIndex, "", System.IO.Path.GetFileName(file), "", "");

                /* 指定したACBファイル名(キューシート名)を指定してキュー情報を取得 */
                CriAtomExAcb acb = CriAtomExAcb.LoadAcbFile(null, file.Replace("\\", "/"), "");

                if (acb != null)
                {
                    /* キュー名リストの作成 */
                    CriAtomEx.CueInfo[] cueInfoList = acb.GetCueInfoList();
                    foreach (CriAtomEx.CueInfo cueInfo in cueInfoList)
                    {
                        MyCueInfo tmpCueInfo = new MyCueInfo();
                        tmpCueInfo.name = cueInfo.name;
                        tmpCueInfo.id = cueInfo.id;
                        tmpCueInfo.comment = cueInfo.userData;

                        if (myAcbInfo.cueInfoList.ContainsKey(cueInfo.id) == false)
                        {
                            myAcbInfo.cueInfoList.Add(cueInfo.id, tmpCueInfo);
                        }
                        else
                        {
                            //  inGame時のサブシーケンスの場合あり
                            //Debug.Log("already exists in the dictionay id:" + cueInfo.id.ToString() +"name:" + cueInfo.name);
                        }
                    }

                    acb.Dispose();
                }
                else
                {
                    Debug.Log("GetAcbInfoList LoadAcbFile. acb is null. " + file);
                }

                myAcbInfoList.Add(myAcbInfo);
                acbIndex++;
            }
        }

        //  directory
        string[] directories = System.IO.Directory.GetDirectories(searchPath);
        foreach (string directory in directories)
        {
            GetAcbInfoListCore(directory, ref acbIndex);
        }
    }

    string GetAcfPath()
    {
        string[] files = System.IO.Directory.GetFiles(searchPath);
        foreach (string file in files)
        {
            if (System.IO.Path.GetExtension(file.Replace("\\", "/")) == ".acf")
            {
                return file;
            }
        }
        return "";
    }
    #endregion
}
