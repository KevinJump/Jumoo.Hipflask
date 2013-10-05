using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jumoo.Hipflask
{
    public partial class HipFlaskDashboard : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HipFlaskSettings s = new HipFlaskSettings();
            List<string> hipsters =  s.GetHipsters();

            HipsterList.DataSource = hipsters;
            HipsterList.DataBind();

            if (s.CheckForUpdate())
            {
                updateCheck.Visible = true; 
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            Hipflask h = new Hipflask();

            Status.Text = "Downloading";

            h.Download("Bootstrap");

            Status.Text = "Unpacking";
            h.Unpack("Bootstrap");
            
            Status.Text = "Installing";
            h.Install("Bootstrap");

            Status.Text = "Done"; 

        }

        protected void HipsterList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (String.Equals(e.CommandName, "DoHipStuff"))
            {
                string hipthing = e.CommandArgument.ToString();

                Status.Text = hipthing; 

                Hipflask h = new Hipflask();
                Status.Text = "Downloading";

                h.Download(hipthing);

                Status.Text = "Unpacking";
                h.Unpack(hipthing);

                Status.Text = "Installing";
                h.Install(hipthing);

                Status.Text = "Done " + hipthing;
            }
        }

        protected void update_Click(object sender, EventArgs e)
        {
            HipFlaskSettings s = new HipFlaskSettings();
            s.UpdateSettingsFile();
            Status.Text = "Updated Settings";
            Response.Redirect(Request.RawUrl);
        }
    }
}