using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinterComingGame.Server
{
    class CommunicationChannel : IDisposable
    {
        private const byte ZeroByte = (byte)'\0';
        private const byte CarriageReturnByte = (byte)'\r';
        private const byte NewLineByte = (byte)'\n';
        private const int MaxReadByteCount = 100;
        private const int MaxInputLineLength = 100;

        private TcpClient _tcpClient;
        private NetworkStream _networkStream;

        public CommunicationChannel(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;

            if (!_tcpClient.Connected)
            {
                throw new InvalidOperationException("TCP client is not connected.");
            }

            _networkStream = _tcpClient.GetStream();
        }

        public bool IsConnected => _tcpClient.Connected;

        public Task SendDataAsync(string data)
        {
            if (!_networkStream.CanWrite)
            {
                throw new InvalidOperationException("Network stream cannot be written to.");
            }

            var buffer = Encoding.ASCII.GetBytes(data);
            var bufferWithNewLine = new byte[buffer.Length + 2];
            Array.Copy(buffer, bufferWithNewLine, buffer.Length);
            bufferWithNewLine[bufferWithNewLine.Length - 2] = CarriageReturnByte;
            bufferWithNewLine[bufferWithNewLine.Length - 1] = NewLineByte;

            return _networkStream.WriteAsync(bufferWithNewLine, 0, bufferWithNewLine.Length);
        }

        public async Task<string> ReadInputLineAsync(/*CancellationToken ct*/)
        {
            if (!_networkStream.CanRead)
            {
                throw new InvalidOperationException("Network stream cannot be read from.");
            }

            var inputLineBuilder = new StringBuilder();


            var totalBytesRead = 0;
            var buffer = new byte[MaxReadByteCount];

            while (!buffer.Contains(NewLineByte) && totalBytesRead <= MaxReadByteCount && inputLineBuilder.Length <= MaxInputLineLength)
            {
                await _networkStream.ReadAsync(buffer, 0, buffer.Length/*, ct*/);

                var bufferLength = GetBufferLength(buffer);

                var readData = Encoding.ASCII.GetString(buffer, 0, bufferLength);

                inputLineBuilder.Append(readData);
            }

            if (totalBytesRead > MaxReadByteCount)
            {
                throw new InvalidOperationException($"Input line exceeded the maximum length of {MaxReadByteCount} bytes.");
            }

            return inputLineBuilder.ToString();
        }

        private static int GetBufferLength(byte[] buffer)
        {
            int GetMinNaturalNumber(int a, int b, int @default)
            {
                if (a >= 0 && a < b)
                {
                    return a;
                }

                return b >= 0 ? b : @default;
            }

            var bufferLength = buffer.Length;

            var zeroBytePosition = Array.IndexOf(buffer, ZeroByte);
            bufferLength = GetMinNaturalNumber(zeroBytePosition, bufferLength, bufferLength);

            var carriageReturnPosition = Array.IndexOf(buffer, CarriageReturnByte);
            bufferLength = GetMinNaturalNumber(carriageReturnPosition, bufferLength, bufferLength);

            var newLinePosition = Array.IndexOf(buffer, NewLineByte);
            bufferLength = GetMinNaturalNumber(newLinePosition, bufferLength, bufferLength);

            return bufferLength;
        }

        public void Dispose()
        {
            _networkStream.Close();
            _tcpClient.Close();
        }
    }
}
