import { Currency } from '../../core/models/Currency';
import { ApiService } from './../../core/services/api.service';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'currencyCode',
  standalone: true
})
export class CurrencyCodePipe implements PipeTransform {

  constructor(private apiService: ApiService){

  }

  transform(value: unknown, ...args: unknown[]): unknown {

    // return this.apiService.get<Currency[]>('Currency/GetCurrencies/' + true).subscribe(res => {

    //   return res.result.find(x => x.id == value)!.code.toString();
    // });
    if(value == 1){
      return "COP"
    }else if(value == 2){
      return "USD"

    }else if(value == 3){
      return "EUR"

    }else if(value == 4){
      return "JPY"

    }else if(value == 5){
      return "GBP"
    }else{
      return ""
    }
  }

}
