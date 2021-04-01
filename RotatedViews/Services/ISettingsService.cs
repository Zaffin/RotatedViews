using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotatedViews.Services
{
    public interface ISettingsService
    {
        string GetDefaultViewNameTemplate();

        bool GetDefaultUseExistingWorkOffsetSetting();

        void SaveUseExistingWorkOffsetSettingAsDefault(bool useExistingWorkOffset);

        void SaveViewNameTemplateAsDefault(string viewNameTemplate);
    }
}
