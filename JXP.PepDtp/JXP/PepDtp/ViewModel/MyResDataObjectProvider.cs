using System;
using System.Windows;
using JXP.Models;
using JXP.PepUtility;
using pep.sdk.reader.View.Textbook;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000064 RID: 100
	public class MyResDataObjectProvider : ResDataObjectProvider
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x000241EC File Offset: 0x000223EC
		public override object GetDataObject(object paras)
		{
			if (!(paras is UserResourceModel))
			{
				return new DataObject();
			}
			if (Application.Current.MainWindow == null)
			{
				return new DataObject();
			}
			UserResourceModel model = (UserResourceModel)paras;
			string strPosType = (model.PosType > 0) ? model.PosType.ToString() : "1";
			BaseBookReader.Instance.Dispatcher.BeginInvoke(new Action(delegate()
			{
				BaseBookReader.Instance.ResourceListDrag(model.ID, strPosType, "01", true);
			}), new object[0]);
			UserResourceModel userResourceModel = new UserResourceModel();
			UtilityHelper.CopyTo<UserResourceModel>(model, userResourceModel);
			return userResourceModel;
		}
	}
}
