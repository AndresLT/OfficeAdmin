import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import { ApiService } from '../../core/services/api.service';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import Swal from 'sweetalert2'
import { LocalService } from '../../core/services/local.service';
import { CreateUserRequest } from '../../core/models/request/CreateUserRequest';
import { ChangeUserPasswordRequest } from '../../core/models/request/ChangeUserPasswordRequest';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatFormFieldModule, MatInputModule, MatIconModule, FormsModule,ReactiveFormsModule, MatButtonModule,MatProgressBarModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  passwordMatch = false
  passwordMatchRegister = false

  loading = false

  actualForm = "login"

  hidePass = true;
  hidePassConfirm = true;

  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  })

  changePasswordForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', Validators.required)
  })

  registerForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', Validators.required),
    name: new FormControl('', Validators.required),
    lastname: new FormControl('', Validators.required),
  })

  constructor(private router: Router, private apiService: ApiService, private localService: LocalService){

  }

  changeForm(form: string){
    this.actualForm = form
    this.hidePass = true
    this.hidePassConfirm = true
    this.loginForm.reset()
    this.changePasswordForm.reset()
    this.registerForm.reset()
  }

  login(){
    this.apiService.get<string>('Admin/Login/'+this.loginForm.controls['username'].value + '/'+this.loginForm.controls['password'].value).subscribe(res => {
      if(res.status == "success"){
        Swal.fire({
          icon: 'success',
          text: res.message
        })
        this.router.navigate(['home'])
        this.localService.set('user',JSON.stringify(res.result))
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
      this.apiService.loading = false
    })
  }

  changePassword(){
    let changePassReq: ChangeUserPasswordRequest = {
      username: this.changePasswordForm.controls['username'].value!,
      password: this.changePasswordForm.controls['password'].value!
    }
    this.apiService.post<string>('Admin/ChangeUserPassword/',changePassReq).subscribe(res =>{
      if(res.status == "success"){
        Swal.fire({
          icon: 'success',
          text: res.message
        })
        this.changeForm('login')
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
      this.apiService.loading = false
    })
  }

  registerUser(){
    let registerUserReq : CreateUserRequest = {
      username: this.registerForm.controls['username'].value!,
      name:this.registerForm.controls['name'].value!,
      lastname:this.registerForm.controls['lastname'].value!,
      password: this.registerForm.controls['password'].value!
    }
    this.apiService.post<string>('Admin/CreateUser/',registerUserReq).subscribe(res =>{
      if(res.status == "success"){
        Swal.fire({
          icon: 'success',
          text: res.message
        })
        this.changeForm('login')
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
      this.apiService.loading = false
    })
  }

  passwordChange(){


    if(this.changePasswordForm.controls['password'].value != this.changePasswordForm.controls['confirmPassword'].value){
      this.passwordMatch = false
    }else{
      this.passwordMatch = true
    }
  }

  passwordChangeRegister(){
    if(this.registerForm.controls['password'].value != this.registerForm.controls['confirmPassword'].value){
      this.passwordMatchRegister = false
    }else{
      this.passwordMatchRegister = true
    }
  }

  validateInput(event: any) {

    var inputValue = event.key;

    // Puedes ajustar esta expresión regular según tus necesidades
    var regex = /^[a-zA-Z0-9]*$/;

    if (!regex.test(inputValue)) {
        event.preventDefault();
    }
  }

  pasteInput(event: any){
    event.preventDefault();
  }

}
