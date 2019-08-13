using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Discord_RPC_Client
{
  public class GUI : Form
  {
    public bool exit = false;

    public Label clientID_Label;
    public TextBox clientID_TextBox;

    public Label details_Label;
    public TextBox details_TextBox;

    public Label state_Label;
    public TextBox state_TextBox;

    public Label startTimestamp_Label;
    public TextBox startTimestamp_TextBox;

    public Label endTimestamp_Label;
    public TextBox endTimestamp_TextBox;

    public Label largeImage_Label;
    public TextBox largeImage_TextBox;

    public Label largeImageTooltip_Label;
    public TextBox largeImageTooltip_TextBox;

    public Label smallImage_Label;
    public TextBox smallImage_TextBox;

    public Label smallImageTooltip_Label;
    public TextBox smallImageTooltip_TextBox;

    public Label update_Label;
    public ProgressBar update_ProgressBar;

    public Button forceUpdate_Button;
    public Button saveConfig_Button;
    public Button loadConfig_Button;
    public Button exit_Button;

    public GUI()
    {
      //*** Set the controls of the form itself.
      Size = new Size(420, 500);
      ResizeRedraw = false;
      MaximizeBox = false;
      FormBorderStyle = FormBorderStyle.FixedSingle;
      FormClosing += (source, e) =>
      {
        if (! exit)
        {
          e.Cancel = true;
          Hide();
        }
        e.Cancel = false;
      };

      //**  Set the controls of the input groups
      #region clientID Data
      clientID_Label = new Label();
      clientID_Label.Text = "Client ID:";
      clientID_Label.Location = new Point(5, 5);
      clientID_Label.AutoSize = true;
      Controls.Add(clientID_Label);
      // ========================================
      clientID_TextBox = new TextBox();
      clientID_TextBox.Text = ConfigHandler.config.Identifiers.ClientID ?? "";
      clientID_TextBox.Location = new Point(5, 25);
      clientID_TextBox.Size = new Size(120, 15);
      clientID_TextBox.MaxLength = 18;
      clientID_TextBox.TextChanged += new EventHandler(clientID_TextBox_TextChanged);
      Controls.Add(clientID_TextBox);
      #endregion
      #region status Data
      state_Label = new Label();
      state_Label.Text = "State:";
      state_Label.Location = new Point(5, 95);
      state_Label.AutoSize = true;
      Controls.Add(state_Label);
      // ========================================
      state_TextBox = new TextBox();
      state_TextBox.Text = ConfigHandler.config.Information.State ?? "";
      state_TextBox.Location = new Point(5, 115);
      state_TextBox.Size = new Size(395, 15);
      state_TextBox.TextChanged += new EventHandler(state_TextBox_TextChanged);
      Controls.Add(state_TextBox);
      #endregion
      #region details Data
      details_Label = new Label();
      details_Label.Text = "Details:";
      details_Label.Location = new Point(5, 50);
      details_Label.AutoSize = true;
      Controls.Add(details_Label);
      // ========================================
      details_TextBox = new TextBox();
      details_TextBox.Text = ConfigHandler.config.Information.Details ?? "";
      details_TextBox.Location = new Point(5, 70);
      details_TextBox.Size = new Size(395, 15);
      details_TextBox.TextChanged += new EventHandler(details_TextBox_TextChanged);
      Controls.Add(details_TextBox);
      #endregion
      #region startTimestamp Data
      startTimestamp_Label = new Label();
      startTimestamp_Label.Text = "Start Timestamp:";
      startTimestamp_Label.Location = new Point(5, 140);
      startTimestamp_Label.AutoSize = true;
      Controls.Add(startTimestamp_Label);
      // ========================================
      startTimestamp_TextBox = new TextBox();
      startTimestamp_TextBox.Text = ConfigHandler.config.Information.StartTimestamp.ToString() ?? "0";
      startTimestamp_TextBox.Location = new Point(5, 160);
      startTimestamp_TextBox.Size = new Size(120, 15);
      startTimestamp_TextBox.MaxLength = 18;
      startTimestamp_TextBox.TextChanged += new EventHandler(startTimestamp_TextBox_TextChanged);
      Controls.Add(startTimestamp_TextBox);
      #endregion
      #region endTimestamp Data
      endTimestamp_Label = new Label();
      endTimestamp_Label.Text = "End Timestamp:";
      endTimestamp_Label.Location = new Point(130, 140);
      endTimestamp_Label.AutoSize = true;
      Controls.Add(endTimestamp_Label);
      // ========================================
      endTimestamp_TextBox = new TextBox();
      endTimestamp_TextBox.Text = ConfigHandler.config.Information.EndTimestamp.ToString() ?? "0";
      endTimestamp_TextBox.Location = new Point(130, 160);
      endTimestamp_TextBox.Size = new Size(120, 15);
      endTimestamp_TextBox.MaxLength = 18;
      endTimestamp_TextBox.TextChanged += new EventHandler(endTimestamp_TextBox_TextChanged);
      Controls.Add(endTimestamp_TextBox);
      #endregion
      #region largeImage Data
      largeImage_Label = new Label();
      largeImage_Label.Text = "Large Image Key:";
      largeImage_Label.Location = new Point(5, 185);
      largeImage_Label.AutoSize = true;
      Controls.Add(largeImage_Label);
      // ========================================
      largeImage_TextBox = new TextBox();
      largeImage_TextBox.Text = ConfigHandler.config.Images.LargeImage ?? "";
      largeImage_TextBox.Location = new Point(5, 205);
      largeImage_TextBox.Size = new Size(395, 15);
      largeImage_TextBox.TextChanged += new EventHandler(largeImage_TextBox_TextChanged);
      Controls.Add(largeImage_TextBox);
      #endregion
      #region largeImageTooltip Data
      largeImageTooltip_Label = new Label();
      largeImageTooltip_Label.Text = "Large Image Tooltip:";
      largeImageTooltip_Label.Location = new Point(5, 230);
      largeImageTooltip_Label.AutoSize = true;
      Controls.Add(largeImageTooltip_Label);
      // ========================================
      largeImageTooltip_TextBox = new TextBox();
      largeImageTooltip_TextBox.Text = ConfigHandler.config.Images.LargeImageTooltip ?? "";
      largeImageTooltip_TextBox.Location = new Point(5, 250);
      largeImageTooltip_TextBox.Size = new Size(395, 15);
      largeImageTooltip_TextBox.TextChanged += new EventHandler(smallImageTooltip_TextBox_TextChanged);
      Controls.Add(largeImageTooltip_TextBox);
      #endregion
      #region largeImage Data
      smallImage_Label = new Label();
      smallImage_Label.Text = "Small Image Key:";
      smallImage_Label.Location = new Point(5, 275);
      smallImage_Label.AutoSize = true;
      Controls.Add(smallImage_Label);
      // ========================================
      smallImage_TextBox = new TextBox();
      smallImage_TextBox.Text = ConfigHandler.config.Images.SmallImage ?? "";
      smallImage_TextBox.Location = new Point(5, 295);
      smallImage_TextBox.Size = new Size(395, 15);
      smallImage_TextBox.TextChanged += new EventHandler(smallImage_TextBox_TextChanged);
      Controls.Add(smallImage_TextBox);
      #endregion
      #region smallImageTooltip Data
      smallImageTooltip_Label = new Label();
      smallImageTooltip_Label.Text = "Small Image Tooltip:";
      smallImageTooltip_Label.Location = new Point(5, 320);
      smallImageTooltip_Label.AutoSize = true;
      Controls.Add(smallImageTooltip_Label);
      // ========================================
      smallImageTooltip_TextBox = new TextBox();
      smallImageTooltip_TextBox.Text = ConfigHandler.config.Images.SmallImageTooltip ?? "";
      smallImageTooltip_TextBox.Location = new Point(5, 340);
      smallImageTooltip_TextBox.Size = new Size(395, 15);
      smallImageTooltip_TextBox.TextChanged += new EventHandler(smallImageTooltip_TextBox_TextChanged);
      Controls.Add(smallImageTooltip_TextBox);
      #endregion
      #region update Data
      update_Label = new Label();
      update_Label.Text = "Update Ticker:";
      update_Label.Location = new Point(5, 365);
      update_Label.AutoSize = true;
      Controls.Add(update_Label);
      // ========================================
      update_ProgressBar = new ProgressBar();
      update_ProgressBar.Maximum = 150;
      update_ProgressBar.Location = new Point(5, 385);
      update_ProgressBar.Size = new Size(395, 15);
      Controls.Add(update_ProgressBar);
      #endregion
      #region button Data
      forceUpdate_Button = new Button();
      forceUpdate_Button.Text = "Force Update";
      forceUpdate_Button.Location = new Point(5, 410);
      forceUpdate_Button.AutoSize = true;
      forceUpdate_Button.Click += new EventHandler(forceUpdate_Button_Click);
      Controls.Add(forceUpdate_Button);
      // ========================================
      saveConfig_Button = new Button();
      saveConfig_Button.Text = "Save Config";
      saveConfig_Button.Location = new Point(forceUpdate_Button.Location.X + forceUpdate_Button.Size.Width + 5, 410);
      saveConfig_Button.AutoSize = true;
      saveConfig_Button.Click += new EventHandler(saveConfig_Button_Click);
      Controls.Add(saveConfig_Button);
      // ========================================
      loadConfig_Button = new Button();
      loadConfig_Button.Text = "Load Config";
      loadConfig_Button.Location = new Point(saveConfig_Button.Location.X + saveConfig_Button.Size.Width + 5, 410);
      loadConfig_Button.AutoSize = true;
      loadConfig_Button.Click += new EventHandler(loadConfig_Button_Click);
      Controls.Add(loadConfig_Button);
      // ========================================
      exit_Button = new Button();
      exit_Button.Text = "Exit";
      exit_Button.Location = new Point(395 - exit_Button.Size.Width - 5, 410);
      exit_Button.AutoSize = true;
      exit_Button.Click += new EventHandler(exit_Button_Click);
      Controls.Add(exit_Button);
      #endregion
    }

    public void UpdateProgressBar(int value)
    {
      update_ProgressBar.Value = value;
    }

    private void clientID_TextBox_TextChanged(object sender, EventArgs e)
    {
      TextBox tb = sender as TextBox;
      if (!tb.Text.All(char.IsDigit))
      {
        int carretPos = tb.SelectionStart;
        tb.Text = string.Concat(tb.Text.Where(char.IsDigit));
        tb.SelectionStart = carretPos - 1;
        return;
      }
      else
      {
        Invoke((MethodInvoker)delegate
        {
          ConfigHandler.SetValue(ref ConfigHandler.config.Identifiers.ClientID, clientID_TextBox.Text);
        });
      }
    }

    private void details_TextBox_TextChanged(object sender, EventArgs e)
    {
      Invoke((MethodInvoker)delegate
      {
        ConfigHandler.SetValue(ref ConfigHandler.config.Information.Details, details_TextBox.Text);
      });
    }
    private void state_TextBox_TextChanged(object sender, EventArgs e)
    {
      Invoke((MethodInvoker)delegate
      {
        ConfigHandler.SetValue(ref ConfigHandler.config.Information.State, state_TextBox.Text);
      });
    }

    private void startTimestamp_TextBox_TextChanged(object sender, EventArgs e)
    {
      TextBox tb = sender as TextBox;
      if (! tb.Text.All(char.IsDigit))
      {
        int carretPos = tb.SelectionStart;
        tb.Text = string.Concat(tb.Text.Where(char.IsDigit));
        tb.SelectionStart = carretPos - 1;
        return;
      }
      else
      {
        Invoke((MethodInvoker)delegate
        {
          ulong startTimestamp_Test = ConfigHandler.config.Information.StartTimestamp;
          ulong.TryParse(startTimestamp_TextBox.Text, out startTimestamp_Test);
          ConfigHandler.SetValue(ref ConfigHandler.config.Information.StartTimestamp, startTimestamp_Test);
        });
      }
    }

    private void endTimestamp_TextBox_TextChanged(object sender, EventArgs e)
    {
      TextBox tb = sender as TextBox;
      if (!tb.Text.All(char.IsDigit))
      {
        int carretPos = tb.SelectionStart;
        tb.Text = string.Concat(tb.Text.Where(char.IsDigit));
        tb.SelectionStart = carretPos - 1;
        return;
      }
      else
      {
        Invoke((MethodInvoker)delegate
        {
          ulong endTimestamp_Test = ConfigHandler.config.Information.EndTimestamp;
          ulong.TryParse(endTimestamp_TextBox.Text, out endTimestamp_Test);
          ConfigHandler.SetValue(ref ConfigHandler.config.Information.EndTimestamp, endTimestamp_Test);
        });
      }
    }

    private void largeImage_TextBox_TextChanged(object sender, EventArgs e)
    {
      Invoke((MethodInvoker)delegate
      {
        ConfigHandler.SetValue(ref ConfigHandler.config.Images.LargeImage, largeImage_TextBox.Text);
      });
    }

    private void largeImageTooltip_TextBox_TextChanged(object sender, EventArgs e)
    {
      Invoke((MethodInvoker)delegate
      {
        ConfigHandler.SetValue(ref ConfigHandler.config.Images.LargeImageTooltip, largeImageTooltip_TextBox.Text);
      });
    }

    private void smallImage_TextBox_TextChanged(object sender, EventArgs e)
    {
      Invoke((MethodInvoker)delegate
      {
        ConfigHandler.SetValue(ref ConfigHandler.config.Images.SmallImage, smallImage_TextBox.Text);
      });
    }

    private void smallImageTooltip_TextBox_TextChanged(object sender, EventArgs e)
    {
      Invoke((MethodInvoker)delegate
      {
        ConfigHandler.SetValue(ref ConfigHandler.config.Images.SmallImageTooltip, smallImageTooltip_TextBox.Text);
      });
    }

    private void forceUpdate_Button_Click(object sender, EventArgs e)
    {
      Invoke((MethodInvoker)delegate
      {
        App.logger.Log("Updating RPC");
        App.rpc.Update();
      });
    }

    private void saveConfig_Button_Click(object sender, EventArgs e)
    {
      Invoke((MethodInvoker)delegate
      {
        ConfigHandler.WriteConfig();
      });
    }

    private void loadConfig_Button_Click(object sender, EventArgs e)
    {
      Invoke((MethodInvoker)delegate
      {
        ConfigHandler.ReadConfig();
        clientID_TextBox.Text = ConfigHandler.config.Identifiers.ClientID;
        details_TextBox.Text = ConfigHandler.config.Information.Details;
        state_TextBox.Text = ConfigHandler.config.Information.State;
        startTimestamp_TextBox.Text = ConfigHandler.config.Information.StartTimestamp.ToString();
        endTimestamp_TextBox.Text = ConfigHandler.config.Information.EndTimestamp.ToString();
        largeImage_TextBox.Text = ConfigHandler.config.Images.LargeImage;
        largeImageTooltip_TextBox.Text = ConfigHandler.config.Images.LargeImageTooltip;
        smallImage_TextBox.Text = ConfigHandler.config.Images.SmallImage;
        smallImageTooltip_TextBox.Text = ConfigHandler.config.Images.SmallImageTooltip;
      });
    }

    private void exit_Button_Click(object sender, EventArgs e)
    {
      exit = true;
      Close();
    }
  }
}
