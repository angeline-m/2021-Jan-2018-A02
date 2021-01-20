﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
#endregion

namespace WebApp.SamplePages
{
    public partial class SearchByDDL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Message.Text = "";
            if (!Page.IsPostBack)
            {
                //this is first time
                LoadArtistList();

            }
        }

        #region Error Handling ODS
        protected void SelectCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }
        #endregion

        protected void LoadArtistList()
        {
            ArtistController sysmgr = new ArtistController();
            List<SelectionList> info = sysmgr.Artists_DDLList();

            //let's assume the data collection needs to be sorted
            info.Sort((x,y) => x.DisplayField.CompareTo(y.DisplayField));

            //setup the ddl
            ArtistList.DataSource = info;

            //OLD WAY
            //ArtistList.DataTextField = "Displayfield";

            //NEW WAY
            ArtistList.DataTextField = nameof(SelectionList.DisplayField);
            ArtistList.DataValueField = nameof(SelectionList.ValueField);
            ArtistList.DataBind();

            //prompt line -- a more "complete and proper" way of inserting a prompt
            ArtistList.Items.Insert(0, new ListItem("Select...", "0"));
        }

        protected void SearchAlbums_Click(object sender, EventArgs e)
        {
            if(ArtistList.SelectedIndex == 0)
            {
                //index 0 is physically pointing to the prompt line
                //Message.Text = "Select an artist for the search";

                //using MessageUserControl for your own message
                MessageUserControl.ShowInfo("Search concern", "Select an artist for the search");
                ArtistAlbumList.DataSource = null;
                ArtistAlbumList.DataBind();
            }
            else
            {
                //user friendly error handling
                //normally when you leave the web page to your class library, you will want to have error handling (aka try/catch)
                //use MessageUserControl to handle errors
                //MessageUserControl has try/catch embedded inside its logic
                MessageUserControl.TryRun(() => {
                    //standard lookup and assignment
                    AlbumController sysmgr = new AlbumController();
                    List<ChinookSystem.ViewModels.ArtistAlbums> info = sysmgr.Albums_GetAlbumsForArtist(int.Parse(ArtistList.SelectedValue));
                    ArtistAlbumList.DataSource = info;
                    ArtistAlbumList.DataBind();
                }, "Success Message title", "your success message goes here");
            }
        }

        protected void ArtistListODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {

        }
    }
}