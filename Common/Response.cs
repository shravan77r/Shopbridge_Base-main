﻿namespace Shopbridge_base.Common
{
    public class Response<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int Count { get; set; }
    }
}
