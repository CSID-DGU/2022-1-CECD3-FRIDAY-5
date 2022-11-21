using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Backend : MonoBehaviour
{
    public static Backend i;
    enum sub {
        member_create,
        member_read,
        member_update,
        member_delete,
        diary_create,
        diary_read,
        diary_update,
        diary_delete,
        tree_create,
        tree_read,
        tree_update,
        tree_delete,
        statistic_read,
        all_tree_read
    }

    private string url = "http://10.70.23.54:5000/";

    private void Start() {
        if(i==null) i=this;

    }

    T GetData<T>(string res){
        return JsonUtility.FromJson<T>(res);
    }

    public bool SignUp() {


        return true;
    }

    public bool Login(string id, string password){
        // 보낼 json string
        Dictionary<string, string> login = new Dictionary<string, string>();
        login.Add("id",id);
        login.Add("password", password);

        // 통신 response를 user에 저장
        User user = null;
        HttpRequest.i.Post(url+sub.member_read.ToString(),DictToJson(login), GetData => user );
        
        if(user==null){
            return false;
        }
        return true;
    }

    public string DictToJson<T>(Dictionary<string,T> dict){
        var x = dict.Select(d =>
                string.Format("\"{0}\": {1}", d.Key, string.Join(",", d.Value)));
        return "{" + string.Join(",", x) + "}";
    }
}
