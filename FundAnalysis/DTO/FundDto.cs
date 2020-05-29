using System;

namespace FundAnalysis.DTO
{
    public class FundDto
    {
        public string dm { get; set; } //基金代码
        public string jc { get; set; } //基金简称
        public string jcen { get; set; } //基金简称英文
        public DateTime? jzrq { get; set; } //日期
        public float? dwjz { get; set; } //单位净值
        public float? ljjz { get; set; } //累计净值
        public float? rzdf { get; set; } //日增长率
        public float? zzf { get; set; } //近1周
        public float? yzf1 { get; set; } //近1月
        public float? yzf3 { get; set; } //近3月
        public float? yzf6 { get; set; } //近6月
        public float? nzf1 { get; set; } //近1年
        public float? nzf2 { get; set; } //近2年
        public float? nzf3 { get; set; } //近3年
        public float? jnzf { get; set; } //今年来
        public float? lnzf { get; set; } //成立来
        public DateTime? clr { get; set; } //成立日
        public float? zdy { get; set; } //自定义
        public float sxf { get; set; } //手册续费
        
        public string d17 { get; set; } //未知
        public string d19 { get; set; } //未知
        public string d21 { get; set; } //未知
        public string d22 { get; set; } //未知
        public string d23 { get; set; } //未知
        public string d24 { get; set; } //未知
    }
}