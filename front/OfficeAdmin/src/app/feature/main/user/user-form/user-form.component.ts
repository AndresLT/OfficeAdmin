import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateUserRequest } from '../../../../core/models/request/CreateUserRequest';
import { ApiService } from '../../../../core/services/api.service';
import { ModifyUserRequest } from '../../../../core/models/request/ModifyUserRequest';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule,MatFormFieldModule, MatInputModule,MatCheckboxModule, MatButtonModule,FormsModule,ReactiveFormsModule, MatIconModule],
  templateUrl: './user-form.component.html',
  styleUrl: './user-form.component.scss'
})
export class UserFormComponent implements OnInit {

  hidePass = true;


  createForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    name: new FormControl('', Validators.required),
    lastname: new FormControl('', Validators.required),
  })

  updateForm = new FormGroup({
    username: new FormControl({value:'',disabled: true}, Validators.required),
    name: new FormControl('', Validators.required),
    lastname: new FormControl('', Validators.required),
    active: new FormControl('', Validators.required)
  })

  constructor(public dialogRef: MatDialogRef<UserFormComponent>,@Inject(MAT_DIALOG_DATA) public data: any, private apiService: ApiService) {}
  ngOnInit(): void {
    console.log('data', this.data);
    if(this.data.action == 'modify'){
      this.setUpdateFields()
    }
  }

  createUser(){
    let newUser : CreateUserRequest = {
      username: this.createForm.controls['username'].value!,
      password: this.createForm.controls['password'].value!,
      name: this.createForm.controls['name'].value!,
      lastname: this.createForm.controls['lastname'].value!
    }

    this.apiService.post<string>('Admin/CreateUser', newUser).subscribe(async res => {
      this.apiService.showMessages(res)
      await new Promise(f => setTimeout(f, 2000));
      if(res.status == "success"){
        this.dialogRef.close('success')
      }
    })
  }

  setUpdateFields(){
    this.updateForm.controls['username'].setValue(this.data.dataToUpdate.username)
    this.updateForm.controls['name'].setValue(this.data.dataToUpdate.name)
    this.updateForm.controls['lastname'].setValue(this.data.dataToUpdate.lastname)
    this.updateForm.controls['active'].setValue(this.data.dataToUpdate.active)
  }

  updateUser(){
    let user : ModifyUserRequest = {
      username: this.updateForm.controls['username'].value!,
      active: JSON.parse(this.updateForm.controls['active'].value!),
      name: this.updateForm.controls['name'].value!,
      lastname: this.updateForm.controls['lastname'].value!
    }


    this.apiService.post<string>('Admin/ModifyUser', user).subscribe(async res => {
      this.apiService.showMessages(res)
      await new Promise(f => setTimeout(f, 2000));
      if(res.status == "success"){
        this.dialogRef.close('success')
      }
    })
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
