using System;

namespace COMPortProject.Core.Command
{
    public abstract class CommandBase
    {
        /// <summary>
        /// 协议头长度0xff*4+length
        /// </summary>
        private const int HEAD_LENGTH = 6;

        /// <summary>
        /// 轴编号长度
        /// </summary>
        private const int AXIS_LENGTH = 1;

        /// <summary>
        /// 加密位长度
        /// </summary>
        private const int ENCRYPT_LENGTH = 1;

        /// <summary>
        /// 校验位长度
        /// </summary>
        private const int SUM_LENGTH = 1;

        /// <summary>
        /// 获取协议体长度
        /// </summary>
        /// <returns></returns>
        protected abstract int GetBodyLength();

        /// <summary>
        /// 序列化协议体
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        protected abstract void SerializationBody(byte[] data, ref int offset);

        /// <summary>
        /// 生成协议码
        /// </summary>
        /// <returns></returns>
        public byte[] Serialization()
        {
            int bodyLength = GetBodyLength();
            byte[] data = new byte[HEAD_LENGTH + AXIS_LENGTH + bodyLength + ENCRYPT_LENGTH + SUM_LENGTH];

            int offset = 0;
            data[offset++] = 0xff;
            data[offset++] = 0xff;
            data[offset++] = 0xff;
            data[offset++] = 0xff;

            Array.Copy(BitConverter.GetBytes((ushort)(AXIS_LENGTH + bodyLength + ENCRYPT_LENGTH + SUM_LENGTH)),
                0, data, offset, sizeof(ushort));
            offset += sizeof(ushort);

            data[offset++] = (byte)AXIS_LENGTH;
            SerializationBody(data, ref offset);

            //EncryptData(ref data);
            CheckOutData(ref data);

            return data;
        }

        protected virtual void CheckOutData(ref byte[] data)
        {
            AddSum(ref data);
        }

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="data"></param>
        private void EncryptData(ref byte[] data)
        {
            byte seed = 0xcc;

            for (int i = HEAD_LENGTH + AXIS_LENGTH; i < data.Length - ENCRYPT_LENGTH - SUM_LENGTH; i++)
            {
                data[i] = (byte)(data[i] ^ (seed - i + HEAD_LENGTH + AXIS_LENGTH));
            }
            data[data.Length - ENCRYPT_LENGTH - SUM_LENGTH] = seed;

        }

        /// <summary>
        /// 生成校验码
        /// </summary>
        /// <param name="data"></param>
        private void AddSum(ref byte[] data)
        {
            int sumPostion = data.Length - SUM_LENGTH;
            int sum = 0;
            for (int i = HEAD_LENGTH; i < sumPostion; i++)
            {
                sum += data[i];
            }
            data[sumPostion] = Convert.ToByte(sum & 0xff);
        }

    }
}
