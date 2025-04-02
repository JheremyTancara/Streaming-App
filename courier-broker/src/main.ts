import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { MicroserviceOptions, Transport } from '@nestjs/microservices';

async function bootstrap() {
  const app = await NestFactory.createMicroservice<MicroserviceOptions>(
    AppModule,
    {
      transport: Transport.RMQ,
      options: {
        urls: [
          'amqps://ewzcpjha:D0gumcnfUoRP9EWoEEGCxzr_zg2uVUNT@beaver.rmq.cloudamqp.com/ewzcpjha',
        ],
        queue: 'backend_queue',
        queueOptions: {
          durable: false,
        },
      },
    },
  );

  await app.listen();
}

bootstrap();
