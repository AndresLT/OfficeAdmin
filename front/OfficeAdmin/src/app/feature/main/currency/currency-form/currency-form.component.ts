import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../../../core/services/api.service';
import { CreateCurrencyRequest } from '../../../../core/models/request/CreateCurrencyRequest';
import { ModifyCurrencyRequest } from '../../../../core/models/request/ModifyCurrencyRequest';
import { UserResponse } from '../../../../core/models/response/UserResponse';
import { LocalService } from '../../../../core/services/local.service';

@Component({
  selector: 'app-currency-form',
  standalone: true,
  imports: [CommonModule,MatFormFieldModule, MatInputModule,MatCheckboxModule, MatButtonModule,FormsModule,ReactiveFormsModule],
  templateUrl: './currency-form.component.html',
  styleUrl: './currency-form.component.scss'
})
export class CurrencyFormComponent implements OnInit {

  createForm = new FormGroup({
    code: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
  })

  updateForm = new FormGroup({
    code: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
    active: new FormControl('', Validators.required)
  })

  User: UserResponse

  constructor(public dialogRef: MatDialogRef<CurrencyFormComponent>,@Inject(MAT_DIALOG_DATA) public data: any,private apiService: ApiService, private localService: LocalService) {
    this.User = JSON.parse(this.localService.get('user'))

  }

  ngOnInit(): void {
    console.log('data', this.data);
    if(this.data.action == 'modify'){
      this.setUpdateFields()
    }
  }

  createCurrency(){
    let newCurrency : CreateCurrencyRequest = {
      code: this.createForm.controls['code'].value!,
      description: this.createForm.controls['description'].value!,
      username: this.User.username
    }

    this.apiService.post<string>('Currency/CreateCurrency', newCurrency).subscribe(async res => {
      this.apiService.showMessages(res)
      await new Promise(f => setTimeout(f, 2000));
      if(res.status == "success"){
        this.dialogRef.close('success')
      }
    })
  }

  setUpdateFields(){
    this.updateForm.controls['code'].setValue(this.data.dataToUpdate.code)
    this.updateForm.controls['description'].setValue(this.data.dataToUpdate.description)
    this.updateForm.controls['active'].setValue(this.data.dataToUpdate.active)
  }

  updateCurrency(){
    let currency : ModifyCurrencyRequest = {
      code: this.updateForm.controls['code'].value!,
      active: JSON.parse(this.updateForm.controls['active'].value!),
      description: this.updateForm.controls['description'].value!,
      username: this.User.username,
      id: this.data.dataToUpdate.id
    }


    this.apiService.post<string>('Currency/ModifyCurrency', currency).subscribe(async res => {
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
