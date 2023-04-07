using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Components.Scrom.API.Entity;
using ETMS.Components.Scrom.Implement.DAL;
using ETMS.Utility;

namespace ETMS.Components.Scrom.Implement.BLL
{
    public class ItemResourceLogic
    {
        private static ItemResourceDataAccess ItemResource = new ItemResourceDataAccess();
        /// <summary>
        /// 根据错误代码获取错误信息
        /// </summary>
        public string LMSGetErrorString(string ECode)
        {
            string ErrString = "";
            switch (ECode)
            {
                case "0":
                    ErrString = "No error";
                    break;
                case "101":
                    ErrString = "General exception";
                    break;
                case "201":
                    ErrString = "Invalid argument error";
                    break;
                case "202":
                    ErrString = "Element cannot have children";
                    break;
                case "203":
                    ErrString = "Element not an array - cannot have count";
                    break;
                case "301":
                    ErrString = "Not initialized";
                    break;
                case "401":
                    ErrString = "Not implemented error";
                    break;
                case "402":
                    ErrString = "Invlid set value,element is a keyword";
                    break;
                case "403":
                    ErrString = "Element is ready only";
                    break;
                case "404":
                    ErrString = "Element is write only";
                    break;
                case "405":
                    ErrString = "Incorrect Data Type";
                    break;
            }
            return ErrString;
        }

        /// <summary>
        /// 获取课件章节树
        /// </summary>
        /// <param name="CoursewareID"></param>
        /// <returns></returns>
        public DataTable GetLessonTree(Guid CoursewareID,Guid itemCourseResID, int UserID)
        {
            return ItemResource.GetLessonTree(CoursewareID, itemCourseResID,UserID);
        }

        /// <summary>
        /// 获取课件章节树
        /// </summary>
        /// <param name="coursewareID"></param>
        /// <param name="itemCourseResID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Catalog> GetCatalog(Guid coursewareID, Guid itemCourseResID, int userID)
        {
            return ItemResource.GetLessonTree(coursewareID, itemCourseResID, userID).ToList<Catalog>();
        }

        /// <summary>
        /// 获取章节下的资源
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public DataTable GetResourceByItem(string ItemID)
        {
            DataTable td = new DataTable();
            return td;
        }

        /// <summary>
        /// 根据资源ID获取课程等信息
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public DataTable GetInfoByRescourceID(Guid ResourceID)
        {   
            return ItemResource.GetInfoByRescourceID(ResourceID);
        }
        
    }
}
