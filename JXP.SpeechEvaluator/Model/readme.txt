{     
 "type":"类别（1:叙述；2:对话）",    
 "audio":"音频文件名",    
 "roles":["角色（数组，叙述类的该字段为空）"    
  {   
   "name":"名称",  
   "image":"头像文件名"  
  }   
 ],    
 "paragraphs":["段落或篇章（数组）"    
  {   
   "index":"序号（1、2、3...）",  
   "audio":"段落的音频文件名",  
   "sentences":["句子（数组）"  
    { 
     "index":"序号（1、2、3...）",
     "role":"角色（对应前面角色的名称，叙述类的该字段为空）",
     "content":"句子内容",
     "hidden":"有提示背诵时隐藏的单词（可能为空）",
     "italic":"斜体等特殊形式显示的单词（可能为空）",
     "image":"图片文件名（可能为空）",
     "audio":"音频文件名"
    } 
   ]  
  }   
 ]    
}


PEP六上：http://192.168.186.61/shanghai_zxd/voice/pep6s/index.json
PEP五下：http://192.168.186.61/shanghai_zxd/voice/pep5x/index.json
PEP八下：http://192.168.186.61/shanghai_zxd/voice/pep8x/index.json


vad_timeout 允许头部静音的最长时间 0-10000毫秒。默认为10000
如果静音时长超过了此值，则认为用户此次无有效音频输入。此参数仅在打开VAD功能时有效。 
vad_speech_tail 允许尾部静音的最长时间 0-10000毫秒。默认为2000
如果尾部静音时长超过了此值，则认为用户音频已经结束，此参数仅在打开VAD功能时有效。 
vad_enable VAD功能开关 是否启用VAD
默认为开启VAD
0（或false）为关闭 

extra_ability string 否 拓展能力（生效条件ise_unite="1", rst="entirety"）
多维度分信息显示（准确度分、流畅度分、完整度打分）
extra_ability值为multi_dimension（字词句篇均适用,如选多个能力，用分号；隔开。例如：add("extra_ability"," syll_phone_err_msg;pitch;multi_dimension")）
单词基频信息显示（基频开始值、结束值）
extra_ability值为pitch ，仅适用于单词和句子题型
音素错误信息显示（声韵、调型是否正确）
extra_ability值为syll_phone_err_msg（字词句篇均适用,如选多个能力，用分号；隔开。例如：add("extra_ability"," syll_phone_err_msg;pitch;multi_dimension")） 