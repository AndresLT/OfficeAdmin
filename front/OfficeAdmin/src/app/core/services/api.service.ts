import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Response } from '../models/response/Response';
import Swal from 'sweetalert2'


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  public loading = false

  constructor(private http: HttpClient) { }

  get<T>(service: string){
    return this.http.get<Response<T>>('https://localhost:7064/api/' + service)
  }

  post<T>(service: string, obj: any){
    return this.http.post<Response<T>>('https://localhost:7064/api/' + service, obj)
  }

  showMessages(res: Response<any>){
    if(res.status == "success"){
      Swal.fire({
        icon: 'success',
        text: res.message
      })
    }else if(res.status == 'info'){
      Swal.fire({
        icon: 'info',
        text: res.message
      })
    }else if(res.status == "warning"){
      Swal.fire({
        icon: 'warning',
        text: res.message
      })
    }else if(res.status == 'error'){
      Swal.fire({
        icon: 'error',
        text: res.message
      })
    }
    this.loading = false
  }
}
