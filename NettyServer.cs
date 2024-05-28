using DotNetty.Handlers.Logging;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nitrans
{
    public class NettyServer
    {
        MultithreadEventLoopGroup bossGroup = new MultithreadEventLoopGroup(1);
        MultithreadEventLoopGroup workGroup = new MultithreadEventLoopGroup();

        public NettyServer() 
        {
            ServerStart(8080);
        }

        async void ServerStart(int port)
        {
            try
            {
                var bootstrap = new ServerBootstrap()
                    .Group(bossGroup, workGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 100)
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(new LoggingHandler());
                    }));
                IChannel boundChannel = await bootstrap.BindAsync(IPAddress.Any, port);
            }
            finally
            {
                await Task.WhenAll(
                    bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                    workGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
            }
        }


    }
}
