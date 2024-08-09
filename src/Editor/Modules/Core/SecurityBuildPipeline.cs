using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGS.Editor
{
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
                string x = "읽어주세요! 보안모드가 활성화 되어있지 않은경우 스크립트 세팅과 UGS 라이브 기능등이 그대로 동작합니다. 게임을 실제로 출시하려고 하는경우 보안옵션을 활성화해야합니다. 자세한건 UGS의 세팅메뉴의 하이퍼링크에서 확인해주세요. 만약 테스트를 위한 개발 빌드라면 이 메시지를 무시하셔도 됩니다.";
                var res = UnityEditor.EditorUtility.DisplayDialog("UGS Warning", x, "이해했습니다.");
                if (res)
                {
                    UnityEditor.EditorPrefs.SetBool("UGS.BuildMsg", true);
                }

            }
        }

    }

}
