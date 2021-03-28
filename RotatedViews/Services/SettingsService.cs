using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotatedViews.Services
{
    public class SettingsService : ISettingsService
    {
        public string GetDefaultViewNameTemplate()
        {
            return Properties.Settings.Default.defaultViewNameTemplate;
        }

        public void SaveViewNameTemplateAsDefault(string viewNameTemplate)
        {
            Properties.Settings.Default.defaultViewNameTemplate = viewNameTemplate;
            Save();
        }

        private void Save()
        {
            Properties.Settings.Default.Save();

        }
    }
}
