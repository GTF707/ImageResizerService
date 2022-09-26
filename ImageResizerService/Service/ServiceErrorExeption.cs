using System;

namespace ImageResizerService.Service
{
    
    [Serializable]
    public class ServiceErrorException : Exception
    {
        public int ErrorNumber { get; set; }
        public ServiceErrorException()
        {

        }

        public ServiceErrorException(int errorNumber)
        {
            ErrorNumber = errorNumber;
        }

    }
}

