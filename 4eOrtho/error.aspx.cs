using _4eOrtho.DAL;
using _4eOrtho.BAL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class error : System.Web.UI.Page
    {
        private int totalRecordsCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(error));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                lvError.DataBind();
        }

        protected void odsErrorList_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["searchText"] = txtSearchVal.Text.Trim();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        public List<usp_GetAllErrorlog_Result> GetErrorListBySearch(int pageSize, int startRowIndex, string searchText)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                ErrorLogEntity entity = new ErrorLogEntity();

                List<usp_GetAllErrorlog_Result> lstgetCountryDetail = entity.GetAllGalleryDetails(pageSize, pageIndex, searchText, out totalRecords);

                lstgetCountryDetail.ForEach(x => x.Date = ConvertToLocal(x.Date));
                totalRecordsCount = totalRecords;
                return lstgetCountryDetail;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        public int GetErrorDataCount(string searchText)
        {
            try
            {
                return totalRecordsCount;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return 0;
            }
        }

        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            List<Int64> lstIds = new List<Int64>();

            foreach (ListViewDataItem item in lvError.Items)
            {
                if (item != null)
                {
                    CheckBox chkDelete = item.FindControl("chkDelete") as CheckBox;
                    if (chkDelete != null && chkDelete.Checked)
                    {
                        lstIds.Add(Convert.ToInt64(lvError.DataKeys[item.DataItemIndex].Value));
                    }
                }
            }
            new ErrorLogEntity().DeleteError(lstIds);
            lvError.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lvError.DataBind();
        }

        public static DateTime ConvertToLocal(DateTime dateTime)
        {
            TimeZoneInfo currentTimeZone;
            currentTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), currentTimeZone);
        }
    }
}