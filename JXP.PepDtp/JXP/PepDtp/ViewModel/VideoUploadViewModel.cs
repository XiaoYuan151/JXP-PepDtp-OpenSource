using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using JXP.Logs;
using JXP.Models;
using JXP.PepDtp.Common;
using JXP.PepDtp.Paramter;
using Newtonsoft.Json;

namespace JXP.PepDtp.ViewModel
{
	// Token: 0x02000077 RID: 119
	public class VideoUploadViewModel : BaseModel
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00029F53 File Offset: 0x00028153
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00029F5B File Offset: 0x0002815B
		public string IntroduceFile
		{
			get
			{
				return this.mIntroduceFile;
			}
			set
			{
				this.mIntroduceFile = value;
				base.OnPropertyChanged<string>(() => this.IntroduceFile);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00029F99 File Offset: 0x00028199
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x00029FA1 File Offset: 0x000281A1
		public string WorksFile
		{
			get
			{
				return this.mWorksFile;
			}
			set
			{
				this.mWorksFile = value;
				base.OnPropertyChanged<string>(() => this.WorksFile);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00029FDF File Offset: 0x000281DF
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00029FE7 File Offset: 0x000281E7
		public string WorkTitle
		{
			get
			{
				return this.mWorkTitle;
			}
			set
			{
				this.mWorkTitle = value;
				base.OnPropertyChanged<string>(() => this.WorkTitle);
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0002A025 File Offset: 0x00028225
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x0002A02D File Offset: 0x0002822D
		public string SubtitlesFile
		{
			get
			{
				return this.mSubtitlesFile;
			}
			set
			{
				this.mSubtitlesFile = value;
				base.OnPropertyChanged<string>(() => this.SubtitlesFile);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0002A06B File Offset: 0x0002826B
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x0002A073 File Offset: 0x00028273
		public string IntroduceWorksFile
		{
			get
			{
				return this.mIntroduceWorksFile;
			}
			set
			{
				this.mIntroduceWorksFile = value;
				base.OnPropertyChanged<string>(() => this.IntroduceWorksFile);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0002A0B1 File Offset: 0x000282B1
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x0002A0B9 File Offset: 0x000282B9
		public bool ApplyChecked
		{
			get
			{
				return this.mApplyChecked;
			}
			set
			{
				this.mApplyChecked = value;
				base.OnPropertyChanged<bool>(() => this.ApplyChecked);
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0002A0F7 File Offset: 0x000282F7
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x0002A0FF File Offset: 0x000282FF
		public bool OperateChecked
		{
			get
			{
				return this.mOperateChecked;
			}
			set
			{
				this.mOperateChecked = value;
				base.OnPropertyChanged<bool>(() => this.OperateChecked);
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0002A13D File Offset: 0x0002833D
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x0002A145 File Offset: 0x00028345
		public bool ShareChecked
		{
			get
			{
				return this.mShareChecked;
			}
			set
			{
				this.mShareChecked = value;
				base.OnPropertyChanged<bool>(() => this.ShareChecked);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0002A183 File Offset: 0x00028383
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x0002A18B File Offset: 0x0002838B
		public bool OtherChecked
		{
			get
			{
				return this.mOtherChecked;
			}
			set
			{
				this.mOtherChecked = value;
				base.OnPropertyChanged<bool>(() => this.OtherChecked);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0002A1C9 File Offset: 0x000283C9
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x0002A1D1 File Offset: 0x000283D1
		public bool ShowWaiting
		{
			get
			{
				return this.mShowWaiting;
			}
			set
			{
				this.mShowWaiting = value;
				base.OnPropertyChanged<bool>(() => this.ShowWaiting);
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002A210 File Offset: 0x00028410
		public void ClearData()
		{
			this.ApplyChecked = true;
			this.OperateChecked = false;
			this.ShareChecked = false;
			this.OtherChecked = false;
			this.ShowWaiting = false;
			this.IntroduceWorksFile = string.Empty;
			this.SubtitlesFile = string.Empty;
			this.WorksFile = string.Empty;
			this.WorkTitle = string.Empty;
			this.IntroduceFile = string.Empty;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002A278 File Offset: 0x00028478
		public bool SelectFile(List<string> lstFormat, long sizeM, out string filePath, out string msg)
		{
			filePath = string.Empty;
			msg = string.Empty;
			if (lstFormat != null && lstFormat.Count == 0)
			{
				return false;
			}
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.CheckFileExists = true;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < lstFormat.Count; i++)
			{
				string text = lstFormat[i];
				if (i == lstFormat.Count - 1)
				{
					stringBuilder.Append("(*" + text + ")|*" + text);
				}
				else
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						"(*",
						text,
						")|*",
						text,
						"|"
					}));
				}
			}
			openFileDialog.Filter = stringBuilder.ToString();
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return false;
			}
			string text2 = Path.GetExtension(openFileDialog.FileName);
			text2 = ((text2 == null) ? text2 : text2.ToLower());
			if (!lstFormat.Contains(text2))
			{
				msg = "选择文件类型不支持上传！";
				return false;
			}
			if (new FileInfo(openFileDialog.FileName).Length > sizeM * 1024L * 1024L)
			{
				msg = string.Format("您所选择的文件大于{0}M", sizeM);
				return false;
			}
			filePath = openFileDialog.FileName;
			return true;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002A3AC File Offset: 0x000285AC
		public string DataCheck()
		{
			if (!this.ApplyChecked && !this.OperateChecked && !this.ShareChecked && !this.OtherChecked)
			{
				return "请选择作品类型!";
			}
			if (string.IsNullOrEmpty(this.IntroduceFile))
			{
				return "请选择作品封面文件!";
			}
			if (!File.Exists(this.IntroduceFile))
			{
				return "选择的作品封面文件不存在!";
			}
			if (string.IsNullOrEmpty(this.WorksFile))
			{
				return "请选择上传作品文件!";
			}
			if (!File.Exists(this.WorksFile))
			{
				return "选择的上传作品文件不存在!";
			}
			if (string.IsNullOrEmpty(this.WorkTitle))
			{
				return "作品标题不能为空!";
			}
			return string.Empty;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002A444 File Offset: 0x00028644
		public bool UploadFile(out string msg, out int fileCount)
		{
			msg = string.Empty;
			fileCount = 2;
			bool result;
			try
			{
				this.ShowWaiting = true;
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.IntroduceFile);
				string destName = Guid.NewGuid().ToString("N") + Path.GetExtension(this.IntroduceFile);
				string destPath = string.Concat(new string[]
				{
					"/gx_21_activity_video/",
					CommonParamter.Instance.OrgsPath,
					"/",
					CommonParamter.Instance.LoginUserId,
					"/sys/resuser"
				});
				Tuple<bool, string> tuple = this.mUpload.UploadFile3(this.IntroduceFile, destPath, destName, null);
				if (!tuple.Item1)
				{
					msg = "作品封面文件上传失败!";
					result = false;
				}
				else
				{
					string item = tuple.Item2;
					string destName2 = Guid.NewGuid().ToString("N") + Path.GetExtension(this.WorksFile);
					string destPath2 = string.Concat(new string[]
					{
						"/gx_21_activity_video/",
						CommonParamter.Instance.OrgsPath,
						"/",
						CommonParamter.Instance.LoginUserId,
						"/sys/resuser"
					});
					Tuple<bool, string> tuple2 = this.mUpload.UploadFile3(this.WorksFile, destPath2, destName2, null);
					if (!tuple2.Item1)
					{
						msg = "上传作品文件上传失败!";
						result = false;
					}
					else
					{
						string item2 = tuple2.Item2;
						string value = string.Empty;
						string destName3 = string.Empty;
						string value2 = string.Empty;
						if (!string.IsNullOrEmpty(this.SubtitlesFile) && File.Exists(this.SubtitlesFile))
						{
							value = Path.GetFileNameWithoutExtension(this.SubtitlesFile);
							destName3 = Guid.NewGuid().ToString("N") + Path.GetExtension(this.SubtitlesFile);
							string destPath3 = string.Concat(new string[]
							{
								"/gx_21_activity_video/",
								CommonParamter.Instance.OrgsPath,
								"/",
								CommonParamter.Instance.LoginUserId,
								"/sys/resuser"
							});
							Tuple<bool, string> tuple3 = this.mUpload.UploadFile3(this.SubtitlesFile, destPath3, destName3, null);
							if (!tuple3.Item1)
							{
								msg = "作品字幕文件上传失败!";
								return false;
							}
							fileCount++;
							value2 = tuple3.Item2;
						}
						string value3 = string.Empty;
						string destName4 = string.Empty;
						string value4 = string.Empty;
						if (!string.IsNullOrEmpty(this.IntroduceWorksFile) && File.Exists(this.IntroduceWorksFile))
						{
							value3 = Path.GetFileNameWithoutExtension(this.IntroduceWorksFile);
							destName4 = Guid.NewGuid().ToString("N") + Path.GetExtension(this.IntroduceWorksFile);
							string destPath4 = string.Concat(new string[]
							{
								"/gx_21_activity_video/",
								CommonParamter.Instance.OrgsPath,
								"/",
								CommonParamter.Instance.LoginUserId,
								"/sys/resuser"
							});
							Tuple<bool, string> tuple4 = this.mUpload.UploadFile3(this.IntroduceWorksFile, destPath4, destName4, null);
							if (!tuple4.Item1)
							{
								msg = "作品介绍文件上传失败!";
								return false;
							}
							fileCount++;
							value4 = tuple4.Item2;
						}
						string strUrl = ConfigHelper.WebServerUrl + "videoActivity/save.json";
						Dictionary<string, string> dictionary = new Dictionary<string, string>();
						dictionary["id"] = Guid.NewGuid().ToString("N");
						dictionary["type"] = this.GetWorksType();
						dictionary["title_grjj"] = fileNameWithoutExtension;
						dictionary["file_path_grjj"] = item;
						dictionary["title_sczp"] = this.WorkTitle;
						dictionary["file_path_sczp"] = item2;
						dictionary["title_zpzm"] = value;
						dictionary["file_path_zpzm"] = value2;
						dictionary["title_zpjs"] = value3;
						dictionary["file_path_zpjs"] = value4;
						VideoUploadResult videoUploadResult = JsonConvert.DeserializeObject<VideoUploadResult>(HttpHelper.HttpPost(strUrl, dictionary, new int?(30000), "", true));
						if (videoUploadResult == null || videoUploadResult.State != 110)
						{
							msg = "上传数据失败!";
							result = false;
						}
						else
						{
							result = true;
						}
					}
				}
			}
			catch (Exception arg)
			{
				msg = "上传数据失败!";
				LogHelper.Instance.Error(string.Format("上传视频相关文件失败:[{0}]。", arg));
				result = false;
			}
			finally
			{
				this.ShowWaiting = false;
			}
			return result;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002A8CC File Offset: 0x00028ACC
		private string GetWorksType()
		{
			if (this.ApplyChecked)
			{
				return "11";
			}
			if (this.OperateChecked)
			{
				return "12";
			}
			if (this.ShareChecked)
			{
				return "13";
			}
			if (this.OtherChecked)
			{
				return "14";
			}
			return "11";
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002A90B File Offset: 0x00028B0B
		public string GetWorksTypeTitle()
		{
			if (this.ApplyChecked)
			{
				return "应用技巧";
			}
			if (this.OperateChecked)
			{
				return "基本操作";
			}
			if (this.ShareChecked)
			{
				return "教学实践分享";
			}
			if (this.OtherChecked)
			{
				return "其他";
			}
			return "应用技巧";
		}

		// Token: 0x04000457 RID: 1111
		private UpLoadHelper mUpload = new UpLoadHelper();

		// Token: 0x04000458 RID: 1112
		private bool mApplyChecked = true;

		// Token: 0x04000459 RID: 1113
		private bool mOperateChecked;

		// Token: 0x0400045A RID: 1114
		private bool mShareChecked;

		// Token: 0x0400045B RID: 1115
		private bool mOtherChecked;

		// Token: 0x0400045C RID: 1116
		private bool mShowWaiting;

		// Token: 0x0400045D RID: 1117
		private string mIntroduceFile;

		// Token: 0x0400045E RID: 1118
		private string mWorksFile;

		// Token: 0x0400045F RID: 1119
		private string mSubtitlesFile;

		// Token: 0x04000460 RID: 1120
		private string mIntroduceWorksFile;

		// Token: 0x04000461 RID: 1121
		private string mWorkTitle;
	}
}
