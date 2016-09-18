using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Entity;

namespace JinHong
{
    public class ApplicationPrivileges
    {
        public static readonly Privilege 财务模块_访问 = new Privilege("财务模块_访问");
        public static readonly Privilege 收入日报表_访问 = new Privilege("收入日报表_访问");
        public static readonly Privilege 收入日报表_操作 = new Privilege("收入日报表_操作");
        public static readonly Privilege 未交的房租_押金明细表_访问 = new Privilege("未交的房租_押金明细表_访问");
        public static readonly Privilege 未交的房租_押金明细表_操作 = new Privilege("未交的房租_押金明细表_操作");
        public static readonly Privilege 已交的房租_押金明细表_访问 = new Privilege("已交的房租_押金明细表_访问");
        public static readonly Privilege 已交的房租_押金明细表_操作 = new Privilege("已交的房租_押金明细表_操作");
        public static readonly Privilege 未转的物业管理费明细表_访问 = new Privilege("未转的物业管理费明细表_访问");
        public static readonly Privilege 未转的物业管理费明细表_操作 = new Privilege("未转的物业管理费明细表_操作");
        public static readonly Privilege 已转的物业管理费明细表_访问 = new Privilege("已转的物业管理费明细表_访问");
        public static readonly Privilege 已转的物业管理费明细表_操作 = new Privilege("已转的物业管理费明细表_操作");
        public static readonly Privilege 月度收支明细表_访问 = new Privilege("月度收支明细表_访问");
        public static readonly Privilege 月度收支明细表_操作 = new Privilege("月度收支明细表_操作");
        public static readonly Privilege 半年度收支明细表_访问 = new Privilege("半年度收支明细表_访问");
        public static readonly Privilege 半年度收支明细表_操作 = new Privilege("半年度收支明细表_操作");
        public static readonly Privilege 年度收支明细表_访问 = new Privilege("年度收支明细表_访问");
        public static readonly Privilege 年度收支明细表_操作 = new Privilege("年度收支明细表_操作");
        public static readonly Privilege 月度收支汇总表_访问 = new Privilege("月度收支汇总表_访问");
        public static readonly Privilege 月度收支汇总表_操作 = new Privilege("月度收支汇总表_操作");
        public static readonly Privilege 财务模块_授权 = new Privilege("财务模块_授权");

        public static readonly Privilege 物业模块_访问 = new Privilege("物业模块_访问");
        public static readonly Privilege 人事管理_访问 = new Privilege("人事管理_访问");
        public static readonly Privilege 任职全厂职工名单_访问 = new Privilege("任职全厂职工名单_访问");
        public static readonly Privilege 任职全厂职工名单_操作 = new Privilege("任职全厂职工名单_操作");
        public static readonly Privilege 退休职工名单_访问 = new Privilege("退休职工名单_访问");
        public static readonly Privilege 退休职工名单_操作 = new Privilege("退休职工名单_操作");
        public static readonly Privilege 消防管理_访问 = new Privilege("消防管理_访问");
        public static readonly Privilege 消防资料_访问 = new Privilege("消防资料_访问");
        public static readonly Privilege 消防资料_操作 = new Privilege("消防资料_操作");
        public static readonly Privilege 物业管理_访问 = new Privilege("物业管理_访问");
        public static readonly Privilege 租赁单位水电费一览表_访问 = new Privilege("租赁单位水电费一览表_访问");
        public static readonly Privilege 租赁单位水电费一览表_操作 = new Privilege("租赁单位水电费一览表_操作");
        public static readonly Privilege 水电费总厂开票一览表_访问 = new Privilege("水电费总厂开票一览表_访问");
        public static readonly Privilege 水电费总厂开票一览表_操作 = new Privilege("水电费总厂开票一览表_操作");
        public static readonly Privilege 停车费统计表_访问 = new Privilege("停车费统计表_访问");
        public static readonly Privilege 停车费统计表_操作 = new Privilege("停车费统计表_操作");
        public static readonly Privilege 钥匙管理_空调设备_访问 = new Privilege("钥匙管理_空调设备_访问");
        public static readonly Privilege 钥匙管理_空调设备_操作 = new Privilege("钥匙管理_空调设备_操作");
        public static readonly Privilege 电信电话_电信收费清单_访问 = new Privilege("电信电话_电信收费清单_访问");
        public static readonly Privilege 电信电话_电信收费清单_操作 = new Privilege("电信电话_电信收费清单_操作");
        public static readonly Privilege 维修记录_访问 = new Privilege("维修记录_访问");
        public static readonly Privilege 维修记录_操作 = new Privilege("维修记录_操作");
        public static readonly Privilege 各楼层租赁单位情况表_访问 = new Privilege("各楼层租赁单位情况表_访问");
        public static readonly Privilege 各楼层租赁单位情况表_操作 = new Privilege("各楼层租赁单位情况表_操作");
        public static readonly Privilege 收缴停车费明细表_访问 = new Privilege("收缴停车费明细表_访问");
        public static readonly Privilege 收缴停车费明细表_操作 = new Privilege("收缴停车费明细表_操作");
        public static readonly Privilege 平面图_访问 = new Privilege("平面图_访问");
        public static readonly Privilege 平面图_操作 = new Privilege("平面图_操作");
        public static readonly Privilege 应收停车费汇总表_访问 = new Privilege("应收停车费汇总表_访问");
        public static readonly Privilege 应收停车费汇总表_操作 = new Privilege("应收停车费汇总表_操作");
        public static readonly Privilege 租赁单位安全管理协议表_访问 = new Privilege("租赁单位安全管理协议表_访问");
        public static readonly Privilege 租赁单位安全管理协议表_操作 = new Privilege("租赁单位安全管理协议表_操作");
        public static readonly Privilege 租赁户入住表_访问 = new Privilege("租赁户入住表_访问");
        public static readonly Privilege 租赁户入住表_操作 = new Privilege("租赁户入住表_操作");
        public static readonly Privilege 租赁户退租表_访问 = new Privilege("租赁户退租表_访问");
        public static readonly Privilege 租赁户退租表_操作 = new Privilege("租赁户退租表_操作");
        public static readonly Privilege 监控探头_访问 = new Privilege("监控探头_访问");
        public static readonly Privilege 监控探头_操作 = new Privilege("监控探头_操作");
        public static readonly Privilege 车位信息管理表_访问 = new Privilege("车位信息管理表_访问");
        public static readonly Privilege 车位信息管理表_操作 = new Privilege("车位信息管理表_操作");
        public static readonly Privilege 抄电表数_访问 = new Privilege("抄电表数_访问");
        public static readonly Privilege 抄电表数_操作 = new Privilege("抄电表数_操作");
        public static readonly Privilege 水电费收费清单_访问 = new Privilege("水电费收费清单_访问");
        public static readonly Privilege 水电费收费清单_操作 = new Privilege("水电费收费清单_操作");
        public static readonly Privilege 物业模块_授权 = new Privilege("物业模块_授权");

        public static readonly Privilege 招商模块_访问 = new Privilege("招商模块_访问");
        public static readonly Privilege 楼宇区域位置图示_访问 = new Privilege("楼宇区域位置图示_访问");
        public static readonly Privilege 租赁户企业基本情况_访问 = new Privilege("租赁户企业基本情况_访问");
        public static readonly Privilege 租赁户企业基本情况_操作 = new Privilege("租赁户企业基本情况_操作");
        public static readonly Privilege 租金_物业费缴纳情况记录管理业务_访问 = new Privilege("租金_物业费缴纳情况记录管理业务_访问");
        public static readonly Privilege 租金_物业费缴纳情况记录管理业务_操作 = new Privilege("租金_物业费缴纳情况记录管理业务_操作");
        public static readonly Privilege 租赁期限变更_续租_退租一览业务_访问 = new Privilege("租赁期限变更_续租_退租一览业务_访问");
        public static readonly Privilege 租赁期限变更_续租_退租一览业务_操作 = new Privilege("租赁期限变更_续租_退租一览业务_操作");
        public static readonly Privilege 退租流程展示表格_访问 = new Privilege("退租流程展示表格_访问");
        public static readonly Privilege 仓库租赁情况_访问 = new Privilege("仓库租赁情况_访问");
        public static readonly Privilege 仓库租赁情况_操作 = new Privilege("仓库租赁情况_操作");
        public static readonly Privilege 合同管理_访问 = new Privilege("合同管理_访问");
        public static readonly Privilege 合同管理_操作 = new Privilege("合同管理_操作");
        public static readonly Privilege 招商模块_授权 = new Privilege("招商模块_授权");

        public static readonly Privilege 系统管理模块_访问 = new Privilege("系统管理模块_访问");
        public static readonly Privilege 角色管理_访问 = new Privilege("角色管理_访问");
        public static readonly Privilege 用户管理_访问 = new Privilege("用户管理_访问");
        public static readonly Privilege 车位管理_访问 = new Privilege("车位管理_访问");
        public static readonly Privilege 楼宇管理_访问 = new Privilege("楼宇管理_访问");


        public static IEnumerable<Privilege> Financial
        {
            get
            {
                yield return 财务模块_访问;
                yield return 收入日报表_访问;
                yield return 收入日报表_操作;
                yield return 未交的房租_押金明细表_访问;
                yield return 未交的房租_押金明细表_操作;
                yield return 已交的房租_押金明细表_访问;
                yield return 已交的房租_押金明细表_操作;
                yield return 未转的物业管理费明细表_访问;
                yield return 未转的物业管理费明细表_操作;
                yield return 已转的物业管理费明细表_访问;
                yield return 已转的物业管理费明细表_操作;
                yield return 月度收支明细表_访问;
                yield return 月度收支明细表_操作;
                yield return 半年度收支明细表_访问;
                yield return 半年度收支明细表_操作;
                yield return 年度收支明细表_访问;
                yield return 年度收支明细表_操作;
                yield return 月度收支汇总表_访问;
                yield return 月度收支汇总表_操作;
                yield return 财务模块_授权;
            }
        }

        public static IEnumerable<Privilege> Estate
        {
            get
            {
                yield return 物业模块_访问;
                yield return 人事管理_访问;
                yield return 任职全厂职工名单_访问;
                yield return 任职全厂职工名单_操作;
                yield return 退休职工名单_访问;
                yield return 退休职工名单_操作;
                yield return 消防管理_访问;
                yield return 消防资料_访问;
                yield return 消防资料_操作;
                yield return 物业管理_访问;
                yield return 租赁单位水电费一览表_访问;
                yield return 租赁单位水电费一览表_操作;
                yield return 水电费总厂开票一览表_访问;
                yield return 水电费总厂开票一览表_操作;
                yield return 停车费统计表_访问;
                yield return 停车费统计表_操作;
                yield return 钥匙管理_空调设备_访问;
                yield return 钥匙管理_空调设备_操作;
                yield return 电信电话_电信收费清单_访问;
                yield return 电信电话_电信收费清单_操作;
                yield return 维修记录_访问;
                yield return 维修记录_操作;
                yield return 各楼层租赁单位情况表_访问;
                yield return 各楼层租赁单位情况表_操作;
                yield return 收缴停车费明细表_访问;
                yield return 收缴停车费明细表_操作;
                yield return 平面图_访问;
                yield return 平面图_操作;
                yield return 应收停车费汇总表_访问;
                yield return 应收停车费汇总表_操作;
                yield return 租赁单位安全管理协议表_访问;
                yield return 租赁单位安全管理协议表_操作;
                yield return 租赁户入住表_访问;
                yield return 租赁户入住表_操作;
                yield return 租赁户退租表_访问;
                yield return 租赁户退租表_操作;
                yield return 监控探头_访问;
                yield return 监控探头_操作;
                yield return 车位信息管理表_访问;
                yield return 车位信息管理表_操作;
                yield return 抄电表数_访问;
                yield return 抄电表数_操作;
                yield return 水电费收费清单_访问;
                yield return 水电费收费清单_操作;
                yield return 物业模块_授权;
            }
        }

        public static IEnumerable<Privilege> Commercial
        {
            get
            {
                yield return 招商模块_访问;
                yield return 楼宇区域位置图示_访问;
                yield return 租赁户企业基本情况_访问;
                yield return 租赁户企业基本情况_操作;
                yield return 租金_物业费缴纳情况记录管理业务_访问;
                yield return 租金_物业费缴纳情况记录管理业务_操作;
                yield return 租赁期限变更_续租_退租一览业务_访问;
                yield return 租赁期限变更_续租_退租一览业务_操作;
                yield return 退租流程展示表格_访问;
                yield return 仓库租赁情况_访问;
                yield return 仓库租赁情况_操作;
                yield return 合同管理_访问;
                yield return 合同管理_操作;
                yield return 招商模块_授权;
            }
        }

        /// <summary>
        /// 系统管理模块
        /// </summary>
        public static IEnumerable<Privilege> SysManage
        {
            get
            {
                yield return 系统管理模块_访问;
                yield return 角色管理_访问;
                yield return 用户管理_访问;
                yield return 车位管理_访问;
                yield return 楼宇管理_访问;
            }
        }



        public static IEnumerable<Privilege> All
        {
            get
            {
                foreach (Privilege privilege in Financial)
                    yield return privilege;
                foreach (Privilege privilege in Estate)
                    yield return privilege;
                foreach (Privilege privilege in Commercial)
                    yield return privilege;
                foreach (Privilege privilege in SysManage)
                    yield return privilege;
            }
        }
    }
}
