import { Injectable } from '@angular/core';
import CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class LocalService {
  private readonly key = '@S3cur3P4ssT3st@';

  constructor() { }
  public set(key: string, value: string) {
    localStorage.setItem(key, this.encrypt(value));
  }

  public get(key: string) {
    let data = localStorage.getItem(key) || "";
    return this.decrypt(data);
  }
  public remove(key: string) {
    localStorage.removeItem(key);
  }

  public clear() {
    localStorage.clear();
  }

  private encrypt(txt: string): string {
    return CryptoJS.AES.encrypt(txt, this.key).toString();
  }

  private decrypt(txtToDecrypt: string) {
    return CryptoJS.AES.decrypt(txtToDecrypt, this.key).toString(CryptoJS.enc.Utf8);
  }
}
