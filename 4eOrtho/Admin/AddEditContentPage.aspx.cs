using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.Utility;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using log4net;
using System.Transactions;
using _4eOrtho.Helper;

namespace _4eOrtho.Admin
{   
    public partial class AddEditContentPage : PageBase
    {
        #region Declaration
        long pageID = 0;
        int languageID = 0;
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditContentPage));
        #endregion

        #region Events
        /// <summary>
        /// PageLoad  Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(CommonLogic.QueryString("id")))
                {
                    pageID = Convert.ToInt32(CommonLogic.QueryString("id"));
                }

                if (!String.IsNullOrEmpty(CommonLogic.QueryString("lid")))
                {
                    languageID = Convert.ToInt32(CommonLogic.QueryString("lid"));
                }
                if (!Page.IsPostBack)
                {
                    BindLanguages();
                    //BindParentPageData(languageID);
                    if (pageID > 0)
                    {
                        BindContentPages();
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource2").ToString();
                        Page.Title = this.GetLocalResourceObject("PageResource1").ToString();
                    }
                    else
                    {
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource1.Text").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }

        }
        /// <summary>
        /// Language Dropdown Selected Index changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindContentPages();
                // BindParentPageData(Convert.ToInt32(ddlLanguage.SelectedItem.Value));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// submit button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            TransactionScope scope = new TransactionScope();
            try
            {
                using (scope)
                {
                    PagesEntity pagesEntity = new PagesEntity();
                    if (!pagesEntity.GetMenuitem(txtMenuName.Text.Trim(), pageID))
                    {
                        _4eOrtho.DAL.Page page = pagesEntity.GetPageByIDandLanguageID(pageID, Convert.ToInt32(ddlLanguage.SelectedItem.Value));

                        if (page == null)
                        {
                            page = pagesEntity.Create();
                            page.CreatedBy = 1;

                        }
                        else
                        {
                            page.LastUpdatedBy = 1;

                        }

                        //if (ddlParentPage.SelectedValue == "0")
                        //{
                        page.ParentID = null;
                        //}
                        //else
                        //{
                        //    page.ParentID = Convert.ToInt32(ddlParentPage.SelectedValue);
                        //}
                        page.LanguageID = Convert.ToInt32(ddlLanguage.SelectedItem.Value);
                        page.Status = chkStatus.Checked;
                        page.IsActive = chkStatus.Checked;
                        //page.RequiredAuthentication = chkRequiredAuthentication.Checked;

                        pageID = pagesEntity.Save(page);

                        PageDetailsEntity pageDetailsEntity = new PageDetailsEntity();
                        PageDetail pageDetail = pageDetailsEntity.GetPageDetailsByPageIdAndLanguageID(pageID, Convert.ToInt32(ddlLanguage.SelectedItem.Value));
                        if (pageDetail == null)
                        {
                            pageDetail = pageDetailsEntity.Create();
                            pageDetail.CreatedBy = 1;
                        }
                        else
                        {
                            pageDetail.LastUpdatedBy = 1;
                        }
                        pageDetail.PageID = pageID;
                        pageDetail.MenuItem = txtMenuName.Text.Trim();
                        pageDetail.URLName = txtURLMenuName.Text.Trim();
                        pageDetail.PageTitle = txtPageTitle.Text.Trim();
                        pageDetail.PageKeyword = txtPageKeyword.Text.Trim();
                        pageDetail.PageMetaDescription = txtPageDescription.Text.Trim().ToString();
                        pageDetail.PageContent = ucHTMLEditorControl.Text;
                        pageDetail.LanguageID = Convert.ToInt32(ddlLanguage.SelectedItem.Value);
                        pageDetail.IsActive = chkStatus.Checked;

                        string checkBoxValues = string.Empty;
                        foreach (ListItem li in chklstRole.Items)
                        {
                            if (li.Selected)
                                //checkBoxValues += li.Value + ",";
                                checkBoxValues += li.Value;
                        }
                        //string commaseparate = checkBoxValues.Remove(checkBoxValues.Length - 1,1);
                        pageDetail.Role = checkBoxValues;
                        //pageDetail.Role = commaseparate;
                        pageDetailsEntity.Save(pageDetail);
                        scope.Complete();
                        if (pageID > 0)
                        {
                            CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("UpdateContentPage").ToString(), divMsg, lblMsg);
                        }
                        else
                        {
                            CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("InsertContentPage").ToString(), divMsg, lblMsg);
                        }
                        Response.Redirect("ListContentPage.aspx?status=s", false);
                    }
                    else
                    {
                        //lblmessage.Text = "Menu Name is already exist";
                        CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("MenuAlreadyExist").ToString(), divMsg, lblMsg);
                        // Response.Redirect("ContentPageList.aspx?status=s", false);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// method for Bind Languages
        /// </summary>
        public void BindLanguages()
        {
            try
            {
                LanguagesEntity languageEntity = new LanguagesEntity();
                List<Language> lstLanguages = languageEntity.GetAllLanguages();

                ddlLanguage.DataSource = lstLanguages;
                ddlLanguage.DataTextField = "LanguageName";
                ddlLanguage.DataValueField = "LanguageID";
                ddlLanguage.DataBind();

//BindParentPageData(Convert.ToInt32(ddlLanguage.SelectedItem.Value));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);

            }
        }
        /// <summary>
        /// method for bind contentpages
        /// </summary>
        public void BindContentPages()
        {
            try
            {
                PageDetailsEntity pageDetailsEntity = new PageDetailsEntity();
                //PageDetail pageDetail = pageDetailsEntity.GetPageDetailsByPageDetailID(pageID);
                PageDetail pageDetail = pageDetailsEntity.GetPageDetailsByPageIdAndLanguageID(pageID, Convert.ToInt32(ddlLanguage.SelectedItem.Value));

                if (pageDetail != null)
                {
                    txtMenuName.Text = pageDetail.MenuItem;
                    txtPageTitle.Text = pageDetail.PageTitle;
                    txtPageKeyword.Text = pageDetail.PageKeyword;
                    txtURLMenuName.Text = pageDetail.URLName;
                    txtPageDescription.Text = pageDetail.PageMetaDescription;

                    ddlLanguage.SelectedIndex = ddlLanguage.Items.IndexOf(ddlLanguage.Items.FindByValue(Convert.ToString(pageDetail.LanguageID)));
                    ucHTMLEditorControl.Text = pageDetail.PageContent;

                    if (!string.IsNullOrEmpty(pageDetail.Role))
                    {
                        for (int i = 0; i < chklstRole.Items.Count; i++)
                        {
                            if (pageDetail.Role.ToLower().Contains(chklstRole.Items[i].Value.ToLower()))
                                chklstRole.Items[i].Selected = true;
                        }
                    }

                    chkStatus.Checked = Convert.ToBoolean(pageDetail.IsActive);
                    //BindParentPageData(Convert.ToInt32(ddlLanguage.SelectedItem.Value));
                    PagesEntity pagesEntity = new PagesEntity();
                    _4eOrtho.DAL.Page page = pagesEntity.GetPageByID(pageDetail.PageID);
                    if (page != null)
                    {
                        chkStatus.Checked = page.Status;
                        //chkRequiredAuthentication.Checked = page.RequiredAuthentication;
                        if (page.ParentID != null)
                        {
                            //if (ddlParentPage.Items.FindByValue(page.ParentID.Value.ToString()) != null)
                            //{
                            //    ddlParentPage.SelectedValue = page.ParentID.Value.ToString();
                            //}
                        }
                    }
                }
                else
                {
                    txtMenuName.Text = 
                    txtPageTitle.Text = "";
                    txtPageKeyword.Text = "";
                    txtURLMenuName.Enabled = false;
                    txtPageDescription.Text = "";
                    ucHTMLEditorControl.Text = "";
                    chkStatus.Checked = false;
                    ScriptManager.RegisterStartupScript(this,this.GetType(), "alert", "alert('Content Page detail does not exist for selected language.Please Enter Content Page detail for Selected Lanaguage')", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// Description : Method for binding tree data
        /// </summary>
        /// <param name="pageWithLevelList"></param>
        /// <param name="parentId"></param>
        /// <param name="currentPageId"></param>
        /// <returns></returns>
        private List<PageWithLevel> SetProperPageTreeData(List<PageWithLevel> pageWithLevelList, long? parentId, long currentPageId)
        {
            try
            {
                List<PageWithLevel> lstPageWithLevel = new List<PageWithLevel>();
                var nodes = pageWithLevelList.Where(x => x.PageID != currentPageId && x.ParentID == parentId).OrderBy(x => x.PageID).ThenBy(x => x.DisplayOrder).ToList();
                foreach (var node in nodes)
                {
                    lstPageWithLevel.Add(node);
                    SetProperPageTreeData(pageWithLevelList, node.PageID, currentPageId);
                }
                return lstPageWithLevel;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }
        /// <summary>
        /// Description : Method for filling dropdownlist
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private string DropDownListItemText(string itemName, int level)
        {
            try
            {
                string appendText = string.Empty;
                for (int i = 2; i <= level; i++)
                {
                    appendText = "----" + appendText;
                }
                if (!string.IsNullOrEmpty(appendText))
                {
                    appendText += " ";
                }
                return appendText + itemName;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }
        #endregion 

    }
}