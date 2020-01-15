using BE;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FeedBackLogic
    {
        public enum Status { Success, NoInternetConnection, DBError, ExistError }

        public FeedBackService feedBackService = new FeedBackService();


        public Status addFeedBack(FeedBack feedback)
        {
            var status = Status.Success;
            try
            {


                var existFeedback = feedBackService.GetFeedbackByID(feedback.FeedbackId);
                if (existFeedback != null)
                {
                    status = Status.ExistError;
                }
                else
                {
                    feedBackService.AddFeedback(feedback);
                }
            }
            catch (Exception)
            {

                status = Status.DBError;
            }
            return status;

        }
    }
}
