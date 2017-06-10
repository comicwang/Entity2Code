using Infoearth.Entity2CodeTool.Helps;
using Infoearth.Entity2CodeTool.Logic.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infoearth.Entity2CodeTool.UI
{
    public partial class FormAddMethod : Form
    {
        public FormAddMethod()
        {
            InitializeComponent();
        }

        private void cmbReposi_DropDown(object sender, EventArgs e)
        {
            try
            {
                EnvDTE.DTE dte = Model.SolutionCommon.Dte;
                bool result = LoadProjectLogic.Load(dte);
                //加载所有表信息
                if (result)
                {
                    List<TemplateEntity> entitys = LoadProjectLogic.GetEntitys();
                    cmbReposi.DataSource = entitys;
                    cmbReposi.DisplayMember = "Entity";
                    cmbReposi.ValueMember = "Data2Obj";
                }
                else
                {
                    MsgBoxHelp.ShowWorning("加载仓储列表出错！");
                }
            }
            catch (Exception ex)
            {
                MsgBoxHelp.ShowError(ex.Message, ex);
            }
        }
    }
}
