using System;
using System.IO.Ports;
using DevForge.Lib.API;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Legacy
{
    public sealed class LegacyPort : ICommPort
    {
        private SerialPort _port;

        public LegacyPort(SerialPort port)
        {
            _port = port;
        }

        public void Open()
        {
            _port.Open();
        }

        public void Close()
        {
            if (_port != null)
            {
                _port.Close();
                _port.Dispose();
            }
            _port = null;
        }

        public void Dispose()
        {
            Close();
        }

        public byte[] ReadBytes(int count)
        {
            if (_port == null)
                return null;

            Console.WriteLine("1| "+count);
            
            var buffer = new byte[count];
            
            Console.WriteLine("2| "+buffer.Length);
            
            int bytesRead = 0;
            
            Console.WriteLine("3| "+bytesRead);
            
            while (bytesRead < count)
            {
                int got = _port.Read(buffer, bytesRead, count - bytesRead);
                
                Console.WriteLine("3b| "+bytesRead+" "+count+" "+got);
                
                if (got <= 0) break;
                
                Console.WriteLine("3c| "+bytesRead+" "+count+" "+got);
                
                bytesRead += got;
                
                Console.WriteLine("3d| "+bytesRead+" "+count+" "+got);
            }
            
            
            /*
            int rest;
            while ((bytesRead < buffer.Length) && (rest = _port.BytesToRead) >= 1)
            {
                Console.WriteLine("4| "+bytesRead);
                
                bytesRead += _port.Read(buffer, bytesRead, rest);
            }*/
            
            Console.WriteLine("5| "+bytesRead);
            
            if (bytesRead < 1)
                return null;
            
            Console.WriteLine("6| "+bytesRead);
            
            if (buffer.Length != bytesRead)
                Array.Resize(ref buffer, bytesRead);
            
            Console.WriteLine("7| "+buffer.Length);
            
            return buffer;
        }

        public bool WriteBytes(byte[] buffer)
        {
            if (_port == null)
                return false;
            _port.Write(buffer, 0, buffer.Length);
            return true;
        }
    }
}