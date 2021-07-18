using Hamster.ZG;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class SecurityBuildPipeline : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    public int callbackOrder => 0;
 
    public void OnPostprocessBuild(BuildReport report)
    { 
    }

 
    public void OnPreprocessBuild(BuildReport report)
    {
        var confirm = UnityEditor.EditorPrefs.GetBool("UGS.BuildMsg", false);
        if (!confirm)
        {
            var res = UnityEditor.EditorUtility.DisplayDialog("UGS Warning", "[�����ʿ�] UGS ���������� Resources ������ ���Ե˴ϴ�. �� ��� apiŰ�� ����� �� �ֱ� ������ ������ ���忡���� ���������� �������� �ʴ°��� �ǰ��մϴ�. ", "OK!");
            if (res)
            {
                UnityEditor.EditorPrefs.SetBool("UGS.BuildMsg", true);
            } 

        } 
    }
     
}
