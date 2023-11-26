import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Response } from '../models/response/Response';

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
}
