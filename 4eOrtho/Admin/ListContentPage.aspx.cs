using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class ListContentPage : PageBase
    {
        #region Declaration

        private List<PageWithLevel> list;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListContentPage));

        #endregion Declaration

        #region Events

        /// <summary>
        /// Page Load - bind data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // lblStatusMsg.Visible = false;
                if (!Page.IsPostBack)
                {
                    BindData();
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// repeater content page with item command
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptListContentPage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() == "delete")
                {
                    long deletePageId = Convert.ToInt32(e.CommandArgument);
                    if (deletePageId > 0)
                    {
                        DeletePageWithChildPage(deletePageId);
                        PageDetailsEntity detailentity = new PageDetailsEntity();
                        _4eOrtho.DAL.PageDetail detail = detailentity.GetPageDetailByPageId(deletePageId);
                        detailentity.Delete(detail);

                        PagesEntity pagesEntity = new PagesEntity();
                        _4eOrtho.DAL.Page page = pagesEntity.GetPageByID(deletePageId);
                        pagesEntity.Delete(page);

                        BindData();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// repeater content page with item databound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptListContentPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    PageWithLevel currentPageWithLevel = e.Item.DataItem as PageWithLevel;
                    HtmlImage imgStatus = e.Item.FindControl("imgStatus") as HtmlImage;

                    if (currentPageWithLevel.Status)
                    {
                        imgStatus.Src = "Images/icon-active.gif";
                        imgStatus.Attributes.Add("title", this.GetLocalResourceObject("Active").ToString());
                    }
                    else
                    {
                        imgStatus.Src = "Images/icon-inactive.gif";
                        imgStatus.Attributes.Add("title", this.GetLocalResourceObject("InActive").ToString());
                    }

                    //HtmlImage imgRequiredAuth = e.Item.FindControl("imgRequiredAuth") as HtmlImage;
                    //if (CommonLogic.GetConfigValue("EnableUserAuthModule") == "1")
                    //{
                    //    imgRequiredAuth.Visible = true;
                    //    if (currentPageWithLevel.RequiredAuthentication)
                    //    {
                    //        imgRequiredAuth.Src = "Images/closed.png";
                    //        imgRequiredAuth.Attributes.Add("title", "Required Authentication");

                    //    }
                    //    else
                    //    {
                    //        imgRequiredAuth.Src = "Images/open.png";
                    //        imgRequiredAuth.Attributes.Add("title", "Not Required Authentication");
                    //    }
                    //}
                    int firstLevelNodes = list.Where(x => x.ParentID == currentPageWithLevel.PageID).Count();
                    Label lblMenuItem = e.Item.FindControl("lblMenuItem") as Label;
                    if (lblMenuItem.Text == "")
                    {
                        lblMenuItem.Text = "N/A";
                    }
                    HtmlTableRow trPageItem = e.Item.FindControl("trPageItem") as HtmlTableRow;
                    if (trPageItem != null)
                    {
                        trPageItem.ID = "node-" + currentPageWithLevel.PageID.ToString();
                        if (currentPageWithLevel.ParentID != null)
                        {
                            trPageItem.Attributes.Add("class", "child-of-node-" + currentPageWithLevel.ParentID.Value.ToString());
                        }
                    }
                    ImageButton imgbtnDelete = e.Item.FindControl("imgbtnDelete") as ImageButton;
                    if (firstLevelNodes > 0)
                    {
                        //imgbtnDelete.OnClientClick = "return confirm('Are you sure you want to delete this record with child(s) record?');";
                        imgbtnDelete.OnClientClick = "return confirm('"+this.GetLocalResourceObject("ChildMessage").ToString() + "')";
                    }
                    else
                    {
                        //imgbtnDelete.OnClientClick = "return confirm('Are you sure you want to delete this record?');";
                        imgbtnDelete.OnClientClick = "return confirm('" + this.GetLocalResourceObject("DeleteMessage").ToString() + "')";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion Events

        #region Helpers

        /// <summary>
        /// for bindData in repeater
        /// </summary>
        private void BindData()
        {
            try
            {
                PagesEntity pagesEntity = new PagesEntity();

                List<PageWithLevel> pageWithLevelList = new List<PageWithLevel>();
                //if (ddlSearchField.SelectedValue != "0" && ddlSearchVal.SelectedValue != "0")
                //{
                //    pageWithLevelList = pagesEntity.GetPageWithLevel(Convert.ToInt32(ddlSearchVal.SelectedValue));
                //}
                //else
                //{
                //    pageWithLevelList = pagesEntity.GetPageWithLevel(0);
                //}
                pageWithLevelList = pagesEntity.GetPageWithLevel(0);
                list = new List<PageWithLevel>();
                SetProperPageTreeData(pageWithLevelList, null);

                if (list != null && list.Count > 0)
                {
                    rptListContentPage.Visible = true;
                    rptListContentPage.DataSource = list;
                    rptListContentPage.DataBind();
                    // trNoDataFound.Visible = false;
                }
                else
                {
                    rptListContentPage.DataSource = null;
                    rptListContentPage.DataBind();
                    rptListContentPage.Visible = false;
                    // trNoDataFound.Visible = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// set proper page tree data
        /// </summary>
        /// <param name="pageWithLevelList"></param>
        /// <param name="parentId"></param>
        private void SetProperPageTreeData(List<PageWithLevel> pageWithLevelList, long? parentId)
        {
            try
            {
                var nodes = pageWithLevelList.Where(x => x.ParentID == parentId).OrderBy(x => x.DisplayOrder).ThenBy(x => x.PageID).ToList();
                foreach (var node in nodes)
                {
                    list.Add(node);
                    SetProperPageTreeData(pageWithLevelList, node.PageID);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// delete page with chil page by page id
        /// </summary>
        /// <param name="deletePageId"></param>
        private void DeletePageWithChildPage(long deletePageId)
        {
            try
            {
                PagesEntity pagesEntity = new PagesEntity();
                List<PageWithLevel> pageWithLevelList = pagesEntity.GetPageWithLevel(1);
                DeletePageWithRecursive(pageWithLevelList, deletePageId);
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// delete page with recurisve with parentid
        /// </summary>
        /// <param name="pageWithLevelList"></param>
        /// <param name="parentId"></param>
        private void DeletePageWithRecursive(List<PageWithLevel> pageWithLevelList, long parentId)
        {
            try
            {
                var nodes = pageWithLevelList.Where(x => x.ParentID == parentId).OrderBy(x => x.DisplayOrder).ThenBy(x => x.PageID).ToList();
                foreach (var node in nodes)
                {
                    SetProperPageTreeData(pageWithLevelList, node.PageID);
                    PageDetailsEntity detailentity = new PageDetailsEntity();
                    PageDetail detail = detailentity.GetPageDetailByPageId(node.PageID);
                    detailentity.Delete(detail);

                    PagesEntity pagesEntity = new PagesEntity();
                    _4eOrtho.DAL.Page page = pagesEntity.GetPageByID(node.PageID);
                    pagesEntity.Delete(page);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion Helpers
    }
}