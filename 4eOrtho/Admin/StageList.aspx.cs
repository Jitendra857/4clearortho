using _4eOrtho.BAL;
using _4eOrtho.DAL;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho.Admin
{
    public partial class StageList : System.Web.UI.Page
    {
        #region Declaration
       
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
      
        int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListCaseCharges));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    this.BindGrid();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }

        }
        private void BindGrid()
        {
         string constr=@"data source=DESKTOP-NGNOP1Q\EXPRESS1;initial catalog=4ClearOrtho;Integrated Security=true;multipleactiveresultsets=True;user id=sa;password=joshi@123;";

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from StageMaster"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            ViewState["Paging"] = dt;
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }

      

        protected void odsCaseCharge_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                e.InputParameters["searchValue"] = string.Empty; // txtSearchVal.Text.Trim();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }
        }

        public void refreshdata()
        {
            SqlConnection con = new SqlConnection(@"data source=DESKTOP-NGNOP1Q\EXPRESS1;initial catalog=4ClearOrtho;Integrated Security=true;multipleactiveresultsets=True;user id=sa;password=joshi@123;");

            SqlCommand cmd = new SqlCommand("select * from StageMaster", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
         //GridView1.DataSource = dt;
         //  GridView1.DataBind();

            GridView1.DataSourceID = null;
            GridView1.DataSource = dt;
            GridView1.DataBind();


        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"data source=DESKTOP-NGNOP1Q\EXPRESS1;initial catalog=4ClearOrtho;Integrated Security=true;multipleactiveresultsets=True;user id=sa;password=joshi@123;");

            int id = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values["StageId"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from StageMaster where StageId =@id", con);
            cmd.Parameters.AddWithValue("id", id);
            int i = cmd.ExecuteNonQuery();
            con.Close();
           this.BindGrid();
        }
        protected void Gridpaging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = ViewState["Paging"];
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

            int id = Convert.ToInt16(GridView1.DataKeys[e.NewEditIndex].Values["StageId"].ToString());

            Response.Redirect(string.Format("AddUpdateStageMaster.aspx?id={0}", id));

            refreshdata();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            refreshdata();
        }

        public SortDirection CurrentSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["sortDirection"];
            }
            set
            {
                ViewState["sortDirection"] = value;
            }
        }
        protected void Gridsorting(object sender, GridViewSortEventArgs e)
        {
            string ColumnTosort = e.SortExpression;

            if (CurrentSortDirection == SortDirection.Ascending)
            {
                CurrentSortDirection = SortDirection.Descending;
                SortGridView(ColumnTosort, DESCENDING);
            }
            else
            {
                CurrentSortDirection = SortDirection.Ascending;
                SortGridView(ColumnTosort, ASCENDING);
            }

        }

        private void SortGridView(string sortExpression, string direction)
        {
            //  You can cache the DataTable for improving performance    
            dynamic dt = ViewState["Paging"];
            DataTable dtsort = dt;
            DataView dv = new DataView(dtsort);
            dv.Sort = sortExpression + direction;

            GridView1.DataSource = dv;
            GridView1.DataBind();
        }
    }
}