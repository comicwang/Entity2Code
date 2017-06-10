using EnvDTE;
using Infoearth.Entity2CodeTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infoearth.Entity2CodeTool.Helps;
using System.IO;
using Infoearth.Entity2CodeTool.Converter;

namespace Infoearth.Entity2CodeTool.Logic.UI
{
    public class LoadProjectLogic
    {
        public static bool Load(EnvDTE.DTE dte)
        {
            if (dte == null)
                throw new ArgumentException("缺少DTE参数");
            if (dte.Solution == null)
                throw new ArgumentException("当前不存在解决方案");
            Projects lstProject = dte.Solution.Projects;
            if (lstProject == null || lstProject.Count == 0)
                throw new ArgumentException("解决方案中不存在项目");
            int total = 0;
            //开始解析解决方案
            foreach (Project prj in lstProject)
            {
                string prjName = prj.Name;

                if (prj.Name.ToLower().EndsWith("iapplication"))
                {

                   // SolutionCommon.ProjectName = 
                    SolutionCommon.IApplication = prjName;
                    ProjectContainer.IApplication = prj;
                    total++;
                }
                else if (prj.Name.ToLower().EndsWith("application"))
                {
                    SolutionCommon.Application = prjName;
                    ProjectContainer.Application = prj;
                    total++;
                }
                else if (prj.Name.ToLower().EndsWith("service"))
                {
                    SolutionCommon.Service = prjName;
                    ProjectContainer.Service = prj;
                    total++;
                }
                else if (prj.Name.ToLower().EndsWith("entities"))
                {
                    SolutionCommon.DomainEntity = prjName;
                    ProjectContainer.DomainEntity = prj;
                    total++;
                }
                else if (prj.Name.ToLower().EndsWith("dto"))
                {
                    SolutionCommon.Data2Object = prjName;
                    ProjectContainer.Data2Object = prj;
                    total++;
                }
                else if (prj.Name.ToLower().EndsWith("infrastructure.context"))
                {
                    SolutionCommon.Infrastructure = prjName;
                    ProjectContainer.Infrastructure = prj;
                    total++;
                }
                else if (prj.Name.ToLower().EndsWith("domain.context"))
                {
                    SolutionCommon.DomainContext = prjName;
                    ProjectContainer.DomainContext = prj;
                    total++;
                }
            }

            if (total < 7)
                throw new ArgumentException("非Entity2Code项目源");

            return true;
        }

        public static List<TemplateEntity> GetEntitys()
        {
            List<TemplateEntity> result = new List<TemplateEntity>();
            string entityDir = ProjectContainer.DomainEntity.ToDirectory();
            string[] files = Directory.GetFiles(entityDir, "*.cs");
            if (null == files || files.Length == 0)
                throw new Exception("Entity2Code DomainEntity ProjectItem is Null");
            foreach (string file in files)
            {
                string entity = Path.GetFileNameWithoutExtension(file);
                result.Add(new TemplateEntity() { Entity = entity + "Repository", Data2Obj = ModelNameConverter.GetData2Obj(entity) });
            }

            return result;
        }
    }
}
