using System.Collections.Generic;

using ETMS.AppContext.Component;
using ETMS.Components.Poll.API;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
namespace ETMS.Components.Poll.Implement
{
    public class DefaultPollFacade : DefaultComponent, IPollFacade
    {
        private Poll_UserResourceQueryResultLogic UserResourceQueryLogic = new Poll_UserResourceQueryResultLogic();

        public System.Data.DataTable GetQueryListForUser(int userID, int organizationID)
        {
            return UserResourceQueryLogic.GetQueryListNoAnswerByUserID(userID, organizationID);
        }

        public IList<Poll_Query> GetQueryListForUser(int userID, int organizationID, int topSize)
        {
            return UserResourceQueryLogic.GetQueryListForUserPagedList(userID, organizationID, topSize);
        }
    }
}

