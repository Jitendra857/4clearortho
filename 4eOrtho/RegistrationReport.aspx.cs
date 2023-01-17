using _4eOrtho.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using log4net;

namespace _4eOrtho
{
    public partial class RegistrationReport : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(RegistrationReport));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    FillYear();
                    BindList();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void BindList()
        {
            string monthDate = "01" + "-" + ddlFromMonth.SelectedItem.Text + "-" + ddlYear.SelectedItem.Value;
            DateTime fromDate = rbtnMonth.Checked ? Convert.ToDateTime(monthDate) : Convert.ToDateTime(txtFromDate.Text.Trim());
            DateTime toDate = rbtnMonth.Checked ? Convert.ToDateTime(monthDate).AddMonths(1) : Convert.ToDateTime(txtToDate.Text.Trim());
            DoctorEntity entity = new DoctorEntity();
            List<GetAllOrthoDoctor_Result> lstDoctor = entity.GetAllOrthoDoctor();
            rptReport.DataSource = lstDoctor.Where(x=> x.CreatedDate > fromDate && x.CreatedDate < toDate).OrderByDescending(x=>x.CreatedDate).ToList();
            rptReport.DataBind();
        }

        /// <summary>
        /// Fill Year Dropdown List
        /// </summary>
        protected void FillYear()
        {
            for (int i = 1950; i <= DateTime.Today.Year; i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlYear.SelectedValue = Convert.ToString(BaseEntity.GetServerDateTime.Year);
            ddlFromMonth.SelectedValue = Convert.ToString(BaseEntity.GetServerDateTime.Month);

            string monthDate = "01" + "-" + ddlFromMonth.SelectedItem.Text + "-" + ddlYear.SelectedItem.Value;
            DateTime fromDate = rbtnMonth.Checked ? Convert.ToDateTime(monthDate) : Convert.ToDateTime(txtFromDate.Text.Trim());
            DateTime toDate = rbtnMonth.Checked ? Convert.ToDateTime(monthDate).AddMonths(1) : Convert.ToDateTime(txtToDate.Text.Trim());

            txtFromDate.Text = fromDate.ToString("MM/dd/yyyy");
            txtToDate.Text = toDate.ToString("MM/dd/yyyy");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindList();
        }
    }
}