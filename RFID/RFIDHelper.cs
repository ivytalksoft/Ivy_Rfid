using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ReaderB;

namespace RFID
{
    public class RFIDHelper
    {
        private static int port = 0;
        private static int handle = 0;
        private static byte ComAdr = 0xff;//读卡器地址
        private static bool IsOper = false;
        private static byte[] bPassWord = new byte[4];
        private static int errcode = 0;

        /// <summary>
        /// 自动打开端口
        /// </summary>
        public static void AutoOpenCard()
        {
            if (IsOper)
            {
                throw new Exception("串口已打开");
            }
            int flag = StaticClassReaderB.AutoOpenComPort(ref port, ref ComAdr,
                            Convert.ToByte(RFIDResources._57600bps), ref handle);
            if (flag == 0)
            {
                IsOper = true;
            }
            else
            {
                throw new Exception(RFIDResources.GetReturnCodeDesc(flag));
            }
        }
        /// <summary>
        /// 打开指定端口
        /// </summary>
        /// <param name="port"></param>
        public static void OpenCard(int port)
        {
            if (IsOper)
            {
                throw new Exception("串口已打开");
            }
            int flag = StaticClassReaderB.OpenComPort(port, ref ComAdr,
                Convert.ToByte(RFIDResources._57600bps), ref handle);
            if (flag == 0)
            {
                IsOper = true;
            }
            else
            {
                throw new Exception(RFIDResources.GetReturnCodeDesc(flag));
            }
        }

        /// <summary>
        /// 读取标签 
        /// 读取出范围内所有标签
        /// </summary>
        /// <returns></returns>
        public static List<string> ReadEPC()
        {
            if (IsOper == false)
            {
                throw new Exception("请先打开串口");
            }
            List<string> epc_lis = new List<string>();
            int CardNum = 0;
            int Totallen = 0;
            byte[] EPC = new byte[5000];
            int fCmdRet = StaticClassReaderB.Inventory_G2(ref ComAdr, 0, 0, 0, EPC, ref Totallen, ref CardNum, port);
            if ((fCmdRet == 1) | (fCmdRet == 2) | (fCmdRet == 3) | (fCmdRet == 4) | (fCmdRet == 0xFB))//代表已查找结束，
            {
                byte[] daw = new byte[Totallen];
                Array.Copy(EPC, daw, Totallen);
                string temps = RFIDResources.ByteArrayToHexString(daw);

                int m = 0;
                for (int CardIndex = 0; CardIndex < CardNum; CardIndex++)
                {
                    byte EPClen = daw[m];
                    string sEPC = temps.Substring(m * 2 + 2, EPClen * 2);
                    epc_lis.Add(sEPC);
                    m = m + EPClen + 1;
                    if (sEPC.Length != EPClen * 2)
                        break;
                }
            }
            return epc_lis;
        }

        /// <summary>
        /// 写标签
        /// </summary>
        /// <param name="epc"></param>
        /// <param name="pwd"></param>
        public static void WriteEPC(string epc, string pwd)
        {
            if (!IsOper)
            {
                throw new Exception("请先打开串口");
            }
            if (string.IsNullOrEmpty(epc))
            {
                throw new Exception("EPC不能为空");
            }
            if (epc.Length % 2 != 0)
            {
                throw new Exception("EPC长度必须是2的倍数");
            }
            if (string.IsNullOrEmpty(pwd))
            {
                throw new Exception("密码不能为空");
            }
            if (pwd.Length != 8)
            {
                throw new Exception("密码长度要为8位");
            }

            byte epcLen = Convert.ToByte(epc.Length / 2);
            byte eNum = Convert.ToByte(epc.Length / 4);
            byte[] EPC = new byte[eNum];
            EPC = RFIDResources.HexStringToByteArray(epc);
            bPassWord = RFIDResources.HexStringToByteArray(pwd);
            int flag = StaticClassReaderB.WriteEPC_G2(ref ComAdr, bPassWord, EPC, epcLen,
                            ref errcode, handle);
            if (flag != 0)
            {
                if (errcode > -1)
                {
                    throw new Exception(RFIDResources.GetErrorCodeDesc(errcode));
                }
                else
                {
                    throw new Exception(RFIDResources.GetReturnCodeDesc(flag));
                }
            }
        }

        /// <summary>
        /// 读取全部数据
        /// </summary>
        /// <param name="epc"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string ReadData(string epc, string pwd)
        {
            if (!IsOper)
            {
                throw new Exception("请先打开串口");
            }
            if (string.IsNullOrEmpty(epc) || string.IsNullOrEmpty(pwd))
            {
                throw new Exception("请补全信息");
            }
            if (pwd.Length != 8)
            {
                throw new Exception("密码长度要为8位");
            }

            byte ENum = Convert.ToByte(epc.Length / 4);
            byte EPCLen = Convert.ToByte(epc.Length / 2);
            byte[] bEPC = new byte[ENum];
            bEPC = RFIDResources.HexStringToByteArray(epc);

            bPassWord = RFIDResources.HexStringToByteArray(pwd);
            byte Num = Convert.ToByte(26);
            byte[] bData = new byte[320];

            int flag = StaticClassReaderB.ReadCard_G2(ref ComAdr, bEPC, RFIDResources.MemUser, 0x00
                                , Num, bPassWord, 0, 0, 0, bData, EPCLen, ref errcode, handle);
            if (flag != 0)
            {
                if (errcode > -1)
                {
                    throw new Exception(RFIDResources.GetErrorCodeDesc(errcode));
                }
                else
                {
                    throw new Exception(RFIDResources.GetReturnCodeDesc(flag));
                }
            }
            byte[] daw = new byte[Num * 2];
            Array.Copy(bData, daw, Num * 2);
            string str = RFIDResources.ByteArrayToHexString(daw);
            str = str.Remove((11 - 1) * 4, 6 * 4);
            return str;
        }
        public static string ReadDataOfTID(string epc, string pwd)
        {
            if (!IsOper)
            {
                throw new Exception("请先打开串口");
            }
            if (string.IsNullOrEmpty(epc) || string.IsNullOrEmpty(pwd))
            {
                throw new Exception("请补全信息");
            }
            if (pwd.Length != 8)
            {
                throw new Exception("密码长度要为8位");
            }

            byte ENum = Convert.ToByte(epc.Length / 4);
            byte EPCLen = Convert.ToByte(epc.Length / 2);
            byte[] bEPC = new byte[ENum];
            bEPC = RFIDResources.HexStringToByteArray(epc);

            bPassWord = RFIDResources.HexStringToByteArray(pwd);
            byte Num = Convert.ToByte(12);
            byte[] bData = new byte[320];

            int flag = StaticClassReaderB.ReadCard_G2(ref ComAdr, bEPC, RFIDResources.MemTID, 0x00
                                , Num, bPassWord, 0, 0, 0, bData, EPCLen, ref errcode, handle);
            if (flag != 0)
            {
                if (errcode > -1)
                {
                    throw new Exception(RFIDResources.GetErrorCodeDesc(errcode));
                }
                else
                {
                    throw new Exception(RFIDResources.GetReturnCodeDesc(flag));
                }
            }
            byte[] daw = new byte[Num * 2];
            Array.Copy(bData, daw, Num * 2);
            string str = RFIDResources.ByteArrayToHexString(daw);
            return str;
        }


        /// <summary>
        /// 按块读数据 
        /// 0-19 每块4个字节
        /// </summary>
        /// <param name="epc"></param>
        /// <param name="pwd"></param>
        /// <param name="block_num"></param>
        /// <returns></returns>
        public static string ReadData(string epc, string pwd, int block_num)
        {
            if (!IsOper)
            {
                throw new Exception("请先打开串口");
            }
            if (string.IsNullOrEmpty(epc) || string.IsNullOrEmpty(pwd))
            {
                throw new Exception("请补全信息");
            }
            if (pwd.Length != 8)
            {
                throw new Exception("密码长度要为8位");
            }
            if (block_num < 0 || block_num > 19)
            {
                throw new Exception("读取地址必须在0-19之间");
            }

            byte ENum = Convert.ToByte(epc.Length / 4);
            byte EPCLen = Convert.ToByte(epc.Length / 2);
            byte[] bEPC = new byte[ENum];
            bEPC = RFIDResources.HexStringToByteArray(epc);

            bPassWord = RFIDResources.HexStringToByteArray(pwd);
            byte BNum = Convert.ToByte(block_num.ToString(), 16);
            byte[] bData = new byte[320];

            int flag = StaticClassReaderB.ReadCard_G2(ref ComAdr, bEPC, RFIDResources.MemUser, BNum
                                , 1, bPassWord, 0, 0, 0, bData, EPCLen, ref errcode, handle);
            if (flag != 0)
            {
                if (errcode > -1)
                {
                    throw new Exception(RFIDResources.GetErrorCodeDesc(errcode));
                }
                else
                {
                    throw new Exception(RFIDResources.GetReturnCodeDesc(flag));
                }
            }
            byte[] daw = new byte[1 * 2];
            Array.Copy(bData, daw, 1 * 2);
            string str = RFIDResources.ByteArrayToHexString(daw);

            return str;
        }

        /// <summary>
        /// 写入全部数据
        /// </summary>
        /// <param name="epc"></param>
        /// <param name="pwd"></param>
        /// <param name="data"></param>
        public static void WriteData(string epc, string pwd, string data)
        {
            if (!IsOper)
            {
                throw new Exception("请先打开串口");
            }
            if (string.IsNullOrEmpty(epc) || string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(data))
            {
                throw new Exception("请补全信息");
            }
            if (pwd.Length != 8)
            {
                throw new Exception("密码长度要为8位");
            }
            if (data.Length % 4 != 0)
            {
                throw new Exception("密码长度要为4的倍数");
            }
            byte WrittenDataNum = 0;
            byte ENum = Convert.ToByte(epc.Length / 4);
            byte EPClen = Convert.ToByte(ENum * 2);
            byte[] bEPC = new byte[ENum];
            bEPC = RFIDResources.HexStringToByteArray(epc);

            bPassWord = RFIDResources.HexStringToByteArray(pwd);
            byte Wnum = Convert.ToByte(data.Length / 4);
            byte[] WriteData = new byte[Wnum * 2];
            WriteData = RFIDResources.HexStringToByteArray(data);
            byte WriteDataLen = Convert.ToByte(Wnum * 2);

            int flag = StaticClassReaderB.WriteCard_G2(ref ComAdr, bEPC,
                                       RFIDResources.MemUser, 0, WriteDataLen, WriteData,
                                       bPassWord, 0, 0, 0, WrittenDataNum, EPClen, ref errcode, handle);
            if (flag != 0)
            {
                if (errcode > -1)
                {
                    throw new Exception(RFIDResources.GetErrorCodeDesc(errcode));
                }
                else
                {
                    throw new Exception(RFIDResources.GetReturnCodeDesc(flag));
                }
            }
        }
        /// <summary>
        /// 写入指定块 数据
        /// 0-19 每块4个字节
        /// </summary>
        /// <param name="epc"></param>
        /// <param name="pwd"></param>
        /// <param name="data"></param>
        /// <param name="block_num"></param>
        public static void WriteData(string epc, string pwd, string data, int block_num)
        {
            if (!IsOper)
            {
                throw new Exception("请先打开串口");
            }
            if (string.IsNullOrEmpty(epc) || string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(data))
            {
                throw new Exception("请补全信息");
            }
            if (pwd.Length != 8)
            {
                throw new Exception("密码长度要为8位");
            }
            if (data.Length % 4 != 0)
            {
                throw new Exception("密码长度要为4的倍数");
            }
            byte WrittenDataNum = 0;
            byte ENum = Convert.ToByte(epc.Length / 4);
            byte EPClen = Convert.ToByte(ENum * 2);
            byte[] bEPC = new byte[ENum];
            bEPC = RFIDResources.HexStringToByteArray(epc);

            bPassWord = RFIDResources.HexStringToByteArray(pwd);
            byte Wnum = Convert.ToByte(data.Length / 4);
            byte[] WriteData = new byte[Wnum * 2];
            WriteData = RFIDResources.HexStringToByteArray(data);
            byte WriteDataLen = Convert.ToByte(Wnum * 2);

            byte BNum = Convert.ToByte(block_num.ToString(), 16);

            int flag = StaticClassReaderB.WriteCard_G2(ref ComAdr, bEPC,
                                       RFIDResources.MemUser, BNum, WriteDataLen, WriteData,
                                       bPassWord, 0, 0, 0, WrittenDataNum, EPClen, ref errcode, handle);
            if (flag != 0)
            {
                if (errcode > -1)
                {
                    throw new Exception(RFIDResources.GetErrorCodeDesc(errcode));
                }
                else
                {
                    throw new Exception(RFIDResources.GetReturnCodeDesc(flag));
                }
            }
        }

        /// <summary>
        /// 修改密码 普通密码
        /// </summary>
        /// <param name="epc"></param>
        /// <param name="old_pwd"></param>
        /// <param name="new_pwd"></param>
        public static void ChangePwd(string epc, string old_pwd, string new_pwd)
        {
            if (!IsOper)
            {
                throw new Exception("请先打开串口");
            }
            if (string.IsNullOrEmpty(epc) || string.IsNullOrEmpty(old_pwd) || string.IsNullOrEmpty(new_pwd))
            {
                throw new Exception("请补全信息");
            }
            if (old_pwd.Length != 8 || new_pwd.Length != 8)
            {
                throw new Exception("密码长度要为8位");
            }
            byte WrittenDataNum = 0;
            byte ENum = Convert.ToByte(epc.Length / 4);
            byte EPClen = Convert.ToByte(ENum * 2);
            byte[] bEPC = new byte[ENum];
            bEPC = RFIDResources.HexStringToByteArray(epc);

            bPassWord = RFIDResources.HexStringToByteArray(old_pwd);
            byte Wnum = Convert.ToByte(new_pwd.Length / 4);
            byte[] WriteData = new byte[Wnum * 2];
            WriteData = RFIDResources.HexStringToByteArray(new_pwd);
            byte WriteDataLen = Convert.ToByte(Wnum * 2);

            int flag = StaticClassReaderB.WriteCard_G2(ref ComAdr, bEPC,
                                       RFIDResources.MemReser, 0x2, WriteDataLen, WriteData,
                                       bPassWord, 0, 0, 0, WrittenDataNum, EPClen, ref errcode, handle);
            if (flag != 0)
            {
                if (errcode > -1)
                {
                    throw new Exception(RFIDResources.GetErrorCodeDesc(errcode));
                }
                else
                {
                    throw new Exception(RFIDResources.GetReturnCodeDesc(flag));
                }
            }

            SetRegiSec(epc, new_pwd);
        }
        /// <summary>
        /// 修改密码 销毁密码
        /// 谨慎操作 用于销毁标签
        /// </summary>
        /// <param name="epc"></param>
        /// <param name="old_pwd"></param>
        /// <param name="new_pwd"></param>
        public static void ChangeAdminPwd(string epc, string old_pwd, string new_pwd)
        {
            if (!IsOper)
            {
                throw new Exception("请先打开串口");
            }
            if (string.IsNullOrEmpty(epc) || string.IsNullOrEmpty(old_pwd) || string.IsNullOrEmpty(new_pwd))
            {
                throw new Exception("请补全信息");
            }
            if (old_pwd.Length != 8 || new_pwd.Length != 8)
            {
                throw new Exception("密码长度要为8位");
            }
            byte WrittenDataNum = 0;
            byte ENum = Convert.ToByte(epc.Length / 4);
            byte EPClen = Convert.ToByte(ENum * 2);
            byte[] bEPC = new byte[ENum];
            bEPC = RFIDResources.HexStringToByteArray(epc);

            bPassWord = RFIDResources.HexStringToByteArray(old_pwd);
            byte Wnum = Convert.ToByte(new_pwd.Length / 4);
            byte[] WriteData = new byte[Wnum * 2];
            WriteData = RFIDResources.HexStringToByteArray(new_pwd);
            byte WriteDataLen = Convert.ToByte(Wnum * 2);

            int flag = StaticClassReaderB.WriteCard_G2(ref ComAdr, bEPC,
                                       RFIDResources.MemReser, 0x0, WriteDataLen, WriteData,
                                       bPassWord, 0, 0, 0, WrittenDataNum, EPClen, ref errcode, handle);
            if (flag != 0)
            {
                if (errcode > -1)
                {
                    throw new Exception(RFIDResources.GetErrorCodeDesc(errcode));
                }
                else
                {
                    throw new Exception(RFIDResources.GetReturnCodeDesc(flag));
                }
            }

            SetRegiSec(epc, new_pwd);
        }

        private static void SetRegiSec(string epc, string pwd)
        {
            if (string.IsNullOrEmpty(epc) || string.IsNullOrEmpty(pwd))
            {
                throw new Exception("请补全信息");
            }
            if (pwd.Length != 8)
            {
                throw new Exception("密码长度要为8位");
            }

            byte Enum = Convert.ToByte(epc.Length / 4);
            byte EPClen = Convert.ToByte(epc.Length / 2);
            byte[] bEPC = new byte[Enum];
            bEPC = RFIDResources.HexStringToByteArray(epc);

            bPassWord = RFIDResources.HexStringToByteArray(pwd);

            List<byte> lis = new List<byte>()
            {
                0x00,0x01,0x02,0x04
            };

            foreach (byte b in lis)
            {
                int flag = StaticClassReaderB.SetCardProtect_G2(ref ComAdr, bEPC, b, 0x02, bPassWord
                               , 0, 0, 0, EPClen, ref errcode, handle);
                if (flag != 0)
                {
                    if (errcode > -1)
                    {
                        throw new Exception(RFIDResources.GetErrorCodeDesc(errcode));
                    }
                    else
                    {
                        throw new Exception(RFIDResources.GetReturnCodeDesc(flag));
                    }
                }

                Thread.Sleep(10);
            }

        }

        /// <summary>
        /// 关闭读卡器
        /// </summary>
        public static void CloseCardManager()
        {
            IsOper = false;
            StaticClassReaderB.CloseSpecComPort(port);
            StaticClassReaderB.CloseComPort();
            GC.Collect(2);
        }

        ~RFIDHelper()
        {
            CloseCardManager();
        }
    }

    public static class RFIDResources
    {
        public const int _9600bps = 0;
        public const int _19200bps = 1;
        public const int _38400bps = 2;
        public const int _56000bps = 4;
        public const int _57600bps = 5;
        public const int _115200bps = 6;

        public const string default_pwd = "00000000";
        public const string my_pwd = "12345678";

        public const int MemReser = 0x00;
        public const int MemEPC = 0x01;
        public const int MemTID = 0x02;
        public const int MemUser = 0x03;

        public static string GetReturnCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "操作成功";
                case 0x01:
                    return "询查时间结束前返回";
                case 0x02:
                    return "指定的询查时间溢出";
                case 0x03:
                    return "本条消息之后，还有消息";
                case 0x04:
                    return "读写模块存储空间已满";
                case 0x05:
                    return "访问密码错误";
                case 0x09:
                    return "销毁密码错误";
                case 0x0a:
                    return "销毁密码不能为全0";
                case 0x0b:
                    return "电子标签不支持该命令";
                case 0x0c:
                    return "对该命令，访问密码不能为全0";
                case 0x0d:
                    return "电子标签已经被设置了读保护，不能再次设置";
                case 0x0e:
                    return "电子标签没有被设置读保护，不需要解锁";
                case 0x10:
                    return "有字节空间被锁定，写入失败";
                case 0x11:
                    return "不能锁定";
                case 0x12:
                    return "已经锁定，不能再次锁定";
                case 0x13:
                    return "参数保存失败,但设置的值在读写模块断电前有效";
                case 0x14:
                    return "无法调整";
                case 0x15:
                    return "询查时间结束前返回";
                case 0x16:
                    return "指定的询查时间溢出";
                case 0x17:
                    return "本条消息之后，还有消息";
                case 0x18:
                    return "读写模块存储空间已满";
                case 0x19:
                    return "电子不支持该命令或者访问密码不能为0";
                case 0xFA:
                    return "有电子标签，但通信不畅，无法操作";
                case 0xFB:
                    return "无电子标签可操作";
                case 0xFC:
                    return "电子标签返回错误代码";
                case 0xFD:
                    return "命令长度错误";
                case 0xFE:
                    return "不合法的命令";
                case 0xFF:
                    return "参数错误";
                case 0x30:
                    return "通讯错误";
                case 0x31:
                    return "CRC校验错误";
                case 0x32:
                    return "返回数据长度有错误";
                case 0x33:
                    return "通讯繁忙，设备正在执行其他指令";
                case 0x34:
                    return "繁忙，指令正在执行";
                case 0x35:
                    return "端口已打开";
                case 0x36:
                    return "端口已关闭";
                case 0x37:
                    return "无效句柄";
                case 0x38:
                    return "无效端口";
                case 0xEE:
                    return "返回指令错误";
                default:
                    return "";
            }
        }
        public static string GetErrorCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "其它错误";
                case 0x03:
                    return "存储器超限或不被支持的PC值";
                case 0x04:
                    return "存储器锁定";
                case 0x0b:
                    return "电源不足";
                case 0x0f:
                    return "非特定错误";
                default:
                    return "";
            }
        }

        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            }

            return sb.ToString().ToUpper();
        }
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

    }

}


