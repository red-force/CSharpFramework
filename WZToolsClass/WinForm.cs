#define Worker
//#define BackgroundWorker
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RF
{
    public partial class GlobalClass
    {
        /// <summary>
        /// for System.Windows.Forms
        /// </summary>
        public partial class WinForm
        {
            #region ControlCollection
            /// <summary>
            /// the collection of control
            /// </summary>
            /// <example>
            /// <see cref="WinForm.ControlCollection.fillControl(String,String,float,float,String)"/>
            /// </example>
            public partial class ControlCollection
            {
                private System.Windows.Forms.Control[] _controlCollection;
                private float maxFontSize = 30;

                /// <summary>
                /// Gets or Sets the max limitation of the font size;
                /// </summary>
                public float MaxFontSize
                {
                    get { return maxFontSize; }
                    set { maxFontSize = value; }
                }
                private float minFontSize = 9;

                /// <summary>
                /// Gets or Sets the min limitation of the font size
                /// </summary>
                public float MinFontSize
                {
                    get { return minFontSize; }
                    set { minFontSize = value; }
                }
                /// <summary>
                /// contain all the controls.
                /// </summary>
                /// <example>
                /// <code language="C#" title="Init ControlCollection And Fill Text">
                /// // init product detail info
				/// RF.GlobalClass.WinForm.ControlCollection rflabels = new RF.GlobalClass.WinForm.ControlCollection(new Control[] {
				///     labelName,
				///     labelMaxPreSaleTime,
				///     labelMinPreSaleTime,
				///     labelOpenTime,
				///     labelCloseTime,
				///     labelEffectiveType,
				///     labelEffectiveDayNum,
				///     labelEntryType,
				///     labelUseCount,
				///     labelBuyLimitation,
				///     labelVisitorType,
				///     labelPrice
				/// });
				/// // 迭代，赋值。
				/// rflabels.fillControl(labelName, product.name,
				/// maxWidth: rflabels.fillControl(labelMaxPreSaleTime, Dict.translate("dayNum", product.maxPreSaleTime),
				/// maxWidth: rflabels.fillControl(labelMinPreSaleTime, Dict.translate("dayNum", product.minPreSaleTime),
				/// maxWidth: rflabels.fillControl(labelOpenTime, product.openTime,
				/// maxWidth: rflabels.fillControl(labelCloseTime, product.closeTime,
				/// maxWidth: rflabels.fillControl(labelEffectiveType, product.effectiveType,
				/// maxWidth: rflabels.fillControl(labelEffectiveDayNum, Dict.translate("dayNum", product.effectiveDayNum),
				/// maxWidth: rflabels.fillControl(labelEntryType, product.entryType,
				/// maxWidth: rflabels.fillControl(labelUseCount, Dict.translate("useCount", product.useCount),
				/// maxWidth: rflabels.fillControl(labelBuyLimitation, Dict.translate("ticketPurchaseLimitationType", (Dict.translateBack("VISITOR_TYPE", product.visitorType))) + Dict.translate("ticketNum", product.buyLimitation),
				/// maxWidth: rflabels.fillControl(labelVisitorType, product.visitorType,
				/// maxWidth: rflabels.fillControl(labelPrice, Dict.translate("price", product.price), minWidth: 11
				/// ))))))))))));
				/// rflabels.adjustFontSize();
                /// </code>
                /// </example>
                public System.Windows.Forms.Control[] Controls
                {
                    get { return _controlCollection; }
                    set { _controlCollection = value; }
                }

                /// <summary>
                /// the collection of control
                /// </summary>
                /// <param name="controls"></param>
                public ControlCollection(System.Windows.Forms.Control[] controls)
                {
                    this._controlCollection = controls;
                }

                /// <summary>
                /// fill text to control
                /// </summary>
                /// <param name="control">the Control ,which is stored in the ControlCollection, to fill to.</param>
                /// <param name="text">the text content to fill</param>
                /// <param name="minWidth">the minimum width of the font</param>
                /// <param name="maxWidth">the maximum width of the font</param>
                /// <param name="fontFamily">font size</param>
                /// <returns>font size</returns>
                /// <example>
                /// <see cref="WinForm.ControlCollection.fillControl(String,String,float,float,String)"/>
                /// </example>
                public float fillControl(System.Windows.Forms.Control control, string text, float minWidth = -1f, float maxWidth = -1f, String fontFamily = "宋体")
                {
                    float result = 0;
                    Func<System.Windows.Forms.Control, String, float> _fillControl = delegate(System.Windows.Forms.Control _control, String _text)
                    {
                        _control.Text = _text;
                        minWidth = -1 == minWidth ? this.MinFontSize : minWidth;
                        float _fontSize = -1 == maxWidth ? _control.Size.Height - 0 : maxWidth;
                        System.Drawing.Font f = new System.Drawing.Font(fontFamily, _fontSize, System.Drawing.GraphicsUnit.Point);
                        System.Drawing.Size _size = System.Windows.Forms.TextRenderer.MeasureText(_text, f);
                        for (float i = _fontSize; i > minWidth; i--)
                        {
                            f = new System.Drawing.Font(fontFamily, i, System.Drawing.GraphicsUnit.Point);
                            _size = System.Windows.Forms.TextRenderer.MeasureText(_text, f);
                            if (_size.Height > _control.Size.Height)
                            {
                                _fontSize = i;
                                continue;
                            }
                            else
                            {
                                if (_size.Width > _control.Size.Width)
                                {
                                    _fontSize = i;
                                    continue;
                                }
                                else
                                {
                                    _fontSize = i;
                                    break;
                                }
                            }

                        }
                        _control.Font = new System.Drawing.Font(new System.Drawing.FontFamily(fontFamily), _fontSize, System.Drawing.GraphicsUnit.Point);
                        return _fontSize;
                    };
                    result = _fillControl(control, text);
                    #region compute the max and min font size
                    //this.MaxFontSize = Math.Max(float.Parse(result.ToString()), this.MaxFontSize);
                    //this.MinFontSize = 0 == this.MinFontSize ? this.MaxFontSize : this.MinFontSize;
                    //this.MinFontSize = Math.Min(float.Parse(result.ToString()), this.MinFontSize);
                    #endregion
                    return result;
                }

                /// <summary>
                /// fill text to control
                /// </summary>
                /// <param name="controlName">the name of the Control ,which is stored in the ControlCollection, to fill to.</param>
                /// <param name="text">the text content to fill</param>
                /// <param name="minWidth">the minimum width of the font</param>
                /// <param name="maxWidth">the maximum width of the font</param>
                /// <param name="fontFamily">font size</param>
                /// <returns>font size</returns>
                /// <example>
                /// <code language="C#" title="Fill the TextBox">
                /// RF.GlobalClass.WinForm.ControlCollection wfcc;
                /// wfcc = new RF.GlobalClass.WinForm.ControlCollection(new Control[] { textBoxKeywords });
                /// String mesg = "Hello Text World";
                /// wfcc.fillControl("textBoxKeywords", mesg);
                /// </code>
                /// </example>
                public float fillControl(String controlName, String text, float minWidth = -1f, float maxWidth = -1f, String fontFamily = "宋体")
                {
                    float result = 0;
                    System.Windows.Forms.Control control = this.Controls.Where(delegate(System.Windows.Forms.Control _control)
                    {
                        Boolean _result = false;
                        if (_control.Name == controlName)
                        {
                            _result = true;
                        }
                        else
                        {
                            // _result = false;
                        }
                        return _result;
                    }).FirstOrDefault();
                    result = fillControl(control: control, text: text, minWidth: minWidth, maxWidth: maxWidth, fontFamily: fontFamily);
                    return result;
                }
                /// <summary>
                /// the max size of font dedicate from all the control
                /// </summary>
                public float FontSizeMax { get; set; }
                /// <summary>
                /// the min size of font dedicate from all the control
                /// </summary>
                public float FontSizeMin { get; set; }

                /// <summary>
                /// adjust the Font Size of all the control with the specified font size value.
                /// </summary>
                /// <param name="fontSize"></param>
                public void adjustFontSize(float fontSize)
                {
                    this.Controls.Select(delegate(System.Windows.Forms.Control _control)
                    {
                        _control.Font = new System.Drawing.Font(_control.Font.FontFamily, fontSize);
                        return _control;
                    }).ToArray();
                }
                /// <summary>
                /// adjust the Font Size of all the control with font size min value.
                /// </summary>
                /// <example>
                /// <code language="C#" title="adjust font size of the whole collection of control">
                /// private void displayProductOption()
				///	{
				///		Product product;
				///		Button btn;
				///		PictureBox pb;
				///		float fontSize = 23;
				///		List&lt;Control&gt; controls = new List&lt;Control&gt;();
				///		for(int i = 0; i&lt; pageSize; i++){
				///			product = productCollection.Get(pageNum * pageSize + i);
				///			btn = this.Controls["ButtonOption" + (i + 1)] as Button;
				///			btn.Text = (product ?? new Product()).name;
				///			btn.Enabled = (btn.Text != "");
				///			pb = this.Controls["pictureBoxProductOption" + (i + 1)] as PictureBox;
				///			pb.Text = (product ?? new Product()).name;
				///			pb.Padding = new System.Windows.Forms.Padding(10, 20, 20, 10);
				///			pb.Enabled = ((pb as Control).Text != "");
				///			fontSize = (btn.Text.Length &gt; 9 ? 9 * 23 / btn.Text.Length : 23);
				///			fontSize = fontSize &lt; 11 ? 11 : fontSize;
				///			btn.Font = new Font(new FontFamily("宋体"), fontSize);
				///			pb.Font = new Font(new FontFamily("宋体"), fontSize);
				///			unselectOptionButton(btn);
				///			//unselectOptionButton(pb);
				///			controls.Add(pb);
				///		}
				///		RF.GlobalClass.WinForm.ControlCollection rfControls 
				///			= new RF.GlobalClass.WinForm.ControlCollection(controls.ToArray());
				///		rfControls.adjustFontSize();
				///		for (int i = 0; i &lt; pageSize; i++)
				///		{
				///			rfControls.Controls.Select(delegate(System.Windows.Forms.Control _control)
				///			{
				///				Control result = null;
				///				unselectOptionButton(_control as PictureBox);
				///				return result;
				///			}).ToArray();
				///		}
				///	}
                /// </code>
                /// </example>
                public void adjustFontSize()
                {
                    this.reloadFontSizeMin();
                    this.Controls.Select(delegate(System.Windows.Forms.Control _control)
                    {
                        _control.Font = new System.Drawing.Font(_control.Font.FontFamily, this.FontSizeMin);
                        return _control;
                    }).ToArray();
                }
                /// <summary>
                /// reload max font size
                /// </summary>
                /// <returns></returns>
                public float reloadFontSizeMax()
                {
                    float result = 0;
                    this.Controls.Select(delegate(System.Windows.Forms.Control _control)
                    {
                        this.FontSizeMax = Math.Max(_control.Font.Size, this.FontSizeMax);
                        return _control;
                    }).ToArray();
                    result = this.FontSizeMax;
                    return result;
                }
                /// <summary>
                /// reload min font size
                /// </summary>
                /// <returns></returns>
                public float reloadFontSizeMin()
                {
                    float result = 0;
                    this.Controls.Select(delegate(System.Windows.Forms.Control _control)
                    {
                        this.FontSizeMin = 0 == this.FontSizeMin ? _control.Font.Size : this.FontSizeMin;
                        this.FontSizeMin = Math.Min(_control.Font.Size, this.FontSizeMin);
                        return _control;
                    }).ToArray();
                    result = this.FontSizeMin;
                    return result;
                }

                /// <summary>
                /// reload both max and min font size
                /// </summary>
                public void reloadFontSize()
                {
                    this.Controls.Select(delegate(System.Windows.Forms.Control _control)
                    {
                        this.FontSizeMax = Math.Max(_control.Font.Size, this.FontSizeMax);
                        this.FontSizeMin = 0 == this.FontSizeMin ? this.FontSizeMax : this.FontSizeMin;
                        this.FontSizeMin = Math.Min(_control.Font.Size, this.FontSizeMin);
                        return _control;
                    }).ToArray();
                }

                /// <summary>
                /// input text to the text field, support to replace the selected text.
                /// </summary>
                /// <param name="currTextBox"></param>
                /// <param name="value">0,1,2,3,4,5,6,7,8,9,0,Backspace,Clear</param>
                public static void inputToTextBoxOneByOne(System.Windows.Forms.TextBox currTextBox, string value)
                {
                    int startIndex = currTextBox.SelectionStart;
                    switch (value)
                    {
                        case "0":
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            if (0 < currTextBox.SelectionLength)
                            {
                                currTextBox.Text = currTextBox.Text.Remove(startIndex, currTextBox.SelectionLength);
                                currTextBox.Text = currTextBox.Text.Insert(startIndex, (value));
                            }
                            else
                            {
                                currTextBox.Text = currTextBox.Text.Insert(startIndex, (value));
                            }
                            startIndex++;
                            break;
                        case "Backspace":
                            if (currTextBox.SelectionLength > 0)
                            {
                                currTextBox.Text = currTextBox.Text.Remove(startIndex, currTextBox.SelectionLength);
                            }
                            else
                            {
                                if (currTextBox.SelectionStart > 0)
                                {
                                    currTextBox.Text = currTextBox.Text.Remove(startIndex - 1, 1);
                                    startIndex--;
                                }
                                else
                                {
                                    // do nothing
                                }
                            }
                            break;
                        case "Clear":
                            currTextBox.Text = "";
                            break;
                        default: break;
                    }
                    currTextBox.Focus();
                    currTextBox.DeselectAll();
                    currTextBox.SelectionStart = startIndex;
                }
            }

            #endregion

            /// <summary>
            /// Message 消息
            /// </summary>
            /// <example>
            /// <see cref="WinForm.Message.Show(System.Windows.Forms.Control, System.Windows.Forms.Label, System.Windows.Forms.Panel, string, int, int, int, float, float)"/>
            /// </example>
            public partial class Message
            {

                /// <summary>
                /// Show Message, need message panel with message label inside, and a timer.
                /// </summary>
                /// <param name="formControl">the form to hold the panelDisplay 消息展示Panel所屬的Form</param>
                /// <param name="labelMessage">the message label instance in the form.用於展示消息的Label，內嵌於Panel中。</param>
                /// <param name="panelDisplay">the panel to display the message in the form 用於展示消息的Panel</param>
                /// <param name="text">the message to display 需要展示的消息</param>
                /// <param name="delayShowTime">the time to delay before the message to show 延時展示的時間長度</param>
                /// <param name="showTime">how long the message is shown.(use default setting is recommended) 消息展示的時間長度（建議不提供由系統智能設置）
                /// <para>-1:smartMode 智能设置, 0: show forever 保持显示</para></param>
                /// <param name="intervalTime">[NOTICE]temporarily not supported 暫時不支持</param>
                /// <param name="minFontWidth">the minimum width of the font to show.消息字體最小寬度</param>
                /// <param name="maxFontWidth">the maximum width of the font to show.消息字體最大寬度</param>
                /// <returns>messageShowResult contains {"panelDisplay":System.Windows.Forms.Panel
                /// ,"labelMessage":System.Windows.Forms.Label
                /// , this.ThreadShowName: WinForm.Thread
                /// , this.ThreadHideName:WinForm.Thread}</returns>
                /// <example>
                /// <code language="C#" title="Show Message ">
                /// RF.GlobalClass.WinForm.Message rfwfMessage = new RF.GlobalClass.WinForm.Message();
                /// rfwfMessage.Show(labelMessage: labelMessage, panelDisplay: panelMessage
                /// , text:Dict.translate("MESSAGE", "Please choose the date to visit"));
                /// </code>
                /// <code language="C#" title="Deal with the mesage show result">
                /// rfwfMessage.LabelMessage = labelMessage;
				/// rfwfMessage.PanelDisplay = panelMessage;
				/// Dictionary&lt;string,object&gt; messageShowResult = rfwfMessage.Show(labelMessage: labelMessage, panelDisplay: panelMessage
                /// , text: Dict.translate("MESSAGE", "loading") + Dict.translate("PUNCTUATION","..."), delayShowTime: 0, showTime:0);
				/// RF.GlobalClass.WinForm.Thread rfwfThread = new RF.GlobalClass.WinForm.Thread(threadName: "QueryScenics"
                /// , workerFunc: (RF.GlobalClass.WinForm.Thread.workerFunctionDelegate)delegate()
				/// {
				///     QueryScenics();
				///     try
				///     {
                ///         if(rfwfMessage.ThreadHide == (RF.GlobalClass.WinForm.Thread)messageShowResult[rfwfMessage.ThreadHideName])){
				///             ((RF.GlobalClass.WinForm.Thread)messageShowResult[rfwfMessage.ThreadHideName]).WorkerThread.Abort();
                ///         }else{/*will not be accessed.*/}
				///     }
				///     catch (NullReferenceException nre)
				///     {
				///         // There will be an NullReferenceException, only because the rfwfMessage's showTime is zero
				///         // , which means there will not be a thread for hiding the display panel.
				///     }
				///     panelMessage.Hide();
				/// }, formControl: this, delayExecuteTime:700, sleepTime:10);
				/// rfwfThread.Start();
                /// </code>
                /// </example>
                public Dictionary<string,object> Show(System.Windows.Forms.Control formControl = null, System.Windows.Forms.Label labelMessage = null, System.Windows.Forms.Panel panelDisplay = null, string text = null, int delayShowTime = 100, int showTime = -1, int intervalTime = 1000, float minFontWidth = -1, float maxFontWidth = -1)
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    // 
                    labelMessage = labelMessage ?? this.LabelMessage ?? new System.Windows.Forms.Label();
                    panelDisplay = panelDisplay ?? this.PanelDisplay ?? new System.Windows.Forms.Panel();
                    text = text ?? labelMessage.Text ?? "";
                    result.Add("panelDisplay", panelDisplay);
                    result.Add("labelMessage", labelMessage);
                    result.Add(this.ThreadShowName, null);
                    result.Add(this.ThreadHideName, null);
                    //timer = timer ?? new System.Windows.Forms.Timer();
                    //timer.Interval = delayShowTime;

                    formControl = formControl ?? panelDisplay.FindForm() ?? labelMessage.FindForm();
                    if (null == formControl) // only when neither formContrl nor labelMessage nor panelDisplay have been provided.
                    {
                        return result;
                    }
                    else
                    {
                        // continue;
                    }

                    WinForm.Thread.workerFunctionDelegate wfdShow;
                    WinForm.Thread wftShow;
                    WinForm.Thread.workerFunctionDelegate wfdHide;
                    WinForm.Thread wftHide;
                    int sleepTime = 100;
                    delayShowTime = (0 < delayShowTime ? delayShowTime : (0));
                    this.DelayShowTime = delayShowTime;
                    showTime = (0 < showTime ? showTime : (0 == showTime ? showTime : GetShowTime(text))); //(labelMessage.Text.Length / 3 + 1) * (1000));
                    this.ShowTime = showTime;

                    #region hide panel
                    wfdHide = delegate()
                    {

                        if (null != panelDisplay && panelDisplay.Visible == true)
                        {

                            panelDisplay.Hide();
                            if (true == this.Closed)
                            {
                                return;
                            }
                        }
                        else
                        {
                            // do nothing
                        }
                    };
                    wftHide = new Thread(threadName: "_s_m_h_" + (text.ToArray().Length > 1 ? ((int)text.ToArray()[0]).ToString() : "") + text.Length.ToString(), workerFunc: wfdHide, formControl: formControl, sleepTime: sleepTime, delayExecuteTime: showTime);

                    result[this.ThreadHideName] = wftHide;
                    this.ThreadHide = wftHide;
                    #endregion

                    #region show panel
                    wfdShow = delegate()
                    {
                        #region check formControl status
                        if (null == formControl)
                        {
                            return;
                        }
                        else
                        {
                            // continue;
                        }
                        #endregion

                        #region make sure there is panelDisplay
                        if (!formControl.Controls.Contains(panelDisplay))
                        {
                            // only when the labelMessage and panelDisplay are not provided.
                            // 
                            #region // panelMessage
                            // 
                            panelDisplay.BackColor = System.Drawing.Color.Transparent;
                            panelDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
                            panelDisplay.BackgroundImage = ResourceImage.messageBox;
                            panelDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;

                            panelDisplay.Cursor = System.Windows.Forms.Cursors.Hand;
                            panelDisplay.Size = new System.Drawing.Size(632, 267);
                            panelDisplay.Location = new System.Drawing.Point(
                                ((formControl.Width - panelDisplay.Size.Width) / 2) //256
                                , ((formControl.Height - panelDisplay.Size.Height) / 2) //288
                                );
                            panelDisplay.Name = "__panelDisplay";
                            //panelDisplay.TabIndex = 9;
                            panelDisplay.Visible = false;
                            panelDisplay.Click += new System.EventHandler(delegate(object sender, EventArgs e)
                            {
                                panelDisplay.Hide();
                            });
                            #endregion

                            formControl.Controls.Add(panelDisplay);
                        }
                        else
                        {
                            // do nothing
                        }
                        this.PanelDisplay = panelDisplay;
                        #endregion

                        #region make sure there is labelMessage
                        if (!panelDisplay.Controls.Contains(labelMessage))
                        {
                            // 
                            #region// labelMessage
                            // 
                            labelMessage.AutoEllipsis = true;
                            labelMessage.BackColor = System.Drawing.Color.Transparent;
                            labelMessage.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                            labelMessage.ForeColor = System.Drawing.Color.IndianRed;
                            labelMessage.Location = new System.Drawing.Point(135, 47);
                            labelMessage.Name = "__labelMessage";
                            labelMessage.Size = new System.Drawing.Size(470, 140);
                            //labelMessage.TabIndex = 0;
                            labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                            labelMessage.Click += new System.EventHandler(delegate(object sender, EventArgs e)
                            {
                                panelDisplay.Hide();
                            });
                            panelDisplay.Controls.Add(labelMessage);
                            #endregion
                        }
                        else
                        {
                            // do nothing
                        }
                        this.LabelMessage = labelMessage;
                        #endregion

                        #region add Text
                        if (null != text)
                        {
                            //labelMessage.Text = text;
                            ControlCollection cc = new ControlCollection((new System.Windows.Forms.Control[] { labelMessage }));
                            cc.fillControl(labelMessage.Name, text, minWidth: minFontWidth, maxWidth: maxFontWidth);
                        }
                        else
                        {
                            // do not change text
                        }
                        #endregion

                        if (true != this.Closed)// || panelDisplay.Visible == false)
                        {
                            panelDisplay.BringToFront();
                            panelDisplay.Show();
                            this.Visible = true;
                            this.Closed = false;
                            if (0 < showTime && text == this.LabelMessage.Text)
                            {
                                wftHide.Start();
                            }
                            else
                            {
                                // do nothing no hide.
                            }
                        }
                        else
                        {
                            // do nothing
                            return;
                        }
                    };
                    wftShow = new Thread(threadName: "_s_m_s_" + (text.ToArray().Length > 1 ? ((int)text.ToArray()[0]).ToString() : "") + text.Length.ToString(), workerFunc: wfdShow, formControl: formControl, sleepTime: sleepTime, delayExecuteTime: delayShowTime);
                    result[this.ThreadShowName] = wftShow;
                    this.ThreadShow = wftShow;
                    wftShow.Start();
                    #endregion

                    #region fold

                    /*
                timer.Tick += new EventHandler(delegate(object sender, EventArgs e)
                {
                    try
                    {
                        int sleepTime = 100;
                        timer.Interval = intervalTime;
                        if (null == panelDisplay)
                        {
                            timer.Stop();
                            return;
                        }
                        else
                        {
                            if (panelDisplay.Visible == false)
                            {
                                timer.Stop();
                                if (null != text)
                                {
                                    labelMessage.Text = text;
                                }
                                else
                                {
                                    // do not change text
                                }
                                showTime = (-1 != showTime ? showTime : (labelMessage.Text.Length / 3 + 1) * (1000));
                                //panelMessage.Show();
                                panelDisplay.Show();
                                for (int i = 0; i < (showTime / sleepTime); i++)
                                {
                                    System.Windows.Forms.Application.DoEvents();
                                    System.Threading.Thread.Sleep((sleepTime));
                                }
                                panelDisplay.Hide();
                            }
                            else
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                });
                timer.Enabled = true;
                timer.Start();
                 * */
                    #endregion

                    return result;
                }

                /// <summary>
                /// Hide Message;
                /// If the display panel has not been shown, it will still be shown latter.
                /// </summary>
                public void Hide()
                {
                    if (null != this.PanelDisplay && this.PanelDisplay.Visible == true)
                    {
                        this.PanelDisplay.Hide();
                    }
                    else
                    {
                        // do nothing
                    }
                }

                /// <summary>
                /// Close Message
                /// If the display panel has not been shown, it will not be shown.
                /// </summary>
                public void Close()
                {
                    this.Visible = false;
                    this.Closed = true;
                    this.Hide();
                }

                /// <summary>
                /// Compute the time to show message base on the length of the text.
                /// </summary>
                /// <param name="text"></param>
                /// <returns></returns>
                private static int GetShowTime(String text)
                {
                    int result =0;
                    result = ((text.Length / 3 + 1) * (1000));
                    return result;
                }

                private int _showTime;

                /// <summary>
                /// the time to show message.
                /// </summary>
                public int ShowTime
                {
                    get { return _showTime; }
                    set { _showTime = value; }
                }

                private System.Windows.Forms.Label _labelMessage;
                /// <summary>
                /// the label to show message
                /// </summary>
                public System.Windows.Forms.Label LabelMessage
                {
                    get { return _labelMessage; }
                    set { _labelMessage = value; }
                }

                private System.Windows.Forms.Panel _panelDisplay;
                /// <summary>
                /// the panel to display message label
                /// </summary>
                public System.Windows.Forms.Panel PanelDisplay
                {
                    get { return _panelDisplay; }
                    set { _panelDisplay = value; }
                }

                /// <summary>
                /// get or set the visibility value of the Message.
                /// 獲取 或 設置 Message的可見性的值。（注意，並不直接影響其可見性，但visible改變時，可以及時結束相關等待線程。）
                /// </summary>
                public bool Visible { get; set; }

                public bool Closed { get; set; }

                /// <summary>
                /// the time to delay the show
                /// 用於控制延遲展示的時間
                /// </summary>
                public int DelayShowTime { get; set; }

                private string _threadHideName = "_s_m_threadHideName";

                /// <summary>
                /// the name of the thread used to hide the display panel
                /// 用於隱藏展示消息的Panel的線程
                /// </summary>
                public string ThreadHideName
                {
                    get { return _threadHideName; }
                    set { _threadHideName = value; }
                }

                private string _threadShowName = "_s_m_threadShowName";

                /// <summary>
                /// the name of the thread used to show the display panel
                /// 用於顯示展示消息的Panel的線程
                /// </summary>
                public string ThreadShowName
                {
                    get { return _threadShowName; }
                    set { _threadShowName = value; }
                }

                public Thread ThreadHide { get; set; }

                public Thread ThreadShow { get; set; }
            }

            #region Thread
            /// <summary>
            /// Multi Thread for WinForm 為WinForm程序提供的多線程封裝
            /// </summary>
            /// <example>
            /// <see cref="WinForm.Thread.Thread(String, Delegate, System.Windows.Forms.Control, int,int )"/>
            /// </example>
            public partial class Thread
            {
                /// <summary>
                /// Multi Thread for WinForm
                /// </summary>
                public Thread()
                {
                }

                /// <summary>
                /// the delegate for the method to be invoke by the FormControl.
                /// </summary>
                public delegate void workerFunctionDelegate();
                private Worker _worker;
                private System.Threading.Thread _workerThread;
                /// <summary>
                /// the new Thread
                /// </summary>
                public System.Threading.Thread WorkerThread
                {
                    get { return _workerThread; }
                    set { _workerThread = value; }
                }

                private System.Windows.Forms.Control _formControl;

                /// <summary>
                /// the WinForm instance
                /// </summary>
                public System.Windows.Forms.Control FormControl
                {
                    get { return _formControl; }
                    set { _formControl = value; }
                }
                // private System.Web.UI.Control _webControl;

                /// <summary>
                /// the method that FormControl will invoke.
                /// </summary>
                public Delegate WorkerFunc { get; set; }

                /// <summary>
                /// the name of the Thread
                /// </summary>
                public string Name { get; set; }

                private int _sleepTime = 100;

                /// <summary>
                /// the time to sleep before FormControl to invoke the method.
                /// </summary>
                public int SleepTime
                {
                    get { return _sleepTime; }
                    set { _sleepTime = value; }
                }

                /// <summary>
                /// Multi Thread for WinForm
                /// </summary>
                /// <param name="threadName">the name of the Thread</param>
                /// <param name="workerFunc">the method to be invoke by the FromControl, an instance of workerFunctionDelegate</param>
                /// <param name="formControl">the form instance to invoke the method</param>
                /// <param name="delayExecuteTime">time to delay before execute the workerFunc</param>
                /// <param name="sleepTime"><see cref="WinForm.Thread.SleepTime"/></param>
                /// <example>
                /// <code language="C#" title="Start a thread">
                ///     WinForm.Thread.workerFunctionDelegate wfd;
                ///     WinForm.Thread wft;
                ///     int sleepTime = 100;
                ///     wfd = delegate()
                ///        {
                ///            if (panelDisplay.Visible == true)
                ///            {
                ///                showTime = (-1 != showTime ? showTime : (labelMessage.Text.Length / 3 + 1) * (1000));
                ///                for (int i = 0; i &lt;  (showTime / sleepTime); i++)
                ///                {
                ///                    System.Windows.Forms.Application.DoEvents();
                ///                    System.Threading.Thread.Sleep((sleepTime));
                ///                    if (!this.Visible)
                ///                    {
                ///                        break;
                ///                    }
                ///                }
                ///                panelDisplay.Hide();
                ///            }
                ///            else
                ///            {
                ///                // do nothing
                ///            }
                ///        };
                ///     wft = new Thread(workerFunc: wfd, formControl: panelDisplay.FindForm(), sleepTime: showTime);
                ///     wft.Start();
                /// </code>
                /// </example>
                public Thread(String threadName = "workerThread", Delegate workerFunc = null, System.Windows.Forms.Control formControl = null, int sleepTime = 100, int delayExecuteTime = 100)
                {
                    this.Name = threadName;
                    this.WorkerFunc = workerFunc;
                    this.FormControl = formControl;
                    this.SleepTime = sleepTime;
                    this.DelayExecuteTime = delayExecuteTime;
                }

                /// <summary>
                /// Start the Thread
                /// </summary>
                public void Start()
                {
                    #region Worker
#if Worker
                    _worker = new Worker();
                    _worker.DelayExecuteTime = this.DelayExecuteTime;
                    _worker.SleepTime = this.SleepTime;
                    _worker.ProgressChanged += new EventHandler<RF.GlobalClass.WinForm.Thread.Worker.ProgressChangedArgs>(this.OnWorkerProgressChanged);

                    #region fold
                    //foreach (System.Diagnostics.ProcessThread pt in System.Diagnostics.Process.GetCurrentProcess().Threads) // Stop the thread with the same name;
                    //{

                    //    if (pt.Name == this.Name)
                    //    {
                    //        if (pt.IsAlive)
                    //        {
                    //            pt.Interrupt();
                    //            pt.Abort();
                    //        }
                    //        else
                    //        {
                    //        }
                    //    }
                    //}
                    #endregion
                    WorkerThread = new System.Threading.Thread(new System.Threading.ThreadStart(_worker.StartWork));
                    WorkerThread.Name = this.Name;
                    WorkerThread.IsBackground = true;
                    WorkerThread.Start();
#endif
                    #endregion

                    #region BackgroundWorker
#if BackgroundWorker
                    System.ComponentModel.BackgroundWorker _bw = new System.ComponentModel.BackgroundWorker();
                    _bw.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWork_DoWork);
                    _bw.WorkerReportsProgress = true;
                    _bw.WorkerSupportsCancellation = true;
                    _bw.RunWorkerAsync();
                    this.BackgroundWork = _bw;
#endif
                    #endregion
                }

                #region BackgroundWorker
#if BackgroundWorker
                void backgroundWork_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
                {
                    try{
                    if (null != this.WorkerFunc)
                        {
                            //System.Threading.Thread.Sleep(this.SleepTime);// this will freezing the interface.
                            this.FormControl.Invoke(this.WorkerFunc);
                            if (this.BackgroundWork.IsBusy)
                            {
                                this.BackgroundWork.CancelAsync();
                            }
                            while (this.BackgroundWork.IsBusy)
                            {
                            }
                        }
                        else { }
                    }catch(Exception ex){
                    }
                }
#endif
                #endregion

                #region Worker
#if Worker
                private void OnWorkerProgressChanged(object sender, RF.GlobalClass.WinForm.Thread.Worker.ProgressChangedArgs e)
                {
                    try
                    {
                        //cross thread - so you don't get the cross theading exception
                        if (this.FormControl.InvokeRequired)
                        {
                            this.FormControl.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                            {
                                OnWorkerProgressChanged(sender, e);
                            });
                            return;
                        }

                        //change control
                        //this.label1.Text += Dict.translate("MESSAGE", "In Reading");//" " + e.Progress;

                        //workerFunctionDelegate w = workerFunction;
                        //w.BeginInvoke(Convert.ToInt32(order.ticketCount), null, null);

                        if (null != this.WorkerFunc)
                        {
                            //System.Threading.Thread.Sleep(this.SleepTime);// this will freezing the interface.
                            this.FormControl.BeginInvoke(this.WorkerFunc);
                        }
                        else { }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                /// <summary>
                /// the Worker Class for the Thread
                /// </summary>
                public partial class Worker
                {
                    #region fold Worker\ ProgressChangedArgs
                    /// <summary>
                    /// Event to be trigged on progress changed
                    /// </summary>
                    public event EventHandler<ProgressChangedArgs> ProgressChanged;

                    /// <summary>
                    /// the on progress changed method to be invoked on worker StartWork
                    /// </summary>
                    /// <param name="e"></param>
                    protected void OnProgressChanged(ProgressChangedArgs e)
                    {
                        if (ProgressChanged != null)
                        {
                            ProgressChanged(this, e);
                        }
                        else
                        {
                            // do not know what to do;
                        }
                    }

                    /// <summary>
                    /// start to work
                    /// </summary>
                    public void StartWork()
                    {
                        try
                        {
                            System.Threading.Thread.Sleep(100);
                            int delayExecuteTime = this.DelayExecuteTime;
                            int sleepTime = this.SleepTime;
                            for (int i = 0; i < (delayExecuteTime / sleepTime); i++)
                            {
                                System.Windows.Forms.Application.DoEvents();
                                System.Threading.Thread.Sleep(sleepTime);
                            }
                            OnProgressChanged(new ProgressChangedArgs("Progress Changed"));
                            System.Threading.Thread.Sleep(100);
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    /// <summary>
                    /// the eventArgs class for progressChanged
                    /// </summary>
                    public class ProgressChangedArgs : EventArgs
                    {
                        /// <summary>
                        /// record if the progress is changed
                        /// </summary>
                        public string Progress { get; private set; }
                        /// <summary>
                        /// the ProgressChangeArgs
                        /// </summary>
                        /// <param name="progress"></param>
                        public ProgressChangedArgs(string progress)
                        {
                            Progress = progress;
                        }
                    }

                    #endregion

                    /// <summary>
                    /// time to delay before execute the workerFunc
                    /// </summary>
                    public int DelayExecuteTime { get; set; }


                    public int SleepTime { get; set; }
                }

                
#endif
                #endregion

#if BackgroundWorker
                public System.ComponentModel.BackgroundWorker BackgroundWork { get; set; }
#endif
                /// <summary>
                /// time to delay before execute the workerFunc
                /// </summary>
                public int DelayExecuteTime { get; set; }
            }
            #endregion
        }
    }
}

/* vim: set si ts=4 sts=4 sw=4 fdm=indent : */
