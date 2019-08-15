using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Discord_RPC_Client
{
  public class GUI : Form
  {
    /// <summary>
    /// A <see cref="bool"/> for whether or not to close the <see cref="GUI"/>.
    /// </summary>
    public bool exit = false;

    /// <summary>
    /// The <see cref="Label"/> for the <see cref="Config.Identifiers.ClientID"/> <see cref="TextBox"/>.
    /// </summary>
    public Label clientID_Label;
    /// <summary>
    /// The <see cref="TextBox"/> for the <see cref="Config.Identifiers.ClientID"/> value.
    /// </summary>
    public TextBox clientID_TextBox;

    /// <summary>
    /// The <see cref="Label"/> for the <see cref="Config.Information.Details"/> <see cref="TextBox"/>.
    /// </summary>
    public Label details_Label;
    /// <summary>
    /// The <see cref="TextBox"/> for the <see cref="Config.Information.Details"/> value.
    /// </summary>
    public TextBox details_TextBox;

    /// <summary>
    /// The <see cref="Label"/> for the <see cref="Config.Information.State"/> <see cref="TextBox"/>.
    /// </summary>
    public Label state_Label;
    /// <summary>
    /// The <see cref="TextBox"/> for the <see cref="Config.Information.State"/> value.
    /// </summary>
    public TextBox state_TextBox;

    /// <summary>
    /// The <see cref="Label"/> for the <see cref="Config.Information.StartTimestamp"/> <see cref="TextBox"/>.
    /// </summary>
    public Label startTimestamp_Label;
    /// <summary>
    /// The <see cref="TextBox"/> for the <see cref="Config.Information.StartTimestamp"/> value.
    /// </summary>
    public TextBox startTimestamp_TextBox;

    /// <summary>
    /// The <see cref="Label"/> for the <see cref="Config.Information.EndTimestamp"/> <see cref="TextBox"/>.
    /// </summary>
    public Label endTimestamp_Label;
    /// <summary>
    /// The <see cref="TextBox"/> for the <see cref="Config.Information.EndTimestamp"/> value.
    /// </summary>
    public TextBox endTimestamp_TextBox;

    /// <summary>
    /// The <see cref="Label"/> for the <see cref="Config.Images.LargeImage"/> <see cref="TextBox"/>.
    /// </summary>
    public Label largeImage_Label;
    /// <summary>
    /// The <see cref="TextBox"/> for the <see cref="Config.Images.LargeImage"/> value.
    /// </summary>
    public TextBox largeImage_TextBox;

    /// <summary>
    /// The <see cref="Label"/> for the <see cref="Config.Images.LargeImageTooltip"/> <see cref="TextBox"/>.
    /// </summary>
    public Label largeImageTooltip_Label;
    /// <summary>
    /// The <see cref="TextBox"/> for the <see cref="Config.Images.LargeImageTooltip"/> value.
    /// </summary>
    public TextBox largeImageTooltip_TextBox;

    /// <summary>
    /// The <see cref="Label"/> for the <see cref="Config.Images.SmallImage"/> <see cref="TextBox"/>.
    /// </summary>
    public Label smallImage_Label;
    /// <summary>
    /// The <see cref="TextBox"/> for the <see cref="Config.Images.SmallImage"/> value.
    /// </summary>
    public TextBox smallImage_TextBox;

    /// <summary>
    /// The <see cref="Label"/> for the <see cref="Config.Images.SmallImageTooltip"/> <see cref="TextBox"/>.
    /// </summary>
    public Label smallImageTooltip_Label;
    /// <summary>
    /// The <see cref="TextBox"/> for the <see cref="Config.Images.SmallImageTooltip"/> value.
    /// </summary>
    public TextBox smallImageTooltip_TextBox;

    /// <summary>
    /// The <see cref="Label"/> for the <see cref="RPC.UpdateProgress"/> <see cref="ProgressBar"/>.
    /// </summary>
    public Label update_Label;
    /// <summary>
    /// The <see cref="ProgressBar"/> for the <see cref="RPC.UpdateProgress"/> value.
    /// </summary>
    public ProgressBar update_ProgressBar;

    /// <summary>
    /// The <see cref="Button"/> for forcing an <see cref="RPC.client"/> update.
    /// </summary>
    public Button forceUpdate_Button;
    /// <summary>
    /// The <see cref="Button"/> for forcing the <see cref="ConfigHandler"/> to save the <see cref="Config"/> to file.
    /// </summary>
    public Button saveConfig_Button;
    /// <summary>
    /// The <see cref="Button"/> for forcing the <see cref="ConfigHandler"/> to load the <see cref="Config"/> from file.
    /// </summary>
    public Button loadConfig_Button;
    /// <summary>
    /// The <see cref="Button"/> for telling the <see cref="GUI"/> to exit.
    /// </summary>
    public Button exit_Button;

    /// <summary>
    /// The initializer for the <see cref="GUI"/> class.
    /// </summary>
    public GUI()
    {
      // Set the controls of the Form itself.
      Size = new Size(420, 500);
      // Make it so that the Form cannot be resized.
      MaximizeBox = false;
      FormBorderStyle = FormBorderStyle.FixedSingle;
      // Handle this if the form closes.
      FormClosing += (source, e) =>
      {
        if (! exit)
        {
          // What ever you do, at the end of this method do not set Cancel to false because it'll bug the F out.
          e.Cancel = true;
          // Hide the GUI since we don't really need to close it (also since we cannot set the 'gui' variable in the App class to null).
          Hide();
        }
      };

      //  Set the controls of the input groups
      #region clientID Data
      clientID_Label = new Label
      {
        Text = "Client ID:",
        Location = new Point(5, 5),
        AutoSize = true
      };
      Controls.Add(clientID_Label);
      // ========================================
      clientID_TextBox = new TextBox
      {
        Text = ConfigHandler.config.GetIdentifiers().ClientID ?? "",
        Location = new Point(5, 25),
        Size = new Size(120, 15),
        MaxLength = 18
      };
      clientID_TextBox.TextChanged += new EventHandler(ClientID_TextBox_TextChanged);
      Controls.Add(clientID_TextBox);
      #endregion
      #region status Data
      state_Label = new Label
      {
        Text = "State:",
        Location = new Point(5, 95),
        AutoSize = true
      };
      Controls.Add(state_Label);
      // ========================================
      state_TextBox = new TextBox
      {
        Text = ConfigHandler.config.GetInformation().State ?? "",
        Location = new Point(5, 115),
        Size = new Size(395, 15)
      };
      state_TextBox.TextChanged += new EventHandler(State_TextBox_TextChanged);
      Controls.Add(state_TextBox);
      #endregion
      #region details Data
      details_Label = new Label
      {
        Text = "Details:",
        Location = new Point(5, 50),
        AutoSize = true
      };
      Controls.Add(details_Label);
      // ========================================
      details_TextBox = new TextBox
      {
        Text = ConfigHandler.config.GetInformation().Details ?? "",
        Location = new Point(5, 70),
        Size = new Size(395, 15)
      };
      details_TextBox.TextChanged += new EventHandler(Details_TextBox_TextChanged);
      Controls.Add(details_TextBox);
      #endregion
      #region startTimestamp Data
      startTimestamp_Label = new Label
      {
        Text = "Start Timestamp:",
        Location = new Point(5, 140),
        AutoSize = true
      };
      Controls.Add(startTimestamp_Label);
      // ========================================
      startTimestamp_TextBox = new TextBox
      {
        Text = ConfigHandler.config.GetInformation().StartTimestamp.ToString() ?? "0",
        Location = new Point(5, 160),
        Size = new Size(120, 15),
        MaxLength = 18
      };
      startTimestamp_TextBox.TextChanged += new EventHandler(StartTimestamp_TextBox_TextChanged);
      Controls.Add(startTimestamp_TextBox);
      #endregion
      #region endTimestamp Data
      endTimestamp_Label = new Label
      {
        Text = "End Timestamp:",
        Location = new Point(130, 140),
        AutoSize = true
      };
      Controls.Add(endTimestamp_Label);
      // ========================================
      endTimestamp_TextBox = new TextBox
      {
        Text = ConfigHandler.config.GetInformation().EndTimestamp.ToString() ?? "0",
        Location = new Point(130, 160),
        Size = new Size(120, 15),
        MaxLength = 18
      };
      endTimestamp_TextBox.TextChanged += new EventHandler(EndTimestamp_TextBox_TextChanged);
      Controls.Add(endTimestamp_TextBox);
      #endregion
      #region largeImage Data
      largeImage_Label = new Label
      {
        Text = "Large Image Key:",
        Location = new Point(5, 185),
        AutoSize = true
      };
      Controls.Add(largeImage_Label);
      // ========================================
      largeImage_TextBox = new TextBox
      {
        Text = ConfigHandler.config.GetImages().LargeImage ?? "",
        Location = new Point(5, 205),
        Size = new Size(395, 15)
      };
      largeImage_TextBox.TextChanged += new EventHandler(LargeImage_TextBox_TextChanged);
      Controls.Add(largeImage_TextBox);
      #endregion
      #region largeImageTooltip Data
      largeImageTooltip_Label = new Label
      {
        Text = "Large Image Tooltip:",
        Location = new Point(5, 230),
        AutoSize = true
      };
      Controls.Add(largeImageTooltip_Label);
      // ========================================
      largeImageTooltip_TextBox = new TextBox
      {
        Text = ConfigHandler.config.GetImages().LargeImageTooltip ?? "",
        Location = new Point(5, 250),
        Size = new Size(395, 15)
      };
      largeImageTooltip_TextBox.TextChanged += new EventHandler(SmallImageTooltip_TextBox_TextChanged);
      Controls.Add(largeImageTooltip_TextBox);
      #endregion
      #region largeImage Data
      smallImage_Label = new Label
      {
        Text = "Small Image Key:",
        Location = new Point(5, 275),
        AutoSize = true
      };
      Controls.Add(smallImage_Label);
      // ========================================
      smallImage_TextBox = new TextBox
      {
        Text = ConfigHandler.config.GetImages().SmallImage ?? "",
        Location = new Point(5, 295),
        Size = new Size(395, 15)
      };
      smallImage_TextBox.TextChanged += new EventHandler(SmallImage_TextBox_TextChanged);
      Controls.Add(smallImage_TextBox);
      #endregion
      #region smallImageTooltip Data
      smallImageTooltip_Label = new Label
      {
        Text = "Small Image Tooltip:",
        Location = new Point(5, 320),
        AutoSize = true
      };
      Controls.Add(smallImageTooltip_Label);
      // ========================================
      smallImageTooltip_TextBox = new TextBox
      {
        Text = ConfigHandler.config.GetImages().SmallImageTooltip ?? "",
        Location = new Point(5, 340),
        Size = new Size(395, 15)
      };
      smallImageTooltip_TextBox.TextChanged += new EventHandler(SmallImageTooltip_TextBox_TextChanged);
      Controls.Add(smallImageTooltip_TextBox);
      #endregion
      #region update Data
      update_Label = new Label
      {
        Text = "Update Ticker:",
        Location = new Point(5, 365),
        AutoSize = true
      };
      Controls.Add(update_Label);
      // ========================================
      update_ProgressBar = new ProgressBar
      {
        Maximum = 150,
        Location = new Point(5, 385),
        Size = new Size(395, 15)
      };
      Controls.Add(update_ProgressBar);
      #endregion
      #region button Data
      forceUpdate_Button = new Button
      {
        Text = "Force Update",
        Location = new Point(5, 410),
        AutoSize = true
      };
      forceUpdate_Button.Click += new EventHandler(ForceUpdate_Button_Click);
      Controls.Add(forceUpdate_Button);
      // ========================================
      saveConfig_Button = new Button
      {
        Text = "Save Config",
        Location = new Point(forceUpdate_Button.Location.X + forceUpdate_Button.Size.Width + 5, 410),
        AutoSize = true
      };
      saveConfig_Button.Click += new EventHandler(SaveConfig_Button_Click);
      Controls.Add(saveConfig_Button);
      // ========================================
      loadConfig_Button = new Button
      {
        Text = "Load Config",
        Location = new Point(saveConfig_Button.Location.X + saveConfig_Button.Size.Width + 5, 410),
        AutoSize = true
      };
      loadConfig_Button.Click += new EventHandler(LoadConfig_Button_Click);
      Controls.Add(loadConfig_Button);
      // ========================================
      exit_Button = new Button
      {
        Text = "Exit",
        Anchor = AnchorStyles.Right,
        Location = new Point(395, 410),
        AutoSize = true
      };
      exit_Button.Click += new EventHandler(Exit_Button_Click);
      Controls.Add(exit_Button);
      #endregion
    }
    
    /// <summary>
    /// Update the progress bar from outside the thread because it's hard to set it otherwise.
    /// </summary>
    /// <param name="value">The value to set the progress to (Typically set by a double, but can cast it to an int).</param>
    public void UpdateProgressBar(int value) => update_ProgressBar.Value = value;

    /// <summary>
    /// Check when the <see cref="clientID_TextBox"/> text changed.
    /// Makes sure all text in the <see cref="TextBox"/> are digits.
    /// Also sets the <see cref="Config.Identifiers.ClientID"/> value.
    /// And recreates the <see cref="RPC"/> client.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="TextBox"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void ClientID_TextBox_TextChanged(object sender, EventArgs e)
    {
      TextBox tb = sender as TextBox;
      if (!tb.Text.All(char.IsDigit))
      {
        // Get the caret position and set it after we concat. (Yes I know it bugs out when selecting, but using Windows forms doesn't work that well).
        int caretPos = tb.SelectionStart;
        tb.Text = string.Concat(tb.Text.Where(char.IsDigit));
        tb.SelectionStart = caretPos - 1;
        return;
      }
      else
      {
        // So if all the characters are Digits then set the config value.
        Invoke((MethodInvoker)delegate
        {
          ConfigHandler.SetValue(ref ConfigHandler.config.GetIdentifiers().ClientID, clientID_TextBox.Text);

          // // We don't really need this because the program seems to bug out when the RPC client gets De-initialized... I wonder why... (Jk)
          // try
          // {
          //   App.rpc.Deinitialize();
          //}
          //catch
          //{
          //  App.logger.Log("RPC Client already de-initialized.");
          //}

          // Just go ahead and recreate the RPC client.
          App.rpc.CreateRPCClient();
        });
      }
    }

    /// <summary>
    /// Get if the <see cref="details_TextBox"/> changed.
    /// Doesn't do anything special other than set the <see cref="Config.Information.Details"/> value.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="TextBox"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void Details_TextBox_TextChanged(object sender, EventArgs e) => Invoke((MethodInvoker)delegate
    {
      ConfigHandler.SetValue(ref ConfigHandler.config.GetInformation().Details, details_TextBox.Text);
    });

    /// <summary>
    /// Get if the <see cref="state_TextBox"/> changed.
    /// Doesn't do anything special other than set the <see cref="Config.Information.State"/> value.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="TextBox"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void State_TextBox_TextChanged(object sender, EventArgs e) => Invoke((MethodInvoker)delegate
    {
      ConfigHandler.SetValue(ref ConfigHandler.config.GetInformation().State, state_TextBox.Text);
    });

    /// <summary>
    /// Get if the <see cref="startTimestamp_TextBox"/> changed.
    /// Makes sure all text in the <see cref="TextBox"/> are digits.
    /// Also sets the <see cref="Config.Information.StartTimestamp"/> value.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="TextBox"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void StartTimestamp_TextBox_TextChanged(object sender, EventArgs e)
    {
      TextBox tb = sender as TextBox;
      if (! tb.Text.All(char.IsDigit))
      {
        // Get the caret position and set it after we concat. (Yes I know it bugs out when selecting, but using Windows forms doesn't work that well).
        int carretPos = tb.SelectionStart;
        tb.Text = string.Concat(tb.Text.Where(char.IsDigit));
        tb.SelectionStart = carretPos - 1;
        return;
      }
      else
      {
        // So if all the characters are Digits then set the config value.
        Invoke((MethodInvoker)delegate
        {
          // We need to make sure the string is still able to become a UInt64 so we try to parse it.
          // If it fails we just set the default.
          ulong startTimestamp_Test = ConfigHandler.config.GetInformation().StartTimestamp;
          ulong.TryParse(startTimestamp_TextBox.Text, out startTimestamp_Test);
          ConfigHandler.SetValue(ref ConfigHandler.config.GetInformation().StartTimestamp, startTimestamp_Test);
        });
      }
    }

    /// <summary>
    /// Get if the <see cref="endTimestamp_TextBox"/> changed.
    /// Makes sure all text in the <see cref="TextBox"/> are digits.
    /// Also sets the <see cref="Config.Information.EndTimestamp"/> value.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="TextBox"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void EndTimestamp_TextBox_TextChanged(object sender, EventArgs e)
    {
      // Get the caret position and set it after we concat. (Yes I know it bugs out when selecting, but using Windows forms doesn't work that well).
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
        // So if all the characters are Digits then set the config value.
        Invoke((MethodInvoker)delegate
        {
          // We need to make sure the string is still able to become a UInt64 so we try to parse it.
          // If it fails we just set the default.
          ulong endTimestamp_Test = ConfigHandler.config.GetInformation().EndTimestamp;
          ulong.TryParse(endTimestamp_TextBox.Text, out endTimestamp_Test);
          ConfigHandler.SetValue(ref ConfigHandler.config.GetInformation().EndTimestamp, endTimestamp_Test);
        });
      }
    }

    /// <summary>
    /// Get if the <see cref="largeImage_TextBox"/> changed.
    /// Doesn't do anything special other than set the <see cref="Config.Information.LargeImage"/> value.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="TextBox"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void LargeImage_TextBox_TextChanged(object sender, EventArgs e) => Invoke((MethodInvoker)delegate
    {
      ConfigHandler.SetValue(ref ConfigHandler.config.GetImages().LargeImage, largeImage_TextBox.Text);
    });

    /// <summary>
    /// Get if the <see cref="largeImageTooltip_TextBox"/> changed.
    /// Doesn't do anything special other than set the <see cref="Config.Information.LargeImageTooltip"/> value.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="TextBox"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void LargeImageTooltip_TextBox_TextChanged(object sender, EventArgs e) => Invoke((MethodInvoker)delegate
    {
      ConfigHandler.SetValue(ref ConfigHandler.config.GetImages().LargeImageTooltip, largeImageTooltip_TextBox.Text);
    });

    /// <summary>
    /// Get if the <see cref="smallImage_TextBox"/> changed.
    /// Doesn't do anything special other than set the <see cref="Config.Information.SmallImage"/> value.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="TextBox"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void SmallImage_TextBox_TextChanged(object sender, EventArgs e) => Invoke((MethodInvoker)delegate
    {
      ConfigHandler.SetValue(ref ConfigHandler.config.GetImages().SmallImage, smallImage_TextBox.Text);
    });

    /// <summary>
    /// Get if the <see cref="smallImageTooltip_TextBox"/> changed.
    /// Doesn't do anything special other than set the <see cref="Config.Information.SmallImageTooltip"/> value.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="TextBox"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void SmallImageTooltip_TextBox_TextChanged(object sender, EventArgs e) => Invoke((MethodInvoker)delegate
    {
      ConfigHandler.SetValue(ref ConfigHandler.config.GetImages().SmallImageTooltip, smallImageTooltip_TextBox.Text);
    });

    /// <summary>
    /// Forces the <see cref="RPC"/> client to update.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="Button"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void ForceUpdate_Button_Click(object sender, EventArgs e) => Invoke((MethodInvoker)delegate
    {
      App.UpdateRPC();
    });

    /// <summary>
    /// Forces the <see cref="ConfigHandler"/> to save the <see cref="Config"/> to file.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="Button"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void SaveConfig_Button_Click(object sender, EventArgs e) => Invoke((MethodInvoker)delegate
    {
      ConfigHandler.WriteConfig();
    });

    /// <summary>
    /// Forces the <see cref="ConfigHandler"/> to load the <see cref="Config"/> from file.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="Button"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void LoadConfig_Button_Click(object sender, EventArgs e) => Invoke((MethodInvoker)delegate { ConfigHandler.ReadConfig(); });

    public void UpdateTextBoxes() => Invoke((MethodInvoker)delegate
    {
      clientID_TextBox.Text = ConfigHandler.config.GetIdentifiers().ClientID;
      details_TextBox.Text = ConfigHandler.config.GetInformation().Details;
      state_TextBox.Text = ConfigHandler.config.GetInformation().State;
      startTimestamp_TextBox.Text = ConfigHandler.config.GetInformation().StartTimestamp.ToString();
      endTimestamp_TextBox.Text = ConfigHandler.config.GetInformation().EndTimestamp.ToString();
      largeImage_TextBox.Text = ConfigHandler.config.GetImages().LargeImage;
      largeImageTooltip_TextBox.Text = ConfigHandler.config.GetImages().LargeImageTooltip;
      smallImage_TextBox.Text = ConfigHandler.config.GetImages().SmallImage;
      smallImageTooltip_TextBox.Text = ConfigHandler.config.GetImages().SmallImageTooltip;
    });

    /// <summary>
    /// Closes the <see cref="GUI"/>.
    /// </summary>
    /// <param name="sender">The sending object (Typically a <see cref="Button"/>).</param>
    /// <param name="e">The event arguments.</param>
    private void Exit_Button_Click(object sender, EventArgs e) => Close();
  }
}
