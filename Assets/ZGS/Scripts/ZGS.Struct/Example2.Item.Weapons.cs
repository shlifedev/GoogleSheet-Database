
/*     ===== Do not touch this. Auto Generated Code. =====    */
/*     If you want custom code generation modify this => 'CodeGeneratorUnityEngine.cs'  */
using Hamster.ZG;
using Hamster.ZG.Http;
using System;
using System.Collections.Generic;
using System.IO;
using Hamster.ZG.Http.Protocol;
using Hamster.ZG.Type;
using System.Reflection;
using UnityEngine;

namespace Example2.Item
{
    [Hamster.ZG.Attribute.TableStruct]
    public class Weapons : ITable
    { 

        public delegate void OnLoadedFromGoogleSheets(List<Weapons> loadedList, Dictionary<int, Weapons> loadedDictionary);

        static bool isLoaded = false;
        public static string spreadSheetID = "1RdqFYcNUxDe3dBGWS2DyCXjWJedYJ5_z9SxjH_d_0HI"; // it is file id
        public static string sheetID = "0"; // it is sheet id
        public static UnityFileReader reader = new UnityFileReader();
        public static Dictionary<int, Weapons> WeaponsMap = new Dictionary<int, Weapons>(); 
        public static List<Weapons> WeaponsList = new List<Weapons>();  


		public Int32 itemIndex;
		public String localeID;
		public String Type;
		public Int32 Grade;
		public Int32 STR;
		public Int32 DEX;
		public Int32 INT;
		public Int32 LUK;
		public String IconName;
		public Int32 Price;
  

#region fuctions


        public static void Write(Weapons data)
        { 
            TypeMap.Init();
            FieldInfo[] fields = typeof(Weapons).GetFields(BindingFlags.Public | BindingFlags.Instance);
            var datas = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                var writeRule = TypeMap.Map[type].Write(fields[i].GetValue(data));
                datas[i] = writeRule; 
            }  
           
#if UNITY_EDITOR
if(Application.isPlaying == false)
{
            UnityEditorWebRequest.Instance.POST_WriteData(spreadSheetID, sheetID, datas[0], datas);
}
else
{
            UnityPlayerWebRequest.Instance.POST_WriteData(spreadSheetID, sheetID, datas[0], datas);
}
#endif
        } 
         


        public static void LoadFromGoogle(OnLoadedFromGoogleSheets onLoaded, bool updateCurrentData = false)
        {
            IZGRequester webInstance = null;
#if UNITY_EDITOR
            if (Application.isPlaying == false)
            {
                webInstance = UnityEditorWebRequest.Instance as IZGRequester;
            }
            else
            {
                webInstance = UnityPlayerWebRequest.Instance as IZGRequester;
            }
#endif
#if !UNITY_EDITOR
                 webInstance = UnityPlayerWebRequest.Instance as IZGRequester;
#endif
            if(updateCurrentData)
            {
                WeaponsMap?.Clear();
                WeaponsList?.Clear(); 
            }
            List<Weapons> callbackParamList = new List<Weapons>();
            Dictionary<int,Weapons> callbackParamMap = new Dictionary<int, Weapons>();
            webInstance.GET_TableData(spreadSheetID, (data, json) => {
            FieldInfo[] fields = typeof(Example2.Item.Weapons).GetFields(BindingFlags.Public | BindingFlags.Instance);
            List<(string original, string propertyName, string type)> typeInfos = new List<(string,string,string)>();
            List<List<string>> typeValuesCList = new List<List<string>>(); 
              if (json != null)
                        {
                            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<GetTableResult>(json);
                            var table= result.tableResult; 
                            var sheet = table["Weapons"];
                                foreach (var pNameAndTypeName in sheet.Keys)
                                {
                                    var split = pNameAndTypeName.Replace(" ", null).Split(':');
                                    var propertyName = split[0];
                                    var type = split[1];
                                    typeInfos.Add((pNameAndTypeName, propertyName, type));
                                    var typeValues = sheet[pNameAndTypeName];
                                    typeValuesCList.Add(typeValues);
                                } 
                            if (typeValuesCList.Count != 0)
                            {
                                int rows = typeValuesCList[0].Count;
                                for (int i = 0; i < rows; i++)
                                {
                                    Example2.Item.Weapons instance = new Example2.Item.Weapons();
                                    for (int j = 0; j < typeInfos.Count; j++)
                                    {
                                        var typeInfo = TypeMap.StrMap[typeInfos[j].type];
                                        var readedValue = TypeMap.Map[typeInfo].Read(typeValuesCList[j][i]); 
                                        fields[j].SetValue(instance, readedValue);
                                    }
                                    //Add Data to Container
                                    callbackParamList.Add(instance);
                                    callbackParamMap .Add(instance.itemIndex, instance);
                                    if(updateCurrentData)
                                    {
                                       WeaponsList.Add(instance);
                                       WeaponsMap.Add(instance.itemIndex, instance);
                                    }
                                } 
                            }
                        }

                      onLoaded?.Invoke(callbackParamList, callbackParamMap);
            });
        }

            


        public static void Load(bool forceReload = false)
        {
            if(isLoaded && forceReload == false)
            {
                 Debug.Log("Weapons is already loaded! if you want reload then, forceReload parameter set true");
                 return;
            }
            /* Clear When Try Load */
            WeaponsMap?.Clear();
            WeaponsList?.Clear(); 
            //Type Map Init
            TypeMap.Init();
            //Reflection Field Datas.
            FieldInfo[] fields = typeof(Example2.Item.Weapons).GetFields(BindingFlags.Public | BindingFlags.Instance);
            List<(string original, string propertyName, string type)> typeInfos = new List<(string,string,string)>();
            List<List<string>> typeValuesCList = new List<List<string>>(); 
            //Load GameData.
            string text = reader.ReadData("Example2.Item");
            if (text != null)
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<GetTableResult>(text);
                var table= result.tableResult; 
                var sheet = table["Weapons"];
                    foreach (var pNameAndTypeName in sheet.Keys)
                    {
                        var split = pNameAndTypeName.Replace(" ", null).Split(':');
                        var propertyName = split[0];
                        var type = split[1];
                        typeInfos.Add((pNameAndTypeName, propertyName, type));
                        var typeValues = sheet[pNameAndTypeName];
                        typeValuesCList.Add(typeValues);
                    } 
                if (typeValuesCList.Count != 0)
                {
                    int rows = typeValuesCList[0].Count;
                    for (int i = 0; i < rows; i++)
                    {
                        Example2.Item.Weapons instance = new Example2.Item.Weapons();
                        for (int j = 0; j < typeInfos.Count; j++)
                        {
                            var typeInfo = TypeMap.StrMap[typeInfos[j].type];
                            var readedValue = TypeMap.Map[typeInfo].Read(typeValuesCList[j][i]); 
                            fields[j].SetValue(instance, readedValue);
                        }
                        //Add Data to Container
                        WeaponsList.Add(instance);
                        WeaponsMap.Add(instance.itemIndex, instance);
                    } 
                }
            }
            isLoaded = true;
        }
 

#endregion

    }
}
        