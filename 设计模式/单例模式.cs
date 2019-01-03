namespace 设计模式
{
    /// <summary>
    /// 单例模式同时应该是 sealed 防止被其他类继承
    /// 双锁定，线程安全
    /// </summary>
    public sealed class SingleModel2
    {
        private static SingleModel2 instance;
        private static readonly object globeLock = new object();

        /// <summary>
        /// 防止被实例化多个 private
        /// </summary>
        private SingleModel2() { }

        public static SingleModel2 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (globeLock)
                    {
                        if (instance == null)
                        {
                            instance = new SingleModel2();
                        }
                    }
                }
                return instance;
            }
        }

    }

    /// <summary>
    /// 保证类的实例只有一个，并且提供一个全局访问方法
    /// 单线程下可行操作
    /// </summary>
    class SingleModel
    {
        private static SingleModel _singleModel;

        private SingleModel() { }

        public static SingleModel GetInstance()
        {
            //?? 运算符称为 null 合并运算符，用于定义可以为 null 值的类型和引用类型的默认值。
            //如果此运算符的左操作数不为 null，则此运算符将返回左操作数；否则返回右操作数。
            return _singleModel ?? (_singleModel = new SingleModel());
        }
    }

    /// <summary>
    ///  多线程可行操作 （lock锁）
    /// </summary>
    class SingleModel1
    {
        private static SingleModel1 _singleModel1;
        private static readonly object MyLock = new object();

        private SingleModel1()
        {

        }

        public static SingleModel1 GetInstance()
        {
            lock (MyLock)
            {
                return _singleModel1 ?? (_singleModel1 = new SingleModel1());
            }
        }
    }

    /// <summary>
    /// 多线程可行操作 （双重锁）
    /// </summary>
    internal class Single2
    {
        private static Single2 _mSingle;
        private static readonly object MyLock = new object();

        private Single2()
        {

        }

        public static Single2 GetInstance()
        {
            if (_mSingle != null) return _mSingle;
            lock (MyLock)
            {
                _mSingle = new Single2();
            }
            return _mSingle;
        }
    }

}
