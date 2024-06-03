using System;
using System.Collections.Generic;
using System.Windows;
using JXP.Models;
using JXP.PepDtp.Model;
using JXP.PepUtility;
using pep.sdk.reader.View.Textbook;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000070 RID: 112
	public class SharedResDataObjectProvider : ResDataObjectProvider
	{
		// Token: 0x060007D8 RID: 2008 RVA: 0x00027158 File Offset: 0x00025358
		public override object GetDataObject(object paras)
		{
			if (!(paras is SharedResDataModel))
			{
				return new DataObject();
			}
			if (Application.Current.MainWindow == null)
			{
				return new DataObject();
			}
			SharedResDataModel sharedResDataModel = (SharedResDataModel)paras;
			string strResType = "01";
			if (sharedResDataModel.ResFlg == SdkConstants.RJ_CLOUD_RES_TYPE || sharedResDataModel.ResFlg == SdkConstants.NQ_RES_TYPE)
			{
				strResType = SdkConstants.RJ_CLOUD_RES_TYPE;
			}
			UserResourceJsonModel centerResData = base.GetCenterResData(sharedResDataModel.ResId, sharedResDataModel.ResFlg, false);
			List<UserResourceModel> list = (centerResData != null) ? centerResData.UserResourcesList : null;
			if (list == null || list.Count == 0)
			{
				return new DataObject();
			}
			UserResourceModel resmodel = list[0];
			BaseBookReader.Instance.Dispatcher.BeginInvoke(new Action(delegate()
			{
				BaseBookReader.Instance.ResourceListDrag(resmodel.ID, "2", strResType, false);
			}), new object[0]);
			return resmodel;
		}
	}
}
