import { Controller } from '@nestjs/common';
import { EventPattern } from '@nestjs/microservices';

@Controller()
export class AppController {
  @EventPattern('sendmessage')
  async camera(msg: string) {
    console.log(msg);
  }
}
