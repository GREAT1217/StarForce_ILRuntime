using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    public static class EntityExtension
    {
        // 关于 EntityId 的约定：
        // 0 为无效
        // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
        // 负值用于本地生成的临时实体（如特效、FakeObject等）
        private static int s_SerialId = 0;

        public static void ShowHotfixMyAircraft(this EntityComponent entityComponent, MyAircraftData data)
        {
            entityComponent.ShowEntity(typeof(MyAircraft), "Aircraft", Constant.AssetPriority.MyAircraftAsset, data);
        }

        public static void ShowHotfixAircraft(this EntityComponent entityComponent, Type logicType, AircraftData data)
        {
            entityComponent.ShowHotfixEntity(logicType, "Aircraft", Constant.AssetPriority.AircraftAsset, data);
        }

        public static void ShowHotfixEntity(this EntityComponent entityComponent, Type logicType, string entityGroup, int priority, EntityData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity drEntity = dtEntity.GetDataRow(data.TypeId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
                return;
            }

            ILUserData userData = new ILUserData(logicType.ToString(), data);
            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(drEntity.AssetName), entityGroup, priority, userData);
        }

        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --s_SerialId;
        }
    }
}
