import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'status',
  standalone: true
})
export class StatusPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    if(value == 1){

      return 'Activo';
    }else{
      return 'Inactivo';

    }

  }

}
