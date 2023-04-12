namespace ETMS.Product
{
    /// <summary>
    /// 产品组件策略
    /// </summary>
    public class ProductComponentStrategy
    {
        //组件策略矩阵
        //===================================================
        /*  模块\产品配置       低配  标配  高配  ******==>
         ====================================================
         * 01 在线课件           1     1    1  
         * 02 在线作业           0     0    1
         * 03 在线测试           0     1    1
         * 04 SCORM课件          0     1    1
         * 05 非SCORM课件        1     1    1
         * 06 导学资料           1     1    1
         * 07 离线作业           0     1    1
         * 08 公共论坛           0     0    1
         * 09 课程论坛           0     1    1
         * 10 班级论坛           0     0    1
         * 11 培训计划           0     0    1
         * 12 课程点评           0     0    1
         * 13 请假               0     0    1
         * 14 导师               0     0    1
         * 15 费用               0     0    1
         * 16 积分               0     0    1
         * 17 满意度调查          0     1    1
         * 18 培训需求调查        0     0    1
         * 19 项目公告            1     1   1
         * 20 项目课程导学资料    1     1   1
         * ......
         * ......
         */
        public static int[][] StrategyMatrix = new int[][]
        { 
            new int[]{1,1,1},//01 在线课件模块
            new int[]{0,0,1},//02 在线作业模块            
            new int[]{0,1,1},//03 在线测试模块             
            new int[]{0,1,1},//04 SCORM课件            
            new int[]{1,1,1},//05 非SCORM课件            
            new int[]{1,1,1},//06 导学资料            
            new int[]{0,1,1},//07 离线作业            
            new int[]{0,0,0},//08 公共论坛            
            new int[]{0,0,0},//09 课程论坛            
            new int[]{0,0,0},//10 班级论坛            
            new int[]{0,0,1},//11 培训计划            
            new int[]{0,0,1},//12 课程点评            
            new int[]{0,0,1},//13 请假            
            new int[]{0,0,1},//14 导师            
            new int[]{0,0,1},//15 费用            
            new int[]{0,0,1},//16 积分             
            new int[]{0,1,1},//17 满意度调查            
            new int[]{0,0,1},//18 培训需求调查
            new int[]{0,0,1},//19 项目公告
            new int[]{1,1,1},//20 项目课程公告                                    
        };


        /// <summary>
        /// 判断组件是否支持
        /// </summary>
        /// <param name="componentType">组件类型</param>
        /// <returns>true,false</returns>
        public static bool IsSupport(ExtendComponentType componentType)
        {
            int componentIndex = (int)componentType;
            //如果组件定义，但产品策略没有定义此组件使用，则返回false
            if (componentIndex > StrategyMatrix.Length)
            {
                return false;
            }
            //注意：枚举是从1开始，数组索引从0开始
            return (StrategyMatrix[componentIndex - 1][(int)ProductDefine.ConfigLevel] == 1);
        }
    }
}
