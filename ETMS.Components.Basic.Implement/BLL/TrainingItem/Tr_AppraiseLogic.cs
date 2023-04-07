using System;
using System.Linq;
using ETMS.Components.Basic.Implement.DAL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Utility;

namespace ETMS.Components.Basic.Implement.BLL.TrainingItem
{
    public partial class Tr_AppraiseLogic
    {
        private static readonly Tr_AppraiseDataAccess DAL = new Tr_AppraiseDataAccess();

        public void Save(Tr_Appraise entity)
        {
            DAL.Save(entity);
        }

        public Tr_Appraise Get(Guid trainingItemID)
        {
            var list = DAL.GetById(trainingItemID).ToList<Tr_Appraise>();
            if (null != list && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        public StandardCalulate GetStandardCalulate(Guid studentCourseID)
        {
            var list = DAL.GetStandardScore(studentCourseID).ToList<StandardCalulate>();
            if (list.Count > 0)
                return list[0];

            return null;
        }
    }
}
